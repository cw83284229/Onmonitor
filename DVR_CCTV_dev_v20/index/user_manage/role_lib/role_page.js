$(function(){
    //解析并显示数据
    queryForPages();

});

function queryForPages() {

    $.ajax({
        url: host+'identity/roles',
        type: 'get',
        success: function (data) {
            //清空数据
            clearDate();
            //查询数据
            fillTable(data);
        },
        complete:function(xhr, textStatus){
            if (xhr.status == 401){
                $.dialog({
                    title: '登录过期，请重新登录!',
                    content: '',
                });
                setTimeout(function(){window.location.href="../../index.html";},1500);
            }
        }
    });
};


//填充数据
function fillTable(data) {
    var obj = data.items;
    for (var i = 0; i < obj.length; i++) {

        var id = obj[i].id;
        var indx = i+1;

        var uname = obj[i].name;

        var tr="<tr id="+id+">"
            +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
            +"<td align=\"center\">"+indx+"</td>"
            +"<td align=\"center\">"+id+"</td>"
            +"<td align=\"center\">"+uname+"</td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-info\" onclick=role_set(this);>设定</button></td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-warning\" onclick=role_cancel(this);>取消</button></td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-default\" onclick=role_Modify(this);>修改</button></td>"
            +"</tr>";
        var $opt = $(tr);
        $("#checklist").append($opt);
    }
}

//清空数据
function clearDate() {
    $("#checklist").html("");
};


