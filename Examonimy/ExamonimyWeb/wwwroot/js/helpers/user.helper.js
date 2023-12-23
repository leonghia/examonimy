import { User } from "../models/user.model.js";

export const RoleId = {
    Administrator: 1,
    Teacher: 2,
    Student: 3
}

export const mapByFullName = (users = [new User()]) => {
    const results = new Map();
    for (const user of users) {
        const firstLetter = user.fullName.charAt(0);
        if (!results.get(firstLetter)) {
            results.set(firstLetter, new Array());
        }
        results.get(firstLetter).push(user);
    }
    return results;
}