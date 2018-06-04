var dataArr = [];
    $(function () {
    
    $('#tblData tbody tr').each(function (index, value) {
        var name = $('.name', value).text();
        var parentID = $('.parentID', value).text();
        var order = parseInt($('.order', value).text());
        dataArr.push({ Name: name, MetaTitle: null, ParentID: parentID, DisplayOrder: order });
    });
    //return dataArr;
});
//console.log(JSON.stringify(datas));
var category = {
    init: function() {
        category.Synchronized();
    },
    Synchronized: function() {
        $('#btnSync').on('click', function() {

            var datas = JSON.stringify(dataArr);
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

            })
        })
    }
}
category.init();
