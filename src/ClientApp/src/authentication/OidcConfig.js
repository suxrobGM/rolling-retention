export const OidcConfig = {
    onSignIn: async (user) => {
        localStorage.setItem("user", JSON.stringify(user));
    },

    onSignOut: async () => {
        localStorage.removeItem("user");
        window.location.href = "https://localhost:6001/Identity/Account/Logout";
    },

    clientId: "spa_client",
    clientSecret: "super_secret_string",
    responseType: "code",
    scope: "app.api.client openid profile offline_access",

    apiHost: "https://api.suxrobgm.net",
    authority: "https://id.suxrobgm.net",

    // apiHost: process.env.NODE_ENV === "development"
    //     ? "http://localhost:5001"
    //     : "https://api.suxrobgm.net",

    // authority: process.env.NODE_ENV === "development"
    //     ? "http://localhost:6001"
    //     : "https://id.suxrobgm.net",

    redirectUri: process.env.NODE_ENV === "development"
        ? "http://localhost:3000"
        : "https://u1002275.plsk.regruhosting.ru"
}