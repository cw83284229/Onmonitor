$(function () {

    var l = abp.localization.getResource('OnMonitor');

    var service = onMonitor.orderMaterials.productInfo;
    var createModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProductInfo/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'OrderMaterials/ProductInfo/EditModal');

    var dataTable = $('#ProductInfoTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                    return l('ProductInfoDeletionConfirmationMessage', data.record.id);
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
            { data: "materialsNumber" },
            { data: "materialsType" },
            { data: "materialsName" },
            { data: "picture" },
            { data: "units" },
            { data: "materiralsPrice" },
            { data: "marketPrice" },
            { data: "remark" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductInfoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});