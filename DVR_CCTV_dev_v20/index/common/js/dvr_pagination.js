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

    $("#goPage").click(goPage);
});
//跳转
function goPage() {
    var index=$("#index").val();
    pageIndex = index;
    queryForPages()
}

function queryForPages() {
    var skip = pageIndex*pageSize-pageSize;

    $.ajax({
        url: host+'app/dVR?SkipCount='+skip+'&MaxResultCount='+pageSize+'&Sorting=id',
        type: 'get',
        data:'',
        success: function (data) {
            //清空数据
            clearDate();
            //查询数据
            fillTable(data);
        },
        complete: function(xhr, textStatus) {
            if (xhr.status == 401){
                $.dialog({
                    title: '登录过期，请重新登录!',
                    content: '',
                });
                setTimeout(function(){window.location.href="../index.html";},1500);
            }
        }
    });
};


function getMyDate(str){
    var oDate = new Date(str),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
        // oHour = oDate.getHours(),
        // oMin = oDate.getMinutes(),
        // oSen = oDate.getSeconds(),
        // +' '+ getzf(oHour) +':'+ getzf(oMin) +':'+getzf(oSen)
        oTime = oYear +'-'+ getzf(oMonth) +'-'+ getzf(oDay);//最后拼接时间
    return oTime;
};
//补0操作
function getzf(num){
    if(parseInt(num) < 10){
        num = '0'+num;
    }
    return num;
};

//填充数据
function fillTable(data) {
    // var trs = "";//不初始化字符串"",会默认"undefined"
    var obj = data.items;


    for (var i in obj) {
        // totalCount++;
        var indx = parseInt(i)+1;
        var id = obj[i].id;
        var fact = !obj[i].factory?"":obj[i].factory;
        var moro = !obj[i].monitoring_room?"":obj[i].monitoring_room;
        var d_id = !obj[i].dvR_ID?"":obj[i].dvR_ID;
        var d_ip = !obj[i].dvR_IP?"":obj[i].dvR_IP;
        var hd = !obj[i].hard_drive?"":obj[i].hard_drive;
        var instime = !obj[i].install_time?"":obj[i].install_time;
        var dt = !obj[i].dvR_type?"":obj[i].dvR_type;
        var ds = !obj[i].dvR_SN?"":obj[i].dvR_SN;
        var rk = !obj[i].remark?"":obj[i].remark;
        var mt = getMyDate(obj[i].lastModificationTime);


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
            +"<td align=\"center\">"+rk+"</td>"
            +"<td class=\"no-print\" align=\"center\">"+mt+"</td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"showDvrStatus btn btn-xs btn-info\" onclick=dvr_showStatus(this);>查看状态</button></td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"dvrModify btn btn-xs btn-default\" onclick=dvr_Modify(this);>修改</button></td>"
            +"</tr>";
        var $opt = $(tr);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
        // $("#checklist").html($opt);
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
        url:host+'app/dVR',
        data:'',
        type: 'get',
        success: function (result) {
            var allPageNum = result.totalCount;
            // totalPageCount = (allPageNum % pageSize == 0) ? (allPageNum / pageSize) : (allPageNum / pageSize + 1);
            // var totalPage = Math.floor(allPageNum/pageSize);
            var totalPage = Math.floor((allPageNum % pageSize === 0) ? (allPageNum / pageSize) : (allPageNum / pageSize + 1));
            totalPageCount = totalPage;
            //尾页
            $("#last").click(function () {
                pageIndex = totalPage;
                $("#index").val(pageIndex);
                queryForPages();
            });
            //总页数
            $("#pageCount").text("总共"+totalPage+"页");
            $("#dvr_total").text(allPageNum+" ");
        }
    });
}


