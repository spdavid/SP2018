import { UserAgentApplication, AuthError } from 'msal';
import { msalConfig } from './msalConfig';
import { PnPFetchClient} from './pnpFetchClient';
import { graph } from '@pnp/graph';
import { sp } from '@pnp/sp';


var msalInstance = new UserAgentApplication(msalConfig);

export class MsalAuth {
    public static LogInUser() {
        msalInstance.handleRedirectCallback(() => { // on success
            console.log("user is logged in");
           
        }, (authErr: AuthError, accountState: string) => {  // on fail
            console.log(authErr);
        });

        let account = msalInstance.getAccount()
console.log(account);

        if (!account) {
            var requestObj = {
                scopes: ["user.read", "mail.read"]
            };
            msalInstance.loginRedirect({});
            return;
        }
        else
        {
            this.initPnPjs();
        }

       

    }

    private static initPnPjs(): void {
        const fetchClientFactory = () => {
          return new PnPFetchClient(msalInstance);
        };
  
        sp.setup({
          sp: {
            fetchClientFactory,
            baseUrl: "https://folkis2018.sharepoint.com/sites/david"
          }
        });
  
        graph.setup({
          graph: {
            fetchClientFactory
          }
        });
      }



}