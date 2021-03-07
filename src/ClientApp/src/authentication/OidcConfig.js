export const OidcConfig = {
    onSignIn: async (user) => {
        localStorage.setItem("user", JSON.stringify(user));
    },

    onSignOut: async () => {
        localStorage.removeItem("user");
        window.location.href = "https://localhost:6001/Identity/Account/Logout";
    },

    authority: "https://localhost:6001",
    clientId: "spa_client",
    clientSecret: "super_secret_string",
    responseType: "code",
    redirectUri: "http://localhost:3000",
    scope: "app.api.client openid profile offline_access",
}