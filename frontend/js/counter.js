function updateCounter() {
    fetch('/api/counter', { method: 'POST' })
        .then(response => response.text())
        .then(count => {
            document.getElementById('counter').textContent = count;
        })
        .catch(error => console.error('Error:', error));
}

document.addEventListener('DOMContentLoaded', updateCounter);
