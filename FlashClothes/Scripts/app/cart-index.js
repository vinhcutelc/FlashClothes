$(document).ready(function () {
    var cart = {
        init: function () {
            cart.regEvents();
        },
        regEvents: function () {
            $('#btnUpdate').off('click').on('click', function () {
                var listProduct = $('.inputQuantity');
                var cartList = [];
                $.each(listProduct, function (i, item) {
                    cartList.push({
                        Quantity: $(item).val(),
                        Product: {
                            ID: $(item).data('id')
                        }
                    })
                })
                $.ajax({
                    url:"/Cart/Update",
                    data: { cartModel: JSON.stringify(cartList) },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.reload();
                        }
                    }
                })
            })

            $('#btnDeleteAll').off('click').on('click', function () {

                $.ajax({
                    url: "/Cart/DeleteAll",
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.reload();
                        }
                    }
                })
            })

            $('.btn-delete').off('click').on('click', function () {

                $.ajax({
                    url: "/Cart/Delete",
                    data: { id: $(this).data('id') },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.reload();
                        }
                    }
                })
            })
        }
    }
    cart.init();
});
