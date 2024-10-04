console.log("Counter script is running");

document.addEventListener('DOMContentLoaded', initializeCounter);

const functionApiUrl = 'https://getportfoliocounter97.azurewebsites.net/api/GetPortfolioCounter?'
const localfunctionApi = 'http://localhost:7071/api/GetPortfolioCounter';

async function initializeCounter() {
    console.log("Initializing counter");
    try {
        const response = await fetch(functionApiUrl);
        console.log("Response status:", response.status);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        console.log("Received data:", data);
        let count;
        if (typeof data.Count !== 'undefined') {
            count = data.Count;
        } else if (typeof data.count !== 'undefined') {
            count = data.count;
        } else if (typeof data === 'number') {
            count = data;
        } else {
            console.error("Unexpected data structure:", data);
            count = 'Data error';
        }
        updateCounter(count);
    } catch (error) {
        console.error("Failed to fetch visit count:", error);
        updateCounter('Error');
    }
}

function updateCounter(count) {
    console.log("Updating counter with value:", count);
    const counterElement = document.getElementById("counter");
    if (counterElement) {
        counterElement.innerText = count;
    } else {
        console.error("Counter element not found");
    }
}