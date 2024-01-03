function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-company-register").length) {
            var t = $("#datatable-company-register").DataTable({
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 2]
                }],
                ajax: {
                    url: "/company/get-waiting-for-approval",
                    datatype: "json",
                    dataSrc: "",
                },
                columns: [
                    {
                        render: function (data, type, row, meta) {
                            return meta.row + meta.settings._iDisplayStart + 1;
                        }
                    },
                    {
                        data: "name",
                    },
                    {
                        data: "email",
                    },
                    {
                        data: "telp",
                    },
                    {
                        data: "image",
                    },
                    {
                        data: "status",
                    },
                    {
                        data: "createdAt",
                    },
                    {
                        data: "updatedAt",
                    },
                    {
                        render: function (data, type, row, meta) {

                            return `
                                <div class="float-right d-flex flex-row">
                                   
                                     <button type="button" class="btn btn-primary btn-sm mr-2"  onclick="updateStatus('${row["vendorGuid"]}',1)">
                                        Approved
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="updateStatus('${row["vendorGuid"]}',2)">
                                        Reject
                                    </button>
                                </div>
                                `;
                        },
                    },
                ],
            });
            t.on("order.dt search.dt", function () {
                t.column(0, { search: "applied", order: "applied" })
                    .nodes()
                    .each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
            }).draw();
        }
    };

    TableManageButtons = (function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            },
        };
    })();

    $("#datatable").dataTable();

    $("#datatable-keytable").DataTable({
        keys: true,
    });

    $("#datatable-responsive").DataTable();

    $("#datatable-scroller").DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true,
    });

    $("#datatable-fixed-header").DataTable({
        fixedHeader: true,
    });

    var $datatable = $("#datatable-checkbox");

    $datatable.dataTable({
        order: [[1, "asc"]],
        columnDefs: [{ orderable: false, targets: [0] }],
    });
    $datatable.on("draw.dt", function () {
        $("checkbox input").iCheck({
            checkboxClass: "icheckbox_flat-green",
        });
    });

    TableManageButtons.init();
}

updateStatus = (vendorGuid, status) => {

    var data_input = {
        "Guid": vendorGuid,
        "userValidatorGuid": null,
        "Status": status
    }

    console.log(JSON.stringify(data_input));

    
    Swal.fire({
        title: 'Confirm Data',
        text: `You will update status this data ?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, update status!'
    }).then((isDelete) => {
        if (isDelete.isConfirmed) {

            $.ajax({
                url: `/vendor/update-status`,
                data: data_input,
                dataType: 'json',
                method: 'PUT',
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {

                    if (response.code != 200) {
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: response.message
                        });
                    } else {
                        //sweet alert message success
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: `${response.message}`,
                            showConfirmButton: false,
                            timer: 1500
                        });

                        //reload only datatable
                        $('#datatable-company-register').DataTable().ajax.reload();
                    }
                },
            })
        }
    })
}
