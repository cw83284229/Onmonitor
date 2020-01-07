//全选
$(function(){

    /*全选按钮状态显示判断*/

    $("#checklist").find("input[type='checkbox']").click(function(){

        /*初始化选择为TURE*/

        $("#checkall")[0].checked = true;

        /*获取未选中的*/

        var nocheckedList = new Array();

        $("#checklist").find('input:not(:checked)').each(function() {

            nocheckedList.push($(this).val());

        });

        /*状态显示*/

        if (nocheckedList.length == $("#checklist").find('input').length) {

            $("#checkall")[0].checked = false;

            $("#checkall")[0].indeterminate = false;

        } else if (nocheckedList.length) {

            $("#checkall")[0].indeterminate = true;

        } else {

            $("#checkall")[0].indeterminate = false;

        }

    });

    // 全选/取消

    $("#checkall").click(function(){

        // alert(this.checked);

        if ($(this).is(':checked')) {

            $("#checklist").find('input').each(function(){

                $(this).prop("checked",true);

            });

        } else {

            $("#checklist").find('input').each(function(){

                $(this).removeAttr("checked",false);

                $(this).prop("checked",false); // 根据官方的建议：具有 true 和 false 两个属性的属性，如 checked, selected 或者 disabled 使用prop()，其他的使用 attr()

            });

        }

    });

});