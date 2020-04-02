$(function () {

    var l = abp.localization.getResource('OnMonitor');

    var service = onMonitor.menusInfos.systemMenu;
    var createModal = new abp.ModalManager(abp.appPath + 'MenusInfos/SystemMenu/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'MenusInfos/SystemMenu/EditModal');

    var dataTable = $('#SystemMenuTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('SystemMenuDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "pid" },
            { data: "title" },
            { data: "icon" },
            { data: "href" },
            { data: "target" },
            { data: "sort" },
            { data: "status" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewSystemMenuButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});