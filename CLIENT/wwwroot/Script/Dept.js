var datenow = new Date();
$(document).ready(function () {
    $('#panel1').show();
    table = $('#Department').dataTable({
        "ajax": {
            url: "/Dept/cloadDepartment",
            type: "GET",
            dataType: "json"
        },
        //dom: 'Bfrtip',
        //buttons: [
        //    {
        //        extend: 'copyHtml5',
        //        exportOptions: {
        //            columns: [0, 1, 2]
        //        }
        //    },
        //    {
        //        extend: 'csvHtml5',
        //        filename: function () {
        //            return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
        //        },
        //        title: 'Table Department',
        //        exportOptions: {
        //            columns: [0, 1, 2]
        //        }
        //    },
        //    {
        //        extend: 'excelHtml5',
        //        filename: function () {
        //            return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
        //        },
        //        title: 'Table Department',
        //        exportOptions: {
        //            columns: [0, 1, 2]
        //        }
        //    },
        //    {
        //        extend: 'pdfHtml5',
        //        filename: function () {
        //            return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
        //        },
        //        title: 'Table Department',
        //        exportOptions: {
        //            columns: [0, 1, 2]
        //        }
        //    },
        //    'print'
        //],
        "columnDefs": [
            { "orderable": false, "targets": 3 },
            { "searchable": false, "targets": 3 }
        ],
        "columns": [
            { "data": "name"},
            {
                "data": "createDate", "render": function (data) {
                    //return moment(data).format('DD/MM/YYYY');
                    return moment(data).format('DD/MM/YYYY, h:mm a');
                }
            },
            {
                "data": "updateDate", "render": function (data) {
                    var dateupdate = "Not Updated Yet";
                    var nulldate = null;
                    if (data == nulldate) {
                        return dateupdate;
                    } else {
                        return moment(data).format('DD/MM/YYYY, h:mm a');
                    }
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return " <td><button type='button' class='btn btn-warning' id='BtnEdit' onclick=GetById('" + row.id + "');>Edit</button> <button type='button' class='btn btn-danger' id='BtnDelete' onclick=Delete('" + row.id + "');>Delete</button ></td >";
                }
            },
        ]
    });
}); //load table department
/*--------------------------------------------------------------------------------------------------*/
document.getElementById("btnadd").addEventListener("click", function () {
    $('#Id').val('');
    $('#Name').val('');
    $('#SaveBtn').show();
    $('#UpdateBtn').hide();
}); //fungsi btn add
/*--------------------------------------------------------------------------------------------------*/
function GetById(id) {
    debugger;
    $.ajax({
        url: "/Dept/GetById/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            $('#Id').val(result.id);
            $('#Name').val(result.name);
            $('#myModal').modal('show');
            $('#UpdateBtn').show();
            $('#SaveBtn').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responsText);
        }
    })
} //get id to edit
/*--------------------------------------------------------------------------------------------------*/
function Save() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Department').DataTable({
        "ajax": {
            url: "/Dept/cloadDepartment"
        }
    });
    var Department = new Object();
    Department.Name = $('#Name').val();
    if ($('#Name').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Dept/Insert',
            data: Department,
        }).then((result) => {
            //if (result.StatusCode == 201) {
            if (isStatusCodeSuccess = true) {
                Swal.fire({
                    icon: 'success',
                    potition: 'center',
                    title: 'Department Add Successfully',
                    timer: 2500
                }).then(function () {
                    table.ajax.reload();
                    $('#myModal').modal('hide');
                    $('#Id').val('');
                    $('#Name').val('');
                });
            }
            else {
                Swal.fire('Error', 'Failed to Input', 'error');
            }
        })
    }
} //function save
/*--------------------------------------------------------------------------------------------------*/
function Edit() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Department').DataTable({
        "ajax": {
            url: "/Dept/cloadDepartment"
        }
    });
    var Department = new Object();
    Department.Id = $('#Id').val();
    Department.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Dept/Insert',
        data: Department
    }).then((result) => {
        debugger;
        //if (result.statusCode == 200) {
        if (isStatusCodeSuccess = true) {
            Swal.fire({
                icon: 'success',
                potition: 'center',
                title: 'Department Update Successfully',
                timer: 2500
            }).then(function () {
                table.ajax.reload();
                $('#myModal').modal('hide');
                $('#Id').val('');
                $('#Name').val('');
            });
        } else {
            Swal.fire('Error', 'Failed to Edit', 'error');
        }
    })
}//function edit
/*--------------------------------------------------------------------------------------------------*/
function Delete(Id) {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Department').DataTable({
        "ajax": {
            url: "/Dept/cloadDepartment"
        }
    });
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.value) {
            //debugger;
            $.ajax({
                url: "/Dept/Delete/",
                data: { Id: Id }
            }).then((result) => {
                debugger;
                if (result.statusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        position: 'center',
                        title: 'Delete Successfully',
                        timer: 2000
                    }).then(function () {
                        table.ajax.reload();
                        $('#myModal').modal('hide');
                        $('#Id').val('');
                        $('#Name').val('');
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'error',
                        text: 'Failed to Delete',
                    })
                    ClearScreen();
                }
            })
        }
    });
} //function delete

//$(document).ready(function () {
//    $.fn.dataTable.ext.errMode = 'none';
//    $('#Department').DataTable({
//        dom: 'Bfrtip',
//        buttons: [
//            'copy', 'csv', 'excel', 'pdf', 'print'
//        ]
//    });
//});