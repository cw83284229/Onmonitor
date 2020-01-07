var setting = {
    isSimpleData : true, //数据是否采用简单 Array 格式，默认false
    treeNodeKey : "id", //在isSimpleData格式下，当前节点id属性
    treeNodeParentKey : "pId", //在isSimpleData格式下，当前节点的父节点id属性
    showLine : true, //是否显示节点间的连线
    callback :{
        onClick : function(event, treeId, treeNode) {
            //添加子节点到指定的父节点, 数据需用set去重?

            var url;
            var regex = /[监控室]$/g;
            var regey = /[-]/g;
            var monitoring_room;
            if (regex.test(treeNode.name)){
                monitoring_room = treeNode.name.split('监控室').shift();
                if (regey.test(monitoring_room)){
                    monitoring_room = monitoring_room.split('-').shift();
                }
                // url = host+'app/dVR/byCondition?Monitoring_room='+monitoring_room;
            }else{
                monitoring_room = treeNode.name;
                // url = host+'app/dVR/byCondition?Monitoring_room='+monitoring_room;
                // var build = treeNode.name.split('-').shift();
                // var floor = treeNode.name.split('-').pop();
                // url = host+'app/dVR/byCondition?Build='+build+'&Floor='+floor;
            }
            url = host+'app/dVR/byCondition?Monitoring_room='+monitoring_room;

            // var monitoring_room = treeNode.name.split('监控室').shift();
            // console.log(monitoring_room);
            // var monitoring_room = treeNode.name;
            // var build = monitoring_room.split('-').shift();
            // var floor = treeNode.name.split('-').pop();
            // var floor = monitoring_room.split('-').pop();
            // console.log("build: "+build+ "--------"+"floor: "+floor);
            // if (floor.match(/[监控室]$/)){
            //     // floor = floor.replace(/[监控室]*/g,"");
            //     // console.log(floor);
            //     build = "";
            //     floor = "";
            // };

            $.ajax({
                // url:host+'app/dVR/byCondition?Monitoring_room='+monitoring_room+'&Build='+build+'&Floor='+floor,
                url: url,
                type:'get',
                success: function (data) {
                    $("#checklist").html("");
                    $("#pagination_module").attr("style","display:none");
                    var obj = data.items;

                    for (var i in obj) {
                        // totalCount++;
                        var indx = parseInt(i) + 1;
                        var id = obj[i].id;
                        var fact = !obj[i].factory?"":obj[i].factory;
                        var moro = !obj[i].monitoring_room?"":obj[i].monitoring_room;
                        var d_id = !obj[i].dvR_ID?"":obj[i].dvR_ID;
                        var d_ip = !obj[i].dvR_IP?"":obj[i].dvR_IP;
                        var hd = !obj[i].hard_drive?"":obj[i].hard_drive;
                        var instime = !obj[i].install_time?"":obj[i].install_time;
                        var dt = !obj[i].dvR_type?"":obj[i].dvR_type;
                        var ds = !obj[i].dvR_SN?"":obj[i].dvR_SN;
                        var rk = !obj[i].remark?"":obj[i].remark;
                        var mt = getMyDate(obj[i].lastModificationTime);


                        var tr = "<tr id=" + id + ">"
                            + "<td class=\"no-print\"><input type=\"checkbox\" class=\"cb\"></td>"
                            + "<td align=\"center\">" + indx + "</td>"
                            + "<td align=\"center\">" + fact + "</td>"
                            + "<td align=\"center\">" + moro + "</td>"
                            + "<td align=\"center\">" + d_id + "</td>"
                            + "<td align=\"center\">" + d_ip + "</td>"
                            + "<td align=\"center\">" + hd + "</td>"
                            + "<td align=\"center\">" + instime + "</td>"
                            + "<td align=\"center\">" + dt + "</td>"
                            + "<td align=\"center\">" + ds + "</td>"
                            + "<td align=\"center\">" + rk + "</td>"
                            + "<td class=\"no-print\" align=\"center\">" + mt + "</td>"
                            + "<td class=\"no-print\"><button type=\"button\" class=\"showDvrStatus btn btn-xs btn-info\" onclick=dvr_showStatus(this);>查看状态</button></td>"
                            + "<td class=\"no-print\"><button type=\"button\" class=\"dvrModify btn btn-xs btn-default\" onclick=dvr_Modify(this);>修改</button></td>"
                            + "</tr>";
                        var $opt = $(tr);//将字符串转换成jQuery对象
                        $("#checklist").append($opt);
                    }
                }
            });

            // 判断是否父节点
            // if(!treeNode.isParent){
            //     //alert("treeId自动编号：" + treeNode.tId + ", 节点id是：" + treeNode.id + ", 节点文本是：" + treeNode.name);
            //     $.ajax({
            //         url: "<%=basePath%>department/testYbTree2.do",//请求的action路径
            //         data:{"pid":treeNode.id},
            //         error: function () {//请求失败处理函数
            //             alert('请求失败');
            //         },
            //         success:function(data)
            //         { //添加子节点到指定的父节点
            //             var jsondata= eval(data);
            //             if(jsondata == null || jsondata == ""){
            //                 //末节点的数据为空   所以不再添加节点  这里可以根据业务需求自己写
            //                 //$("#treeFrame").attr("src",treeNode.url);
            //             }
            //             else{
            //                 var treeObj = $.fn.zTree.getZTreeObj("demotree");
            //                 //treeNode.halfCheck = false;
            //                 var parentZNode = treeObj.getNodeByParam("id", treeNode.id, null);//获取指定父节点
            //                 newNode = treeObj.addNodes(parentZNode,jsondata, false);
            //             }
            //         }
            //     });
            // }
        }
    },
//checkable : true //每个节点上是否显示 CheckBox
};



var zNodes =[
    { name:"IDPBG智能监控", open:true,
        children: [
            //父节点1
            { name:"GL监控清单", open:true,
                children: [
                    //父节点11
                    {
                        name:"B10-1.5F监控室",
                        // children: [
                        //     //父节点111
                        //     {
                        //         name:"B10-2F",
                        //         // children:[
                        //         //     //父节点1111
                        //         //     {
                        //         //         name:"N08-1",
                        //         //         children:[
                        //         //             //镜头数据
                        //         //             {
                        //         //                 name:"B08-1",
                        //         //             },
                        //         //             {
                        //         //                 name:"B08-1/N2001 B10-2F北一门",
                        //         //             }
                        //         //         ]
                        //         //     }
                        //         // ]
                        //     },
                        //     {
                        //         name:"B10-3F",
                        //     },
                        //     {
                        //         name:"B10-4F",
                        //     },
                        //     {
                        //         name:"B10-5F",
                        //     },
                        //     {
                        //         name:"B10-6F",
                        //     },
                        //     {
                        //         name:"B10-1.5F",
                        //     },
                        //     {
                        //         name:"B10-1F",
                        //     },
                        //     {
                        //         name:"B25-4F",
                        //     },
                        // ]
                    },
                    {
                        name:"B11-4F监控室",
                        // children: [
                        //     //父节点111
                        //     {
                        //         name:"B11-1.5F",
                        //     },
                        //     {
                        //         name:"B11-2F",
                        //     },
                        //     {
                        //         name:"B11-3F",
                        //     },
                        //     {
                        //         name:"B11-4F",
                        //     },
                        //     {
                        //         name:"B11-5F",
                        //     },
                        //     {
                        //         name:"B12-5F",
                        //     },
                        // ]
                    },
                    {
                        name:"B06-2F监控室",
                        // children: [
                        //     //父节点111
                        //     {
                        //         name:"B05-1.5F",
                        //     },
                        //     {
                        //         name:"B05-4F",
                        //     },
                        //     {
                        //         name:"B05-6F",
                        //     },
                        //     {
                        //         name:"B06-2F",
                        //     },
                        //     {
                        //         name:"B06-3F",
                        //     },
                        //     {
                        //         name:"B06-4F",
                        //     },
                        //     {
                        //         name:"B21-2F",
                        //     },
                        //     {
                        //         name:"B21-3F",
                        //     },
                        //     {
                        //         name:"B21-4F",
                        //     },
                        // ]
                    },
                    {
                        name:"B08-4F监控室",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"B07-2F",
                        //     },
                        //     {
                        //         name:"B07-4F",
                        //     },
                        //     {
                        //         name:"B08-4F",
                        //     },
                        //     {
                        //         name:"B08-3F",
                        //     },
                        //     {
                        //         name:"B08-2F",
                        //     },
                        // ]
                    },
                    {
                        name:"B23-2F监控室",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"B23-2F",
                        //     },
                        //     {
                        //         name:"B23-3F",
                        //     },
                        // ]

                    },
                    {
                        name:"C32-2F监控室",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"C21-1F",
                        //     },
                        //     {
                        //         name:"C32-1F",
                        //     },
                        //     {
                        //         name:"C32-4F",
                        //     },
                        //     {
                        //         name:"C33-3F",
                        //     },
                        //     {
                        //         name:"C33-4F",
                        //     },
                        // ]
                    },
                    {
                        name:"C01-2F",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"C01-1F",
                        //     },
                        //     {
                        //         name:"C01-1.5F",
                        //     },
                        //     {
                        //         name:"C01-2F",
                        //     },
                        //     {
                        //         name:"C01-3F",
                        //     },
                        //     {
                        //         name:"C01-4F"
                        //     },
                        // ]
                    },
                    {
                        name:"C41-4F监控室",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"C41-2F",
                        //     },
                        //     {
                        //         name:"C41-3F",
                        //     },
                        //     {
                        //         name:"C41-4F",
                        //     },
                        // ]
                    },
                    {
                        name:"C03-3F",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"C03-1F",
                        //     },
                        //     {
                        //         name:"C03-1.5F",
                        //     },
                        //     {
                        //         name:"C03-2F",
                        //     },
                        //     {
                        //         name:"C04-1F",
                        //     },
                        //     {
                        //         name:"C02-1.5F",
                        //     },
                        //     {
                        //         name:"C02-1F",
                        //     },
                        //     {
                        //         name:"C03-3F",
                        //     },
                        //     {
                        //         name:"C04-2F",
                        //     },
                        // ]
                    },
                    {
                        name:"C03-4F",
                    },
                    {
                        name:"C07-3F监控室",
                    },
                    {
                        name:"B07-1F监控室",
                    },
                    {
                        name:"C06-3F监控室",
                        // children: [
                        //     //楼层节点
                        //     {
                        //         name:"C06-3F",
                        //     },
                        //     {
                        //         name:"C09-1F",
                        //     },
                        //     {
                        //         name:"C09-2F",
                        //     },
                        //     {
                        //         name:"C09-5F",
                        //     },
                        // ]
                    },
                    {
                        name:"A03-3F监控室",
                    },
                    // {
                    //     name:"A03-4F监控室",
                    // },
                    // {
                    //     name:"A4-4F监控室",
                    // },
                    {
                        name:"A02-4F监控室",
                    },
                    {
                        name:"C01-5F",
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
                        // children: [
                        //     //父节点111
                        //     {
                        //         name:"G10-1.5F",
                        //     },
                        //     {
                        //         name:"G10-2F",
                        //     },
                        //     {
                        //         name:"G10-3F",
                        //     },
                        //     {
                        //         name:"G10-4F",
                        //     },
                        //     {
                        //         name:"G11-1F",
                        //     },
                        //     {
                        //         name:"G11-4F",
                        //     },
                        //     {
                        //         name:"G11-5F",
                        //     },
                        //     {
                        //         name:"G11-3F",
                        //     },
                        // ]
                    },
                    {
                        name:"G14-6F监控室",
                        // children: [
                        //     {
                        //         name: "G13-1F",
                        //     },
                        //     {
                        //         name: "G13-5F",
                        //     },
                        //     {
                        //         name: "G13-6F",
                        //     },
                        //     {
                        //         name: "G14-2F",
                        //     },
                        //     {
                        //         name: "G14-6F",
                        //     },
                        //     {
                        //         name: "G14-1F",
                        //     },
                        // ]
                    },
                    {
                        name:"E10-4F监控室",
                        // children: [
                        //     {
                        //         name: "E10-4F",
                        //     }
                        // ]
                    },
                    {
                        name:"F18-3F监控室",
                        // children: [
                        //     {
                        //         name: "F18-3F",
                        //     },
                        //     {
                        //         name: "F18-2.75F",
                        //     },
                        // ]
                    },
                    {
                        name:"G17监控室",
                        // children: [
                        //     {
                        //         name: "G17-3",
                        //     },
                        //     {
                        //         name: "G17-2",
                        //     },
                        //     {
                        //         name: "G17-1",
                        //     },
                        // ]
                    },
                ]
            }
            //父节点3

        ]
    }
]

$(document).ready(function () {
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
});