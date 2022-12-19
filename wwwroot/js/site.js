// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

function AddToCartAlert() {
    Swal.fire({
        icon: 'success',
        title: 'Đã thêm vào giỏ hàng',
        showConfirmButton: false,
        timer: 1500
    });
}

$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        loop: true,
        autoplay: true,
        autoplayTimeout: 2000,
        autoplayHoverPause: true
    });
});

var btn = $('#backtotop');

$(window).scroll(function () {
    if ($(window).scrollTop() > 300) {
        btn.addClass('show');
    } else {
        btn.removeClass('show');
    }
});
btn.on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, '300');
});

$(document).ready(function () {
    var output = document.getElementById('ImageUrlPreview');
    output.src = $("#ProfilePicture").val();
})
$("#ProfilePicture").on("change", function () {
    var output = document.getElementById("ImageUrlPreview");
    output.src = $(this).val();
})

$(".RadioType").click(function () {
    $(".RadioType").prop("checked", false);
    $(this).prop("checked", true);
});

let searchForm = document.querySelector('.search-form');

document.querySelector('#search-btn').onclick = () => {
    searchForm.classList.toggle('active');
}