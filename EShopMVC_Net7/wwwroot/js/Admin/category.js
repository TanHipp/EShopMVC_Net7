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
                tittle: "Thêm mới danh mục sản phẩm",
                url: "", // Bổ sung sau
                primaryButtonText: "Thêm mới",
            }
        },

        closeModal() {
            // Xóa dữ liệu trong biến _updinData = null
            this._updinData.name = "";
            this._updinData.slug = "";
        },


        openModalUpdate() {
            this._modal.show();
            this._modalSetting = {
                tittle: "Cập nhật danh mục sản phẩm",
                url: "",
                primaryButtontext: "Cập nhật",
            }

        },



        saveCategory() {

        },

    }));
});