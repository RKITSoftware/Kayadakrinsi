import { insertedHandler, insertingHandler, loadedHandler, loadingHandler, modifiedHandler, modifyingHandler, pushHandler, removedHandler, removingHandler, updatedHandler, updatingHandler, rowClickHandler, rowCollapsedHandler, rowCollapsingHandler, rowDblClickHandler, rowExpandedHandler, rowExpandingHandler, rowInsertedHandler, rowInsertingHandler, rowPreparedHandler, rowRemovedHandler, rowRemovingHandler, rowUpdatedHandler, rowUpdatingHandler, rowValidatingHandler } from "../Event.js";
import { stocks, sectors } from "./Data.js";

$(() => {

    let chartInstance;

    let chartInstances = [];

    var storeArray = new DevExpress.data.ArrayStore({
        data: stocks,
        errorHandler: function (error) {
            console.log(error, error.message);
        },
        key: "id",
        onInserted: insertedHandler,
        onInserting: insertingHandler,
        onLoaded: loadedHandler,
        onLoading: loadingHandler,
        onModified: modifiedHandler,
        onModifying: modifyingHandler,
        onPush: pushHandler,
        onRemoved: removedHandler,
        onRemoving: removingHandler,
        onUpdated: updatedHandler,
        onUpdating: updatingHandler
    });

    const grid = $("#grid").dxDataGrid({

        dataSource: storeArray,
        keyExpr: "id",

        pager: {
            allowedPageSizes: [5, 10, 'all'],
            //displayMode:"compact", //"compact"|"full"
            infoText: "At page {0} of {1} ({2} items)",
            showInfo: true,
            showNavigationButtons: true,
            showPageSizeSelector: true,
            visible: true,
        },
        paging: {
            //enabled:false,
            pageSize: 5,
            pageIndex: 0,
        },

        scrolling: {
            columnRenderingMode: "virtual",//"virtual"|"standard"
            mode: "standard",//"virtual"|"standard"|"infinite"
            preloadEnabled: false,
            rowRenderingMode: "virtual",//"virtual"|"standard"
            useNative: false,
            showScrollbar: "always", //"always"|"never"|"onScroll"|"onHover"
        },

        editing: {
            mode: "popup", // 'batch' | 'cell' | 'row' | 'form' | 'popup' Default:row
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },

        groupPanel: { visible: true },

        filterRow: { visible: true },
        filterSyncEnabled: true,
        filterPanel: {
            //customizeText: function (e) {
            //    return `[${e.filterValue[0]} ${e.filterValue[1]} ${e.filterValue[2]}] `;
            //},
            visible: true,
            //filterEnabled: false,
            texts: {
                clearFilter: "Clear all filters",
                createFilter: "Create new filter",
                filterEnabledHint: "Enable filters"
            }
        },
        //filterValue: ["id", "=", 1],

        selection: {
            mode: 'multiple',
        },

        columns: [{
            dataField: "id",
            dataType: "Number",
            fixed: true,
            //width: 100,
            validationRules: [{ type: "numeric", message: "Enter id in digits!" }]
            //visible:false,
        },
        {
            dataField: "companyName",
            //width: 500,
        },
        {
            dataField: "price",
            dataType: "Number",
            alignment: "center",
            allowEditing: false,
            allowFiltering: false,
            allowFixing: false,
            allowGrouping: false,
            allowHeaderFiltering: false,
            allowHiding: false,
            alloReordering: false,
            allowResizing: false,
            allowSerach: false,
            allowSorting: false,
            visibleIndex: 1,
            //width: 500,
            validationRules: [{ type: "numeric", message: "Enter price in digits!" }]
        },
        {
            dataField: "change",
            dataType: "number",
            //width: 500,
            validationRules: [{ type: "numeric", message: "Enter change in numbers!" }]
        },
        {
            dataField: "marketCap",
            // width: 500,
            //groupIndex: 0,
            //visible: false
        },
        {
            dataField: "sectorId",
            //width:500,
            //groupIndex: 0,
            lookup: {
                dataSource: sectors,
                valueExpr: "ID",
                displayExpr: "Name",
            }
        },
        {
            caption: "Statictics",
            cellTemplate(container, options) {
                const { price, change } = options.data;

                // Create chart data based on price and change
                const chartData = [
                    { category: 'Price', value: price },
                    { category: 'Change', value: change }
                ];

                const $chartContainer = $('<div>'); // Create a div element

                $chartContainer.appendTo(container); // Append the div to the container

                // Initialize the chart and store its instance in a variable
                chartInstance = $chartContainer.dxChart({
                    dataSource: chartData,
                    series: {
                        argumentField: 'category',
                        valueField: 'value',
                        type: 'bar'
                    },
                    legend: {
                        visible: false
                    },
                    tooltip: {
                        enabled: true
                    },
                    size: {
                        width: 150,
                        height: 100
                    },
                    title: options.column.caption
                }).dxChart("instance");

                chartInstances.push(chartInstance);
            },
        }],
        columnFixing: { enabled: true },
        columnChooser: { enabled: true },
        allowColumnReordering: true,
        allowColumnResizing: true,

        summary: {
            groupItems: [{
                alignByColumn: true,
                column: "price",
                showInColumn: "marketCap",
                summaryType: "sum",
                showInGroupFooter: true,
                displayFormat: "This Sector contains : {0} {1}s.",
            },
            {
                alignByColumn: true,
                column: "companyName",
                summaryType: "count",
                showInGroupFooter: true,
            }],
            recalculateWhileEditing: true,
            skipEmptyValues: true, //Default : true,
            texts: {
                avg: "Average is {0}",
                avgOtherColumn: "Average of {1} is {0}",
            },
            totalItems: [{
                column: "change",
                summaryType: "avg",
                valueFormat: {
                    type: "fixedPoint",
                    precision: 6
                },
            },
            {
                column: "id",
                summaryType: "count",
                customizeText(itemInfo) {
                    return `Total posts: ${itemInfo.value}`;
                },
            },
            {
                name: 'SelectedRowsSummary',
                showInColumn: 'price',
                displayFormat: 'Sum: {0}',
                /*valueFormat: 'currency',*/
                summaryType: 'custom',
            }],
            calculateCustomSummary(options) {
                if (options.name === 'SelectedRowsSummary') {
                    if (options.summaryProcess === 'start') {
                        options.totalValue = 0;
                    }
                    if (options.summaryProcess === 'calculate') {
                        if (options.component.isRowSelected(options.value.id)) {
                            options.totalValue += options.value.price;
                        }
                    }
                }
            },
        },
        selectedRowKeys: [1, 4, 7],
        sortByGroupSummaryInfo: [{
            summaryItem: "count",
            sortOrder: "desc",
            groupColumn: "sectorId",
        }],
        onSelectionChanged(e) {
            e.component.refresh(true);
        },
       
        searchPanel: { visible: true },
        showBorders: true,

        export: {
            enabled: true,
            formats: ['pdf'],
            allowExportSelectedData: true,
            customizeExcelCell: function (obj) {
                console.log(obj);
                
                //console.log(obj);
            },
        },
        //onExporting(e) {
        //    console.log("Exporting event triggered", e);
        //    const { jsPDF } = window.jspdf;

        //    var doc = new jsPDF();

        //    DevExpress.pdfExporter.exportDataGrid({
        //        jsPDFDocument: doc,
        //        component: e.component,
        //        indent: 5,
        //    }).then(() => {
        //        doc.save('Stocks.pdf');
        //    });
        //    e.cancel = true; // Cancel default export to prevent any other format
        //},
        onExporting(e) {
            e.cancel = true;
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Posts');

            // Assuming chartInstance is accessible here

            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet,
                topLeftCell: { row: 2, column: 2 },
                autoFilterEnabled: true,
                customizeCell(options) {
                    const { gridCell, excelCell } = options;

                    if (gridCell.rowType === 'data' && gridCell.column.index === 6) {
                        excelCell.value = undefined; // Clear the cell value for the image

                        // Get the SVG representation of the chart
                        const svg = chartInstance.svg();

                        // Create a canvas element
                        const canvas = document.createElement('canvas');
                        const ctx = canvas.getContext('2d');

                        try {
                            // Create Canvg instance from the SVG string
                            const canvgInstance = canvg.Canvg.fromString(ctx, svg);

                            // Render the SVG to the canvas
                            canvgInstance.render();

                            // Get the base64-encoded PNG image
                            const imgBase64 = canvas.toDataURL('image/png').replace(/^data:image\/png;base64,/, '');

                            const image = workbook.addImage({
                                base64: imgBase64,
                                extension: 'png',
                            });

                            worksheet.getRow(excelCell.row).height = 120;

                            // Add the image to the worksheet
                            worksheet.addImage(image, {
                                tl: { col: excelCell.col - 1, row: excelCell.row - 1 },
                                br: { col: excelCell.col, row: excelCell.row },
                            });

                        } catch (error) {
                            console.error('Error converting SVG to PNG:', error);
                        }
                    }
                },
            }).then(() => {
                workbook.xlsx.writeBuffer().then((buffer) => {
                    //Without filesaver
                    //const url = window.URL.createObjectURL(new Blob([buffer], { type: 'application/octet-stream' }))
                    //var a = document.createElement(`a`);
                    //a.href = url;
                    //a.download = "posts.xlsx";
                    //a.click();

                    //Using filesaver
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Posts.xlsx');
                });
            });
        },

        onRowClick: rowClickHandler,
        onRowCollapsed: rowCollapsedHandler,
        onRowCollapsing: rowCollapsingHandler,
        onRowDblClick: rowDblClickHandler,
        onRowExpanded: rowExpandedHandler,
        onRowExpanding: rowExpandingHandler,
        onRowInserted: rowInsertedHandler,
        onRowInserting: rowInsertingHandler,
        //onRowPrepared: rowPreparedHandler,
        onRowRemoved: rowRemovedHandler,
        onRowRemoving: removingHandler,
        onRowUpdated: rowUpdatedHandler,
        onRowUpdating: rowUpdatingHandler,
        onRowValidating: rowValidatingHandler,

    }).dxDataGrid("instance");

});














//onExporting({ component }) {
//    console.log("Exporting event triggered");
//    const { jsPDF } = window.jspdf;
//    const doc = new jsPDF();
//    const chartsToExport = [];

//    chartInstances.forEach(chartInstance => {
//        const svg = chartInstance.svg();
//        const canvas = document.createElement('canvas');
//        const ctx = canvas.getContext('2d');

//        try {
//            const canvgInstance = canvg.Canvg.fromString(ctx, svg);
//            canvgInstance.render();
//            const imgBase64 = canvas.toDataURL('image/png').replace(/^data:image\/png;base64,/, '');
//            chartsToExport.push({ instance: chartInstance, image: imgBase64 });
//        } catch (error) {
//            console.error('Error converting SVG to PNG:', error);
//        }
//    });

//    DevExpress.pdfExporter.exportDataGrid({
//        jsPDFDocument: doc,
//        component,
//        margin: {
//            top: 10,
//            right: 10,
//            bottom: 10,
//            left: 10,
//        },
//        topLeft: { x: 5, y: 5 },
//        customizeCell({ gridCell, pdfCell }) {
//            if (gridCell.rowType === 'data' && gridCell.column.index === 6) {
//                pdfCell.value = undefined;

//                // Get cell dimensions using gridCell properties (might differ based on version)
//                const cellWidth = 90; // Assuming a default width if not available
//                const cellHeight = 90; // Assuming a default height if not available
//                chartInstances.forEach((c) => {
//                    const imgBase64 = chartsToExport.find(chart => chart.instance === c)?.image;
//                    if (imgBase64) {
//                        const adjustedXOffset = 0 + (cellWidth - c._rect[2]) / 2;
//                        const adjustedYOffset = 0 + (cellHeight - c._rect[3]) / 2;
//                        doc.addImage(imgBase64, 'PNG', adjustedXOffset, adjustedYOffset, c._rect[2], c._rect[3]);
//                    } else {
//                        console.warn('Chart image not found for instance:', c);
//                    }
//                });
//            }
//        },
//    }).then(() => {
//        doc.save('DataGrid.pdf');
//    });
//}, 