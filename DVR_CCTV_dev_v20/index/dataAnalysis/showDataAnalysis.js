
// 基于准备好的dom，初始化echarts实例
var DVRTotal = echarts.init(document.getElementById('DVRTotalAbout'));
var CamTotal = echarts.init(document.getElementById('CamTotalAbout'));
var AbNormal = echarts.init(document.getElementById('AbNormalAbout'));
var YearTotal = echarts.init(document.getElementById('YearTotalAbout'));

// 指定图表的配置项和数据
DVRTotal.setOption({
    title: {
        text: '主机概况'
    },
    tooltip: {},
    legend: {
        data:['DVR主机总数(台)']
    },
    xAxis:[
        {
            type: 'category',
            boundaryGap: false,
            // data: ["B10监控室", "B11监控室", "B06监控室", "B08监控室", "B23监控室", "C32监控室", "C01监控室", "C41监控室", "C03监控室", "C07监控室", "B07监控室", "C06监控室", "A03监控室", "A2监控室", "C01监控室"]
            data:[],
            axisLabel: {
                interval:0,
                rotate:40
            },
            grid: {
                left: '10%',
                bottom:'35%'
            },
        }
    ],
    yAxis: {
        type: 'value',
        show: false
    },
    series: [
        {
            name: 'DVR主机总数(台)',
            type: 'bar',
            // data: [27, 68, 49, 40, 56, 35, 63, 26, 19, 39, 22, 15, 78, 47, 35],
            data: [],
            itemStyle: {
                normal: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0, color: '#3168DD' // 0% 处的颜色
                        }, {
                            offset: 1, color: '#D246DD' // 100% 处的颜色
                        }],
                        global: false // 缺省为 false
                    },
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            }
        }
    ]
});

CamTotal.setOption({
    title: {
        text: '镜头概况'
    },
    tooltip: {},
    legend: {
        data:['镜头总数(路)','镜头异常总数(路)']
    },
    xAxis: [
        {
            boundaryGap: false,
            data: [],
            axisLabel: {
                interval:0,
                rotate:40
            },
            grid: {
                left: '10%',
                bottom:'35%'
            },
        }
    ],
    yAxis: [
        {
            type: 'value',
            show: false
        },
    ],
    series: [
        {
            name: '镜头总数(路)',
            type: 'bar',
            data: [627, 268, 149, 440, 956, 335,463,326,919,539,222,715,378,747,535],
            // color: '#5BC9DD'
            itemStyle: {
                normal: {
                    // color: '#5BC9DD',
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0, color: '#26AFDD' // 0% 处的颜色
                        }, {
                            offset: 1, color: '#B3A7DD' // 100% 处的颜色
                        }],
                        global: false // 缺省为 false
                    },
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            }
        },
        {
            name: '镜头异常总数(路)',
            type: 'bar',
            data: [13,41,51,55,74,93,28,51,14,27,33,46,61,38,94],
            // color: '#DD605D'
            itemStyle: {
                normal: {
                    color: '#DD605D',
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            }
        }
    ]
});

var x = [];
var y = [];
var camX = [];
var camY = [];
var camZ = [];
$.ajax({
    type:'get',
    url:host+'app/reportForms/dVRCameraTotal',
    dataType:'JSON',
    beforeSend:function(){
        DVRTotal.showLoading();
        CamTotal.showLoading();
    },
    success:function(data){
        for (var i=0;i<data.length;i++){
            x.push(data[i].dvrRoom);
            y.push(data[i].dvrTotal);
            camX.push(data[i].dvrRoom);
            camY.push(data[i].cameraTotal);
            camZ.push(data[i].cameraAnomaly)
        };

        x.pop();
        y.pop();
        camX.pop();
        camY.pop();
        camZ.pop();

        DVRTotal.hideLoading();
        CamTotal.hideLoading();

        DVRTotal.setOption({
            xAxis:{
                data:x
            },
            series:{
                data:y
            }
        });

        CamTotal.setOption({
            xAxis:{
                data:camX
            },
            series:[
                {data:camY},
                {data:camZ},
            ]
        });

    }
});

AbNormal.setOption({
    title: {
        text: '镜头/镜头维修统计'
    },
    tooltip: {},
    legend: {
        data:['镜头总数(路)','镜头维修总数(路)']
    },
    xAxis: [
        {
            boundaryGap: false,
            data: [],
            axisLabel: {
                interval:0,
                rotate:40
            },
            grid: {
                left: '10%',
                bottom:'35%'
            },
        }
    ],
    yAxis: [
        {
            type: 'value',
            show: false
        },
    ],
    series: [
        {
            name: '镜头总数(路)',
            type: 'bar',
            data: [],
            // color: '#5BC9DD'
            itemStyle: {
                normal: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0, color: '#FA0DFF' // 0% 处的颜色
                        }, {
                            offset: 1, color: '#07F9FF' // 100% 处的颜色
                        }],
                        global: false // 缺省为 false
                    },
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            }
        },
        {
            name: '镜头维修总数(路)',
            type: 'bar',
            data: [],
            itemStyle: {
                normal: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0, color: '#00FD01' // 0% 处的颜色
                        }, {
                            offset: 1, color: '#FEFF00' // 100% 处的颜色
                        }],
                        global: false // 缺省为 false
                    },
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            }
        }
    ]
});

var AbNormalX = [];
var AbNormal_camY = [];
var AbNormal_camRepY = [];
$.ajax({
    url: host+'Camera/CameraByBuild',
    type: 'get',
    dataType: 'json',
    beforeSend:function(){
        AbNormal.showLoading();
    },
    success: function (data) {
        for (var i in data){
            AbNormalX.push(i);
            AbNormal_camY.push(data[i]);
        }
        getAbnormal_camRep()
    }
});
function getAbnormal_camRep(){

    $.ajax({
        url: host+'CameraRepair/CameraRepairByBuild',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            for (var i in data){
                AbNormal_camRepY.push(data[i]);
            }
            AbNormal.hideLoading();

            AbNormal.setOption({
                xAxis:{
                    data:AbNormalX
                },
                series:[
                    {data:AbNormal_camY},
                    {data:AbNormal_camRepY},
                ]
            });

        }
    })

}


YearTotal.setOption({
    // backgroundColor: 'green',

    title: {
        text: '年份-镜头/镜头维修总数'
    },
    tooltip: {},
    legend: {
        data:['每年镜头总数(路)','每年镜头维修总数(路)']
    },
    xAxis: {
        data: []
    },
    yAxis: {},
    series: [
        {
            name: '每年镜头总数(路)',
            type: 'line',
            smooth: true,
            data: [],
            // color: '#DD9418'
            itemStyle: {
                normal: {
                    color: '#DD9418',
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            },
        },
        {
            name: '每年镜头维修总数(路)',
            type: 'line',
            // smooth: true,
            data: [],
            // color: '#FF2AB2'
            itemStyle: {
                normal: {
                    color: '#FF2AB2',
                    label: {
                        show: true,
                        position: 'top',
                        formatter: '{c}'　　　　//这是关键，在需要的地方加上就行了
                    }
                }
            },
        },
    ]
});
var YearTotalX = [];
var camYearTotalY = [];
var camRepYearTotalY = [];
$.ajax({
    url: host+'Camera/VintageAnalysis',
    type: 'get',
    dataType: 'json',
    beforeSend:function(){
        YearTotal.showLoading();
    },
    success: function (data) {
        for (var i in data){
            YearTotalX.push(i);
            camYearTotalY.push(data[i]);
        }
        getCamRepYear();
    }
});

function getCamRepYear(){

    $.ajax({
        url: host+'CameraRepair/VintageAnalysis',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            for (var i in data){
                // YearTotalX.push(i);
                camRepYearTotalY.push(data[i])
            }
            YearTotal.hideLoading();

            YearTotal.setOption({
                xAxis:{
                    data:YearTotalX
                },
                series:[
                    {data:camYearTotalY},
                    {data:camRepYearTotalY},
                ]
            });
        }
    })

}



// 使用刚指定的配置项和数据显示图表。
// AbNormal.setOption(AbNormal_option);
// YearTotal.setOption(YearTotal_option);

