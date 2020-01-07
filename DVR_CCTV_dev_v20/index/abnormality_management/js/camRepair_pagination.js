var pageSize = 20;//每页行数
var pageIndex = 1;//当前页
var totalPageCount = 0;//总页数
// var totalCount = "0";//总记录数

$(function(){
    //解析并显示数据
    queryForPages();
    //加载总页数
    getAllpage();
    //构建分页
    build_page_info();

});

function queryForPages() {
    //跳转rows
    // var skip = pageIndex*pageSize;
    // var skip = 0;
    var skip = pageIndex*pageSize-pageSize;
    $.ajax({
        // url: host+'app/cameraRepair/repairsList?Sorting=id&SkipCount='+skip+'&MaxResultCount='+pageSize,
        // url: host+'app/cameraRepair/repairsList?Sorting=id&SkipCount=0&MaxResultCount=1',
        url: host+'app/cameraRepair/repairsListByCondition?RepairState=false&Sorting=CollectTime&SkipCount='+skip+'&MaxResultCount='+pageSize,
        type: 'get',
        beforeSend:function(){
            clearDate();
            // show image here
            $("#pagination_module").attr("style","display:none");
            $("#select_pagination_module").attr("style","display:none");
            $("#wait").css("display", "block");
        },
        success: function (data) {
            console.log(data);
            //清空数据
            // clearDate();
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
            // hide image here
            $("#wait").css("display", "none");
            $("#pagination_module").removeAttr("style","display:none");
        }
    });
};

function getMyDate(str){
    var oDate = new Date(str),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
        oTime = oYear +'-'+ getzf(oMonth) +'-'+ getzf(oDay);
    return oTime;
};
function getzf(num){
    if(parseInt(num) < 10){
        num = '0'+num;
    }
    return num;
};

function compRep(reps, idx) {
    var cs_id = "pd"+idx;

    if (reps != false){
        $("#"+cs_id).css("color","#00FF00")
    }else {
        $("#"+cs_id).css("color","red")
    }
}

function compNS(ns, idx) {
    var cs_id = "ns"+idx;

    if (ns == "Y"){
        $("#"+cs_id).css("color","#00FF00")
    }else if (ns == "N"){
        $("#"+cs_id).css("color","red")
    }else {
        $("#"+cs_id).css("color","blue")
    }

}

//填充数据
function fillTable(data) {
    // var trs = "";//不初始化字符串"",会默认"undefined"
    var obj = data.items;
    var num=1;
    for (var i = 0; i < obj.length; i++) {

        if (!obj[i].repairState) {

            var reps = obj[i].repairState;

            var indx = num++;

            var id = obj[i].id;
            var re_monirom = !obj[i].dvR_Room ? "" : obj[i].dvR_Room; //此处为缺少字段，后台补充后添加
            var re_dvrId = obj[i].dvR_ID;

            var dept = !obj[i].department ? "" : obj[i].department;

            var chId = !obj[i].channel_ID ? "" : obj[i].channel_ID;
            var build = !obj[i].build ? "" : obj[i].build;
            var floor = !obj[i].floor ? "" : obj[i].floor;

            var re_direct = !obj[i].direction ? "" : obj[i].direction;
            var re_location = obj[i].location;
            var cam_id = obj[i].camera_ID;
            var ant = getMyDate(obj[i].anomalyTime);
            var ct = getMyDate(obj[i].collectTime);
            var at = obj[i].anomalyType;
            var ag = obj[i].anomalyGrade;
            var reg = obj[i].registrar;
            var rs = !reps ? "N" : "Y";
            var rt = !obj[i].repairedTime ? "" : getMyDate(obj[i].repairedTime);
            var acd = obj[i].accendant;
            var rpd = obj[i].repairDetails;
            var rpf = obj[i].repairFirm;
            var suv = obj[i].supervisor;
            var rep = obj[i].replacePart;
            var pra = obj[i].projectAnomaly;

            var ns = obj[i].noSignal;
            if (ns == null) {
                ns = "未知"
            } else if (ns == true) {
                ns = "N"
            } else {
                ns = "Y"
            }
            // var ns = !obj[i].noSignal?"Y":"N";

            var rmk = obj[i].remark;

            var tr = "<tr id=" + id + ">"
                + "<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
                + "<td align=\"center\">" + indx + "</td>"
                + "<td align=\"center\">" + re_monirom + "</td>"
                + "<td align=\"center\">" + re_dvrId + "</td>"

                + "<td align=\"center\">" + dept + "</td>"
                + "<td align=\"center\">" + build + "</td>"
                + "<td align=\"center\">" + floor + "</td>"


                + "<td align=\"center\">" + re_direct + "</td>"
                + "<td align=\"center\">" + re_location + "</td>"
                + "<td align=\"center\">" + cam_id + "</td>"
                // +"<td align=\"center\">"+ant+"</td>"
                + "<td align=\"center\">" + ct + "</td>"
                + "<td align=\"center\">" + at + "</td>"
                + "<td align=\"center\">" + ag + "</td>"
                + "<td align=\"center\">" + reg + "</td>"
                + "<td align=\"center\" id=\"pd" + indx + "\"><b>" + rs + "</b></td>"
                + "<td align=\"center\">" + rt + "</td>"
                + "<td align=\"center\">" + acd + "</td>"
                + "<td align=\"center\">" + rpd + "</td>"
                + "<td align=\"center\">" + rpf + "</td>"
                + "<td align=\"center\">" + suv + "</td>"
                + "<td align=\"center\">" + rep + "</td>"
                + "<td align=\"center\" id=\"ns" + indx + "\"><b>" + ns + "</b></td>"
                // +"<td align=\"center\"></td>"
                + "<td align=\"center\">" + rmk + "</td>"
                + "<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-info\" onclick=\"noSigConfirm(this)\">查看</button></td>" +
                +"<td align=\"center\"></td>"

                + "<td class=\"no-print\"><button type=\"button\" class=\"dvrModify btn btn-xs btn-default\" onclick=camRepair_Modify(this);>修改</button></td>"
                + "</tr>";
            var $opt = $(tr);//将字符串转换成jQuery对象
            $("#checklist").append($opt);
            // $("#checklist").html($opt);
            compRep(reps, indx);
            compNS(ns, indx);
        }
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
        url: host+'app/cameraRepair/repairsListByCondition?RepairState=false&SkipCount=0&MaxResultCount=2000000',
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



