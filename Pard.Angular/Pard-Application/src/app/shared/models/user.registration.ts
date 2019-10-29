export class UserRegistration {
    email: string;
    login: string;
    password: string;
    firstName: string;
    lastName: string;
    isAdmin: boolean;
    isSuperAdmin: boolean;

    constructor(email: string, login: string, password: string, firstName: string, lastName: string) {
        this.email = email;
        this.lastName = lastName;
        this.login = login;
        this.firstName = firstName;
        this.password = password;
        this.isAdmin = false;
        this.isSuperAdmin = false;
    }
}
