
    $(document).ready(function () {


        //$('#btnUpdate').on('click', function () {
        //    selectedEmployeeId = 0;
        //    $('#employeeId').val(0);
        //    $('#txtFirstName').val('');
        //    $('#txtLastName').val('');

        //    $('#employeeModal').modal('show');
        //});

        // Add click event handler to the "Update" buttons

        // Show modal for adding new employee
        $('#addEmployee').on('click', function () {
            selectedEmployeeId = 0;
            $('#employeeId').val(0);
            $('#txtFirstName').val('');
            $('#txtLastName').val('');

            $('#employeeModal').modal('show');
        });
    $('#saveEmployee').on('click', function () {
            debugger;
    var employee = {
        EmployeeId: $('#employeeId').val(),
    FirstName: $('#txtFirstName').val(),
    LastName: $('#txtLastName').val(),
    Gender: $('#genderDropdown').val()
            };

    if (employee.FirstName == "" || employee.LastName == "" || employee.Gender == 0 || employee.Gender == null)
    return  toastr.error("Fill Missing Fields!");

    $.ajax({
        url: '/Admin/AddEmployee',
    type: 'POST',
    data: employee,
    success: function (response) {
        if (response.success) {
            if (employee.EmployeeId > 0)
                toastr.success("Updated successfully!");

            else
                toastr.success("Added successfully!");

                        setTimeout(function () {
                            location.reload();
                        }, 2000);

    $('#employeeModal').modal('hide');
                    } else {
        alert('Failed to update employee. Please check the errors.');
                    }
                },
    error: function () {
        alert('Error occurred while updating employee.');
                }
            });
            //$('#employeeModal').modal('show');
        });
    });


function ConfirmDelete(employeeId) {
    // Show a confirmation message using Toastr
    toastr.options = {
        closeButton: true,
        progressBar: true,
        positionClass: 'toast-top-right',
        timeOut: '5000',
        extendedTimeOut: '2000',
    };

    toastr.warning('Are you sure you want to delete this employee?If Yes then Click "Close" Icon.', 'Confirmation', {
        closeButton: true,
        positionClass: 'toast-top-center',
        tapToDismiss: false,
        onclick: function () {
            // User clicked on the confirmation message
            // You can perform additional actions if needed
        },
        onCloseClick: function () {
            // User clicked on the close button
            // Implement the deletion logic if the user confirmed
            Delete(employeeId);
        }
    });
}
    function Edit(id, firstName, lastName, gender) {
        debugger;

    $('#employeeId').val(id);
    $('#txtFirstName').val(firstName)
    $('#txtLastName').val(lastName);
    if(gender =="Male")
    $('#genderDropdown').val(1);
    else
    $('#genderDropdown').val(2);

    $('#employeeModal').modal('show');
   }
    function Delete(id)
    {

         debugger;
    var employee = {
        EmployeeId: id,
    IsActive: false,
            };
    $.ajax({
        url: '/Admin/DeleteEmployee',
    type: 'POST',
    data: employee,
    success: function (response) {
                if (response.success) {
        toastr.success("Deleted successfully!");
                    setTimeout(function () {
                        location.reload();
                    }, 1000);

    $('#employeeModal').modal('hide');
                } else {
        alert('Failed to update employee. Please check the errors.');
                }
            },
    error: function () {
        alert('Error occurred while updating employee.');
            }
        });
    }
