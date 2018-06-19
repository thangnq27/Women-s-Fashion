var redirect={
    init: function() {
        redirect.categoryClick();
    },
    categoryClick: function (e) {
        //e.preventDefault();
        $('.cssCategory').on('click',
            function() {
                var catname = $(this).data('id');
                alert(catname);

            });
    }
}
redirect.init();