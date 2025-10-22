const Base_URL = 'http://127.0.0.1:5124'

class _CityAPI { 

    async GetAllCities() {
        const URL = `${Base_URL}/api/City/all`;
        const response = await fetch(URL, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if(!response.ok) {
            console.error('Could not get cities from the API!')
            return null;
        }

        return response.json();
    }

    // Returns true if successful and false if failed
    async CreateNewCity(city) {
        const URL = `${Base_URL}/api/City/new`;
        const response = await fetch(URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(city)
        });

        if(!response.ok) {
            console.error('Could not create new city.')
            if(response.status === 400) { /* Bad Request */
                alert(await response.text())
            }
            return false;
        }

        return true;
    }

}

export const CityAPI = new _CityAPI();