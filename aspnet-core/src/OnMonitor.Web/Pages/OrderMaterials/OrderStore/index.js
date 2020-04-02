$(function () {

    var l = abp.localization.getResource('OnMonitor');

    var service = onMonitor.orderMaterials.orderStore;
    var createModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/OrderStore/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/OrderStore/EditModal');

    var dataTable = $('#OrderStoreTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                    return l('OrderStoreDeletionConfirmationMessage', data.record.id);
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
            { data: "orderMaterialId" },
            { data: "billMaterialId" },
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

    $('#NewOrderStoreButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});