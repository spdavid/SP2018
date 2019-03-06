
interface IPersonResult {
    avatar_url: string;
    html_url: string;
    login : string;
}


class GitHubPersonSearch {

    resultsElement: HTMLDivElement;
    buttonSearch: HTMLInputElement;
    textSearch: HTMLInputElement;

    constructor() {
        console.log(this);
        this.buttonSearch = document.getElementById("searchButton") as HTMLInputElement;
        this.textSearch = document.getElementById("textSearch") as HTMLInputElement;
        this.resultsElement = document.getElementById("results") as HTMLDivElement;
        this.buttonSearch.onclick = this.DoSearch;

    }

    private DoSearch = () => {
        let searchText = this.textSearch.value;
        this.SearchGitHub(searchText).then(results => {
            console.log(results);
            this.renderResults(results);
        }).catch(error => {
            console.log(error);
        });
    }

    private renderResults = (results: Array<IPersonResult>) => {
        console.log(this.resultsElement);
        this.resultsElement.innerHTML = '';
        results.forEach(item => {
            this.resultsElement.innerHTML +=
                `<div class='item'>
                    <img src='${item.avatar_url}' />
                    <div>
                        ${item.login}
                        <a href='${item.html_url}'>${item.html_url}</a>
                    </div>
                </div>`;
        });
    }

    private SearchGitHub(text: string): Promise<Array<IPersonResult>> {
        return new Promise((resolve, reject) => {
            let url = "https://api.github.com/search/users?q=" + text;
            fetch(url).then((response) => {
                if (response.ok) { 
                    console.log(response);
                    response.json().then(r => {
                        console.log(r)
                        resolve(r.items);   
                    });
                }
                else {
                    reject(response.status);
                    console.log(response.status);
                    console.log(response.statusText);
                }
            });
        });
    }


}

document.addEventListener("DOMContentLoaded",  (event) => {
    new GitHubPersonSearch();
});
