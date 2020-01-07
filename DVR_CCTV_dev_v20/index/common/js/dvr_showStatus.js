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
    //显示数据
    show_ku_data();
});

function showTime(d_data) {

    // var ip = d_data.dvR_IP;
    // var name = d_data.dvR_usre;
    // var password = d_data.dvR_possword;
    var dvr_id = d_data.dvR_ID;

    $.ajax({
        url: host+'Camera/GetDVRTime?DVR_ID='+dvr_id,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var timeDate = JSON.parse(data);
            console.log(timeDate);
            var ku_time = timeDate.DVRTime;
            var xt_time = timeDate.ServerTime;
            var isOK = timeDate.IsOk;

            $("#ku_time_status").text(ku_time);
            $("#xt_time_status").text(xt_time);

            if (isOK){
                $("#time_tf").text("OK");
                $("#time_tf").css("color","#00FF00");
            }else {
                $("#time_tf").text("NG");
                $("#time_tf").css("color","red");
            }

        }

    })

}

//显示数据
function show_ku_data() {
    //ajax根据id请求到dvr库数据
    //添加此请求则取消数据遍历
    $.ajax({
        url: host+'app/dVR/' + getID(),
        type: 'get',
        success: function (dvr_data) {
            console.log(dvr_data);

            $("#ku_dvrID").text(dvr_data.dvR_ID);
            $("#ku_dvrIP").text(dvr_data.dvR_IP);
            $("#ku_hardDisk_status").text(Math.floor(Math.round(dvr_data.hard_drive/1024)*0.909629+1) + " TB");
            $("#ku_sn_status").text(dvr_data.dvR_SN);
            $("#ku_dvr_channel").text(dvr_data.dvR_Channel);

            hashMap.put("cam_dvrID",dvr_data.dvR_ID);

            showTime(dvr_data);
            showKuChannel(dvr_data);
        }
    });
};

//获取传递过来的数据列id
function getID() {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object(); //theRequest为对象参数，调用请用theRequest.id
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);//substr()方法返回从参数值开始到结束的字符串；
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
        }
        var id = theRequest.id;
        return id;
    }
};

function showKuChannel(d_data) {
    var dvr_id = d_data.dvR_ID;

    //ajax根据dvr id请求到camera库数据
    $.ajax({
        // url: host+'app/camera/byDVRID?DVRID=' + dvr_id,
        // url: host+'app/camera/byCondition?DVR_ID='+dvr_id+'&Sorting=id&SkipCount=0&MaxResultCount=20000',
        url: host+'app/camera/byDVRID?DVRID='+dvr_id,
        type: 'get',
        success: function (result) {
            console.log(result);
            var cam_data = result;

            // for (var i=0;i<cam_data.length;i++){
            //     console.log("系统通道： ");
            //     console.log(cam_data[i].channel_ID);
            // }

            showXT(d_data, cam_data);
        }
    });
};


function showXT(d_data, cam_data) {
    //获取系统请求参数
    // var ip = d_data.dvR_IP;
    // var name = d_data.dvR_usre;
    // var password = d_data.dvR_possword;
    var dvr_id = d_data.dvR_ID;

    var ku_SN = d_data.dvR_SN;
    var ku_HD = Math.floor(Math.round(d_data.hard_drive/1024)*0.909629+1);

    console.log("库数据: ");
    console.log(cam_data);

    // var c_data = 0;

    $("#ku_cam_channel").text(cam_data.length);
    //系统数据ajax请求
    $.ajax({
        url: host+'Camera/GetDVRInfo?DVR_ID='+dvr_id,
        // url: st_host,
        type: 'GET',
        dataType: 'json',
        success: function (sys_data) {
            var data = JSON.parse(sys_data);
            console.log("系统数据： ");
            console.log(data);

            //系统通道数
            var xt_ch_num = data.Channelname.length;
            $("#xt_channel_num").text(xt_ch_num);
            //系统SN
            var xt_SN = data.DVR_SN;
            $("#xt_sn_status").text(xt_SN);
            //系统硬盘容量
            var xt_hardDisk = data.HardDrive;
            $("#xt_hardDisk_status").text(xt_hardDisk+" TB");

            compareSN(ku_SN,xt_SN);
            compare_hard_disk(ku_HD,xt_hardDisk);

            var dvr_HD = data.DVRDisk;
            for (var k in dvr_HD){
                var hdtb = Math.round(dvr_HD[k].Disk/1024/1024);
                var indx = ++k;
                $("#dvr_hd_chan").append('<span style="color: blue">硬盘'+indx+':&nbsp;&nbsp;'+hdtb+'&nbsp;TB&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>');
                // console.log(dvr_HD[i]);
            }

            var sys_d = data.Channelname;


            for(var i in sys_d){

                //系统通道
                var channel = sys_d[i].Name;
                var xt_chaNum = sys_d[i].Number;


                var c_data = 0;
                for (var j=0;j<cam_data.length;j++){
                    if (cam_data[j].channel_ID === xt_chaNum){
                        c_data = cam_data[j];
                    }
                }

                //库通道
                var cam_channel = c_data.camera_ID + " " + c_data.build + "-" + c_data.floor + c_data.direction + c_data.location;
                cam_channel = cam_channel.replace(/[null]{4}/," ");
                // cam_channel = cam_channel.replace(/[undefined]/g,"无");

                if(cam_channel.match(/^[undefined]/)){
                    cam_channel = "无"
            }

                if (channel.match(/[无]$/)){
                    var putCamID = channel.split('无').shift();
                    hashMap.put(xt_chaNum,putCamID);
                }

                var index = xt_chaNum;

                //数据显示
                var s_opt = '<tr><td id="'+index+'">'+index+'</td><td align="center">'+cam_channel+'</td><td align="center">'+channel+'</td><td align="center" id="pd'+index+'" style="font-size: large"></td><td align="center"><button type="button" class="btn btn-xs btn-info" onclick="showStatus(this)">修正</button></td><td align="center"><button type="button" class="btn btn-xs btn-default" onclick="asyncData(this)">同步</button></td></tr>';
                var $opt = $(s_opt);
                $("#checklist").append($opt);
                accomplishResult(channel,cam_channel,index);
            }


        },
        error: function () {
            alert("资源不存在！");
        }
    });
};

//通道比较
function accomplishResult(xt_ch,ku_ch,idx) {
    //去除空格
    var xt_ch_str = xt_ch.replace(/[-_]*[-_]*\s*/g,"");
    var ku_ch_str = ku_ch.replace(/[-_]*[-_]*\s*/g,"");

    var css_id = "pd"+idx;
    if (xt_ch_str == ku_ch_str){
        $("#"+css_id).text("OK");
        $("#"+css_id).css("color","#00FF00");
    }else {
        $("#"+css_id).text("NG");
        $("#"+css_id).css("color","red");
    }
};

//SN比较
function compareSN(ks,xs) {
    if (ks === xs){
        $(".pz").html("OK");
        $(".pz").css("color","#00FF00");
    }else {
        $(".pz").html("NG");
        $(".pz").css("color","red");
    }
};

//容量比较
function compare_hard_disk(ku_d,xt_d) {
    if (ku_d == xt_d){
        $(".ph").html("OK");
        $(".ph").css("color","#00FF00");
    }else {
        $(".ph").html("NG");
        $(".ph").css("color","red");
    }
};

//同步
function correcTime() {

    $.ajax({
        url: host+'app/dVR/' + getID(),
        type: 'get',
        success: function (dv_data) {
            KuTime(dv_data);
        }
    });

}
function KuTime(dv_data) {
    // var dvr_ip = dv_data.dvR_IP;
    // var dvr_name = dv_data.dvR_usre;
    // var dvr_pwd = dv_data.dvR_possword;
    var dv_id = dv_data.dvR_ID;

    $.ajax({
        url: host+'Camera/PostDVRTime?DVR_ID='+dv_id,
        type: 'post',
        success: function (time_data) {
            if (time_data == "修改失败"){
                $.dialog({
                    title: '同步失败!',
                    content: '',
                });
            }else {
                $.dialog({
                    title: '同步成功!',
                    content: '',
                });
                setTimeout(function(){location.reload();},500);
            }

        }
    })

}

//修正
function showStatus(obj) {

    $.ajax({
        url: host+'app/dVR/' + getID(),
        type: 'get',
        success: function (dv_data) {
            console.log(obj);
            KuChannel(dv_data,obj);
        }
    });
};

function KuChannel(dv_data,obj) {
    var dvr_id = dv_data.dvR_ID;

    //ajax根据dvr id请求到camera库数据
    $.ajax({
        url: host+'app/camera/byDVRID?DVRID=' + dvr_id,
        type: 'get',
        success: function (cam_data) {
            correction(obj,cam_data);
        }
    });
};

function correction(obj,cam_data) {
    var Channel_id = obj.parentNode.parentNode.firstChild.id;

    // var i = Channel_id - 1;
    // var ChannelName = cam_data[i].camera_ID + " " + cam_data[i].build + "-" + cam_data[i].floor + cam_data[i].direction + cam_data[i].location;

    var c_data=0;
    for (var i=0;i<cam_data.length;i++){
        if (cam_data[i].channel_ID == Channel_id){
            c_data = cam_data[i];
        }
    }

    var ca_id = c_data.camera_ID;

    console.log(ca_id);

    $.ajax({
        url: host+'Camera/SetChannelName?CameraID='+ca_id,
        type:'post',
        success:function (res_dat) {
            // console.log(res_dat);
            var c_res_dat = res_dat.toUpperCase();
            if (c_res_dat == '"OK"'){
                $.dialog({
                    title: '修正成功!',
                    content: res_dat,
                });
                setTimeout(function(){location.reload();},500);
            }else {
                $.dialog({
                    title: '修正失败!',
                    content: res_dat,
                });
            }
        },
        error: function () {
            $.dialog({
                title: '修正失败!',
                content: '',
            });
        }
    });

}
