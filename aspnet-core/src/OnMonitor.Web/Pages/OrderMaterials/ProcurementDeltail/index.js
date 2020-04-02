$(function () {

    var l = abp.localization.getResource('OnMonitor');

    var service = onMonitor.orderMaterials.procurementDeltail;
    var createModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProcurementDeltail/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProcurementDeltail/EditModal');

    var dataTable = $('#ProcurementDeltailTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                    return l('ProcurementDeltailDeletionConfirmationMessage', data.record.id);
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
            { data: "procurementContentId" },
            { data: "productInfoId" },
            { data: "count" },
            { data: "price" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProcurementDeltailButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});