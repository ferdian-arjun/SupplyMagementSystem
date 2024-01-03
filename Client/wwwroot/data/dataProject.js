function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-project").length) {
            var t = $("#datatable-project").DataTable({
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 2]
                }],
                ajax: {
                    url: "project/get",
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
                        data: "description",
                    },
                    {
                        data: "startDate",
                    },
                    {
                        data: "endDate",
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
                                    <button type="button" class="btn btn-primary btn-sm mr-2" data-toggle="modal" data-target="#modalAddVendor"  onclick="addVendorModalProject('${row["guid"]}')">
                                        Select Vendor
                                    </button>
                                    <button type="button" class="btn btn-success btn-sm mr-2" data-toggle="modal" data-target="#modalEdit"  onclick="editModalProject('${row["guid"]}')">
                                        Edit
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm mr-2"  onclick="deleteModalProject('${row["guid"]}')">
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

//create data
$("#form-create-project").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    var data_input = new Object();
    data_input.Name = $("#inputName").val();
    data_input.Description = $("#inputDescription").val();
    data_input.StartDate = $("#inputStartDate").val();
    data_input.EndDate = $("#inputEndDate").val();

    console.log(data_input);

    $.ajax({
        url: '/project/post',
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
                $('#datatable-project').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err);
        }
    })
});

//Edit
editModalProject = (guid) => {
    $.ajax({
        url: `/project/get/${guid}`,
    }).done((result) => {
        console.log(result);

        //set value
        $('#inputProjectGuidEdit').val(`${result.guid}`);
        $('#inputNameEdit').val(`${result.name}`);
        $('#inputDescriptionEdit').val(`${result.description}`);
        $('#inputStartDateEdit').val(`${result.startDate}`);
        $('#inputEndDateEdit').val(`${result.endDate}`);
        $('#inputStatusEdit').val(`${result.status}`);

        if (result.status == "OnPlan") document.getElementById('inputStatusEdit').selectedIndex = 0;
        if (result.status == "OnProgress") document.getElementById('inputStatusEdit').selectedIndex = 1;
        if (result.status == "Done") document.getElementById('inputStatusEdit').selectedIndex = 2;
        if (result.status == "Canceled") document.getElementById('inputStatusEdit').selectedIndex = 3;

    }).fail((result) => {
        console.log(result);
    });
}

//update
$("#form-edit-project").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    var data_input = {
        "Guid": $("#inputProjectGuidEdit").val(),
        "Name": $("#inputNameEdit").val(),
        "Description": $("#inputDescriptionEdit").val(),
        "StartDate": $("#inputStartDateEdit").val(),
        "EndDate": $("#inputEndDateEdit").val(),
        "Status": $("#inputStatusEdit").val()
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/project/update`,
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
                $('#datatable-project').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});

//delete 
deleteModalProject = (guid) => {

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
                url: `/project/deleted/${guid}`,
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
                        $('#datatable-project').DataTable().ajax.reload();
                    }
                },
            })
        }
    })
}


//ADD VENDOR
addVendorModalProject = (guid) => {
    
    deleteAllOptions();
    
    $.ajax({
        url: `/project/get/${guid}`,
    }).done((result) => {
        console.log(result);

        //set value
        $('#inputProjectGuidAddVendor').val(`${result.guid}`);
        $('#inputNameAddVendor').val(`${result.name}`);
        
        
    }).fail((result) => {
        console.log(result);
    });

    $.ajax({
        url: `/vendor/get`,
    }).done((result) => {
        console.log(result);
        // Assuming the JSON data is an array of objects with 'value' and 'text' properties
        result.forEach(item => {
            addOption(item.guid, item.companyName);
        });
    });
}

$("#form-add-vendor").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    var data_input = {
        "projectGuid": $("#inputProjectGuidAddVendor").val(),
        "vendorGuid": $("#inputVendorGuidAddVendor").val(),
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/project/add-vendor`,
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
                document.getElementById("modalAddVendor").className = "modal fade";
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
                $('#datatable-project').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});


// Function to add options dynamically
function addOption(value, text) {
    var select = document.getElementById('inputVendorGuidAddVendor');
    var option = document.createElement('option');
    option.value = value;
    option.text = text;
    select.appendChild(option);
}

function deleteAllOptions() {
    var select = document.getElementById('inputVendorGuidAddVendor');
    select.innerHTML = ''; // Set innerHTML to an empty string to remove all options
}
