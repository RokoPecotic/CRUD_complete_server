import { CityAPI } from "/assets/js/cityAPI.js";

window.onload = (e) => {
    document.getElementById('get-all-cities-button')?.addEventListener('click', LoadTable);
    document.getElementById('clear-all-cities-button')?.addEventListener('click', ClearTable);
    document.getElementById('home-button')?.addEventListener('click', () => { window.location.href = '/index.html'; });
    ClearTable();
}

async function LoadTable() {
    const cities = await CityAPI.GetAllCities();
    if (!cities) {
        console.error('Could not load cities.');
        return;
    }

    const table = document.getElementById('city-table');
    if (!table) {
        console.error('Could not find city table.');
        return;
    }

    let data = `
        <thead>
            <tr>
                <th>ID</th>
                <th>Residents</th>
                <th>City Name</th>
                <th>Mayor</th>
                <th>Climate</th>
                <th>Date Added</th>
            </tr>
        </thead>
    `;

    data += '<tbody>';
    cities.forEach(e => {
        data = data + `
            <tr>
                <td>${e.id}</td>
                <td>${e.residents}</td>
                <td>${e.cityname}</td>
                <td>${e.mayor}</td>
                <td>${e.climate}</td>
                <td>${e.datenow}</td>
            </tr>
        `;
    });
    data += '</tbody>';

    table.innerHTML = data;
}

function ClearTable() {
    const table = document.getElementById('city-table');
    if (!table) {
        console.error('Could not find city table.');
        return;
    }
    table.innerHTML = '';
}
