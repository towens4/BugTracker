
var s,
PaginationModule = {
    settings: {
        rowsShown: 0,
        rowsTotal: 0,
        numPages: 0,
        currentPage: null,
        paginationButtons: $('.paginationDirection'),
        
    },

    init: function (totalRows, currentPage) {
        s = this.settings;

        s.rowsShown = 10;
        s.rowsTotal = $('.table-body tr').length;
        //this.currentPage = currentPage;
        s.numPages = Math.ceil(s.rowsTotal / s.rowsShown);

        
        s.paginationButtons = $('.paginationDirection');

        //this.pageNumMapper.numPages = s.numPages;

        PaginationModule.BasePagination();

        s.currentPage = $("#nav a").attr('rel') == '0' ? parseInt($("#nav a").attr('rel')) + 1 : parseInt($("#nav a").attr('rel'));

        if (s.numPages == 1) {
            $('.pagination-direction').addClass('direction-disabled')
        }
        else {
            PaginationModule.pageNumMapper[this.settings.currentPage]();
        }
    },

    bindUIActions: function () {
        s.paginationButtons.on('click', function () {
            
            PaginationModule.directionalActions(this)
        })
    },

    directionalActions: function (button) {
        var currentDirection = $(button).data('direction');
        var currentPage = $('#nav a').hasClass('active') ? $("#nav a.active").attr('rel') : "";
        /*if ($('#nav a').hasClass('active'))
            currentPage = $("#nav a.active").attr('rel');*/

        console.log("string current page: ", currentPage);
        var tempCurrentPage = (parseInt(currentPage) + 1);
        console.log(tempCurrentPage);
        console.log('current direction: ', currentDirection);
        const pageList = $('#nav a');



        if (currentDirection == 'Next' && tempCurrentPage < this.settings.numPages) {
            //currentPage = (parseInt(currentPage) + 1);
            currentPage = (parseInt(currentPage) + 1);
            var startItem = currentPage * this.settings.rowsShown;

            var endItem = startItem + rowsShown;
            $('#nav a').removeClass('active');
            $(pageList[(tempCurrentPage - 1) + 1]).addClass('active');
            this.TransisionPage(startItem, endItem)

        }
        var previous = currentDirection == 'Previous' && tempCurrentPage >= 1;
        console.log("Is previous: ", previous);
        if (currentDirection == 'Previous' && tempCurrentPage > 1) {
            //currentPage = (parseInt(currentPage) + 1)
            currentPage = (parseInt(currentPage) - 1);
            var startItem = currentPage * this.settings.rowsShown;
            var endItem = startItem + this.settings.rowsShown;
            $('#nav a').removeClass('active');
            $(pageList[(tempCurrentPage - 1) - 1]).addClass('active');
            this.TransisionPage(startItem, endItem);

        }

        this.SetDirectionButtons(pageList, currentPage);
    },

    SetDirectionButtons: function (pageList, currentPage) {
        currentPage = parseInt(currentPage) + 1;
        if (pageList.length > 2) {
            $(pageList).each(function (index, page) {
                if (currentPage != pageList[index] && currentPage != pageList[pageList.length - 1]) {
                    $('.pagination-direction').removeClass('direction-disabled');
                    /*$('.pagination-direction').on('click', function () {
                        DirectionalButtonEvent(this)
                    })*/
                }
            })
        }
        
        if (currentPage == 1)
            this.pageNumMapper[currentPage]();
        else if (currentPage == this.settings.numPages)
            this.pageNumMapper.numPages();
    },

    TransisionPage: function (startItem, endItem) {
        $('.table-body tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    },

    BasePagination: function () {
        const module = this;

        $('.table').after('<div id="nav"></div>');
        $('#nav').append('<span id="paginationNext" class="pagination-direction" data-direction="Previous">Previous</span>')
        for (i = 0; i < this.settings.numPages; i++) {
            var pageNum = i + 1;
            $('#nav').append('<a href="#" class="page" rel="' + i + '">' + pageNum + '</a> ');
        }
        $('#nav').append('<span id="paginationPrevious" class="pagination-direction" data-direction="Next">Next</span>')
        $('.table-body tr').hide();
        $('.table-body tr').slice(0, this.settings.rowsShown).show();
        $('#nav a:first').addClass('active');
        $('#nav a').bind('click', function () {
            const pageList = $('#nav a');
            $('#nav a').removeClass('active');
            $(this).addClass('active');
            var currPage = $(this).attr('rel');

            var startItem = currPage * module.settings.rowsShown;
            var endItem = startItem + module.settings.rowsShown;
            //currentPage = currPage;
            module.directionalActions(pageList, currPage);

            module.TransisionPage(startItem, endItem);

            /*$('.table-body tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);*/
        });  
    },
    pageNumMapper: {
        1: () => {

            $('.pagination-direction[data-direction="Previous"]').addClass('direction-disabled')
            //$('#paginationPrevious').off('click');
            $('.pagination-direction[data-direction="Next"]').removeClass('direction-disabled')
            /*$('#paginationNext').on('click', function () {
                DirectionalButtonEvent(this)
            });*/
        },
        numPages: () => {
            $('.pagination-direction[data-direction="Next"]').addClass('direction-disabled')
            //$('#paginationNext').off('click');
            $('.pagination-direction[data-direction="Previous"]').removeClass('direction-disabled')
            /*$('#paginationPrevious').on('click', function () {
                DirectionalButtonEvent(this)
            });*/
        }
    }
}

