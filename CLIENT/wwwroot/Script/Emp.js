var datenow = new Date();
var Departments = [];
$(document).ready(function () {
    table = $('#Employee').dataTable({
        "ajax": {
            url: "/emp/cloadEmp",
            type: "GET",
            dataType: "json",
            dataSrc: ""
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
            { "orderable": false, "targets": 8 },
            { "searchable": false, "targets": 8 }
        ],
        "columns": [
            //{ "data": "id" },
            { "data": "nameEmp" },
            { "data": "deptName" },
            { "data": "email" },
            {
                "data": "birthDate", "render": function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            { "data": "phoneNumber" },
            { "data": "address" },
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
                    return " <td><button type='button' class='btn btn-warning' id='BtnEdit' onclick=GetById('" + row.email + "');>Edit</button> <button type='button' class='btn btn-danger' id='BtnDelete' onclick=Delete('" + row.email + "');>Delete</button ></td >";
                }
            },
        ]
    });
}); //load table department
/*--------------------------------------------------------------------------------------------------*/
function LoadDepartment(element) {
    if (Departments.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Dept/cloadDepartment",
            success: function (data) {
                Departments = data.data;
                renderDepartment(element);
            }
        });
    }
    else {
        renderDepartment(element);
    }
}

function renderDepartment(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(Departments, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadDepartment($('#DepartmentOption'));
/*--------------------------------------------------------------------------------------------------*/
document.getElementById("btnadd").addEventListener("click", function () {
    clearscreen();
    $('#SaveBtn').show();
    $('#UpdateBtn').hide();
    LoadDepartment($('#DepartmentOption'));
    $('#divemail').show();
    $('#divpass').show();
}); //fungsi btn add

function clearscreen() {
    $('#Email').val('');
    $('#Password').val('');
    $('#FirstName').val('');
    $('#LastName').val('');
    LoadDepartment($('#DepartmentOption'));
    $('#BirthDate').val('');
    $('#PhoneNumber').val('');
    $('#Address').val('');
}
/*--------------------------------------------------------------------------------------------------*/
function GetById(Email) {
    debugger;
    $.ajax({
        url: "/Emp/GetById/" + Email,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        data: { "Email": Email },
        success: function (result) {
            debugger;
            $('#FirstName').val(result[0].firstName);
            $('#LastName').val(result[0].lastName);
            $('#DepartmentOption').val(result[0].deptId);
            $('#BirthDate').val(moment(result[0].birthDate).format('YYYY-MM-DD'));
            $('#PhoneNumber').val(result[0].phoneNumber);
            $('#Address').val(result[0].address);
            $('#myModal').modal('show');
            $('#UpdateBtn').show();
            $('#SaveBtn').hide();
            $('#Email').val(result[0].email);
            $('#Password').val(result[0].password);
            $('#divemail').hide();
            $('#divpass').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responsText);
        }
    })
} //get id to edit
/*--------------------------------------------------------------------------------------------------*/
function Save() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Employee').DataTable({
        "ajax": {
            url: "/Emp/cloadEmp"
        }
    });
    var Employee = new Object();
    Employee.email = $('#Email').val();
    Employee.Password = $('#Password').val();
    Employee.firstName = $('#FirstName').val();
    Employee.lastName = $('#LastName').val();
    Employee.deptModelId = $('#DepartmentOption').val();
    Employee.birthDate = $('#BirthDate').val();
    Employee.phoneNumber = $('#PhoneNumber').val();
    Employee.address = $('#Address').val();
    if ($('#Email').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Emp/Insert',
            data: Employee
        }).then((result) => {
            if (result.statusCode === 200 || result.statusCode === 201 || result.statusCode === 204) {
                Swal.fire({
                    icon: 'success',
                    potition: 'center',
                    title: 'Employee Add Successfully',
                    timer: 2500
                }).then(function () {
                    table.ajax.reload();
                    $('#myModal').modal('hide');
                    clearscreen();
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
    var table = $('#Employee').DataTable({
        "ajax": {
            url: "/Emp/cloadEmp"
        }
    });
    var Employee = new Object();
    Employee.email = $('#Email').val();
    Employee.Password = $('#Password').val();
    Employee.firstName = $('#FirstName').val();
    Employee.lastName = $('#LastName').val();
    Employee.deptModelId = $('#DepartmentOption').val();
    Employee.birthDate = $('#BirthDate').val();
    Employee.phoneNumber = $('#PhoneNumber').val();
    Employee.address = $('#Address').val();
    $.ajax({
        type: 'POST',
        url: '/Emp/Edit',
        data: Employee
    }).then((result) => {
        debugger;
        if (result.statusCode === 200 || result.statusCode === 201 || result.statusCode === 204) {
            Swal.fire({
                icon: 'success',
                potition: 'center',
                title: 'Employee Update Successfully',
                timer: 2500
            }).then(function () {
                table.ajax.reload();
                $('#myModal').modal('hide');
                clearscreen();
            });
        } else {
            Swal.fire('Error', 'Failed to Edit', 'error');
        }
    })
}//function edit
/*--------------------------------------------------------------------------------------------------*/
function Delete(Email) {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Employee').DataTable({
        "ajax": {
            url: "/Emp/cloadEmp"
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
                url: "/Emp/Delete/",
                data: {Email:Email}
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
                        clearscreen();
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