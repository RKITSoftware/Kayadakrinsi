import { stocks, menuItems } from './Data.js';

$(() => {
    const menu = $("#menu").dxMenu({
        items: menuItems,
        displayExpr: "name",
        selectByClick: true,
        selectionMode: 'single',
        onSelectionChanged: function (e) {
            const selectedItem = e.addedItems[0];
            if (selectedItem.name === 'Account Details') {
                popover.show();
            }
            if (selectedItem.name === 'Purchase') {
                popup.show();
            }
        }
    }).dxMenu("instance");

    const popover = $("#popover").dxPopover({
        showTitle: true,
        title: "Account Details",
        showCloseButton: true,
        shading: true,
        target: "#menu",
        width: "20vw",
        position: { of: "#menu", at: "right", offset: { x: "-790", y: "60" } },
        contentTemplate: function () {
            const template = $('<div>').append(`
                <p>Username: TestAccount</p>
                <p>PAN: AWSED2312A</p>
            `);

            template.dxScrollView({
                width: '100%',
                height: '100%',
            });

            return template;
        }
    }).dxPopover("instance");

    const grid = $("#grid").dxDataGrid({
        dataSource: new DevExpress.data.DataSource({
            store: new DevExpress.data.ArrayStore({
                key: 'id',
                data: stocks,
            }),
            filter: ['isPurchased', '=', true],
        }),
        editing: {
            allowAdding: false, // Disable the + button
        },
        columns: [
            { dataField: 'name' },
            { dataField: 'todayHigh' },
            { dataField: 'todayLow' },
            { dataField: 'purchasePrice' },
            { dataField: 'currentPrice' },
            { dataField: 'quantity' },
            {
                dataField: 'profitLossPercentage',
                format: {
                    type: 'fixedPoint',
                    precision: 2,
                },
            },
        ],
    }).dxDataGrid("instance");

    const panel = $("#panel").dxLoadPanel({
        animation: {
            show: { type: 'fadeIn', from: 0, to: 1, duration: 700 },
        },
        closeOnOutsideClick: true,
        focusStateEnabled: true,
        hoverStateEnabled: true,
        hint: "Please wait while loading",
        indicatorSrc: "https://www.icegif.com/wp-content/uploads/2023/07/icegif-1262.gif",
        minHeight: 50,
        minWidth: 30,
        maxHeight: "100vh",
        maxWidth: "100vw",
        position: { of: '#grid' },
        shadingColor: 'rgba(0,0,0,0.4)',
        visible: false,
        showIndicator: true,
        showPane: true,
        shading: true,
    }).dxLoadPanel("instance");

    const toast = $("#toast").dxToast({
        animation: {
            show: { type: "fadeIn", from: 0, to: 1, duration: 100 }
        },
        closeOnClick: true,
        closeOnOutsideClick: true,
        closeOnSwipe: true,
        deferRendering: true,
        displayTime: 2000,
        focusStateEnabled: true,
        height: "8vh",
        hint: "Notification",
        message: "Stock(s) purchased successfully!",
        position: {
            of: ".container",
            at: { x: "right", y: "top" },
            offset: "-276 18",
        },
        shading: false,
        shadingColor: 'rgba(0,0,50,0.1)',
        type: 'success',
    }).dxToast("instance");

    let nonPurchasedStocks = stocks.filter(stock => !stock.isPurchased);

    const popup = $("#popup").dxPopup({
        animation: {
            show: { type: 'fadeIn', from: 0, to: 1, duration: 700 },
        },
        closeOnOutsideClick: true,
        contentTemplate: function () {
            const formTemplate = $('<div id="formContainer"></div>');

            formTemplate.dxForm({
                formData: {
                    stockId: null,
                    purchasePrice: null,
                    quantity: null
                },
                items: [
                    {
                        dataField: "stockId",
                        label: { text: "Stock" },
                        editorType: "dxSelectBox",
                        editorOptions: {
                            dataSource: nonPurchasedStocks,
                            displayExpr: "name",
                            valueExpr: "id",
                            onValueChanged: function (e) {
                                const selectedStock = nonPurchasedStocks.find(stock => stock.id === e.value);
                                $("#formContainer").dxForm("instance").updateData("purchasePrice", selectedStock ? selectedStock.price : null);
                            }
                        },
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "purchasePrice",
                        label: { text: "Purchase Price" },
                        editorType: "dxNumberBox",
                        editorOptions: {
                            readOnly: true
                        }
                    },
                    {
                        dataField: "quantity",
                        label: { text: "Quantity" },
                        editorType: "dxNumberBox",
                        validationRules: [{ type: "required" }, { type: "numeric", min: 1 }]
                    }
                ]
            });

            formTemplate.dxScrollView({
                width: '100%',
                height: '100%',
            });

            return formTemplate;
        },
        height: "50vh",
        hoverStateEnabled: true,
        resizeEnabled: true,
        position: 'top',
        rtlEnabled: false,
        shadingColor: "rgba(0,0,100,0.1)",
        showCloseButton: true,
        showTitle: true,
        title: "Purchase share",
        width: "30vw",
        toolbarItems: [{
            widget: 'dxButton',
            toolbar: 'bottom',
            location: 'after',
            options: {
                text: 'Submit',
                onClick: function () {
                    const formInstance = $("#formContainer").dxForm("instance");
                    const formData = formInstance.option("formData");
                    const selectedStock = nonPurchasedStocks.find(stock => stock.id === formData.stockId);

                    if (selectedStock) {
                        selectedStock.isPurchased = true;
                        selectedStock.purchasePrice = formData.purchasePrice;
                        selectedStock.quantity = formData.quantity;
                        selectedStock.currentPrice = selectedStock.price;
                        selectedStock.profitLossPercentage = ((selectedStock.currentPrice - formData.purchasePrice) / formData.purchasePrice) * 100;

                        popup.hide();
                        panel.show();
                        setTimeout(() => {
                            nonPurchasedStocks = stocks.filter(stock => !stock.isPurchased);

                            // Update the SelectBox data source
                            const formEditor = formInstance.getEditor('stockId');
                            formEditor.option('dataSource', nonPurchasedStocks);
                            grid.refresh();
                            formInstance.resetValues();
                            grid.refresh();
                            panel.hide();
                            toast.show();
                        }, 2000);
                    } else {
                        toast.option({
                            type: 'error',
                            message: 'Something went wrong!'
                        });
                        popup.hide();
                        toast.show();
                    }
                }
            }
        }]
    }).dxPopup("instance");
});
