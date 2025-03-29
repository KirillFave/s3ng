export function getTokenFromLocalStorage() {
    const data = localStorage.getItem("accessToken")
    if (!data) return ""

    try {
        return JSON.parse(data);
    } catch (error) {
        return "";
    }
}

export function setAccessTokenToLocalStorage(token) {
    localStorage.setItem("accessToken", JSON.stringify(token))
}

export function removeAccessTokenToLocalStorage() {
    localStorage.removeItem("accessToken")
}