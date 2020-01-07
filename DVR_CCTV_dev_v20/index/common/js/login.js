$(function(){
    $('#loginButton').click(checkLogin);
});

function checkLogin(){
    var name = $("#loginName").val();
    var pwd = $("#loginPass").val();
    $.ajax({
        url:"login.do",
        type:"post",
        async:false,//同步：意思是当有返回值以后才会进行后面的js程序
        data:{"name":name,"pwd":pwd},
        success:function (msg) {
            // $("#msg").html(result);
            //此处写重定向页面
            /**
             * ajax只接受最后返回的值，不会响应跳转请求更改浏览器地址栏地址转向的，
             * 需要用js判断ajax的返回值是否要跳转，然后设置location.href实现跳转。
             */
            if (msg) {//根据返回值进行跳转
                window.location.href = '../../dataAnalysis/index.html';
            }
        }
    });
};