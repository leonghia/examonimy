export class UserLogin {
    email = "";
    password = "";
    rememberMe = false;

    constructor(email = "", password = "", rememberMe = false) {
        this.email = email;
        this.password = password;
        this.rememberMe = rememberMe;
    }
}