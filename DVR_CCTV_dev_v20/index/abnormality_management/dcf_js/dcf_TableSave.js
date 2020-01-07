$(function () {
    $("#dcf_save_btn").click(saveTables);
});

function saveTables() {
    var dvr_ch = $("#DVR_CH_NUM_add_input").val();

    var dvr_on = $("#DVR_ON_ST_add_input option:selected");
    if (dvr_on == "true"){
        dvr_on = true;
    }else if (dvr_on == "false"){
        dvr_on = false;
    }else {
        dvr_on = true;
    }

    var info_chk = $("#abnormal_info_add_input option:selected");
    if (info_chk == "true"){
        info_chk = true;
    }else if (info_chk == "false"){
        info_chk = false;
    }else {
        info_chk = true;
    }

    var hd_al = $("#HD_AL_add_input").val();

    var chan_chk = $("#CH_INFO_AN_add_input option:selected");
    if (chan_chk == "true"){
        chan_chk = true;
    }else if (chan_chk == "false"){
        chan_chk = false;
    }else {
        chan_chk = true;
    }

    var post_data = {
        "dvR_ID": $("#DVR_ID_add_input").val().toUpperCase(),
        "dvR_SN": $("#DVR_SN_add_input").val(),
        "dvR_type": $("#DVR_TYPE_add_input").val(),
        "dvR_Channel": parseInt(dvr_ch),
        "dvrdisk": $("#HD_INFO_add_input").val(),
        "dvrChannelInfo": $("#DVR_CH_LOC_INFO_add_input").val(),
        "libraryChannelInfo": $("#Library_CH_LOC_add_input").val(),
        "dvR_Online": Boolean(dvr_on),
        "infoChenk": Boolean(info_chk),
        "diskTotal": parseInt(hd_al),
        "channelChenk": Boolean(chan_chk),
        "remark": $("#remark_add_input").val()
    };

    // 1、发送ajax请求保存
    $.ajax({
        url:host+"app/dVRCheckInfo",
        type:"POST",
        dataType : 'json',
        contentType : 'application/json',
        data : JSON.stringify(post_data),
        success:function(result){
            console.log(result.status);
            //1、关闭模态框
            $("#DcfAddModal").modal('hide');

            $.dialog({
                title: '保存成功!',
                content: '',
            });
            setTimeout(function(){location.reload();},500);
        },
        error: function (erroMsg) {
            var obj= JSON.parse(erroMsg.responseText);
            var cont = obj.error.message;
            $.dialog({
                title: '保存失败!',
                content: cont,
            });
        }
    });

}