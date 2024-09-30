// Function to fetch visitor count from the API
function getVisitorCount() {
  fetch(
    'https://cloud-resume-visitor-counter-api.azurewebsites.net/api/getvisitorcount',
    {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    }
  )
    .then((response) => response.json())
    .then((data) => {
      // Update the counter on the webpage
      console.log('HEREEEE');
      document.querySelector('.counter').textContent = data.count;

      // Call updateVisitorCount with the current count
      updateVisitorCount();
    })
    .catch((error) => {
      console.error('Error fetching visitor count:', error);
    });
}

// Function to update visitor count on the API
function updateVisitorCount() {
  fetch(
    'https://cloud-resume-visitor-counter-api.azurewebsites.net/api/updatevisitorcount',
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({}),
    }
  )
    .then((response) => response.json())
    .then((data) => {
      console.log('Visitor count updated successfully:', data);
    })
    .catch((error) => {
      console.error('Error updating visitor count:', error);
    });
}

// Fetch the visitor count and update on page load
document.addEventListener('DOMContentLoaded', getVisitorCount);
