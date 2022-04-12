export default class ShipsService {
    static async Get(query, callback) {

        let dest = "https://localhost:44396/api/ship"

        fetch(dest + query)
        .then(response => {
          return response.json()
        })
        .then(data => {
          callback(data);
        })
    }
}