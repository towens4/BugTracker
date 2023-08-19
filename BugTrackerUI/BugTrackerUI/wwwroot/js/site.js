// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    /*$("#applicationDropdown").on('click', function (event) {
        event.preventDefault();
        console.log('dropdown clicked');
        const dropdownList = $('.dropdown-item');
        console.log(dropdownList);
        $("#addApplicationDropdown").on('click', function () {
            
            //$("#applicationInput").toggle()
            //$(this).toggle();
        })
        
    })*/

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