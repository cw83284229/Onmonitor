var pageSize = "10";//每页行数
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
    var skip = pageIndex*pageSize;
    $.ajax({
        url: 'http://172.30.147.224/api/app/dVR?SkipCount='+skip+'&MaxResultCount='+pageSize+'&Sorting=id',
        type: 'get',
        data:'',
        success: function (data) {
            console.log(data);
            //清空数据
            clearDate();
            //查询数据
            fillTable(data);
        }
    });
};


//填充数据
function fillTable(data) {
    // var trs = "";//不初始化字符串"",会默认"undefined"
    var obj = data.items;
    for (var i in obj) {
        totalCount++;
        var indx = parseInt(i)+1;
        var id = obj[i].id;
        var fact = obj[i].factory;
        var moro = obj[i].monitoring_room;
        var d_id = obj[i].dvR_ID;
        var d_ip = obj[i].dvR_IP;
        var hd = obj[i].hard_drive;
        var instime = obj[i].install_time;
        var dt = obj[i].dvR_type;
        var ds = obj[i].dvR_SN;
        var mt = obj[i].lastModificationTime;

        console.log(id);
        var tr="<tr id="+id+">"
            +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
            +"<td align=\"center\">"+indx+"</td>"
            +"<td align=\"center\">"+fact+"</td>"
            +"<td align=\"center\">"+moro+"</td>"
            +"<td align=\"center\">"+d_id+"</td>"
            +"<td align=\"center\">"+d_ip+"</td>"
            +"<td align=\"center\">"+hd+"</td>"
            +"<td align=\"center\">"+instime+"</td>"
            +"<td align=\"center\">"+dt+"</td>"
            +"<td align=\"center\">"+ds+"</td>"
            +"<td align=\"center\">"+mt+"</td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"showDvrStatus btn btn-xs btn-info\">查看状态</button></td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"dvrModify btn btn-xs btn-default\">修改</button></td>"
            +"</tr>";
        var $opt = $(tr);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
    }
    totalPageCount = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
}

//清空数据
function clearDate() {
    $("#checklist").html("");
};

//显示分页
function build_page_info(){
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
        url:'http://172.30.147.224/api/app/dVR',
        data:'',
        type: 'get',
        success: function (result) {
            var allPageNum = result.totalCount;
            var totalPage = Math.floor(allPageNum/pageSize);
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


