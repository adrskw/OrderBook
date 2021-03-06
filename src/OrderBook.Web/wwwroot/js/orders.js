$(function () {
    const requestUrl = $("#ordersTable").data("request-url");
    const detailsUrl = $("#ordersTable").data("details-url");
    const editUrl = $("#ordersTable").data("edit-url");
    const ordersStatus = $("#ordersTable").data("orders-status");

    var table = $("#ordersTable").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: requestUrl,
            type: "POST",
            data: {
                "statusOfListedOrders": ordersStatus
            }
        },
        "columns": [
            {
                "data": "orderDate", render: function (data, type, row) {
                    let d = new Date(data);

                    return ("0" + d.getDate()).slice(-2) + "." +
                        ("0" + (d.getMonth() + 1)).slice(-2) + "." +
                        d.getFullYear() + "<br>" +
                        ("0" + d.getHours()).slice(-2) + ":" +
                        ("0" + d.getMinutes()).slice(-2);
                }
            },
            {
                "data": "products", render: function (data, type, row) {
                    let str = "";
                    for (var i = 0; i < data.length; i++) {
                        str += data[i].product.name + " x" + data[i].quantity + "<br>";
                    }

                    return str;
                }
            },
            {
                "data": "customer", render: function (data, type, row) {
                    let str = data.firstName + " " + data.lastName;
                    if (data.companyName) {
                        str += "<br>" + data.companyName;
                    }

                    return str;
                }
            },
            {
                "data": "status", render: function (data, type, row) {
                    switch (data) {
                        case 0:
                            return "anulowane";
                        case 1:
                            return "nieopłacone";
                        case 2:
                            return "opłacone";
                        case 3:
                            return "w realizacji";
                        case 4:
                            return "do wysłania";
                        case 5:
                            return "wysłane";
                        case 6:
                            return "zwrócone";
                        case 7:
                            return "zwrot środków";
                    }
                }
            },
            {
                "data": "totalToPay", render: function (data, type, row) {
                    return data.toString().replace(".", ",");
                }
            },
            {
                "data": "id", render: function (id, type, row) {
                    return '<div class="dropdown"><button class="btn btn-outline-primary dropdown-toggle" type="button" data-toggle="dropdown" data-boundary="window" aria-haspopup="true" aria-expanded="false">Akcje</button><div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton"><a class="dropdown-item" href="' + detailsUrl + "/" + id + '">Szczegóły</a><a class="dropdown-item" href="' + editUrl + "/" + id + '">Edytuj</a><a class="dropdown-item" href="#" data-record-id="' + id + '" data-record-title="' + row.customer.firstName + ' ' + row.customer.lastName + '" data-toggle="modal" data-target="#confirmDelete">Usuń</a></div></div>';
                }
            }
        ],
        "columnDefs": [
            {
                className: "align-middle text-right text-nowrap",
                width: "1%",
                targets: [0, 3, 4, 5]
            },
            {
                className: "align-middle",
                targets: "_all"
            }
        ],
        "autoWidth": true,
        "ordering": false,
        "order": [[1, "asc"]],
        "lengthChange": true,
        "paging": true,
        "pagingType": "simple_numbers",
        "language": {
            "decimal": ",",
            "thousands": " ",
            "processing": "Przetwarzanie...",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ pozycji",
            "info": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "infoEmpty": "Pozycji 0 z 0 dostępnych",
            "infoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "infoPostFix": "",
            "loadingRecords": "Wczytywanie...",
            "zeroRecords": "Nie znaleziono pasujących pozycji",
            "emptyTable": "Brak danych",
            "paginate": {
                "first": "<i class=\"fas fa-angle-double-left fa-lg\"></i>",
                "last": "<i class=\"fas fa-angle-double-right fa-lg\"></i>",
                "previous": "<i class=\"fas fa-angle-left fa-lg\"></i>",
                "next": "<i class=\"fas fa-angle-right fa-lg\"></i>"
            },
            "aria": {
                "sortAscending": ": aktywuj, by posortować kolumnę rosnąco",
                "sortDescending": ": aktywuj, by posortować kolumnę malejąco"
            }
        }
    });

    $("#ordersTable thead th").removeClass("text-right");
});