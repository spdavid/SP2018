//let url = "https://api.github.com/search/repositories?q=sharepoint";
//function callbackf(resp: Response) {
//}
//fetch(url).then(callbackf);
function GetResults() {
    var searchText = document.getElementById("txtSearchText").value;
    var resultsElement = document.getElementById("results");
    var url = "https://api.github.com/search/repositories?q=" + searchText;
    fetch(url).then(function (resp) {
        if (resp.ok) {
            resp.json().then(function (obj) {
                console.log(obj);
                var items = obj.items;
                resultsElement.innerHTML = "";
                items.forEach(function (item) {
                    resultsElement.innerHTML += "<div class='item'>" + item.full_name + "</div>";
                });
            });
        }
        else {
            console.log(resp.status);
            console.log(resp.statusText);
        }
    });
}
//# sourceMappingURL=github.js.map