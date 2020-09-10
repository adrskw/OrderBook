$(function () {
    $("#confirmDelete").on("show.bs.modal", function (e) {
        var data = $(e.relatedTarget).data();
        $(".title", this).text(data.recordTitle);
        $("#recordId").val(data.recordId);
    });
});