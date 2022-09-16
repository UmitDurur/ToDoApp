import { UserManager, WebStorageStateStore, User } from 'oidc-client';
 
export default class AuthService {
    private userManager: UserManager;
 
    constructor() {
        const STS_DOMAIN: string = 'https://localhost:44352';
 
        const settings: any = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            
            authority: STS_DOMAIN,
            client_id: "ToDoApp_Vue",
            client_secret:"1q2w3e*",
            userName:"admin",
            Password:"1q2w3E*",
            redirect_uri: 'https://localhost:8080/authentication/login-callback',
            //automaticSilentRenew: true,
            //silent_redirect_uri: 'https://localhost:8080/silent-renew.html',
            response_type: 'code',
            scope: 'ToDoApp',
            //post_logout_redirect_uri: 'https://localhost:8080/',
            //filterProtocolClaims: true,
        };
 
        this.userManager = new UserManager(settings);
    }
 
    public getUser(): Promise<User|null> {
            return this.userManager.getUser();
    }
 
    public login(): Promise<void> {
        return this.userManager.signinRedirect();
    }
 
    public logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }
 
    public getAccessToken(): Promise<string> {
        return this.userManager.getUser().then((data: any) => {
            return data.access_token;
        });
    }
}