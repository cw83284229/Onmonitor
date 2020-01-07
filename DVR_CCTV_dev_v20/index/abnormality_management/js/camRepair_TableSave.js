//自定义一个HaspMap类
function HashMap(){
    this.map = {};
}
HashMap.prototype = {
    put : function(key , value){
        this.map[key] = value;
    },
    get : function(key){
        if(this.map.hasOwnProperty(key)){
            return this.map[key];
        }
        return null;
    },
    remove : function(key){
        if(this.map.hasOwnProperty(key)){
            return delete this.map[key];
        }
        return false;
    },
    removeAll : function(){
        this.map = {};
    },
    keySet : function(){
        var _keys = [];
        for(var i in this.map){
            _keys.push(i);
        }
        return _keys;
    }
};
HashMap.prototype.constructor = HashMap;
var hashMap = new HashMap();

$(function () {

    $("#camrepair_save_btn").click(saveTables);

});


//获取用户输入镜头编号
function getCam_Id() {
    //删除之前的错误提醒信息
    $("#cam_id_help-block").text('');
    //为表单的必填文本框添加相关事件（blur、focus、keyup）
    $("#Camera_ID_add_input").blur(function(){
        var cam_id_value = $("#Camera_ID_add_input").val();
        if (cam_id_value == null || cam_id_value == ""){
            $("#cam_id_help-block").text('');
            return;
        }
        showMsg(cam_id_value);
    }).keyup(function(){
        //triggerHandler 防止事件执行完后，浏览器自动为标签获得焦点
        $(this).triggerHandler("blur");
    }).focus(function(){
        $(this).triggerHandler("blur");
    });
};


//查询镜头编号是否存在
function showMsg(camrepair_id) {
    $.ajax({
        url: host+"app/camera/byCameraID?CameraID="+camrepair_id,
        type:"get",
        success:function (result) {

            if(result.length !== 0){
                $("#cam_id_help-block").text("镜头输入正确！").css("color","green");
                $("#camrepair_save_btn").removeAttr("disabled","disabled");
            }else {
                $("#cam_id_help-block").text("系统中无此镜头！请重新输入").css("color","red")
                //阻止提交
                $("#camrepair_save_btn").attr("disabled","disabled");
            }
        }
    })
};


function getMyDate(){
    var oDate = new Date(),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
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

var Now_Time = getMyDate();

function getCamRepStat(camID) {

    $.ajax({
        url: host+'app/cameraRepair/repairsListByCondition?Camera_ID='+camID+'&SkipCount=0&MaxResultCount=2000000',
        type: 'get',
        success: function (data) {
            var obj = data.items;
            var flag = true;
            if (obj.length == 0){
                finalSave(flag);
            }else {
                flag = obj[0].repairState;
                finalSave(flag);
            }
        }
    });

}

function finalSave(flag) {
    var reparstate=$("#RepairState_add_input option:selected");
    var nosignal=$("#NoSignal_add_input option:selected");
    var anomalyType=$("#AnomalyType_add_input option:selected");

    if (flag == false){
        $("#cam_id_help-block").html("<b>该镜头目前状态未修复，请确保该镜头所有历史修复状态为已修复后重试！</b>").css("color","red");
        //阻止提交
        $("#camrepair_save_btn").attr("disabled","disabled");
        return;
    }

    var cameId = $("#Camera_ID_add_input").val();

    var post_data = {
        "camera_ID": cameId.toUpperCase(),
        "anomalyTime": $("#AnomalyTime_add_input").val(),
        "anomalyTime": Now_Time,
        "collectTime": Now_Time,
        "anomalyType": anomalyType.val(),
        "anomalyGrade": $("#AnomalyGrade_add_input").val(),
        "registrar": $("#Registrar_add_input").val(),
        "repairState": false,
        "repairedTime": "",
        "accendant": "",
        "repairDetails": "",
        "repairFirm": "",
        "supervisor": "",
        "replacePart": "",
        "projectAnomaly": "",
        "noSignal": false,
        "remark": $("#remark_add_input").val()
    };

    console.log(post_data);
    // 1、发送ajax请求保存
    $.ajax({
        url:host+"app/cameraRepair",
        type:"POST",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(post_data),
        success:function(result){
            //1、关闭模态框
            $("#dvrAddModal").modal('hide');

            $.dialog({
                title: '保存成功!',
                content: '',
            });
            setTimeout(function(){location.reload();},500);
        },
        error: function (erroMsg) {
            var obj= JSON.parse(erroMsg.responseText);
            var cont = obj.error.message;
            $.dialog({
                title: '保存失败!',
                content: cont,
            });
        }
    });


}


//点击保存，保存表格。
function saveTables(){

    var camID_ness = $("#Camera_ID_add_input").val();
    if(camID_ness == null || camID_ness == ""){
        $("#cam_id_help-block").html("<b>该值不能为空！</b>").css("color","red");
        //阻止提交
        $("#camrepair_save_btn").attr("disabled","disabled");
        return;
    }

    getCamRepStat(camID_ness);

    // var repStat = getCamRepStat(camID_ness);
    // console.log(repStat);
    // console.log(repStat.get('v1'));
    // var flag = repStat.get("0");
    // // repStat.forEach(function (k,repStat) {
    // //     flag = repStat[k];
    // //    console.log(repStat[k]);
    // // });
    // console.log(flag);
    // var flag;
    // if (repStat.length != 0){
    //     flag = repStat.pop();
    // }else {
    //     flag = repStat;
    // }
    // console.log("flag的值");
    // console.log(flag);

    // if (repStat == false){
    //     $("#cam_id_help-block").html("<b>该镜头目前状态未修复，请确保该镜头所有历史修复状态为已修复后重试！</b>").css("color","red");
    //     //阻止提交
    //     $("#camrepair_save_btn").attr("disabled","disabled");
    //     return;
    // }


    //异常时间不能超过统计时间
    // var anorTime = $("#AnomalyTime_add_input").val();
    // var regiTime = Now_Time;
    //
    // if (anorTime > regiTime){
    //     $("#AnomalyTime_help-block").html("<b>异常时间不能超过您现在统计的时间！请关闭后重试</b>").css("color","red");
    //     //阻止提交
    //     $("#camrepair_save_btn").attr("disabled","disabled");
    //     return;
    // }

    // var cameId = $("#Camera_ID_add_input").val();
    //
    // var post_data = {
    //     "camera_ID": cameId.toUpperCase(),
    //     "anomalyTime": $("#AnomalyTime_add_input").val(),
    //     "anomalyTime": Now_Time,
    //     "collectTime": Now_Time,
    //     "anomalyType": anomalyType.val(),
    //     "anomalyGrade": $("#AnomalyGrade_add_input").val(),
    //     "registrar": $("#Registrar_add_input").val(),
    //     "repairState": false,
    //     "repairedTime": "",
    //     "accendant": "",
    //     "repairDetails": "",
    //     "repairFirm": "",
    //     "supervisor": "",
    //     "replacePart": "",
    //     "projectAnomaly": "",
    //     "noSignal": false,
    //     "remark": $("#remark_add_input").val()
    // };
    //
    // console.log(post_data);
    // // 1、发送ajax请求保存
    // $.ajax({
    //     url:host+"app/cameraRepair",
    //     type:"POST",
    //     dataType : 'json',
    //     contentType : 'application/json',
    //     data : JSON.stringify(post_data),
    //     success:function(result){
    //         //1、关闭模态框
    //         $("#dvrAddModal").modal('hide');
    //
    //         $.dialog({
    //             title: '保存成功!',
    //             content: '',
    //         });
    //         // setTimeout(function(){location.reload();},500);
    //     },
    //     error: function (erroMsg) {
    //         var obj= JSON.parse(erroMsg.responseText);
    //         var cont = obj.error.message;
    //         $.dialog({
    //             title: '保存失败!',
    //             content: cont,
    //         });
    //     }
    // });
};
