export default class AccountService {
    static async Login(username, password) {
        let response = await fetch('https://localhost:44396/api/account/login', {
            credentials: 'same-origin',
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ username, password })
        })
        .then(response => console.log(response.headers.get('set-cookie')));
        return response;
    }
}