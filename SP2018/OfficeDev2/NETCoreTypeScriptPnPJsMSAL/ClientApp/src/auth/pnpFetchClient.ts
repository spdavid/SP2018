
import { BearerTokenFetchClient, FetchOptions, isUrlAbsolute } from '@pnp/common';
import { UserAgentApplication, AuthenticationParameters } from 'msal';

export class PnPFetchClient extends BearerTokenFetchClient {
  constructor(private authContext: UserAgentApplication) {
    super(null);
  }

  public async fetch(url: string, options: FetchOptions = {}): Promise<Response> {
      console.log("I am fetching");
    if (!isUrlAbsolute(url)) {
      throw new Error('You must supply absolute urls to PnPFetchClient.fetch.');
    }

    const token = await this.getToken(this.getResource(url));
    this.token = token as string;
    return super.fetch(url, options);
  }

  private async getToken(resource: string): Promise<string | undefined> {

    const request: AuthenticationParameters = {
    };

    if (resource.indexOf('sharepoint') !== -1) {
      request.scopes = [`${resource}/AllSites.FullControl`];
    } else if (resource.indexOf('graph') !== -1) {
      request.scopes = [`${resource}/Mail.Read`, `${resource}/User.Read`, `${resource}/Files.Read`]
    }
    console.log(resource);
    try {
      const response = await this.authContext.acquireTokenSilent(request);

      return response.accessToken;
    } catch (error) {
      if (this.requiresInteraction(error.errorCode)) {
        this.authContext.loginRedirect(request);
      } else {
        throw error;
      }
    }
  }

  private requiresInteraction(errorCode: string) {
    if (!errorCode || !errorCode.length) {
      return false;
    }
    return errorCode === "consent_required" ||
      errorCode === "interaction_required" ||
      errorCode === "login_required";
  }

  private getResource(url: string): string {
    const parser = document.createElement('a') as HTMLAnchorElement;
    parser.href = url;
    return `${parser.protocol}//${parser.hostname}`;
  }
}
