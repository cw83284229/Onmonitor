function getCam(id){
    //此处写ajax请求json数组对象数据源(若为get[id]方式,则取消数据遍历)
    $.ajax({
        url: host+'app/camera/'+id,
        type: 'get',
        success: function (result) {
            var obj = result;
            var data = new Date();
            year = data.getFullYear();
            month = data.getMonth()+1;
            day = data.getDate();
            var Now_time = year+'-'+month+'-'+day;

            $("#moni_room").val(obj.monitoring_room);
            $("#id_input").val(obj.id);
            $("#dvr_id").val(obj.dvR_ID);
            $("#channel_id").val(obj.channel_ID);
            $("#cam_id").val(obj.camera_ID);
            $("#build").val(obj.build);
            $("#floor_modify").val(obj.floor);
            $("#direct").val(obj.direction);
            $("#location").val(obj.location);
            $("#cam_type").val(obj.camera_Tpye);
            $("#dept").val(obj.department);
            $("#cost_code").val(obj.cost_code);
            $("#inst_time").val(obj.install_time);
            $("#manu").val(obj.manufacturer);
            $("#catg").val(obj.category);
            $("#alarm").val(obj.alarm_ID);
            $("#remark_modify").val(obj.remark);
            $("#camModificationTime").text(Now_time);
        }
    });
}

//点击更新，更新员工信息
$("#cam_update_btn").click(function(){
    //1、获取信息

    var modify_data = {
        "monitoring_room": $("#moni_room").val(),
        "dvR_ID": $("#dvr_id").val(),
        "channel_ID": parseInt($("#channel_id").val()),
        "camera_ID": $("#cam_id").val(),
        "build": $("#build").val(),
        "floor": $("#floor_modify").val(),
        "direction": $("#direct").val(),
        "location": $("#location").val(),
        "camera_Tpye": $("#cam_type").val(),
        "department": $("#dept").val(),
        "cost_code": $("#cost_code").val(),
        "install_time": $("#inst_time").val(),
        "manufacturer": $("#manu").val(),
        "category": $("#catg").val(),
        "alarm_ID": $("#alarm").val(),
        "remark": $("#remark_modify").val()
    };

    console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = $("#id_input").val();

    console.log(put_id);
    //bug待修复,400 bad Request
    $.ajax({
        url:host+"app/camera/"+put_id,
        type:"PUT",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#camUpdateModal").modal('hide');

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


