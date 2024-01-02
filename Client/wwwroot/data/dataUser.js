function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-user").length) {
            var t = $("#datatable-user").DataTable({
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 2]
                }],
                ajax: {
                    url: "user/get",
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
                        data: "username",
                    },
                    {
                        data: "fullName",
                    },
                    {
                        data: "email",
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
                                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalRole('${row["guid"]}')">
                                        Edit
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalRole('${row["guid"]}')">
                                        Delete
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

function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(` ${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}

//create data
$("#form-create-user").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    var data_input = new Object();
    data_input.Username = $("#inputUserName").val();
    data_input.Email = $("#inputEmail").val();
    data_input.Fullname = $("#inputFullName").val();
    data_input.Password = $("#inputPassword").val();

    console.log(data_input);

    $.ajax({
        url: '/user/post',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
            console.log(response);

            if (response.code != 201) {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message
                });

            } else {

                //idmodal di hide
                document.getElementById("modalCreate").className = "modal fade";
                $('.modal-backdrop').remove();


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${response.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#datatable-user').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err);
        }
    })
});

//Edit
editModalRole = (guid) => {
    $.ajax({
        url: `/user/get/${guid}`,
    }).done((result) => {
         console.log(result);

        //set value
        $('#inputUserGuidEdit').val(`${result.guid}`);
        $('#inputFullNameEdit').val(`${result.fullName}`);
        $('#inputEmailEdit').val(`${result.email}`);
        $('#inputUserNameEdit').val(`${result.username}`);

    }).fail((result) => {
        console.log(result);
    });
}

//update
$("#form-edit-user").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();
 
    var data_input = {
        "Guid": $("#inputUserGuidEdit").val(),
        "FullName": $("#inputFullNameEdit").val(),
        "UserName": $("#inputUserNameEdit").val(),
        "Email": $("#inputEmailEdit").val()
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/user/update`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            console.log(response);

            if (response.code != 200) {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message
                });
            } else {

                //idmodal di hide
                document.getElementById("modalEdit").className = "modal fade";
                $('.modal-backdrop').remove();


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${response.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#datatable-user').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});

//delete 
deleteModalRole = (guid) => {

    console.log(guid);

    Swal.fire({
        title: 'Delete Data',
        text: `You will delete this data ?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete!'
    }).then((isDelete) => {
        if (isDelete.isConfirmed) {

            $.ajax({
                url: `/user/deleted/${guid}`,
                method: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {
                    
                    if (response.code != 200) {
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: response.message
                        });
                    } else {
                        Swal.fire(
                            'Deleted!',
                            `Data deleted successfully`,
                            'success'
                        );
                        
                        //reload only datatable
                        $('#datatable-user').DataTable().ajax.reload();
                    }
                },
            })
        }
    })
}