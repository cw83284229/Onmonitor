$(function () {
    //设定权限
    $("#role_permission_set").click(rps_comfirm);
    //取消权限
    $("#cancelRole_permission").click(rpc_comfirm);
});

//设定权限
function rps_comfirm() {
    var roleName_set = arr_tmp[0];
    var role_permission_set = $("#role_permission_name option:selected").val();

    $.ajax({
        url: host+'app/urseJurisdiction/gramtPermissionForRole?roleName='+roleName_set+'&permissionName='+role_permission_set,
        type: 'post',
        contentType: 'application/json',
        data: '',
        success: function () {
            $("#setRoleModal").modal('hide');
            var d = $.dialog({
                title: '设定成功!',
                content: '',
            });
            setTimeout(function () {
                d.close().remove();
            }, 500);
        },
        error: function () {
            $.dialog({
                title: '设定失败!',
                content: '',
            });
        },
        complete: function () {
            arr_tmp.pop();
        }
    })

}

//取消权限
function rpc_comfirm() {
    var roleName_cancel = arr_tmp[0];
    var role_permission_cancel = $("#permission_cancel option:selected").val();

    $.ajax({
        url: host+'app/urseJurisdiction/prohibitPermissionForRole?roleName='+roleName_cancel+'&permissionName='+role_permission_cancel,
        type: 'post',
        contentType: 'application/json',
        data: '',
        success: function () {
            $("#cancelUserModal").modal('hide');
            var d = $.dialog({
                title: '取消成功!',
                content: '',
            });
            setTimeout(function () {
                d.close().remove();
            }, 500);
        },
        error: function () {
            $.dialog({
                title: '取消失败!',
                content: '',
            });
        },
        complete: function () {
            arr_tmp.pop();
        }
    })
}


