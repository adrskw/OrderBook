$(function () {
    $("#invoiceFormData").hide();
    $("#CustomerInvoiceIsInvoiceRequired").click(function () {
        if ($(this).is(":checked")) {
            $("#invoiceFormData").show(300);
        }
        else {
            $("#invoiceFormData").hide(200);
        }
    });

    $("#orderedProducts select").removeAttr("multiple");
    let productsCount = 1;

    $('#addProduct').on('click', function () {
        const defaultProduct = $("#orderedProducts div.form-row:first").clone();

        defaultProduct.find("select").val("");
        defaultProduct.find("input").val("");
        $('#orderedProducts').append(defaultProduct);
    });

    $('#removeProduct').on('click', function () {
        const products = $("#orderedProducts").children();

        if (products.length > 1) {
            products.last().remove();
        }
    });
});