var GitHubPersonSearch = /** @class */ (function () {
    function GitHubPersonSearch() {
        var _this = this;
        this.DoSearch = function () {
            var searchText = _this.textSearch.value;
            _this.SearchGitHub(searchText).then(function (results) {
                console.log(results);
                _this.renderResults(results);
            }).catch(function (error) {
                console.log(error);
            });
        };
        this.renderResults = function (results) {
            console.log(_this.resultsElement);
            _this.resultsElement.innerHTML = '';
            results.forEach(function (item) {
                _this.resultsElement.innerHTML +=
                    "<div class='item'>\n                    <img src='" + item.avatar_url + "' />\n                    <div>\n                        " + item.login + "\n                        <a href='" + item.html_url + "'>" + item.html_url + "</a>\n                    </div>\n                </div>";
            });
        };
        console.log(this);
        this.buttonSearch = document.getElementById("searchButton");
        this.textSearch = document.getElementById("textSearch");
        this.resultsElement = document.getElementById("results");
        this.buttonSearch.onclick = this.DoSearch;
    }
    GitHubPersonSearch.prototype.SearchGitHub = function (text) {
        return new Promise(function (resolve, reject) {
            var url = "https://api.github.com/search/users?q=" + text;
            fetch(url).then(function (response) {
                if (response.ok) {
                    console.log(response);
                    response.json().then(function (r) {
                        console.log(r);
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
    };
    return GitHubPersonSearch;
}());
document.addEventListener("DOMContentLoaded", function (event) {
    new GitHubPersonSearch();
});
//# sourceMappingURL=Assignment.js.map