var dataArr = [];
    $(function () {
    
    $('#tblData tbody tr').each(function (index, value) {
        
        var name = $('.name', value).text();
        var parentID = parseInt($('.parentID', value).text());
        var order = parseInt($('.order', value).text());
        //var createby = '@Request.RequestContext.HttpContext.Session["someKey"]';
        //var usernameLogin = '@Session["USER_SESSION"]';
        dataArr.push({ Name: name, MetaTitle: null, ParentID: parentID, DisplayOrder: order, Createby: userlogin });
    });
    //return dataArr;
});

var category = {
    init: function () {
        
        category.Synchronized();
    },
    Synchronized: function() {
        $('#btnSync').on('click',
            function() {
                //var tmp = $.session.get("USER_SESSION");
                //var usernameLogin = '@Session["USER_SESSION"]';
                //var userlogin = '@Session["UserLogin"]';

                //alert(userlogin);
                var datas = JSON.stringify(dataArr);
                console.log(datas);
                $.ajax({
                    url: '/Admin/ProductCategory/Synchronized',
                    dataType: 'json',
                    type: 'POST',
                    data: { dataJson: datas },
                    success: function(result) {
                        if (result.status == true) {
                            alert('success');
                        } else
                            alert('false');
                    }

                });
            });
    }
}
category.init();
