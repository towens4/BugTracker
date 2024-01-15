// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    var rowsShown = 10;
    var rowsTotal = $('.table-body tr').length;
    var numPages = Math.ceil(rowsTotal / rowsShown);

    PaginationModule.init(rowsTotal);


    var url = window.location.href;
    const partialUrl = "/Error/ErrorContainer";

    
    
     
    var currentApplication = "";
  

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

        

        
    });


    $("#applicationItem").on('hover', function () {
        console.log("On app item");
        $(this).sibling().toggle();
    })


})