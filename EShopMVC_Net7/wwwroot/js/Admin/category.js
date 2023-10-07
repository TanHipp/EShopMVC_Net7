document.addEventListener("alpine:init", () => {
    Alpine.data("category", () => ({
        _list: [],
        _modal: {},
        _noti: {},
        _modalSetting: {
            title: "",
            url: "",
            primaryButtontext: "",
        },
        _updinData: {
            id: 0,
            name: "",
            slug: "",
        },


        init() {
            var config = {
                durations: {
                    success: 2000

                },
                labels: {
                    success: "Thành công"
                }
            };
            this._modal = new bootstrap.Modal("#categoryUpdinModal");
            this._noti = new AWN(config); // AWN là 1 object
            this.$watch('_updinData.name', (newVal, oldVal) => {
                this._updinData.slug = newVal.toLowerCase()
                    .normalize("NFD")
                    .replace(/[\u0300-\u036f]/g, "")
                    .replace(/đ/g, "d")
                    .replace(/[^a-z]/g, "-");
            });


            this.refreshData();

        },

        refreshData() {
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
                title: "Thêm mới danh mục sản phẩm",
                url: "/Admin/Category/Upsert/",
                primaryButtonText: "Thêm mới",
            },
                // Xoa du lieu khi modal 
                this._updinData = {
                    id: 0,
                    name: "",
                    slug: "",
                };
        },
        openModalUpdate(id) {
            this._modal.show();
            this._modalSetting = {
                title: "Cập nhật danh mục sản phẩm",
                url: "/Admin/Category/Upsert/" + id,
                primaryButtonText: "Cập nhật",
            }
            fetch("Admin/Category/Detail/" + id)
                .then(res => res.json())
                .then(json => {
                    this._updinData = json;
                });

        },

        closeModal() {
            // Xóa dữ liệu trong biến _updinData = null
            this._updinData.name = "";
            this._updinData.slug = "";
        },


        saveCategory() {
            fetch(this._modalSetting.url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(this._updinData) // Chuyển đối tượng của JS thành kiểu chuỗi có dạng JSON
            })
                .then(res => {
                    this._modal.hide();
                    return res.text();
                })

                .then(text => {
                    this._noti.success(text);
                    this.refreshData();

                })
                .catch(err => {
                    alert("Lỗi !");
                })
        },

        RemoveCategory(id) {
            var url = "/Admin/Category/Delete/" + id;
            this._noti.confirm("Xác nhận xóa", () => {
                fetch(url)
                    .then(res => {
                        if (res.status == 200) {
                            this._noti.success("Xóa thành công...!");
                        } else {
                            this._noti.alert("Xóa sản phẩm thất bại");
                        }
                    });
                this.refreshData();
            });
        }
    }));
});