$(function () {
    $("#gate_save_btn").click(saveTables);
});

function saveTables() {
    var isAlertor=$("#isAlertor_add_input option:selected");
    if (isAlertor == "true"){
        isAlertor = true;
    }else if (isAlertor == "false"){
        isAlertor = false;
    }else {
        isAlertor = true;
    }

    var isOpenOrClosed=$("#isOpenOrClosed_add_input option:selected");
    if (isOpenOrClosed == "true"){
        isOpenOrClosed = true;
    }else if (isOpenOrClosed == "false"){
        isOpenOrClosed = false;
    }else {
        isOpenOrClosed = true;
    }

    var post_data = {
        "monitoring_room": $("#monitor_room_add_input").val(),
        "alarmHost_ID": $("#alarm_host_add_input").val(),
        "alarm_ID": $("#alarm_id_add_input").val(),
        "build": $("#gate_build_add_input").val(),
        "floor": $("#gate_floor_add_input").val(),
        "location": $("#gate_location_add_input").val(),
        "geteType": $("#gate_type_add_input").val(),
        "sensorType": $("#censor_type_add_input").val(),
        "department": $("#depat_add_input").val(),
        "cost_code": $("#cos_code_add_input").val(),
        "install_time": $("#inst_time_add_input").val(),
        "category": $("#inst_manufacturer_add_input").val(),
        "camera_ID": $("#camera_ID_add_input").val(),
        "isAlertor": Boolean(isAlertor),
        "isOpenOrClosed": Boolean(isOpenOrClosed),
        "remark": $("#remark_add_input").val()
    };

    // 1、发送ajax请求保存
    $.ajax({
        url:host+"app/alarm",
        type:"POST",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(post_data),
        success:function(result){
            console.log(result.status);
            //1、关闭模态框
            $("#gateAddModal").modal('hide');

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