//本地时间
function getMyDate(str){
    var oDate = new Date(str),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
        oTime = oYear +'-'+ getzf(oMonth) +'-'+ getzf(oDay);
    return oTime;
};
function getNowDate(){
    var oDate = new Date(),
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

var Update_time = getNowDate();

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


function getCamRepair(id){

    $('#repairState').select2();
    $('#noSignal').select2();

    //此处写ajax请求json数组对象数据源
    $.ajax({
        url: host+'app/cameraRepair/'+id,
        type: 'get',
        success: function (result) {
            console.log(result);
            var obj = result;
            hashMap.put("id",obj.id);

            hashMap.put("camera_ID",obj.camera_ID);
            $("#camera_ID").text(obj.camera_ID);

            $("#anomalyTime").text(obj.anomalyTime);
            hashMap.put("anomalyTime",obj.anomalyTime);

            $("#collectTime").text(getMyDate(obj.collectTime));
            hashMap.put("collectTime",getMyDate(obj.collectTime));

            $("#anomalyType").text(obj.anomalyType);
            hashMap.put("anomalyType",obj.anomalyType);

            $("#anomalyGrade").text(obj.anomalyGrade);
            hashMap.put("anomalyGrade",obj.anomalyGrade);

            $("#registrar").text(obj.registrar);
            hashMap.put("registrar",obj.registrar);

            hashMap.put('repairState',obj.repairState);

            $("#accendant").val(obj.accendant);
            $("#repairDetails").val(obj.repairDetails);
            $("#repairFirm").val(obj.repairFirm);
            $("#supervisor").val(obj.supervisor);
            $("#replacePart").val(obj.replacePart);
            $("#projectAnomaly").val(obj.projectAnomaly);
            // $("#noSignal").val(!obj.noSignal?"未确认":"已确认");
            $("#remark").val(obj.remark);

        }
    });
}

//点击更新，更新信息
$("#camrepair_update_btn").click(function(){

    var repStat=$("#repairState option:selected").val();
    if (repStat == "false"){
        repStat = false
    }else if (repStat == "true"){
        repStat = true
    }else {
        repStat = hashMap.get('repairState')
    }

    var noSign=$("#noSignal option:selected").val();

    // var repdT = $("#repairedTime").val();
    // var repT = getNowDate();
    //
    // if (repdT > repT){
    //     $("#repairedTime_help-block").html("<b>修复时间不能超过您现在的时间！请关闭后重试</b>").css("color","red");
    //     //阻止提交
    //     $("#camrepair_update_btn").attr("disabled","disabled");
    //     return;
    // }

    var modify_data = {
        "camera_ID": hashMap.get('camera_ID'),
        "anomalyTime": hashMap.get('anomalyTime'),
        "collectTime": hashMap.get('collectTime'),
        "anomalyType": hashMap.get('anomalyType'),
        "anomalyGrade": hashMap.get('anomalyGrade'),
        "registrar": hashMap.get('registrar'),

        "repairState": Boolean(repStat),

        "repairedTime": Update_time,
        "accendant": $("#accendant").val(),
        "repairDetails": $("#repairDetails").val(),
        "repairFirm": $("#repairFirm").val(),
        "supervisor": $("#supervisor").val(),
        "replacePart": $("#replacePart").val(),
        "projectAnomaly": $("#projectAnomaly").val(),
        "noSignal": Boolean(noSign),
        "remark": $("#remark").val()
    };

    console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = hashMap.get('id');
    $.ajax({
        url:host+"app/cameraRepair/"+put_id,
        type: 'put',
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#camrepairUpdateModal").modal('hide');

            $.dialog({
                title: '修改成功!',
                content: '',
            });
            //此处需修改为刷新当前页面 callback(){}
            //怎么回到本页面？queryForPages()？
            setTimeout(function(){location.reload();},500);
        },
        error: function () {
            $.dialog({
                title: '修改失败!',
                content: '',
            });
            //修改失败后做什么？
        }
    });
});


