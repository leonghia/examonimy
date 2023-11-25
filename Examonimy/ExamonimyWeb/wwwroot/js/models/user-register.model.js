export class UserRegister {
    fullName = "";
    email = "";
    username = "";
    password = "";

    constructor(fullName = "", email = "", username = "", password = "") {
        this.fullName = fullName;
        this.email = email;
        this.username = username;
        this.password = password;
    }
}