export const saveTokenInCookie = (tokenName = "", token = "") => {
    document.cookie = `${tokenName}=${token}; max-age=604800`;
}

export const getTokenFromCookie = (tokenName = "") => {
    const cookieArr = document.cookie.split("; ");
    for (const cookie of cookieArr) {
        if (cookie.startsWith(tokenName))
            return cookie.split("=")[1];
    }
    return null;
}