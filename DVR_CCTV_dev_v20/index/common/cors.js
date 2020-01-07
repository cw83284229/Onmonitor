var host = '/api/';
var st_host = '/api/DVRInfo';
var sc_host = '/api/DVRClannel';

$(function () {
    // $(".change-pwd").onclick(changePwd);

    $("#old_pwd").on("focus",function () {
        $("#confirmPwd_help_block").html("");
        $("#oldPwd_help_block").html("");
        $("#pwd_modify_btn").removeAttr("disabled","disabled");
    });

    $("#new_pwd").on("focus",function () {
        $("#confirmPwd_help_block").html("");
        $("#oldPwd_help_block").html("");
        $("#pwd_modify_btn").removeAttr("disabled","disabled");
    });

    $("#confirm_pwd").on("focus",function () {
        $("#confirmPwd_help_block").html("");
        $("#oldPwd_help_block").html("");
        $("#pwd_modify_btn").removeAttr("disabled","disabled");
    });

    //获取用户信息
    $.ajax({
        url: host+'identity/my-profile',
        type: 'get',
        success: function (usr_data) {
            var my_profile = usr_data.userName;
            $(".my-profile").html("您好, "+"<b>"+my_profile+"</b>");
            accomplishRole(my_profile);
        }
    })

});

//修改密码操作，截止2019-12-30，后台未提供修改密码API
function changePwd() {
    $("#confirmPwd_help_block").html("");
    $("#oldPwd_help_block").html("");
    $("#pwd_modify_btn").removeAttr("disabled","disabled");

    $("#pwdUpdateModal form")[0].reset();
    //弹出修改模态框
    $("#pwdUpdateModal").modal({
        backdrop:"static"
    });

}
//点击修改，修改密码
function pwd_modify(){
    var currentPwd = $("#old_pwd").val();
    var newPwd = $("#new_pwd").val();
    var comfirmPwd = $("#confirm_pwd").val();

    if (newPwd == '' || comfirmPwd == '' || currentPwd== ''){
        $("#confirmPwd_help_block").html("<b>密码不能为空！</b>").css("color","red");
        //阻止提交
        $("#pwd_modify_btn").attr("disabled","disabled");
        return;
    }

    if (newPwd !== comfirmPwd){
        $("#confirmPwd_help_block").html("<b>新密码和确认密码不一致！</b>").css("color","red");
        //阻止提交
        $("#pwd_modify_btn").attr("disabled","disabled");
        return;
    }

    var pwd_data={
        "currentPassword": currentPwd,
        "newPassword": comfirmPwd
    };

    //修改失败提示用户检查密码格式?
    $.ajax({
        url: host+'identity/my-profile/change-password',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(pwd_data),
        success: function () {
            $("#pwdUpdateModal").modal('hide');

            $.dialog({
                title: '密码修改成功!',
                content: '',
            });

            setTimeout(function(){window.location.href="../../index.html";},500);
        },
        error: function () {
            $("#confirmPwd_help_block").html("<b>密码须包含大小写字母及数字和特殊字符 ! 请尝试“大小写字母+数字+.”的组合！</b>").css("color","red");
            // $.dialog({
            //     title: '密码修改失败!',
            //     content: '',
            // });
        }
    });
};

//角色判断，是否有使用权限管理模块的权限
function accomplishRole(u_name) {

    if (u_name == "idpbg" || u_name == "admin"){
        $("#permission_manger").removeAttr("style","display:none");
    }

}