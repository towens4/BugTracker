// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    var url = window.location.href;
    const partialUrl = "/Error/ErrorContainer";

    
    
     
    var currentApplication = "";
    function createTableRow(errorModel)
    {
        /*var tableBody = $('.error-table');
        var newRow = tableBody.insertRow();

        //Gets row number
        const lastRow = tableBody.rows(tableBody.rows.length - 1);
        const rowNumber = lastRow.rowIndex;
        newRow.insertCell(0).textContent = rowNumber

        for (var key in errorModel)
        {
            const currentKey = errorModel.hasOwnProperty(key);
            if (currentKey != "ApplicationId" || currentKey != "ErrorId")
            {
                const cell = newRow.insertCell();
                cell.textContent = errorModel[key]
            }
            
        }*/

        var newRow = $('.table-row').clone().removeClass('table-row');
    }

    var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7240/BugTrackerHub").build();

    connection.on("SendErrorDetails", (message, data) => {
        //createTableRow(data);
        const dataModel = { applicationId: currentApplication }

        if (url === partialUrl)
        {
            AJAXRequest("GET", "/Error/ErrorList", dataModel, null, function (result) {
                $(".error-container").empty();
                $(".error-container").html(result);
            })

        }
        
        console.log(message, data);
        console.log("Successfully Connectioned");
    })
    /**
     * Check app added signal works
     */
    connection.on("SendAppAddedSignal", data => {
        console.log(data);
        AJAXRequest("GET", "/Application/RefreshApplicationList", null, null, function (result) {
            $(".dropdown-container").html(result)
            console.log("Refresh successful");
        }, function (err) {
            console.log(err.toString());
        })
        console.log("Add added signal received");
    })

    connection.start().then(function () {
        console.log("Connected to BugTrackerHub");
    }).catch(function (err) {
        console.error(err.toString());
    })

    /*$('#registerSubmit').on('click', function () {

        dataModel = {
            Email: $('#emailField').val(),
            Password: $('#passwordField').val(),
            ConfirmPassword: $('#confirmPasswordField').val()
        }
        console.log(dataModel);
        AJAXRequest("POST", "/Account/Register", dataModel, null, function () {
            console.log("Register Successful")
        }, function (jqXHR, textStatus, errorThrown) {
            console.log('POST error: ', textStatus, errorThrown, jqXHR)
        })
    })*/

    /*AJAXRequest("GET","/Error/PostUserId", null, null, function ()
    {
        console.log("Post Success");
    }, function () {
        console.log("Post unsuccessful")
    })*/
    

    if ($('.table').length)
    {
        $('.table > tbody > tr').each(function () {
            const checkbox = $(this).find('.checkbox');
            
            const isChecked = $(checkbox).prop('checked');

            if (isChecked)
                $(this).addClass('table-success');

            
        })
    }

    $('.checkbox').change(function () {
        console.log("Checkbox clicked")
        var row = $(this).closest('tr');
        const isChecked = $(this).prop('checked');
        const errorId = $(this).data('id');

        const model =
        {
            ErrorId: errorId,
            IsCompleted: isChecked
        }

        AJAXRequest("PUT", "/Error/UpdateCompletionStatus", model, null, function (result) {
            console.log("Updated")
        }, function (error) {
            console.log("Failure to update")
        });

        if (isChecked)
            $(row).addClass('table-success');
        else
            $(row).removeClass('table-success')
    })

    /*if ($('.table').exists())
    {
        //get checked checkbox in current row 
        if ($('#checked').val() == 'true')
        {
            
        }
    }*/

    $(".dropdown-item").on('click', function () {
        currentApplication = $(this).find("#applicationId").val();
    })

    $("#applicationDropdown").on('click', function (event) {
        event.stopPropagation(); // Prevent click event from propagating to document
        console.log('dropdown clicked');
        const dropdownList = $('.dropdown-item');
        console.log(dropdownList);
    });

    $("#addApplicationDropdown").on('click', function (event) {
        event.stopPropagation(); // Prevent click event from propagating to dropdown
        console.log('add application clicked');

        // Replace the "Add Application" dropdown with an input field
        const dropdown = $("#applicationDropdown");
        dropdown.replaceWith('<input type="text" id="applicationInput" placeholder="Enter application name">');

        // Focus on the input field
        $("#applicationInput").focus();

        // Prevent dropdown from closing when input field is active
        $("#applicationInput").on('click', function (event) {
            event.stopPropagation(); // Prevent click event from propagating to input field
        });

        $('#applicationInput').on('keypress', function (event) {
            if (event.which === 13)
            {
                const inputValue = $(this).val();

                var model = { ApplicationName: inputValue }

                $.ajax({
                    url: "/Application/AddApplication",
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(model),
                    success: function () {
                        console.log("Add application request Succeeded");
                        $("#applicationInput").remove();
                    }, error: function (jqr, textStatus, errorThrow) {
                        console.log("Error in add application request: ", jqr)
                    }
                })

                /*AJAXRequest("POST", "/Application/AddApplication", model, null, function () {
                    console.log("Add application request Succeeded");
                }, function (jqr, textStatus, errorThrown) {
                    console.log("Error in add application request: ", jqr)
                });*/
            }
        })

        
    });

    $("#btnAdd").on('click', function () {
        const val = $(this).val()
        $.Post({
            url: "/Application/AddApplicationTest",
            method: "POST",
            success: function () {
                console.log("Add application request Succeeded");
            }, error: function (jqr, textStatus, errorThrow) {
                console.log("Error in add application request: ", jqr)
            }
        })
    })

    $("#applicationItem").on('hover', function () {
        console.log("On app item");
        $(this).sibling().toggle();
    })


})