$(function () {

    $("#loginName").on("focus",function () {
        $("#error_show").html('');
        $("#loging_show").html('');
    }).on("keydown",function () {
        $("#error_show").html('');
        $("#loging_show").html('');
    });
    $("#loginPass").on("focus",function () {
        $("#error_show").html('');
        $("#loging_show").html('');
    }).on("keydown",function () {
        $("#error_show").html('');
        $("#loging_show").html('');
    });

    document.onkeydown = function (event) {
        var e = event || window.event ||arguments.callee.caller.arguments[0];
        if (e && e.keyCode == 13) {
            loginAccount()
        }
    };

});

function loginAccount() {
    $("#error_show").html('');
    $("#loging_show").html('');

    //验证
    var loginAccount = {
        "userNameOrEmailAddress": $("#loginName").val(),
        "password": $("#loginPass").val(),
        "rememberMe": true,
        "tenanId": ""
    };

    $.ajax({
        url: host+'account/login',
        type: 'post',
        contentType : 'application/json',
        data : JSON.stringify(loginAccount),
        beforeSend: function(){
            $("#loging_show").html("登录中...")
        },
        success: function (data) {
            if(data.result == 1){
                window.location.href='./index/index.html';
            }else if (data.result == 4){
                $("#loging_show").html('');
                $("#error_show").text("密码输入错误次数太多，账号已被锁定，请5分钟后再试!")
            }else {
                $("#loging_show").html('');
                $("#error_show").text("用户名或密码错误，请重新输入!")
            }
        }
    });
}