//用户名，密码数据验证
$(function () {
    //显示tip

    $("#username").on("focus",function () {
        $("#reg_hd").html('');
        $("#reg_usr_tip").html('<b style="color: #2aabd2">6～18个字符，可使用字母、数字、下划线</b>');
    }).on("blur",function () {
        $("#reg_usr_tip").html('');
    });

    $("#password").on("focus",function () {
        $("#reg_hd").html('');
        $("#reg_pwd_tip").html('<b style="color: #2aabd2">6～18个字符，须包含大小写字母及数字和特殊字符</b>');
    }).on("blur",function () {
        $("#reg_pwd_tip").html('');
    })

});

//验证密码
function tsPwd(e) {
    return !/^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).+$/.test(e)
}


function Reg_check() {
    var username = $("#username").val().trim();
    var password = $("#password").val().trim();

    if (username == "" || password == ""){
        //用户名和密码不能为空
        $("#reg_hd").html('<b style="color: red">用户名或密码不能为空 !</b>');
        return;
    }

    if (username.toUpperCase() == password.toUpperCase()){
        //用户名和密码不能相同
        $("#reg_hd").html('<b style="color: red">用户名和密码不能相同 !</b>');
        return;
    }

    //验证密码
    if (tsPwd(password)){
        //密码须包含大小写字母及数字和特殊字符
        $("#reg_hd").html('<b style="color: red">密码须包含大小写字母及数字和特殊字符 ! 请尝试“字母+数字+.”的组合</b>');
        return;
    }

    //后台User API未写完，此处无法判断
    $.ajax({
        url: host+'identity/users?Filter='+username,
        type: 'get',
        success: function (data) {
            var obj = data.items;
            if (obj.userName == username){
                //用户名已存在
                $("#reg_hd").html('<b style="color: red">用户名已存在 !</b>');
                return;
            }else {
                regUser(username,password)
            }
        },
        error: function () {
            $("#reg_hd").html('<b style="color: blue">服务器繁忙，请稍后再试</b>');
        }
    });

}

function regUser(usr,pwd) {
    var email = usr+"@foxconn.com";

    var regData = {
            "userName": usr,
            "emailAddress": email,
            "password": pwd,
            "appName": "string"
        };

    $.ajax({
        url: host+'account/register',
        type: 'post',
        contentType : 'application/json',
        data : JSON.stringify(regData),
        success: function () {
            // $.dialog({
            //     title: '添加成功 !',
            //     content: '',
            // });
            // //跳回到用户管理页面
            // setTimeout(function(){window.location.href = "./user.html";},500);
            window.location.href = "./user.html";
        },
        error: function () {
            $("#reg_hd").html('<b style="color: red">密码须包含大小写字母及数字和特殊字符 ! 请尝试“字母+数字+.”的组合</b>');
            // $("#reg_hd").html('<b style="color: blue">服务器繁忙，请稍后再试</b>');
        }
    })

}