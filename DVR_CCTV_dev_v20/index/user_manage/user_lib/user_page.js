var pageSize = 20;//每页行数
var pageIndex = 1;//当前页
var totalPageCount = 0;//总页数
// var totalCount = 0;//总记录数

$(function(){
    //解析并显示数据
    queryForPages();
    //加载总页数
    getAllpage();
    //构建分页
    build_page_info();

});

function queryForPages() {

    var skip = pageIndex*pageSize-pageSize;

    $.ajax({
        url: host+'identity/users?Sorting=id&SkipCount='+skip+'&MaxResultCount='+pageSize,
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

        var uname = obj[i].userName;
        var name = !obj[i].name?"":obj[i].name;

        var tr="<tr id="+id+">"
            +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
            +"<td align=\"center\">"+indx+"</td>"
            +"<td align=\"center\">"+id+"</td>"
            +"<td align=\"center\">"+uname+"</td>"
            +"<td align=\"center\">"+name+"</td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-info\" onclick=getUserRole(this);>获取</button></td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-primary\" onclick=addUserRole(this);>设定</button></td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-info\" onclick=setUserRole(this);>设定</button></td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-warning\" onclick=cancelUserRole(this);>取消</button></td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-default\" onclick=user_Modify(this);>修改</button></td>"
            +"</tr>";
        var $opt = $(tr);
        $("#checklist").append($opt);
    }
}

//清空数据
function clearDate() {
    $("#checklist").html("");
};

//显示分页
function build_page_info(){
    $("#index").val(pageIndex);

    //首页
    $("#firstPage").click(function () {
        //var index=$("#index").val();
        pageIndex = "1";
        $("#index").val(pageIndex);
        queryForPages();
    });
//上一页
    $("#previous").click(function () {
        if (pageIndex > 1) {
            pageIndex--;
        }
        $("#index").val(pageIndex);
        queryForPages();
    });  //下一页
    $("#next").click(function () {
        if (pageIndex <= totalPageCount) {
            pageIndex++;
        }
        $("#index").val(pageIndex);
        queryForPages();
    });
};

//总页数
function getAllpage() {
    $.ajax({
        url:host+'identity/users?Sorting=id&SkipCount=0&MaxResultCount=2000000',
        data:'',
        type: 'get',
        success: function (result) {
            var allPageNum = result.totalCount;
            // var totalPage = Math.floor(allPageNum/pageSize);
            var totalPage = Math.floor((allPageNum % 20 === 0) ? (allPageNum / 20) : (allPageNum / 20 + 1));
            totalPageCount = totalPage;
            //尾页
            $("#last").click(function () {
                pageIndex = totalPage;
                $("#index").val(pageIndex);
                queryForPages();
            });
            //总页数
            $("#pageCount").text("总共"+totalPage+"页");
        }
    });
}



