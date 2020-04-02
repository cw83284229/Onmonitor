$(function () {

    var l = abp.localization.getResource('OnMonitor');

    var service = onMonitor.orderMaterials.procurementContent;
    var createModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProcurementContent/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProcurementContent/EditModal');

    var dataTable = $('#ProcurementContentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                    return l('ProcurementContentDeletionConfirmationMessage', data.record.id);
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
            { data: "manufacturer" },
            { data: "procurementTime" },
            { data: "procurementMethod" },
            { data: "remark" },
            { data: "isShipments" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProcurementContentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});