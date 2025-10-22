// JavaScript file that implement logic for write-new-city.html

import { CityAPI } from "/assets/js/cityAPI.js"

window.onload = (e) => {
    document.getElementById('home-button')?.addEventListener('click', () => { window.location.href = '/index.html' });
    document.getElementById('clear-all-fields-button')?.addEventListener('click', OnClearButtonClick);
    document.getElementById('send-city-button')?.addEventListener('click', OnSendCityButonClick);
}

function OnClearButtonClick() {
    document.getElementById('residents').value = '';
    document.getElementById('cityname').value = '';
    document.getElementById('mayor').value = '';
    document.getElementById('climate').value = '';
}

async function OnSendCityButonClick() {
    let city = {};

    const residents = document.getElementById('residents');
    if(!residents) {
        alert('Residents field is empty!')
        return;
    }

    city.residents = residents.value;

    const cityname = document.getElementById('cityname');
    if(!cityname) {
        alert('City name field is empty!')
        return;
    }

    city.cityname = cityname.value;

    const mayor = document.getElementById('mayor');
    if(!mayor) {
        alert('Mayor field is empty!')
        return;
    }

    city.mayor = mayor.value;

    const climate = document.getElementById('climate');
    if(!climate) {
        alert('Climate field is empty!')
        return;
    }

    city.climate = climate.value;
    const success = await CityAPI.CreateNewCity(city);
    if(success) {
        alert('City successfully sent')
        OnClearButtonClick();
    }
    
}