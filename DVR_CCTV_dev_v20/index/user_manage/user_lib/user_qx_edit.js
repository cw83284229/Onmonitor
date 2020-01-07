$(function () {
    //设定权限
    $("#user_permission_set").click(rps_user);
    //取消权限
    $("#user_permission_cancel").click(rpc_user);
});

//设定权限
function rps_user(id) {
    var userID_addSet = arr_tmp[0];
    var user_permission_set = $("#user_permission_name_set option:selected").val();

    $.ajax({
        url: host+'app/urseJurisdiction/gramtPermissionForUser/'+userID_addSet+'?permissionName='+user_permission_set,
        type: 'post',
        contentType: 'application/json',
        data: '',
        success: function () {
            $("#setUserModal").modal('hide');
            var d = $.dialog({
                overlay: true,
                shadow: true,
                flat: true,
                draggable: true,
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
function rpc_user() {
    var userID_cancelSet = arr_tmp[0];
    var permission_cancel = $("#permission_cancel option:selected").val();

    $.ajax({
        url: host+'app/urseJurisdiction/prohibitPermissionForUser/'+userID_cancelSet+'?permissionName='+permission_cancel,
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

//添加角色
function role_add_save() {
    Array.prototype.indexOf = function(val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };
    Array.prototype.remove = function(val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.splice(index, 1);
        }
    };

    var roleName = [];

    var userId = arr_tmp[0];
    var roleName1 = $("#role_ID_addInput_1").val();
    var roleName2 = $("#role_ID_addInput_2").val();
    var roleName3 = $("#role_ID_addInput_3").val();
    var roleName4 = $("#role_ID_addInput_4").val();

    roleName.push(roleName1);
    roleName.push(roleName2);
    roleName.push(roleName3);
    roleName.push(roleName4);

    if (roleName1 == ''){
        roleName.remove(roleName1);
    }
    if (roleName2 == ''){
        roleName.remove(roleName2);
    }
    if (roleName3 == ''){
        roleName.remove(roleName3);
    }
    if (roleName4 == ''){
        roleName.remove(roleName4);
    }

    var put_ur_data = {
        "roleNames": roleName
    };

    $.ajax({
        url: host+'identity/users/'+userId+'/roles',
        type: 'put',
        contentType: 'application/json',
        data: JSON.stringify(put_ur_data),
        success: function () {
            $("#roleAddModal").modal('hide');
            var d = $.dialog({
                title: '角色添加成功!',
                content: '',
            });
            setTimeout(function () {
                d.close().remove();
            }, 500);
        },
        error: function () {
            $.dialog({
                title: '角色添加失败!',
                content: '',
            });
        },
        complete: function () {
            arr_tmp.pop();
        }
    })

}

