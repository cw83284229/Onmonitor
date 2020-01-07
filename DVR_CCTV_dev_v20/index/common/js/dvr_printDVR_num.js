$(function () {

    giveDvrVal();

    var Now_Time = getMyDate();

    $(".update_time").html('<b>'+Now_Time+'</b>');

    fillPrintDVR_entry1();
    fillPrintDVR_entry2();
    fillPrintDVR_entry3();
});

function getMyDate(){
    var oDate = new Date(),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
        oTime = oYear +'-'+ getzf(oMonth) +'-'+ getzf(oDay);//最后拼接时间
    return oTime;
};
//补0操作
function getzf(num){
    if(parseInt(num) < 10){
        num = '0'+num;
    }
    return num;
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
        var id = theRequest.id.split(",");
        return id;
    }
};

var DVR_rowID1;
var DVR_rowID2;
var DVR_rowID3;

function giveDvrVal() {

    var id_grou = getID();
    DVR_rowID1 = id_grou[0];
    DVR_rowID2 = id_grou[1];
    DVR_rowID3 = id_grou[2];

}

function fillPrintDVR_entry1() {

    $.ajax({
        url: host+'app/dVR/'+DVR_rowID1,
        type: 'get',
        success: function (data) {
            var bud = data.camera_build;
            var caf = data.camera_foor;
            var ip = data.dvR_IP;
            var str = bud + '/' + caf + ' / IP: ' + ip;

            $("#floor_ip1").html('<b>'+str+'</b>');

            fillDVR_row1(data);
        }
    });

}

function fillDVR_row1(data) {
    var dvID_pr = data.dvR_ID;
    $("#dvrID_title1").text(dvID_pr);

    $.ajax({
        url: host+'app/camera/byCondition?DVR_ID='+dvID_pr+'&Sorting=id&SkipCount=0&MaxResultCount=100',
        type: 'get',
        success: function (result) {

            var obj = result.items;
            for (var i = 0; i < 24; i++) {

                // if (obj[i] == undefined){
                //     obj[i] = ""
                // }

                var indx = i+1;
                var build = obj[i].build;
                var floor = obj[i].floor;
                var direc = !obj[i].direction?"":obj[i].direction;
                var loc = obj[i].location;
                var insTime = obj[i].install_time;
                var camID = obj[i].camera_ID;

                // var build = !obj[i].build?"":obj[i].build;

                var tr="<tr>"

                    +"<td align=\"center\">"+indx+"</td>"
                    +"<td align=\"center\">"+build+"</td>"
                    +"<td align=\"center\">"+floor+"</td>"
                    +"<td align=\"center\">"+direc+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+loc+"</td>"
                    +"<td align=\"center\">"+insTime+"</td>"
                    +"<td align=\"center\" class=\"caId\">"+camID+"</td>"

                    +"</tr>";

                var $opt = $(tr);//将字符串转换成jQuery对象
                $("#checklist1").append($opt);
            }

        }
    })

}


function fillPrintDVR_entry2() {

    $.ajax({
        url: host+'app/dVR/'+DVR_rowID2,
        type: 'get',
        success: function (data) {
            var bud = data.camera_build;
            var caf = data.camera_foor;
            var ip = data.dvR_IP;
            var str = bud + '/' + caf + ' / IP: ' + ip;

            $("#floor_ip2").html('<b>'+str+'</b>');

            fillDVR_row2(data)
        }
    });
    
}

function fillDVR_row2(data) {

    var dvID_pr = data.dvR_ID;
    $("#dvrID_title2").text(dvID_pr);

    $.ajax({
        url: host+'app/camera/byCondition?DVR_ID='+dvID_pr+'&Sorting=id&SkipCount=0&MaxResultCount=100',
        type: 'get',
        success: function (result) {

            var obj = result.items;
            for (var i = 0; i < 24; i++) {

                // if (obj[i] == undefined){
                //     obj[i] = ""
                // }

                var indx = i+1;
                var build = !obj[i].build?"":obj[i].build;
                var floor = !obj[i].floor?"":obj[i].floor;
                var direc = !obj[i].direction?"":obj[i].direction;
                var loc = !obj[i].location?"":obj[i].location;
                var insTime = !obj[i].install_time?"":obj[i].install_time;
                var camID = !obj[i].camera_ID?"":obj[i].camera_ID;

                // var build = !obj[i].build?"":obj[i].build;

                var tr="<tr>"

                    +"<td align=\"center\">"+indx+"</td>"
                    +"<td align=\"center\">"+build+"</td>"
                    +"<td align=\"center\">"+floor+"</td>"
                    +"<td align=\"center\">"+direc+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+loc+"</td>"
                    +"<td align=\"center\">"+insTime+"</td>"
                    +"<td align=\"center\" class=\"caId\">"+camID+"</td>"

                    +"</tr>";

                var $opt = $(tr);//将字符串转换成jQuery对象
                $("#checklist2").append($opt);
            }

        }
    })

}

function fillPrintDVR_entry3() {

    $.ajax({
        url: host+'app/dVR/'+DVR_rowID3,
        type: 'get',
        success: function (data) {
            var bud = data.camera_build;
            var caf = data.camera_foor;
            var ip = data.dvR_IP;
            var str = bud + '/' + caf + ' / IP: ' + ip;

            $("#floor_ip3").html('<b>'+str+'</b>');

            fillDVR_row3(data)
        }
    });

}

function fillDVR_row3(data) {

    var dvID_pr = data.dvR_ID;
    $("#dvrID_title3").text(dvID_pr);

    $.ajax({
        url: host+'app/camera/byCondition?DVR_ID='+dvID_pr+'&Sorting=id&SkipCount=0&MaxResultCount=100',
        type: 'get',
        success: function (result) {

            var obj = result.items;
            for (var i = 0; i < 24; i++) {

                // if (obj[i] == undefined){
                //     obj[i] = ""
                // }

                var indx = i+1;
                var build = obj[i].build;
                var floor = obj[i].floor;
                var direc = !obj[i].direction?"":obj[i].direction;
                var loc = obj[i].location;
                var insTime = obj[i].install_time;
                var camID = obj[i].camera_ID;

                // var build = !obj[i].build?"":obj[i].build;

                var tr="<tr>"

                    +"<td align=\"center\" style=\" white-space:nowrap\">"+indx+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+build+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+floor+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+direc+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+loc+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\">"+insTime+"</td>"
                    +"<td align=\"center\" style=\" white-space:nowrap\" class=\"caId\">"+camID+"</td>"

                    +"</tr>";

                var $opt = $(tr);//将字符串转换成jQuery对象
                $("#checklist3").append($opt);
            }

        }
    })

}