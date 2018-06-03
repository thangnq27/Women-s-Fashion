var category = {
    init: function() {
        category.Synchronized();
    },
    Synchronized: function() {
        $('#btnSync').on('click', function() {


            $.ajax({
                url: '/ProductCategory/Synchronized',
                dataType: 'json',
                type: 'POST',

            })
        })
    }
}
category.init();