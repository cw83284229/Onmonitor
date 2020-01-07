$(function () {
    //点击新增按钮弹出模态框。
    $("#cam_add_modal_btn").click(function(){
        $("#camAddModal form")[0].reset();
        $("#camAddModal").modal({
            backdrop:"static"
        });
        $("#cam_help-block").text('');
        $("#cam_save_btn").removeAttr("disabled","disabled");
    });

    cam_avoidRepeat();

    $("#cam_save_btn").click(saveTables);
});

//避免重复提交
function cam_avoidRepeat() {

    $("#cam_help-block").text('');
    //为表单的必填文本框添加相关事件（blur、focus、keyup）
    $("#camera_ID_add_input").blur(function(){
        var cam_id_value = $("#camera_ID_add_input").val();
        if (cam_id_value == null || cam_id_value == ""){
            $("#cam_help-block").text('');
            return;
        }
        camMsg(cam_id_value);
    }).keyup(function(){
        //triggerHandler 防止事件执行完后，浏览器自动为标签获得焦点
        $(this).triggerHandler("blur");
    }).focus(function(){
        $(this).triggerHandler("blur");
    });

}

//查询镜头编号是否存在
function camMsg(cam_id_value) {
    $.ajax({
        // url: host+"app/camera/byCondition?CameraID="+cam_id_value,
        url: host+'app/camera/byCondition?Camera_ID='+cam_id_value,
        type:"get",
        success: function (cam_result) {

            if(cam_result.items.length === 0){
                // $("#cam_help-block").text("ok").css("color","green");
                $("#cam_help-block").text('');
                $("#cam_save_btn").removeAttr("disabled","disabled");
            }else {
                $("#cam_help-block").text("系统中已存在该镜头！请勿重复提交！").css("color","red");
                //阻止提交
                $("#cam_save_btn").attr("disabled","disabled");
            }
        }
    })
};

//点击保存，保存表格。
function saveTables(){
    var cam_ness = $("#camera_ID_add_input").val();
    if(cam_ness == null || cam_ness === ""){
        $("#cam_help-block").html("<b>该值不能为空！</b>").css("color","red")
        //阻止提交
        $("#cam_save_btn").attr("disabled","disabled");
        return;
    }

    var post_data = {
        "monitoring_room": $("#moniRoom_add_input").val(),
        "dvR_ID": $("#dvR_ID_add_input").val(),
        "channel_ID": $("#channel_ID_add_input").val(),
        "camera_ID": $("#camera_ID_add_input").val(),
        "build": $("#build_add_input").val(),
        "floor": $("#floor_add_input").val(),
        "direction": $("#direction_add_input").val(),
        "location": $("#location_add_input").val(),
        "camera_Tpye": $("#camera_Tpye_add_input").val(),
        "department": $("#department_add_input").val(),
        "cost_code": $("#cost_code_add_input").val(),
        "install_time": $("#install_time_add_input").val(),
        "manufacturer": $("#manufacturer_add_input").val(),
        "category": $("#category_type_add_input").val(),
        "alarm_ID": $("#alarm_ID_add_input").val(),
        "remark": $("#remark_add_input").val()
    };

    console.log(post_data);
    //1、发送ajax请求保存
    $.ajax({
        url:host+"app/camera",
        type:"POST",
        dataType : 'json',
        contentType : 'application/json',
        //将js对象转为字符串
        data : JSON.stringify(post_data),
        success:function(){
            //1、关闭模态框
            $("#camAddModal").modal('hide');

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
        }
    });
};
