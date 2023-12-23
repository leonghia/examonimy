export class User {
    id = 0;
    fullName = "";
    userName = "";
    email = "";
    dateOfBirth;
    role;
    profilePicture = "";
}

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