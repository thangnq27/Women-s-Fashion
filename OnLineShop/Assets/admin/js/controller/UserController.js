var user = {
    init: function() {
        user.registerEvents();
    },
    registerEvents: function() {
        $('.btn-active').off('click').on('click',
            function(e) {
                e.preventDefault();
                var id = $(this).data('id');
                var btn = $(this);
                $.ajax({
                    url: '/Admin/User/ChangeStatus',
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function(result) {
                        if (result.status == true) {
                            btn.text('Active');
                        } else {
                            btn.text('Lock');
                        }
                    }
                });
            });
    }
}
user.init();