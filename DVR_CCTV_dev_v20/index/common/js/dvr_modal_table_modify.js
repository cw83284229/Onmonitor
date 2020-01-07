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

function getDVR(id){
    //此处写ajax请求json数组对象数据源(若为get[id]方式,则取消数据遍历)
    $.ajax({
       url: host+'app/dVR/'+id,
       type: 'get',
       success: function (result) {
            var obj = result;

           var data = new Date();
           year = data.getFullYear();
           month = data.getMonth()+1;
           day = data.getDate();
           var Now_time = year+'-'+month+'-'+day;

           $("#id_dvrInput").val(obj.id);
           $("#factory_update_input").val(obj.factory);
           $("#monitoring_update_input").val(obj.monitoring_room);
           $("#DVR_ID_update_input").val(obj.dvR_ID);
           $("#DVR_IP_update_input").val(obj.dvR_IP);
           $("#Hard_drive_update_input").val(obj.hard_drive);
           $("#install_time_update_input").val(obj.install_time);
           $("#DVR_type_update_input").val(obj.dvR_type);
           $("#inst_manufacturer_update_input").val(obj.manufacturer);
           $("#DVR_SN_update_input").val(obj.dvR_SN);
           $("#lastModificationTime_update_input").text(Now_time);
           $("#cam_build").val(obj.camera_build);
           $("#cam_floor").val(obj.camera_foor);
           $("#home_server").val(obj.home_server);
           $("#hard_drive").val(obj.hard_drive);
           $("#dvr_port").val(obj.dvR_port);
           $("#dvr_u").val(obj.dvR_usre);
           hashMap.put("password",obj.dvR_possword);
           $("#dvr_cha").val(obj.dvR_Channel);
           $("#depat").val(obj.department);
           $("#cos_code").val(obj.cost_code);
           $("#remark_modify").val(obj.remark);
           $("#ModificationTime").text(Now_time);

       } 
    });
}

//点击更新，更新信息
$("#dvr_update_btn").click(function(){
    // //1、获取信息
    // var data = new Date();
    // year = data.getFullYear();
    // month = data.getMonth()+1;
    // day = data.getDate();
    // var Now_time = year+'-'+month+'-'+day;

    var dv_ps = $("#dvr_p").val();
    if (dv_ps == "" || dv_ps == null){
        dv_ps = hashMap.get("password");
    }

    var modify_data = {
        "factory": $("#factory_update_input").val(),
        "monitoring_room": $("#monitoring_update_input").val(),
        "camera_build": $("#cam_build").val(),
        "camera_foor": $("#cam_floor").val(),
        "dvR_ID": $("#DVR_ID_update_input").val(),
        "home_server": $("#home_server").val(),
        "hard_drive": parseInt($("#hard_drive").val()),
        "dvR_IP": $("#DVR_IP_update_input").val(),
        "dvR_port": $("#dvr_port").val(),
        "dvR_usre": $("#dvr_u").val(),
        "dvR_possword": dv_ps,
        "install_time": $("#install_time_update_input").val(),
        "manufacturer": $("#inst_manufacturer_update_input").val(),
        "dvR_type": $("#DVR_type_update_input").val(),
        "dvR_SN": $("#DVR_SN_update_input").val(),
        "dvR_Channel": parseInt($("#dvr_cha").val()),
        "department": $("#depat").val(),
        "cost_code": $("#cos_code").val(),
        "remark": $("#remark_modify").val()
    };

    console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = $("#id_dvrInput").val();
    $.ajax({
        url:host+"app/dVR/"+put_id,
        type:"PUT",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#dvrUpdateModal").modal('hide');

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


