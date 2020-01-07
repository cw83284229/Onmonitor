var dcf_pageSize = "20";//每页行数
var dcf_pageIndex = "1";//当前页
var dcf_totalPageCount = "0";//总页数
var dcf_totalCount = "0";//总记录数

$(function(){
    //解析并显示数据
    queryForPages();
    //加载总页数
    getAllpage();
    //构建分页
    build_page_info();

});

function queryForPages() {
    var skip = dcf_pageIndex*dcf_pageSize-dcf_pageSize;
    // var skip = 0;
    $.ajax({
        url: host+'app/dVRCheckInfo?Sorting=id&SkipCount='+skip+'&MaxResultCount='+dcf_pageSize,
        type: 'get',
        success: function (data) {
            console.log(data);
            //清空数据
            clearDate();
            //查询数据
            fillTable(data);
        },
        complete:function(xhr, textStatus){
            if (xhr.status == 401){
                $.dialog({
                    title: '登录过期，请重新登录!',
                    content: '',
                });
                setTimeout(function(){window.location.href="../../index.html";},1500);
            }
        }
    });
};

function dcfComp(dvon, indx) {
    var dvof = "dv_on"+indx;

    if (dvon == "掉线"){
        $("#"+dvof).css("color","red");
    }else if (dvon == "在线"){
        $("#"+dvof).css("color","#00FF00")
    }
}

//填充数据
function fillTable(data) {
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
    // dcf_totalPageCount = (dcf_totalCount % dcf_pageSize == 0) ? (dcf_totalCount / dcf_pageSize) : (dcf_totalCount / dcf_pageSize + 1);
    // console.log(dcf_totalPageCount);
}

//清空数据
function clearDate() {
    $("#checklist").html("");
};

//显示分页
function build_page_info(){
    $("#index").val(dcf_pageIndex);

    //首页
    $("#firstPage").click(function () {
        //var index=$("#index").val();
        dcf_pageIndex = "1";
        $("#index").val(dcf_pageIndex);
        queryForPages();
    });
//上一页
    $("#previous").click(function () {
        if (dcf_pageIndex > 1) {
            dcf_pageIndex--;
        }
        $("#index").val(dcf_pageIndex);
        queryForPages();
    });  //下一页
    $("#next").click(function () {
        if (dcf_pageIndex <= dcf_totalPageCount) {
            dcf_pageIndex++;
        }
        $("#index").val(dcf_pageIndex);
        queryForPages();
    });
};

//总页数
function getAllpage() {
    $.ajax({
        url:host+'app/dVRCheckInfo',
        data:'',
        type: 'get',
        success: function (result) {
            var allPageNum = result.totalCount;
            // var totalPage = Math.floor(allPageNum/dcf_pageSize);
            var totalPage = Math.floor((allPageNum % 20 === 0) ? (allPageNum / 20) : (allPageNum / 20 + 1));
            dcf_totalPageCount = totalPage;
            //尾页
            $("#last").click(function () {
                dcf_pageIndex = totalPage;
                $("#index").val(dcf_pageIndex);
                queryForPages();
            });
            //总页数
            $("#pageCount").text("总共"+totalPage+"页");
        }
    });
}



