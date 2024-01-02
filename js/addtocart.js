$(document).ready(function () {
    ShowcountItem();
    $('body').on('click', '#cartt', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tquantity = $('#quantity_value').val();
        if (tquantity != null) {
            quantity = parseInt(tquantity);
        }   
        $.ajax({
            url: "/Cart/AddToCart",
            type: 'POST', 
            data: { id: id, quantity: quantity }, 
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Are you sure you want to remove the product from the cart?");
        if (conf == true) {
            $.ajax({
                url: "/Cart/Delete",
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    $('#checkout_items').html(rs.Count);
                    $('#tr_' + id).remove();
                }
            });
        }

    });
    $('body').on('click', '.btnRemoveAll', function (e) {
        e.preventDefault();
        var conf = confirm("Are you sure you want to remove all the product from the cart?");
        if (conf == true) {
            DeleteAll();
        }

    });
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);

    });
    $('#btnRemoveSelected').click(function () {
        var selectedIds = $('.checkbox:checked').map(function () {
            return $(this).data('id');
        }).get();

        if (selectedIds.length === 0) {
            alert('Vui lòng chọn ít nhất một sản phẩm.');
            return;
        }

        var conf = confirm('Are you sure you want to remove selected products from the cart?');
        if (conf) {
            deleteSelectedProducts(selectedIds);
        }
    });

});

function ShowcountItem(){
    $.ajax({
        url: "/Cart/ShowCountitem",
        type: 'GET',
        success: function (rs) {
                $('#checkout_items').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: "/Cart/DeleteAll",
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {     
                LoadCart();
            }
        }
    });
}
function Update(id, quantity) {
    $.ajax({
        url: "/Cart/Update",
        type: 'POST',
        data: { id: id ,quantity:quantity},
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function deleteSelectedProducts(ids) {
    $.ajax({
        url: '/Cart/DeleteSelected',
        type: 'POST',
        data: { ids: ids },
        success: function (response) {
            if (response.Success) {
                ids.forEach(function (id) {
                    $('#tr_' + id).remove();
                });
                alert('Các sản phẩm đã được xóa thành công.');
            } else {
                alert('Đã xảy ra lỗi: ' + response.Message);
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: "/Cart/Partial_cart",
        type: 'GET',
        success: function (rs) {
            $('#loadcart').html(rs);
            ShowcountItem();
        }
    });
}

