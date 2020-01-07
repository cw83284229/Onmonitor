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


function getRole(id){

    //此处写ajax请求json数组对象数据源
    $.ajax({
        url: host+'identity/roles/'+id,
        type: 'get',
        success: function (obj) {
            hashMap.put("id",obj.id);
            $("#role_name").val(obj.name);
        }
    });
}

//点击更新，更新信息
$("#role_update_btn").click(function(){

    var name = $("#role_name").val();

    var modify_data = {
        "concurrencyStamp": "string",
        "name": name,
        "isDefault": true,
        "isPublic": true
    };

    //2、发送ajax请求保存更新的数据
    var put_id = hashMap.get('id');
    $.ajax({
        url:host+"identity/roles/"+put_id,
        type: 'put',
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#roleUpdateModal").modal('hide');

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