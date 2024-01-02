$
$(document).ready(function () {
    var pageSize = 10; // Số sản phẩm trên mỗi trang
    var totalItems = $('.product-grid .product-item').length; // Tổng số sản phẩm
    var pageCount = Math.ceil(totalItems / pageSize); // Tính tổng số trang

    for (var i = 0; i < pageCount; i++) {
        $('#pagination').append('<a href="#" class="page-link">' + (i + 1) + '</a>');
    }

    $('.product-grid .product-item').slice(pageSize).hide();
    $('.page-link:first').addClass('active');

    $('.page-link').click(function (e) {
        e.preventDefault();
        $('.page-link').removeClass('active');
        $(this).addClass('active');

        var currentPage = $(this).index();
        var startItem = currentPage * pageSize;
        var endItem = startItem + pageSize;
        $('.product-grid .product-item').hide().slice(startItem, endItem).show();
    });
});
