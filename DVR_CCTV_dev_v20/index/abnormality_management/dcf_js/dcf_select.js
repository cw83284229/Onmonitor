$(function () {
    $("#DVR_Online_select").select2();
    $("#InfoChenk_select").select2();
    $("#ChannelChenk_select").select2();

    $("#DCF_select_btn").click(getSelectResult);
});

function dcfComp(dvon, indx) {
    var dvof = "dv_on"+indx;

    if (dvon == "掉线"){
        $("#"+dvof).css("color","red");
    }else if (dvon == "在线"){
        $("#"+dvof).css("color","#00FF00")
    }
}

function getSelectResult() {
    var DVR_ID = $("#DVR_ID_select").val().toUpperCase();
    var DVR_SN = $("#DVR_SN_select").val();
    var DVR_type = $("#DVR_type_select").val();
    var DVR_Channel = $("#DVR_Channel_select").val();
    var DVRDISK = $("#DVRDISK_select").val();
    var DVRChannelInfo = $("#DVRChannelInfo_select").val();
    var LibraryChannelInfo = $("#LibraryChannelInfo_select").val();

    var DVR_Online = $("#DVR_Online_select option:selected").val();

    var InfoChenk = $("#InfoChenk_select option:selected").val();

    var DiskTotal = $("#DiskTotal_select").val();

    var ChannelChenk = $("#ChannelChenk_select option:selected").val();

    var Remark = $("#Remark_select").val();

    $.ajax({
        url: host+'app/dVRCheckInfo/dVRInfoByCondition?DVR_ID='+DVR_ID+'&DVR_SN='+DVR_SN+'&DVR_type='+DVR_type+'&DVR_Channel='+DVR_Channel+'&DVRDISK='+DVRDISK+'&DVRChannelInfo='+DVRChannelInfo+'&LibraryChannelInfo='+LibraryChannelInfo+'&DVR_Online='+DVR_Online+'&InfoChenk='+InfoChenk+'&DiskTotal='+DiskTotal+'&ChannelChenk='+ChannelChenk+'&Remark='+Remark,
        type: 'get',
        success: function (data) {

            $("#checklist").html("");
            $("#pagination_module").attr("style","display:none");

            var obj = data.items;
            for (var i = 0; i < obj.length; i++) {
                // dcf_totalCount++;

                var id = obj[i].id;
                var indx = i+1;

                var dvR_ID = !obj[i].dvR_ID?"":obj[i].dvR_ID;
                var dvR_SN = !obj[i].dvR_SN?"":obj[i].dvR_SN;
                var dvR_type = !obj[i].dvR_type?"":obj[i].dvR_type;
                var dvR_Channel = !obj[i].dvR_Channel?"":obj[i].dvR_Channel;
                // var dvrdisk = obj[i].dvrdisk;

                // var dvrChannelInfo = obj[i].dvrChannelInfo;
                // var libraryChannelInfo = obj[i].libraryChannelInfo;

                var dvR_Online = obj[i].dvR_Online;
                if (dvR_Online == null){
                    dvR_Online = "未知"
                }else if (dvR_Online == false){
                    dvR_Online = "掉线";
                }else {
                    dvR_Online = "在线"
                }

                var infoChenk = !obj[i].infoChenk?"未检查":"已检查";
                var diskTotal = !obj[i].diskTotal?"":obj[i].diskTotal;
                var channelChenk = !obj[i].channelChenk?"未检查":"已检查";
                var remark = !obj[i].remark?"":obj[i].remark;

                var tr="<tr id="+id+">"
                    +"<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
                    +"<td align=\"center\">"+indx+"</td>"
                    +"<td align=\"center\">"+dvR_ID+"</td>"
                    +"<td align=\"center\">"+dvR_SN+"</td>"
                    +"<td align=\"center\">"+dvR_type+"</td>"
                    +"<td align=\"center\">"+dvR_Channel+"</td>"
                    // +"<td align=\"center\">"+dvrdisk+"</td>"
                    +"<td align=\"center\" id=\"dv_on"+indx+"\">"+dvR_Online+"</td>"
                    +"<td align=\"center\">"+infoChenk+"</td>"
                    +"<td align=\"center\">"+diskTotal+"</td>"
                    +"<td align=\"center\">"+channelChenk+"</td>"
                    +"<td align=\"center\">"+remark+"</td>"
                    +"<td class=\"no-print\"><button type=\"button\" class=\"btn btn-xs btn-default\" onclick=DCF_Modify(this);>修改</button></td>"
                    +"</tr>";
                var $opt = $(tr);//将字符串转换成jQuery对象
                $("#checklist").append($opt);

                dcfComp(dvR_Online, indx)
            }
        },
        error: function (erroMsg) {
            var obj= JSON.parse(erroMsg.responseText);
            var cont = obj.error.message;
            $.dialog({
                title: '筛选失败!',
                content: cont,
            });
        },
        complete: function () {
            $("#DcfSelect").attr("style","display:none");
        }
    });

}