var select_pageSize = 20;//每页行数
var select_pageIndex = 1;//当前页
var select_totalPageCount = 0;//总页数

$(function () {
    $("#RepairState_select").select2();
    $("#NoSignal_select").select2();

    $("#camrepair_select_btn").click(camera_select);
});

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


function camera_select() {
    $("#pagination_module").attr("style","display:none");
    $("#select_pagination_module").removeAttr("style","display:none");
    //解析并显示数据
    select_queryForPages();
    //加载总页数
    select_getAllpage();
    // 构建分页
    select_build_page_info();

};


function select_queryForPages() {

    var repOp = $('#RepairState_select option:selected').val();
    var noSigOp = $('#NoSignal_select option:selected').val();
    var dvId = $('#camRepairDvr_id_select').val().toUpperCase();
    var build = $('#camRepairBuild_select').val().toUpperCase();
    var floor = $('#camRepairFloor_select').val().toUpperCase();
    var camId = $('#camRepair_id_select').val().toUpperCase();
    var anTime = $('#AnomalyTime_select').val();
    var clTime = $('#CollectTime_select').val();
    var alType = $('#AnomalyType_select').val();
    var anGrade = $('#AnomalyGrade_select').val();
    var reg = $('#Registrar_select').val();
    var rep = !repOp?false:repOp;
    var repTime = $('#RepairedTime_select').val();
    var acd = $('#Accendant_select').val();
    var repDetail = $('#RepairDetails_select').val();
    var repFirm = $('#RepairFirm_select').val();
    var sup = $('#Supervisor_select').val();
    var repPart = $('#ReplacePart_select').val();
    var proAn = $('#ProjectAnomaly_select').val();
    var noSig = !noSigOp?false:noSigOp;

    var skip = select_pageIndex*select_pageSize-select_pageSize;

    $.ajax({
        url: host+'app/cameraRepair/repairsListByCondition?DVR_ID='+dvId+'&Build='+build+'&floor='+floor+'&Camera_ID='+camId+'&AnomalyTime='+anTime+'&CollectTime='+clTime+'&AnomalyType='+alType+'&AnomalyGrade='+anGrade+'&Registrar='+reg+'&RepairState='+rep+'&RepairedTime='+repTime+'&Accendant='+acd+'&RepairDetails='+repDetail+'&RepairFirm='+repFirm+'&Supervisor='+sup+'&ReplacePart='+repPart+'&ProjectAnomaly='+proAn+'&NoSignal='+noSig+'&Sorting=id&SkipCount='+skip+'&MaxResultCount='+select_pageSize,
        type: 'get',
        success: function (data) {
            //清空数据
            select_clearDate();
            //查询数据
            select_fillTable(data);
        },
        error: function (erroMsg) {
            var obj= JSON.parse(erroMsg.responseText);
            var cont = obj.error.message;
            $.dialog({
                title: '筛选失败!',
                content: cont,
            });
        },
        complete: function () {
            $("#camrepairSelect").attr("style","display:none");
        }
    });

}

function select_fillTable(data) {
    $("#checklist").html("");
    $("#pagination_module").attr("style","display:none");

    var obj = data.items;
    for (var i = 0; i < obj.length; i++) {

        var indx = parseInt(i)+1;
        var id = obj[i].id;
        var reps = obj[i].repairState;
        var re_monirom = !obj[i].dvR_Room?"":obj[i].dvR_Room; //此处为缺少字段，后台补充后添加
        var re_dvrId = obj[i].dvR_ID;
        var dept = !obj[i].department?"":obj[i].department;
        var chId = !obj[i].channel_ID?"":obj[i].channel_ID;
        var build = !obj[i].build?"":obj[i].build;
        var floor = !obj[i].floor?"":obj[i].floor;
        var re_direct = !obj[i].direction?"":obj[i].direction;
        var re_location = obj[i].location;
        var cam_id = obj[i].camera_ID;
        var ant = getMyDate(obj[i].anomalyTime);
        var ct = getMyDate(obj[i].collectTime);
        var at = obj[i].anomalyType;
        var ag = obj[i].anomalyGrade;
        var reg = obj[i].registrar;
        var rs = !reps?"N":"Y";
        var rt = !obj[i].repairedTime?"":getMyDate(obj[i].repairedTime);
        var acd = obj[i].accendant;
        var rpd = obj[i].repairDetails;
        var rpf = obj[i].repairFirm;
        var suv = obj[i].supervisor;
        var rep = obj[i].replacePart;
        var pra = obj[i].projectAnomaly;

        var ns = obj[i].noSignal;
        if (ns == null){
            ns = "未知"
        }else if (ns == true){
            ns = "N"
        }else {
            ns = "Y"
        }

        var rmk = obj[i].remark;

        var tr="<tr id="+id+">"
            +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
            +"<td align=\"center\">"+indx+"</td>"
            +"<td align=\"center\">"+re_monirom+"</td>"
            +"<td align=\"center\">"+re_dvrId+"</td>"
            +"<td align=\"center\">"+dept+"</td>"
            +"<td align=\"center\">"+build+"</td>"
            +"<td align=\"center\">"+floor+"</td>"
            +"<td align=\"center\">"+re_direct+"</td>"
            +"<td align=\"center\">"+re_location+"</td>"
            +"<td align=\"center\">"+cam_id+"</td>"
            // +"<td align=\"center\">"+ant+"</td>"
            +"<td align=\"center\">"+ct+"</td>"
            +"<td align=\"center\">"+at+"</td>"
            +"<td align=\"center\">"+ag+"</td>"
            +"<td align=\"center\">"+reg+"</td>"
            +"<td align=\"center\" id=\"pd"+indx+"\"><b>"+rs+"</b></td>"
            +"<td align=\"center\">"+rt+"</td>"
            +"<td align=\"center\">"+acd+"</td>"
            +"<td align=\"center\">"+rpd+"</td>"
            +"<td align=\"center\">"+rpf+"</td>"
            +"<td align=\"center\">"+suv+"</td>"
            +"<td align=\"center\">"+rep+"</td>"
            +"<td align=\"center\" id=\"ns"+indx+"\"><b>"+ns+"</b></td>"
            +"<td align=\"center\">"+rmk+"</td>"
            +"<td align=\"center\" class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-info\" onclick=\"noSigConfirm(this)\">查看</button></td>"+
            +"<td align=\"center\"></td>"
            +"<td class=\"no-print\"><button type=\"button\" class=\"dvrModify btn btn-xs btn-default\" onclick=camRepair_Modify(this);>修改</button></td>"
            +"</tr>";
        var $opt = $(tr);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
        compRep(reps, indx);
        compNS(ns,indx);
    }
}

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
    var repOp = $('#RepairState_select option:selected').val();
    var noSigOp = $('#NoSignal_select option:selected').val();
    var dvId = $('#camRepairDvr_id_select').val().toUpperCase();
    var build = $('#camRepairBuild_select').val().toUpperCase();
    var floor = $('#camRepairFloor_select').val().toUpperCase();
    var camId = $('#camRepair_id_select').val().toUpperCase();
    var anTime = $('#AnomalyTime_select').val();
    var clTime = $('#CollectTime_select').val();
    var alType = $('#AnomalyType_select').val();
    var anGrade = $('#AnomalyGrade_select').val();
    var reg = $('#Registrar_select').val();
    var rep = !repOp?false:repOp;
    var repTime = $('#RepairedTime_select').val();
    var acd = $('#Accendant_select').val();
    var repDetail = $('#RepairDetails_select').val();
    var repFirm = $('#RepairFirm_select').val();
    var sup = $('#Supervisor_select').val();
    var repPart = $('#ReplacePart_select').val();
    var proAn = $('#ProjectAnomaly_select').val();
    var noSig = !noSigOp?false:noSigOp;

    var camRep_sk = 0;
    var camRep_sp = 2000000;
    $.ajax({
        // url:host+'app/camera/byCondition?Build='+build+'&Floor='+floor+'&DVR_ID='+dv_id+'&CameraID='+cam_id+'&Location='+location+'&SkipCount='+select_skip+'&MaxResultCount='+select_pageSize+'&Sorting=id',
        url: host+'app/cameraRepair/repairsListByCondition?DVR_ID='+dvId+'&Build='+build+'&floor='+floor+'&Camera_ID='+camId+'&AnomalyTime='+anTime+'&CollectTime='+clTime+'&AnomalyType='+alType+'&AnomalyGrade='+anGrade+'&Registrar='+reg+'&RepairState='+rep+'&RepairedTime='+repTime+'&Accendant='+acd+'&RepairDetails='+repDetail+'&RepairFirm='+repFirm+'&Supervisor='+sup+'&ReplacePart='+repPart+'&ProjectAnomaly='+proAn+'&NoSignal='+noSig+'&Sorting=id&SkipCount='+camRep_sk+'&MaxResultCount='+camRep_sp,
        type: 'get',
        success: function (result) {
            var select_allPageNum = result.totalCount;
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