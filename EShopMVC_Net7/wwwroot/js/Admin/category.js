document.addEventListener("alpine:init", () => {
    Alpine.data("category", () => ({
        _list: [],
        init() {
            fetch("/Admin/Category/ListAll")
                .then(x => x.json())
                .then(json => {
                    this._list = json;
                })
                .catch(err => {
                    console.log(err);
                });
        }
    }));
});