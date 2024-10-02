import os
import logging
import azure.functions as func
from azure.cosmos import CosmosClient

# Connection details to Cosmos DB (Use environment variables for security)
ENDPOINT = os.getenv("COSMOS_DB_ENDPOINT")
KEY = os.getenv("COSMOS_DB_KEY")
DATABASE_NAME = "websiteData"
CONTAINER_NAME = "pageViews"

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    # Initialize Cosmos DB client
    client = CosmosClient(ENDPOINT, KEY)
    database = client.get_database_client(DATABASE_NAME)
    container = database.get_container_client(CONTAINER_NAME)

    # Fetch the current view count (assume only one document stores the count)
    page_data = container.read_item(item="view_counter", partition_key="website")
    current_count = page_data['viewCount']

    # Increment the view count
    updated_count = current_count + 1
    container.upsert_item({
        'id': 'view_counter',
        'viewCount': updated_count,
        'partitionKey': 'website'
    })

    return func.HttpResponse(f"Website has been viewed {updated_count} times.")
