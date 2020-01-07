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


function getDCF(id) {

    $('#DVR_ON_ST').select2();
    $('#abnormal_info').select2();
    $('#CH_INFO_AN').select2();

    //此处写ajax请求json数组对象数据源
    $.ajax({
        url: host+'app/dVRCheckInfo/'+id,
        type: 'get',
        success: function (result) {
            console.log(result);
            var obj = result;
            hashMap.put("id",obj.id);

            $("#DVR_ID").val(obj.dvR_ID);
            $("#DVR_SN").val(obj.dvR_SN);
            $("#DVR_TYPE").val(obj.dvR_type);
            $("#DVR_CH_NUM").val(obj.dvR_Channel);
            // $("#HD_INFO").val(obj.dvrdisk);
            hashMap.put("hd_info",obj.dvrdisk);
            // $("#DVR_ON_ST").val(obj.location);
            // $("#abnormal_info").val(obj.geteType);
            $("#HD_AL").val(obj.diskTotal);
            // $("#CH_INFO_AN").val(obj.department);

            hashMap.put("dvrChannelInfo",obj.dvrChannelInfo);
            hashMap.put("libraryChannelInfo",obj.libraryChannelInfo);

            hashMap.put("dvR_Online",obj.dvR_Online);
            hashMap.put("infoChenk",obj.infoChenk);
            hashMap.put("channelChenk",obj.channelChenk);

            $("#remark").val(obj.remark);

        }
    });

}

//点击更新，更新信息
$("#dcf_update_btn").click(function(){

    var dvR_Online=$("#DVR_ON_ST option:selected").val();
    if (dvR_Online == "false"){
        dvR_Online = false
    }else if (dvR_Online == "true"){
        dvR_Online = true
    }else {
        dvR_Online = hashMap.get('dvR_Online')
    }

    var abnormal_info=$("#abnormal_info option:selected").val();
    if (abnormal_info == "false"){
        abnormal_info = false
    }else if (abnormal_info == "true"){
        abnormal_info = true
    }else {
        abnormal_info = hashMap.get('infoChenk')
    }

    var channelChenk=$("#CH_INFO_AN option:selected").val();
    if (channelChenk == "false"){
        channelChenk = false
    }else if (channelChenk == "true"){
        channelChenk = true
    }else {
        channelChenk = hashMap.get('channelChenk')
    }

    var dvr_ch = $("#DVR_CH_NUM").val();
    var dvrChInfo = hashMap.get('dvrChannelInfo');
    var libCh = hashMap.get('libraryChannelInfo');
    var hd_al = $("#HD_AL").val();

    var dvd = hashMap.get('hd_info');

    var modify_data = {
        "dvR_ID": $("#DVR_ID").val().toUpperCase(),
        "dvR_SN": $("#DVR_SN").val(),
        "dvR_type": $("#DVR_TYPE").val(),
        "dvR_Channel": parseInt(dvr_ch),
        "dvrdisk": dvd,
        "dvrChannelInfo": dvrChInfo,
        "libraryChannelInfo": libCh,
        "dvR_Online": Boolean(dvR_Online),
        "infoChenk": Boolean(abnormal_info),
        "diskTotal": parseInt(hd_al),
        "channelChenk": Boolean(channelChenk),
        "remark": $("#remark").val()
    };

    // console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = hashMap.get('id');
    $.ajax({
        url:host+"app/dVRCheckInfo/"+put_id,
        type: 'put',
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#DcfUpdateModal").modal('hide');

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