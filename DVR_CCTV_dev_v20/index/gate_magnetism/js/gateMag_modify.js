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


function getGateMag(id) {

    $('#isAlertor').select2();
    $('#isOpenOrClosed').select2();

    //此处写ajax请求json数组对象数据源
    $.ajax({
        url: host+'app/alarm/'+id,
        type: 'get',
        success: function (result) {
            console.log(result);
            var obj = result;
            hashMap.put("id",obj.id);

            $("#monitor_room").val(obj.monitoring_room);
            $("#alarm_host").val(obj.alarmHost_ID);
            $("#alarm_id").val(obj.alarm_ID);
            $("#gate_build").val(obj.build);
            $("#gate_floor").val(obj.floor);
            $("#gate_location").val(obj.location);
            $("#gate_type").val(obj.geteType);
            $("#censor_type").val(obj.sensorType);
            $("#depat").val(obj.department);
            $("#cos_code").val(obj.cost_code);
            $("#inst_time").val(obj.install_time);
            $("#inst_manufacturer").val(obj.category);
            $("#camera_ID").val(obj.camera_ID);

            hashMap.put("isAlertor",obj.isAlertor);
            hashMap.put("isOpenOrClosed",obj.isOpenOrClosed);

            $("#remark").val(obj.remark);

        }
    });

}

//点击更新，更新信息
$("#gate_update_btn").click(function(){

    var isAlertor=$("#isAlertor option:selected").val();
    if (isAlertor == "false"){
        isAlertor = false
    }else if (isAlertor == "true"){
        isAlertor = true
    }else {
        isAlertor = hashMap.get('isAlertor')
    }

    var isOpenOrClosed=$("#isOpenOrClosed option:selected").val();
    if (isOpenOrClosed == "false"){
        isOpenOrClosed = false
    }else if (isOpenOrClosed == "true"){
        isOpenOrClosed = true
    }else {
        isOpenOrClosed = hashMap.get('isOpenOrClosed')
    }


    var modify_data = {
        "monitoring_room": $("#monitor_room").val(),
        "alarmHost_ID": $("#alarm_host").val(),
        "alarm_ID": $("#alarm_id").val(),
        "build": $("#gate_build").val(),
        "floor": $("#gate_floor").val(),
        "location": $("#gate_location").val(),
        "geteType": $("#gate_type").val(),
        "sensorType": $("#censor_type").val(),
        "department": $("#depat").val(),
        "cost_code": $("#cos_code").val(),
        "install_time": $("#inst_time").val(),
        "category": $("#inst_manufacturer").val(),
        "camera_ID": $("#camera_ID").val(),
        "isAlertor": isAlertor,
        "isOpenOrClosed": isOpenOrClosed,
        "remark": $("#remark").val()
    };

    console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = hashMap.get('id');
    $.ajax({
        url:host+"app/alarm/"+put_id,
        type: 'put',
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#gateUpdateModal").modal('hide');

            $.dialog({
                title: '修改成功!',
                content: '',
            });
            setTimeout(function(){location.reload();},500);
        },
        error: function () {
            $.dialog({
                title: '修改失败!',
                content: '',
            });
        }
    });
});