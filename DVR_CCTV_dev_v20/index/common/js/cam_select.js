var select_pageSize = 24;//每页行数
var select_pageIndex = 1;//当前页
var select_totalPageCount = 0;//总页数

var url;

$(function () {
    $("#select_search").click(camera_select);
});

function camera_select() {
    $("#pagination_module").attr("style","display:none");
    $("#tree_pagination_module").attr("style","display:none");
    $("#select_pagination_module").removeAttr("style","display:none");
    //解析并显示数据
    select_queryForPages();
    //加载总页数
    select_getAllpage();
    // 构建分页
    select_build_page_info();

};

function select_queryForPages() {
    var dv_id = $('#camDvr_id_select').val().toUpperCase();
    var cam_id = $('#cam_id_select').val().toUpperCase();
    var build = $('#build_select').val().toUpperCase();
    var floor = $('#floor_select').val().toUpperCase();
    var location = $('#location_select').val();

    var skip = select_pageIndex*select_pageSize-select_pageSize;

    if (cam_id === "" || cam_id == null){
        //主机参数请求数据按通道号排序
        if (dv_id == "" || dv_id == null){
            url = host+'app/camera/byCondition?Build='+build+'&Floor='+floor+'&DVR_ID='+dv_id+'&CameraID='+cam_id+'&Location='+location+'&SkipCount='+skip+'&MaxResultCount='+select_pageSize+'&Sorting=id';
        }else {
            url = host+'app/camera/byCondition?DVR_ID='+dv_id+'&Sorting=channel_ID&SkipCount='+skip+'&MaxResultCount=2000';
        }
    }else{
        url = host+'app/camera/byCondition?Camera_ID='+cam_id;
        $("#select_pagination_module").attr("style","display:none");
    }


    $.ajax({
        url: url,
        type: 'get',
        success: function (data) {
            //清空数据
            select_clearDate();
            //查询数据
            select_fillTable(data);
            //加载总页数
            // getAllpage(data);
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
function select_fillTable(data) {
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
        var manu = !obj[i].manufacturer?"":obj[i].manufacturer;

        var catg = obj[i].category;
        // var alm = !obj[i].alarm_ID?"":obj[i].alarm_ID;

        var rmk = !obj[i].remark?"":obj[i].remark;
        var modtime = getMyDate(obj[i].lastModificationTime);

        //此处勿动，使用其它方式会使checkbox checkall失效！
        var s_opt = '<tr id="'+t_id+'"><td></td><td class="no-print"><input type="checkbox" id="check2" class="cb"></td><td align="center">'+index+'</td><td align="center">'+mor+'</td><td align="center">'+id+'</td><td align="center">'+channel+'</td><td class="print_wrap" align="center">'+cam_id+'</td><td align="center">'+build+'</td><td align="center">'+floor+'</td><td align="center">'+direct+'</td><td align="center">'+loc+'</td><td align="center">'+cam_type+'</td><td align="center">'+dep+'</td><td align="center">'+cost+'</td><td align="center">'+ins_time+'</td><td align="center">'+catg+'</td><td align="center">'+rmk+'</td><td class="no-print" align="center">'+modtime+'</td><td class="no-print"><button type="button" class="btn btn-xs btn-info" onclick="showVideo(this)">查看</button></td><td class="no-print"><button id="camModify" type="button" class="btn btn-xs btn-default" onclick="cam_Modify(this)">修改</button></td></tr>';

        var $opt = $(s_opt);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
        // $("#checklist").html(trs);
    }
    // select_totalPageCount = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
};

//清空数据
function select_clearDate() {
    $("#checklist").html("");
};

//显示分页
function select_build_page_info(){
    $("#select_index").val(select_pageIndex);
    //首页
    $("#select_firstPage").click(function () {
        //var index=$("#index").val();
        select_pageIndex = "1";
        $("#select_index").val(select_pageIndex);
        select_queryForPages();
    });
//上一页
    $("#select_previous").click(function () {
        if (select_pageIndex > 1) {
            select_pageIndex--;
        }
        $("#select_index").val(select_pageIndex);
        select_queryForPages();
    });  //下一页
    $("#select_next").click(function () {
        if (select_pageIndex <= select_totalPageCount) {
            select_pageIndex++;
        }
        $("#select_index").val(select_pageIndex);
        select_queryForPages();
    });
};

//总页数
function select_getAllpage() {
    var dv_id = $('#camDvr_id_select').val();
    var cam_id = $('#cam_id_select').val();
    var build = $('#build_select').val();
    var floor = $('#floor_select').val();
    var location = $('#location_select').val();

    var select_skip = 0;
    var select_pageSize = 20000;
    $.ajax({
        // url:host+'app/camera/byCondition?Build='+build+'&Floor='+floor+'&DVR_ID='+dv_id+'&CameraID='+cam_id+'&Location='+location+'&SkipCount='+select_skip+'&MaxResultCount='+select_pageSize+'&Sorting=id',
        url: url,
        data:'',
        type: 'get',
        success: function (result) {
            var select_allPageNum = result.totalCount;
            // totalPageCount = (allPageNum % pageSize == 0) ? (allPageNum / pageSize) : (allPageNum / pageSize + 1);
            // var totalPage = Math.floor(select_allPageNum/20);
            var totalPage = Math.floor((select_allPageNum % 20 === 0) ? (select_allPageNum / 20) : (select_allPageNum / 20 + 1));
            select_totalPageCount = totalPage;
            //尾页
            $("#select_last").click(function () {
                select_pageIndex = totalPage;
                $("#select_index").val(select_pageIndex);
                select_queryForPages();
            });
            //总页数
            $("#select_pageCount").text("总共"+totalPage+"页");
        }
    });
}


