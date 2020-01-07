$(function () {

    new Vue({
        el: '#app',
        data: {
            project_name:'', //工程名称
            project_order:'', //工程单号
            project_specification:'', //开工日期
            start_date:'', //开工日期
            over_date:'', //完工日期
            check_time:'', //验收时间
            manufacturer_name:'', //厂商名称
            work_type:'', //监控施工类型
            acceptance_result:'', //验收结果
            other_specification:'', //其它说明
            // authorizer:'', //核准
            // verifier:'', //审核
            // agent:'', //承办
            camera_id1:'',
            camera_id2:'',
            camera_id3:'',
            camera_id4:'',
            camera_id5:'',
            camera_id6:'',
            camera_id: [] ,//提交镜头数组

            build_1:'',
            build_2:'',
            build_3:'',
            build_4:'',
            build_5:'',
            build_6:'',
            build: [],

            floor_1:'',
            floor_2:'',
            floor_3:'',
            floor_4:'',
            floor_5:'',
            floor_6:'',
            floor: [],

        },
        methods: {
            showInfo: function (event) {
                hashMap.put("project_name",this.project_name);
                hashMap.put("project_order",this.project_order);
                hashMap.put("project_specification",this.project_specification);
                hashMap.put("start_date",this.start_date);
                hashMap.put("over_date",this.over_date);
                hashMap.put("check_time",this.check_time);
                hashMap.put("manufacturer_name",this.manufacturer_name);
                hashMap.put("work_type",this.work_type);
                hashMap.put("acceptance_result",this.acceptance_result);
                hashMap.put("remark",this.other_specification);
                // hashMap.put("authorizer",this.authorizer);
                // hashMap.put("verifier",this.verifier);
                // hashMap.put("agent",this.agent);
                this.camera_id.push(this.camera_id1);
                this.camera_id.push(this.camera_id2);
                this.camera_id.push(this.camera_id3);
                this.camera_id.push(this.camera_id4);
                this.camera_id.push(this.camera_id5);
                this.camera_id.push(this.camera_id6);
                var camalaSelia = JSON.stringify(this.camera_id);
                hashMap.put("camera_id",camalaSelia);

                this.build.push(this.build_1);
                this.build.push(this.build_2);
                this.build.push(this.build_3);
                this.build.push(this.build_4);
                this.build.push(this.build_5);
                this.build.push(this.build_6);
                var buildSelia = JSON.stringify(this.build);
                hashMap.put("build",buildSelia);

                this.floor.push(this.floor_1);
                this.floor.push(this.floor_2);
                this.floor.push(this.floor_3);
                this.floor.push(this.floor_4);
                this.floor.push(this.floor_5);
                this.floor.push(this.floor_6);
                var floorSelia = JSON.stringify(this.floor);
                hashMap.put("floor",buildSelia);

                showVue(hashMap);
            }
        }
    });

});


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

//保存提交数据
function showVue(hashMap) {
    //通过hashMap.get('key')的方式读取值并发送ajax保存数据,提交成功后提示"保存成功"，然后回到当前页 location.reload();

    var pro_post = {
        "projectManageType": "工程新增表",
        "projectName": hashMap.get('project_name'),
        "projectOrder": hashMap.get('project_order'),
        "startWorkDate": hashMap.get('start_date'),
        "completeDate": hashMap.get('over_date'),
        "acceptanceData": hashMap.get('check_time'),
        "manufacturerName": hashMap.get('manufacturer_name'),
        "projectSpecifications": hashMap.get('project_specification'),
        "build": hashMap.get('build'),
        "floor": hashMap.get('floor'),
        "camera_ID": hashMap.get('camera_id'),
        "acceptanceResult": hashMap.get('acceptance_result'),
        "remark": hashMap.get('remark'),
    };

    $.ajax({
        url: host+'app/projectManages',
        type: 'post',
        contentType : 'application/json',
        data : JSON.stringify(pro_post),
        success: function () {
            //执行镜头组提交
            postCamGroup();

        },
        error: function () {
            $.dialog({
                title: '提交失败!',
                content: '',
            });
        }
    });

};


function postCamGroup() {

    Array.prototype.indexOf = function(val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };
    Array.prototype.remove = function(val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.splice(index, 1);
        }
    };


    var post_list = [];
    var post1 = {
        "monitoring_room": $("#monRoom_1").val(),
        "dvR_ID": $("#dvrID_1").val(),
        "channel_ID": Number($("#chanel_1").val()),
        "camera_ID": $("#camID_1").val(),
        "build": $("#build_1").val(),
        "floor": $("#floor_1").val(),
        "direction": $("#direct_1").val(),
        "location": $("#loc_1").val(),
        "monitorClassification": $("#monTyp_1").val(),
        "camera_Tpye": $("#camTyp_1").val(),
        "department": $("#dep_1").val(),
        "cost_code": $("#cos_1").val(),
        "install_time": $("#insT_1").val(),
        "manufacturer": $("#manu_1").val(),
        "category": $("#cat_1").val(),
        "alarm_ID": $("#ala_1").val(),
        "remark": $("#rem_1").val()
    };
    var post2 = {
        "monitoring_room": $("#monRoom_2").val(),
        "dvR_ID": $("#dvrID_2").val(),
        "channel_ID": Number($("#chanel_2").val()),
        "camera_ID": $("#camID_2").val(),
        "build": $("#build_2").val(),
        "floor": $("#floor_2").val(),
        "direction": $("#direct_2").val(),
        "location": $("#loc_2").val(),
        "monitorClassification": $("#monTyp_2").val(),
        "camera_Tpye": $("#camTyp_2").val(),
        "department": $("#dep_2").val(),
        "cost_code": $("#cos_2").val(),
        "install_time": $("#insT_2").val(),
        "manufacturer": $("#manu_2").val(),
        "category": $("#cat_2").val(),
        "alarm_ID": $("#ala_2").val(),
        "remark": $("#rem_2").val()
    };
    var post3 = {
        "monitoring_room": $("#monRoom_3").val(),
        "dvR_ID": $("#dvrID_3").val(),
        "channel_ID": Number($("#chanel_3").val()),
        "camera_ID": $("#camID_3").val(),
        "build": $("#build_3").val(),
        "floor": $("#floor_3").val(),
        "direction": $("#direct_3").val(),
        "location": $("#loc_3").val(),
        "monitorClassification": $("#monTyp_3").val(),
        "camera_Tpye": $("#camTyp_3").val(),
        "department": $("#dep_3").val(),
        "cost_code": $("#cos_3").val(),
        "install_time": $("#insT_3").val(),
        "manufacturer": $("#manu_3").val(),
        "category": $("#cat_3").val(),
        "alarm_ID": $("#ala_3").val(),
        "remark": $("#rem_3").val()
    };
    var post4 = {
        "monitoring_room": $("#monRoom_4").val(),
        "dvR_ID": $("#dvrID_4").val(),
        "channel_ID": Number($("#chanel_4").val()),
        "camera_ID": $("#camID_4").val(),
        "build": $("#build_4").val(),
        "floor": $("#floor_4").val(),
        "direction": $("#direct_4").val(),
        "location": $("#loc_4").val(),
        "monitorClassification": $("#monTyp_4").val(),
        "camera_Tpye": $("#camTyp_4").val(),
        "department": $("#dep_4").val(),
        "cost_code": $("#cos_4").val(),
        "install_time": $("#insT_4").val(),
        "manufacturer": $("#manu_4").val(),
        "category": $("#cat_4").val(),
        "alarm_ID": $("#ala_4").val(),
        "remark": $("#rem_4").val()
    };
    var post5 = {
        "monitoring_room": $("#monRoom_5").val(),
        "dvR_ID": $("#dvrID_5").val(),
        "channel_ID": Number($("#chanel_5").val()),
        "camera_ID": $("#camID_5").val(),
        "build": $("#build_5").val(),
        "floor": $("#floor_5").val(),
        "direction": $("#direct_5").val(),
        "location": $("#loc_5").val(),
        "monitorClassification": $("#monTyp_5").val(),
        "camera_Tpye": $("#camTyp_5").val(),
        "department": $("#dep_5").val(),
        "cost_code": $("#cos_5").val(),
        "install_time": $("#insT_5").val(),
        "manufacturer": $("#manu_5").val(),
        "category": $("#cat_5").val(),
        "alarm_ID": $("#ala_5").val(),
        "remark": $("#rem_5").val()
    };
    var post6 = {
        "monitoring_room": $("#monRoom_6").val(),
        "dvR_ID": $("#dvrID_6").val(),
        "channel_ID": Number($("#chanel_6").val()),
        "camera_ID": $("#camID_6").val(),
        "build": $("#build_6").val(),
        "floor": $("#floor_6").val(),
        "direction": $("#direct_6").val(),
        "location": $("#loc_6").val(),
        "monitorClassification": $("#monTyp_6").val(),
        "camera_Tpye": $("#camTyp_6").val(),
        "department": $("#dep_6").val(),
        "cost_code": $("#cos_6").val(),
        "install_time": $("#insT_6").val(),
        "manufacturer": $("#manu_6").val(),
        "category": $("#cat_6").val(),
        "alarm_ID": $("#ala_6").val(),
        "remark": $("#rem_6").val()
    };

    post_list.push(post1);
    post_list.push(post2);
    post_list.push(post3);
    post_list.push(post4);
    post_list.push(post5);
    post_list.push(post6);

    if (post1.camera_ID === '' || post1.camera_ID == null){
        post_list.remove(post1);
    }
    if (post2.camera_ID === '' || post2.camera_ID == null){
        post_list.remove(post2);
    }
    if (post3.camera_ID === '' || post3.camera_ID == null){
        post_list.remove(post3);
    }
    if (post4.camera_ID !== '' || post4.camera_ID !== null){
        post_list.remove(post4);
    }
    if (post5.camera_ID !== '' || post5.camera_ID !== null){
        post_list.remove(post5);
    }
    if (post6.camera_ID !== '' || post6.camera_ID !== null){
        post_list.remove(post6);
    }

    console.log(post_list);

    $.ajax({
        url: host+'app/camera/list',
        type: 'post',
        contentType : 'application/json',
        data : JSON.stringify(post_list),
        success: function () {

            $.dialog({
                title: '提交成功!',
                content: '',
            });
        },
        error: function () {
            $.dialog({
                title: '提交失败!',
                content: '',
            });
        }
    })

}
