var treeB = [];
var treeF = [];

var setting = {
    isSimpleData : true, //数据是否采用简单 Array 格式，默认false
    treeNodeKey : "id", //在isSimpleData格式下，当前节点id属性
    treeNodeParentKey : "pId", //在isSimpleData格式下，当前节点的父节点id属性
    showLine : true, //是否显示节点间的连线
    callback :{
        onClick : function(event, treeId, treeNode) {

            treeB = [];
            treeF = [];

            var treeBuild = treeNode.name.split('-').shift();
            var treeFloor = treeNode.name.split('-').pop();
            // camera_tree(build,floor);

            if (treeFloor.match(/[监控室]$/)){
                // floor = floor.replace(/[监控室]*/g,"");
                // console.log(floor);
                treeFloor="";
            };

            treeB.push(treeBuild);
            treeF.push(treeFloor);

            camera_tree();


        }
    },
//checkable : true //每个节点上是否显示 CheckBox
};



var zNodes =[
    {name:"IDPBG智能监控", open:true,
        children: [
            //父节点1
            {
                name:"GL监控清单", open:true,
                children: [
                    //父节点11
                    {
                        name:"B10-1.5F监控室",
                        children: [
                            //父节点111
                            {
                                name:"B10-2F",
                            },
                            {
                                name:"B10-3F",
                            },
                            {
                                name:"B10-4F",
                            },
                            {
                                name:"B10-5F",
                            },
                            {
                                name:"B10-6F",
                            },
                            {
                                name:"B10-1.5F",
                            },
                            {
                                name:"B10-1F",
                            },
                            {
                                name:"B25-4F",
                            },
                        ]
                    },
                    {
                        name:"B11-4F监控室",
                        children: [
                            //父节点111
                            {
                                name:"B11-1.5F",
                            },
                            {
                                name:"B11-2F",
                            },
                            {
                                name:"B11-3F",
                            },
                            {
                                name:"B11-4F",
                            },
                            {
                                name:"B11-5F",
                            },
                            {
                                name:"B12-5F",
                            },
                        ]
                    },
                    {
                        name:"B06-2F监控室",
                        children: [
                            //父节点111
                            {
                                name:"B05-1.5F",
                            },
                            {
                                name:"B05-4F",
                            },
                            {
                                name:"B05-6F",
                            },
                            {
                                name:"B06-2F",
                            },
                            {
                                name:"B06-3F",
                            },
                            {
                                name:"B06-4F",
                            },
                            {
                                name:"B21-2F",
                            },
                            {
                                name:"B21-3F",
                            },
                            {
                                name:"B21-4F",
                            },
                        ]
                    },
                    {
                        name:"B08-4F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"B07-2F",
                            },
                            {
                                name:"B07-4F",
                            },
                            {
                                name:"B08-4F",
                            },
                            {
                                name:"B08-3F",
                            },
                            {
                                name:"B08-2F",
                            },
                        ]
                    },
                    {
                        name:"B23-2F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"B23-2F",
                            },
                            {
                                name:"B23-3F",
                            },
                        ]

                    },
                    {
                        name:"C32-2F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"C21-1F",
                            },
                            {
                                name:"C32-1F",
                            },
                            {
                                name:"C32-4F",
                            },
                            {
                                name:"C33-3F",
                            },
                            {
                                name:"C33-4F",
                            },
                        ]
                    },
                    {
                        name:"C01-2F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"C01-1F",
                            },
                            {
                                name:"C01-1.5F",
                            },
                            {
                                name:"C01-2F",
                            },
                            {
                                name:"C01-3F",
                            },
                            {
                                name:"C01-4F"
                            },
                        ]
                    },
                    {
                        name:"C41-4F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"C41-2F",
                            },
                            {
                                name:"C41-3F",
                            },
                            {
                                name:"C41-4F",
                            },
                        ]
                    },
                    {
                        name:"C03-3F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"C03-1F",
                            },
                            {
                                name:"C03-1.5F",
                            },
                            {
                                name:"C03-2F",
                            },
                            {
                                name:"C04-1F",
                            },
                            {
                                name:"C02-1.5F",
                            },
                            {
                                name:"C02-1F",
                            },
                            {
                                name:"C03-3F",
                            },
                            {
                                name:"C04-2F",
                            },
                        ]
                    },
                    {
                        name:"C07-3F监控室",
                    },
                    {
                        name:"B07-1F监控室",
                    },
                    {
                        name:"C06-3F监控室",
                        children: [
                            //楼层节点
                            {
                                name:"C06-3F",
                            },
                            {
                                name:"C09-1F",
                            },
                            {
                                name:"C09-2F",
                            },
                            {
                                name:"C09-5F",
                            },
                        ]
                    },
                    {
                        name:"A03-3F",
                    },
                    // {
                    //     name:"A03-4F",
                    // },
                    {
                        name:"A4-4F",
                    },
                    {
                        name:"A2-4F监控室",
                    },
                    {
                        name:"C01-5F监控室",
                    },
                ]
            },
            //父节点2
            {
                name:"LH监控清单", open:true,
                children: [
                    //父节点21
                    {
                        name:"G10-1.5F监控室",
                        children: [
                            //父节点111
                            {
                                name:"G10-1.5F",
                            },
                            {
                                name:"G10-2F",
                            },
                            {
                                name:"G10-3F",
                            },
                            {
                                name:"G10-4F",
                            },
                            {
                                name:"G11-1F",
                            },
                            {
                                name:"G11-4F",
                            },
                            {
                                name:"G11-5F",
                            },
                            {
                                name:"G11-3F",
                            },
                        ]
                    },
                    {
                        name:"G14-6F监控室",
                        children: [
                            {
                                name: "G13-1F",
                            },
                            {
                                name: "G13-5F",
                            },
                            {
                                name: "G13-6F",
                            },
                            {
                                name: "G14-2F",
                            },
                            {
                                name: "G14-6F",
                            },
                            {
                                name: "G14-1F",
                            },
                        ]
                    },
                    {
                        name:"E10-4F监控室",
                        children: [
                            {
                                name: "E10-4F",
                            }
                        ]
                    },
                    {
                        name:"F18-3F监控室",
                        children: [
                            {
                                name: "F18-3F",
                            },
                            {
                                name: "F18-2.75F",
                            },
                        ]
                    },
                    {
                        name:"G17监控室",
                        children: [
                            {
                                name: "G17-3",
                            },
                            {
                                name: "G17-2",
                            },
                            {
                                name: "G17-1",
                            },
                        ]
                    },
                ]
            },
        ]
    }
];

$(document).ready(function () {
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
});


// function camera_tree(build, floor) {
//     console.log("执行camera_tree方法");
//     $("#camDvr_id_select").val("");
//     $("#cam_id_select").val("");
//     $("#build_select").val("");
//     $("#floor_select").val("");
//     $("#location_select").val("");
//
//     $("#pagination_module").attr("style","display:none");
//     $("#select_pagination_module").attr("style","display:none");
//     $("#tree_pagination_module").removeAttr("style","display:none");
//     // $("#tree_index").val(0);
//     //解析并显示数据
//     tree_queryForPages(build,floor);
//     //加载总页数
//     tree_getAllpage(build,floor);
//     // 构建分页
//     tree_build_page_info();
//
// };

// var tree_pageSize = 20;//每页行数
var tree_pageIndex = 1;//当前页
var tree_totalPageCount = 0;//总页数

//首页
function tree_firstPage() {
    var build = treeB[0];
    var floor = treeF[0];
    $("#tree_index").text(1);
    tree_pageIndex = 1;
    var first_url = 'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount=0&MaxResultCount=20&Sorting=id';
    tree_queryForPages(first_url)
}
//上一页
function tree_previous() {
    var build = treeB[0];
    var floor = treeF[0];
    if (tree_pageIndex > 1) {
        tree_pageIndex--;
    }
    $("#tree_index").text(tree_pageIndex);
    var skip = tree_pageIndex*20;
    var previous_url = 'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount='+skip+'&MaxResultCount=20&Sorting=id';
    tree_queryForPages(previous_url)
}
//下一页
function tree_next() {
    var build = treeB[0];
    var floor = treeF[0];
    if (tree_pageIndex <= tree_totalPageCount) {
        tree_pageIndex++;
    }
    $("#tree_index").text(tree_pageIndex);
    var skip = tree_pageIndex*20;
    var next_url = 'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount='+skip+'&MaxResultCount=20&Sorting=id';
    tree_queryForPages(next_url)
}
//尾页
function tree_last() {
    var build = treeB[0];
    var floor = treeF[0];
    var skip = tree_totalPageCount*20;
    tree_pageIndex = tree_totalPageCount;
    $("#tree_index").text(tree_totalPageCount);
    var last_url = 'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount='+skip+'&MaxResultCount=20&Sorting=id';
    tree_queryForPages(last_url)
}


function camera_tree() {
    // location.reload();
    console.log("执行camera_tree方法");
    $("#camDvr_id_select").val("");
    $("#cam_id_select").val("");
    $("#build_select").val("");
    $("#floor_select").val("");
    $("#location_select").val("");

    $("#pagination_module").attr("style","display:none");
    $("#select_pagination_module").attr("style","display:none");
    $("#tree_pagination_module").removeAttr("style","display:none");
    // $("#tree_index").val(0);

    // //解析并显示数据
    // tree_queryForPages();
    //加载总页数
    tree_getAllpage();
    // // 构建分页
    // tree_build_page_info();
    tree_firstPage();

}

function tree_queryForPages(url) {

    // var build = treeB[0];
    // var floor = treeF[0];
    //
    // console.log(tree_pageSize);
    // console.log(tree_pageIndex);
    // console.log(tree_totalPageCount);
    // console.log("***********");
    // console.log(build+" : "+floor);
    // console.log("------------------------------------")
    //
    // var skip = tree_pageIndex*tree_pageSize;

    $.ajax({
        // url:host+'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount='+skip+'&MaxResultCount='+tree_pageSize+'&Sorting=id',
        url:host+url,
        type:'get',
        success: function (data) {

            //清空数据
            tree_clearDate();
            //查询数据
            tree_fillTable(data);
        }
    });
};


function getMyDate(str){
    var oDate = new Date(str),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth()+1,
        oDay = oDate.getDate(),
        // oHour = oDate.getHours(),
        // oMin = oDate.getMinutes(),
        // oSen = oDate.getSeconds(),
        // +' '+ getzf(oHour) +':'+ getzf(oMin) +':'+getzf(oSen)
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

//填充数据
function tree_fillTable(data) {
    // var trs = "";//不初始化字符串"",会默认"undefined"
    var obj = data.items;
    // 解析数据，生成表格
    for (var i = 0; i < obj.length; i++) {
        // totalCount++;
        var t_id = obj[i].id;
        var index = i+1;
        var mor = !obj[i].monitoring_room?"":obj[i].monitoring_room;

        var id = !obj[i].dvR_ID?"":obj[i].dvR_ID;
        var channel = !obj[i].channel_ID?"":obj[i].channel_ID;
        var cam_id = !obj[i].camera_ID?"":obj[i].camera_ID;
        var build = !obj[i].build?"":obj[i].build;
        var floor = !obj[i].floor?"":obj[i].floor;
        var direct = !obj[i].direction?"":obj[i].direction;
        var loc = !obj[i].location?"":obj[i].location;
        var cam_type = !obj[i].camera_Tpye?"":obj[i].camera_Tpye;
        var dep = !obj[i].department?"":obj[i].department;
        var cost = !obj[i].cost_code?"":obj[i].cost_code;
        var ins_time = !obj[i].install_time?"":obj[i].install_time;
        var manu = !obj[i].manufacturer?"":obj[i].manufacturer;

        var catg = obj[i].category;
        // var alm = !obj[i].alarm_ID?"":obj[i].alarm_ID;

        var rmk = !obj[i].remark?"":obj[i].remark;
        var modtime = getMyDate(obj[i].lastModificationTime);

        console.log(obj[i].remark);
        //此处勿动，使用其它方式会使checkbox checkall失效！
        var s_opt = '<tr id="'+t_id+'"><td></td><td class="no-print"><input type="checkbox" id="check2" class="cb"></td><td align="center">'+index+'</td><td align="center">'+mor+'</td><td align="center">'+id+'</td><td align="center">'+channel+'</td><td class="print_wrap" align="center">'+cam_id+'</td><td align="center">'+build+'</td><td align="center">'+floor+'</td><td align="center">'+direct+'</td><td align="center">'+loc+'</td><td align="center">'+cam_type+'</td><td align="center">'+dep+'</td><td align="center">'+cost+'</td><td align="center">'+ins_time+'</td><td align="center">'+catg+'</td><td align="center">'+rmk+'</td><td class="no-print" align="center">'+modtime+'</td><td class="no-print"><button type="button" class="btn btn-xs btn-info" onclick="showVideo(this)">查看</button></td><td class="no-print"><button id="camModify" type="button" class="btn btn-xs btn-default" onclick="cam_Modify(this)">修改</button></td></tr>';

        var $opt = $(s_opt);//将字符串转换成jQuery对象
        $("#checklist").append($opt);
        // $("#checklist").html(trs);
    }
    // tree_totalPageCount = (totalCount % pageSize == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
};

//清空数据
function tree_clearDate() {
    $("#checklist").html("");
};

//显示分页
// function tree_build_page_info(){
//     tree_pageIndex = 1;
//     $("#tree_index").val(tree_pageIndex);
//     // $("#tree_index").val();
//     //首页
//     $("#tree_firstPage").click(function () {
//         //var index=$("#index").val();
//         tree_pageIndex = "1";
//         $("#tree_index").val(tree_pageIndex);
//         tree_queryForPages();
//     });
// //上一页
//     $("#tree_previous").click(function () {
//         if (tree_pageIndex > 1) {
//             tree_pageIndex--;
//         }
//         $("#tree_index").val(tree_pageIndex);
//         tree_queryForPages();
//     });  //下一页
//     $("#tree_next").click(function () {
//         if (tree_pageIndex < tree_totalPageCount) {
//             tree_pageIndex++;
//         }
//         // if (tree_pageIndex%2 == 0){
//             $("#tree_index").val(tree_pageIndex);
//         // }else {
//         //     var treeChange = tree_pageIndex-1;
//         //     $("#tree_index").val(treeChange);
//         // }
//
//         tree_queryForPages();
//     });
// };

//总页数
function tree_getAllpage() {

    var build = treeB[0];
    var floor = treeF[0];

    // if (floor.match(/[监控室]$/)){
    //     // floor = floor.replace(/[监控室]*/g,"");
    //     // console.log(floor);
    //     floor="";
    // };
    // console.log(build+" : "+floor);

    var tree_skip = 0;
    var tree_pageSize = 20000;
    $.ajax({
        url:host+'app/camera/byCondition?Build='+build+'&Floor='+floor+'&SkipCount='+tree_skip+'&MaxResultCount='+tree_pageSize+'&Sorting=id',
        type: 'get',
        success: function (result) {
            console.log(result);
            var tree_allPageNum = result.totalCount;
            // totalPageCount = (allPageNum % pageSize == 0) ? (allPageNum / pageSize) : (allPageNum / pageSize + 1);
            var totalPage = Math.floor(tree_allPageNum/20);
            // tree_totalPageCount = 100;
            tree_totalPageCount = totalPage;
            // tree_totalPageCount = (tree_allPageNum % 20 === 0) ? (tree_allPageNum / 20) : (tree_allPageNum / 20 + 1);
            //尾页
            // $("#tree_last").click(function () {
            //     tree_pageIndex = totalPage;
            //     $("#tree_index").val(tree_pageIndex);
            //     tree_queryForPages();
            // });
            //总页数
            $("#tree_pageCount").text("总共"+totalPage+"页");
        }
    });
}




