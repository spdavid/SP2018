import { Configuration} from 'msal';
export const msalConfig : Configuration = {
    auth: {
        clientId: "6d6f87ce-1fef-44a1-b956-fc72c7eb4b40",
        authority: "https://login.microsoftonline.com/d4dbfa86-7647-4f41-8f35-c0a5c82c2cd3"
    },
    cache: {
        cacheLocation: "localStorage",
        storeAuthStateInCookie: true
    }
};
