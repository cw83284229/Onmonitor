var pageSize = "20";//每页行数
var pageIndex = "1";//当前页
var totalPageCount = "0";//总页数
var totalCount = "0";//总记录数

$(function(){
    //解析并显示数据
    queryForPages();
    //加载总页数
    getAllpage();
    //构建分页
    build_page_info();

});

function queryForPages() {

    // var skip = 0;
    var skip = pageIndex*pageSize-pageSize;

    $.ajax({
        url: host+'app/alarm?Sorting=id&SkipCount='+skip+'&MaxResultCount='+pageSize,
        type: 'get',
        success: function (data) {
            console.log(data);
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
        // totalCount++;

        var id = obj[i].id;
        var indx = i+1;
        var monitoring_room = obj[i].monitoring_room;
        var alarmHost_ID = obj[i].alarmHost_ID;
        var alarm_ID = obj[i].alarm_ID;
        var build = obj[i].build;
        var floor = obj[i].floor;
        var location = obj[i].location;
        var geteType = obj[i].geteType;
        var sensorType = obj[i].sensorType;
        var department = obj[i].department;
        var cost_code = obj[i].cost_code;
        var install_time = obj[i].install_time;
        var category = obj[i].category;
        var camera_ID = obj[i].camera_ID;
        var isAlertor = !obj[i].isAlertor?"无":"有";
        var isOpenOrClosed = !obj[i].isOpenOrClosed?"否":"是";
        var remark = obj[i].remark;

        var tr="<tr id="+id+">"
            +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
            +"<td align=\"center\">"+indx+"</td>"
            +"<td align=\"center\">"+monitoring_room+"</td>"
            +"<td align=\"center\">"+alarmHost_ID+"</td>"
            +"<td align=\"center\">"+alarm_ID+"</td>"
            +"<td align=\"center\">"+build+"</td>"
            +"<td align=\"center\">"+floor+"</td>"
            +"<td align=\"center\">"+location+"</td>"
            +"<td align=\"center\">"+geteType+"</td>"
            +"<td align=\"center\">"+sensorType+"</td>"
            +"<td align=\"center\">"+department+"</td>"
            +"<td align=\"center\">"+cost_code+"</td>"
            +"<td align=\"center\">"+install_time+"</td>"
            +"<td align=\"center\">"+category+"</td>"
            +"<td align=\"center\">"+camera_ID+"</td>"
            +"<td align=\"center\">"+isAlertor+"</td>"
            +"<td align=\"center\">"+isOpenOrClosed+"</td>"
            +"<td align=\"center\">"+remark+"</td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-default\" onclick=gateMag_Modify(this);>修改</button></td>"
            +"</tr>";
        var $opt = $(tr);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
    }
    // totalPageCount = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
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
        url:host+'app/alarm',
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



