document.addEventListener("alpine:init", () => {
    Alpine.data("category", () => ({
        _list: [],
        _modal: {},
        _modalSetting: {
            tittle: "",
            url: "",
            primaryButtontext: "",
        },
        _updinData: {
            id: 0,
            name: "",
            slug: "",
        },


        init() {
            this._modal = new bootstrap.Modal("#categoryUpdinModal");
            this.$watch('_updinData.name', (newVal, oldVal) => {
                this._updinData.slug = newVal.toLowerCase()
                    .normalize("NFD")
                    .replace(/[\u0300-\u036f]/g, "")
                    .replace("đ", "d")
                    .replace(/[^a-z]/g, "-");
            });
            fetch("/Admin/Category/ListAll")
                .then(x => x.json())
            .then(json => {
                this._list = json;
            })
            .catch(err => {
                console.log(err);
            });
    },

        openModalAdd() {
        this._modal.show();
        this._modalSetting = {
            tittle: "Them danh muc san pham",
            url: "", //Bo sung sau
            primaryButtontext: "Them moi",
        }

    },

        openModalUpdate() {
        this._modal.show();
        this._modalSetting = {
            tittle: "Cap nhat danh muc san pham",
            url: "",
            primaryButtontext: "Cap nhat",
        }

    },

        saveCategory() {

    },

    }));
});