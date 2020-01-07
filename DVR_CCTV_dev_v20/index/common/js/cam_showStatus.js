$(function () {
    console.log(getID());
});

//获取传送过来的id值
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
}