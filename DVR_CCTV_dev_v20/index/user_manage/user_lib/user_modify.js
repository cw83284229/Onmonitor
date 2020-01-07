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


function getUserMod(id) {

    //此处写ajax请求json数组对象数据源
    $.ajax({
        url: host+'identity/users/'+id,
        type: 'get',
        success: function (result) {
            var obj = result;
            hashMap.put("id",obj.id);
            hashMap.put("userName",obj.userName);

            $("#username").val(obj.name);
        }
    });

}

//点击更新，更新信息
$("#user_update_btn").click(function(){
    var userName = hashMap.get("userName");
    var uname = $("#username").val();
    var email = hashMap.get("userName")+"@foxconn.com";
    var modify_data = {
        // "password": "string",
        // "concurrencyStamp": "string",
        "userName": userName,
        "name": uname,
        // "surname": "string",
        "email": email,
        // "phoneNumber": "string",
        // "twoFactorEnabled": true,
        // "lockoutEnabled": true,
        // "roleNames": [
        //     "string"
        // ]
    };

    // console.log(modify_data);

    //2、发送ajax请求保存更新的数据
    var put_id = hashMap.get('id');
    $.ajax({
        url:host+"identity/users/"+put_id,
        type: 'put',
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(modify_data),
        success:function(result){
            //1、关闭模态框
            $("#userUpdateModal").modal('hide');

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