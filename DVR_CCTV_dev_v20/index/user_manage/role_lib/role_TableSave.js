$(function () {
    $("#role_save_btn").click(save_role);
});

function save_role() {
    var ro_n = $("#role_addName").val();

    var post_data={
        "name": ro_n,
        "isDefault": true,
        "isPublic": true
    };

    $.ajax({
        url: host+'identity/roles',
        type: 'post',
        contentType : 'application/json',
        data: JSON.stringify(post_data),
        success: function () {
            $("#roleAddModal").modal('hide');

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
