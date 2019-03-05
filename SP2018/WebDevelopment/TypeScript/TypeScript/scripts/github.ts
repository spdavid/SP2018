



//let url = "https://api.github.com/search/repositories?q=sharepoint";
//function callbackf(resp: Response) {

//}
//fetch(url).then(callbackf);

function GetResults() {
    let searchText = (document.getElementById("txtSearchText") as HTMLInputElement).value;
    let resultsElement = document.getElementById("results") as HTMLDivElement;
    let url = "https://api.github.com/search/repositories?q=" + searchText;
    fetch(url).then((resp) => {
        if (resp.ok) {
            resp.json().then(obj => {
                console.log(obj);
                let items = obj.items as Array<any>;
                resultsElement.innerHTML = "";
                items.forEach(item => {
                    resultsElement.innerHTML += "<div class='item'>" + item.full_name + "</div>"
                });
            });
        }
        else {
            console.log(resp.status);
            console.log(resp.statusText);

        }

    });

}