$(function () {
    //点击新增按钮弹出模态框。
    $("#dvr_add_modal_btn").click(function(){
        $("#dvrAddModal form")[0].reset();
        $("#dvrAddModal").modal({
            backdrop:"static"
        });
        $("#host_id_help-block").text('');
        $("#dvr_save_btn").removeAttr("disabled","disabled");
    });

    avoidRepeat();

    $("#dvr_save_btn").click(saveTables);
});

//避免重复提交
function avoidRepeat() {

    $("#host_id_help-block").text('');
    //为表单的必填文本框添加相关事件（blur、focus、keyup）
    $("#host_num_add_input").blur(function(){
        var host_id_value = $("#host_num_add_input").val();
        if (host_id_value == null || host_id_value == ""){
            $("#host_id_help-block").text('');
            return;
        }
        hostMsg(host_id_value);
    }).keyup(function(){
        //triggerHandler 防止事件执行完后，浏览器自动为标签获得焦点
        $(this).triggerHandler("blur");
    }).focus(function(){
        $(this).triggerHandler("blur");
    });

}

//查询主机编号是否存在
function hostMsg(host_id_value) {
    $.ajax({
        url: host+"app/dVR/byCondition?DVR_ID="+host_id_value,
        type:"get",
        success: function (host_result) {

            if(host_result.items.length === 0){
                // $("#host_id_help-block").text("ok").css("color","green");
                $("#host_id_help-block").text('');
                $("#dvr_save_btn").removeAttr("disabled","disabled");
            }else {
                $("#host_id_help-block").text("系统中已存在该主机！请勿重复提交！").css("color","red")
                //阻止提交
                $("#dvr_save_btn").attr("disabled","disabled");
            }
        }
    })
};

//点击保存，保存表格。
function saveTables(){
    var dvr_ness = $("#host_num_add_input").val();
    if(dvr_ness == null || dvr_ness === ""){
        $("#host_id_help-block").html("<b>该值不能为空！</b>").css("color","red");
        //阻止提交
        $("#dvr_save_btn").attr("disabled","disabled");
        return;
    }

    var post_data = {
        "factory": $("#factory_add_input").val(),
        "monitoring_room": $("#monitor_room_add_input").val(),
        "camera_build": $("#cam_build_add_input").val(),
        "camera_foor": $("#cam_floor_add_input").val(),
        "dvR_ID": $("#host_num_add_input").val(),
        "home_server": $("#home_server_add_input").val(),
        "hard_drive": $("#disk_capacity_add_input").val(),
        "dvR_IP": $("#host_ip_add_input").val(),
        "dvR_port": $("#dvr_port_add_input").val(),
        "dvR_usre": $("#dvr_u_add_input").val(),
        "dvR_possword": $("#dvr_p_add_input").val(),
        "install_time": $("#inst_time_add_input").val(),
        "manufacturer": $("#inst_manufacturer_add_input").val(),
        "dvR_type": $("#dev_type_add_input").val(),
        "dvR_SN": $("#sn_num_add_input").val(),
        "dvR_Channel": $("#dvr_cha_add_input").val(),
        "department": $("#depat_add_input").val(),
        "cost_code": $("#cos_code_add_input").val(),
        "remark": $("#remark_add_input").val()
    };

    // console.log(post_data);
    //1、发送ajax请求保存
    $.ajax({
        url:host+"app/dVR",
        type:"POST",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(post_data),
        success:function(result){
            console.log(result.status);
            //1、关闭模态框
            $("#dvrAddModal").modal('hide');

            $.dialog({
                title: '保存成功!',
                content: '',
            });
            setTimeout(function(){location.reload();},500);
        },
        error: function () {
            $.dialog({
                title: '保存失败!',
                content: '',
            });
            //保存失败后做什么？
        },
        complete: function(xhr, textStatus) {
            console.log(xhr.status);
            console.log(textStatus)
        }
    });
};
