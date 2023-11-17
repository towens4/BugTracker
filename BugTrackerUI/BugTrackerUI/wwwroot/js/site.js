// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    var rowsShown = 10;
    var rowsTotal = $('.table-body tr').length;
    var numPages = Math.ceil(rowsTotal / rowsShown);

    PaginationModule.init(rowsTotal);

    /*function SetDirectionButtons(pageList, currentPage)
    {
        currentPage = parseInt(currentPage) + 1;
        if (pageList.length > 2) {
            $(pageList).each(function (index, page) {
                if (currentPage != pageList[index] && currentPage != pageList[pageList.length - 1]) {
                    $('.pagination-direction').removeClass('direction-disabled');
                    
                }
            })
        }

        if (currentPage == 1)
            pageNumMapper[currentPage]();
        else if (currentPage == numPages)
            pageNumMapper.numPages();
    }

    function TransisionPage(startItem, endItem)
    {
        $('.table-body tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    }

    function DirectionalButtonEvent(button)
    {

        
        var currentDirection = $(button).data('direction');
        var currentPage = $('#nav a').hasClass('active') ? $("#nav a.active").attr('rel') : "";
        

        console.log("string current page: ", currentPage);
        var tempCurrentPage = (parseInt(currentPage) + 1);
        console.log(tempCurrentPage);
        console.log('current direction: ', currentDirection);
        const pageList = $('#nav a');


        
            if (currentDirection == 'Next' && tempCurrentPage < numPages) {
                
                currentPage = (parseInt(currentPage) + 1);
                var startItem = currentPage * rowsShown;
                
                var endItem = startItem + rowsShown;
                $('#nav a').removeClass('active');
                $(pageList[(tempCurrentPage - 1) + 1]).addClass('active');
                TransisionPage(startItem, endItem)
                
            }
            var previous = currentDirection == 'Previous' && tempCurrentPage >= 1;
            console.log("Is previous: ", previous);
            if (currentDirection == 'Previous' && tempCurrentPage > 1) {
                
                currentPage = (parseInt(currentPage) - 1);
                var startItem = currentPage * rowsShown;
                var endItem = startItem + rowsShown;
                $('#nav a').removeClass('active');
                $(pageList[(tempCurrentPage - 1)- 1]).addClass('active');
                TransisionPage(startItem, endItem);
               
            }

        SetDirectionButtons(pageList, currentPage);
    }

    $('.table').after('<div id="nav"></div>');
    $('#nav').append('<span id="paginationNext" class="pagination-direction" data-direction="Previous">Previous</span>')
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#nav').append('<a href="#" class="page" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#nav').append('<span id="paginationPrevious" class="pagination-direction" data-direction="Next">Next</span>')
    $('.table-body tr').hide();
    $('.table-body tr').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {
        const pageList = $('#nav a');
        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        //currentPage = currPage;
        SetDirectionButtons(pageList, currPage);

        TransisionPage(startItem, endItem);
        
        
    });  
    var currentDirection = "";
    var currentPage = $("#nav a").attr('rel') == '0' ? parseInt($("#nav a").attr('rel')) + 1 : parseInt($("#nav a").attr('rel'));

    const pageNumMapper =
    {
        1: () =>
        {

            $('.pagination-direction[data-direction="Previous"]').addClass('direction-disabled')
            //$('#paginationPrevious').off('click');
            $('.pagination-direction[data-direction="Next"]').removeClass('direction-disabled')
            
        },
        numPages: () =>
        {
            $('.pagination-direction[data-direction="Next"]').addClass('direction-disabled')
            //$('#paginationNext').off('click');
            $('.pagination-direction[data-direction="Previous"]').removeClass('direction-disabled')
           
        }
    }

    if (numPages == 1) {
        $('.pagination-direction').addClass('direction-disabled')
    }
    else
    {
        pageNumMapper[currentPage]();
    }

    $('.pagination-direction').on('click', function () {
        DirectionalButtonEvent(this)
    })*/


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