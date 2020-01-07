var pageSize = "20";//每页行数
var pageIndex = "1";//当前页
var totalPageCount = "0";//总页数

$(function(){
    $("#camDvr_id_select").val("");
    $("#cam_id_select").val("");
    $("#build_select").val("");
    $("#floor_select").val("");
    $("#location_select").val("");
    $("#select_pagination_module").attr("style","display:none");
    $("#tree_pagination_module").attr("style","display:none");

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
    console.log(index);
    pageIndex = index;
    queryForPages()
}

function queryForPages() {
    var skip = pageIndex*pageSize-pageSize;
    // if(skip === 20){
    //     skip = 0;
    // }
    $.ajax({
        url: host+'app/camera?Sorting=id&SkipCount='+skip+'&MaxResultCount='+pageSize,
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
    // 解析数据，生成表格
    for (var i = 0; i < obj.length; i++) {
        // totalCount++;
        var t_id = obj[i].id;
        var index = i+1;

        var mor = !obj[i].monitoring_room?"":obj[i].monitoring_room;

        var id = !obj[i].dvR_ID?"":obj[i].dvR_ID;
        var channel = !obj[i].channel_ID?"":obj[i].channel_ID;
        var cam_id = !obj[i].camera_ID?"":obj[i].camera_ID;
        var build = !obj[i].build?"":obj[i].build;
        var floor = !obj[i].floor?"":obj[i].floor;
        var direct = !obj[i].direction?"":obj[i].direction;
        var loc = !obj[i].location?"":obj[i].location;
        var cam_type = !obj[i].camera_Tpye?"":obj[i].camera_Tpye;
        var dep = !obj[i].department?"":obj[i].department;
        var cost = !obj[i].cost_code?"":obj[i].cost_code;
        var ins_time = !obj[i].install_time?"":obj[i].install_time;
        // var manu = !obj[i].manufacturer?"":obj[i].manufacturer;

        var catg = !obj[i].category?"":obj[i].category;
        // var alm = !obj[i].alarm_ID?"":obj[i].alarm_ID;

        var rmk = !obj[i].remark?"":obj[i].remark;
        var modtime = getMyDate(obj[i].lastModificationTime);

        //此处勿动，使用其它方式会使checkbox checkall失效！
        var s_opt = '<tr id="'+t_id+'"><td></td><td class="no-print"><input type="checkbox" id="check2" class="cb"></td><td align="center">'+index+'</td><td align="center">'+mor+'</td><td align="center">'+id+'</td><td align="center">'+channel+'</td><td class="print_wrap" align="center">'+cam_id+'</td><td align="center">'+build+'</td><td align="center">'+floor+'</td><td align="center">'+direct+'</td><td align="center">'+loc+'</td><td align="center">'+cam_type+'</td><td align="center">'+dep+'</td><td align="center">'+cost+'</td><td align="center">'+ins_time+'</td><td align="center">'+catg+'</td><td align="center">'+rmk+'</td><td class="no-print" align="center">'+modtime+'</td><td class="no-print"><button type="button" class="btn btn-xs btn-info" onclick="showVideo(this)">查看</button></td><td class="no-print"><button id="camModify" type="button" class="btn btn-xs btn-default" onclick="cam_Modify(this)">修改</button></td></tr>';

        var $opt = $(s_opt);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
        // $("#checklist").html(trs);
    }
    // totalPageCount = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
};

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
        url:host+'app/camera',
        data:'',
        type: 'get',
        success: function (result) {
            var allPageNum = result.totalCount;
            // totalPageCount = (allPageNum % pageSize == 0) ? (allPageNum / pageSize) : (allPageNum / pageSize + 1);
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
            $("#cam_total").text(allPageNum+" ");

        }
    });
}


