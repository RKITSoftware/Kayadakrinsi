import { insertedHandler, insertingHandler, loadedHandler, loadingHandler, modifiedHandler, modifyingHandler, pushHandler, removedHandler, removingHandler, updatedHandler, updatingHandler, rowClickHandler, rowCollapsedHandler, rowCollapsingHandler, rowDblClickHandler, rowExpandedHandler, rowExpandingHandler, rowInsertedHandler, rowInsertingHandler, rowPreparedHandler, rowRemovedHandler, rowRemovingHandler, rowUpdatedHandler, rowUpdatingHandler, rowValidatingHandler } from "../Event.js";

$(() => {

    DevExpress.setTemplateEngine({
        compile: (element) => $(element).html(),
        render: (template, data) => Mustache.render(template, data),
    });

    const store = new DevExpress.data.CustomStore({
        loadMode: "raw",
        key: "id",
        load: async function (loadOptions) {
            var x = await $.ajax({
                url: "https://jsonplaceholder.typicode.com/posts",
                method: "GET",
                dataType: "json",
                data: loadOptions,
            });
            console.log("LoadOptions ", loadOptions);
            return x;
        },
        byKey: function (key) {
            var d = new $.Deferred();
            $.get("https://jsonplaceholder.typicode.com/posts/" + key)
                .done(function (dataItem) {
                    console.log("DataItem", dataItem);
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        cacheRawData: true,
        errorHandler: function (error) {
            console.log(error.message);
        },
        insert: function (values) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/posts",
                method: "POST",
                data: values
            })
        },
        remove: function (key) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/posts/" + encodeURIComponent(key),
                method: "DELETE",
            })
        },
        update: function (key, values) {
            return $.ajax({
                url: "https://jsonplaceholder.typicode.com/posts/" + encodeURIComponent(key),
                method: "PUT",
                data: values
            })
        },
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
        onUpdating: updatingHandler,
    });

    function switchTheme() {
        // Get the current theme link element
        var themeLink = $("#theme-link");
        // Determine the current theme
        var currentTheme = themeLink.attr("href");
        // Set the new theme based on the current theme
        var newTheme = currentTheme.includes("light") ? "../Content/dx.material.blue.dark.css" : "../Content/dx.material.blue.light.css";
        // Change the href attribute to the new theme
        themeLink.attr("href", newTheme);
    }

    const grid = $("#grid").dxDataGrid({

        dataSource: store,
        keyExpr: "id", // only applies to ArrayStore

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
            pageSize: 10,
            pageIndex: 0,
        },

        scrolling: {
            columnRenderingMode: "virtual",//"virtual"|"standard" Default(standard)
            mode: "standard",//"virtual"|"standard"|"infinite" Default(standard)
            preloadEnabled: false,
            rowRenderingMode: "virtual",//"virtual"|"standard" Default(standard)
            useNative: false,
            scrollByThumb: true,
            showScrollbar: "always", //"always"|"never"|"onScroll"|"onHover"
        },

        editing: {
            mode: "form", // 'batch' | 'cell' | 'row' | 'form' | 'popup'
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            confirmDelete: true, //Default:true
            refreshMode: "full", //'full' | 'reshape' | 'repaint' Default:full
            selectTextOnEditStart: true, //Default:false
            startEditAction: "dblClick", //'click' | 'dblClick' Default:click
        },

        grouping: {
            allowCollapsing: true,
            autoExpandAll: false,
            contextMenuEnabled: true,
            expandMode: "rowClick",//'buttonClick' | 'rowClick'
            texts: {
                groupByThisColumn: "Use this column for grouping",
                groupContinuedMessage: "Continued from previous page",
                groupContinuesMessage: "Continues on next page",
                ungroup: "Remove this column from grouping",
                ungroupAll: "Remove grouping"
            }
        },
        groupPanel: {
            allowColumnDragging: true,
            emptyPanelText: "Grouping isn't defined yet!",
            visible: true,
        },

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
        filterRow: {
            visible: true,
            applyFilter: "onClick",// 'auto' | 'onClick'
            applyFilterText: "Apply all filters",
            betweenEndText: "End at",
            betweenStartText: "Start from",
            operationDescriptions: {
                startsWith: "Begins with",
                endsWith: "Concludes at",
            },
            resetOperationText: "Reset all",
            showOperationChooser: true,
        },
        headerFilter: {
            visible: true,
            allowSearch: true,
            searchTimeout: 200,
            texts: {
                cancel: "Drop",
                emptyValue: "Search...",
                ok: "Done"
            }
        },
        filterSyncEnabled: true,
        filterValue: ["id", "=", 1],
        filterBuilder: {
            customOperations: [{
                name: '1',
                caption: '1',
                dataTypes: ['number'],
                icon: 'filter',
                hasValue: false,
                calculateFilterExpression(value, feild) {
                    return [feild.dataField, '=', 1];
                },
            }],
            allowHierarchicalFields: true,
        },
        filterBuilderPopup: {
            //position: {
            //    of: window, at: 'top', my: 'top', offset: { y: 10 },
            //},
            title: "Adjust filters here",
            shadingColor: "rgba(0,0,0,0.8)"
        },

        sorting: {
            ascendingText: "Sort in ASCENDING manner",
            clearText: "Clear sorting of this column",
            descendingText: "Sort in DESCENDING manner",
            mode: "multiple", // 'multiple' | 'none' | 'single' Default:single
            showSortIndexes: true,
        },

        selection: {
            allowSelectAll: true,
            mode: "multiple", //'multiple' | 'none' | 'single' Default:none
            selectAllMode: "page", //'allPages' | 'page' Default:allPages
            deferred: false,
            showCheckBoxesMode: "onLongTap", //'always' | 'none' | 'onClick' | 'onLongTap' Default:onClick
        },
        selectionFilter: ["title", "startswith", "a"], // Works only when deffered selection is true

        //columnWidth: 500, //Has a lower priority than the column.width property.
        allowColumnReordering: true,
        columnHidingEnabled: true, //Ignored if allowColumnResizing is true and columnResizingMode is "widget".
        allowColumnResizing: false, //If set to true adaptive column will not work
        columnMinWidth: 100,
        columnAutoWidth: true,
        columnResizingMode: "widget", //"nextColumn"|"widget" Applies only if allowColumnResizing is true 
        columnChooser: {
            allowSearch: true,
            emptyPanelText: "Drag columns to hide it.",
            enabled: true,
            height: 300,
            mode: "dragAndDrop",//"dragAndDrop"|"select"
            searchTimeout: 400,
            title: "More columns",
            width: 300,
        },
        columnFixing: {
            enabled: true,
            texts: {
                fix: "Fix Column",
                unfix: "Unfix Column",
                leftPosition: "To the Left",
                rightPosition: "To the Right"
            }
        },
        customizeColumns: function (columns) {
            console.log(columns);
            columns[0].alignment = "center";
            //columns[0].allowHiding = false;
            columns[1].alignment = "center";
            //columns[0].groupIndex = 1;
        },
        columns: [
            {
                caption: "Ids",
                isBand: true,
                columns: [{
                    // alignment: 'center',
                    //allowEditing: false,
                    dataField: "id",
                    //dataType: "number",
                    //groupIndex:1,
                    validationRules: [{
                        type: "required",
                        message: "Id is required!"
                    }, {
                        type: "numeric",
                        message: "Id must be a number!"
                    }],
                },
                {
                    dataField: "userId",
                    dataType: "number",
                    //groupIndex: 0,
                    validationRules: [{
                        type: "required",
                        message: "userId is required!"
                    }, {
                        type: "numeric",
                        message: "userId must be a number!"
                    }],
                    sortIndex: 0,
                    sortOrder: "desc",
                }]
            },
            {
                dataField: "title",
                allowGrouping: false,
                sortIndex: 1,
                sortOrder: "asc",
                //hidingPriority: 0,
                cellTemplate(container, options) {
                    $('<div>')
                        //.append($('<img>', {
                        //    src: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRViCufR7cSePmGlcYB-T_3OZrwLc2ppRhU0w&usqp=CAU"
                        //}))
                        .append(`<span class="dx-icon-gift"> ${options.value}</span>`)
                        .appendTo(container);
                },
            },
            {
                dataField: "body",
                //visible: false,
                hidingPriority: 1,
            },
            {
                type: "buttons",
                caption: "Options",
                buttons: ["edit", "delete", {
                    name: "Copy",
                    icon: "copy",
                    onClick: function (e) {
                        const data = e.row.data;
                        var textToCopy = `id:${data.id}, userId:${data.userId}, title:${data.title}, body:${data.body}`;
                        navigator.clipboard.writeText(textToCopy);
                        DevExpress.ui.notify('Row copied to clipboard!', "success", 500);
                    }
                }],
                //visible: false,
                //hidingPriority: 1,
            },
            //{
            //    type: "adaptive",
            //    width: 50,
            //    caption: "More",
            //}
        ],

        stateStoring: {
            enabled: true,
            type: "custom", //'custom' | 'localStorage' | 'sessionStorage' Default:localStorage
            savingTimeout: 500,
            storageKey: "StateStoring",
            customLoad() {
                console.log("state:", grid.state());
                return JSON.parse(localStorage.getItem("StateStoring"));
            },
            customSave(state) {
                if (state) {
                    for (let i = 0; i < state.columns.length; i++) {
                        state.columns[i].filterValue = null;
                    }
                }
                localStorage.setItem("StateStoring", JSON.stringify(state));
            }
        },

        showBorders: true,
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,

        //rowTemplate(container, item) {
        //    const { data } = item;
        //    const markup = '<tr class=\'main-row\'>'
        //        + `<td></td>`
        //        + `<td>${data.id}</td>`
        //        + `<td>${data.userId}</td>`
        //        + `<td>${data.title}</td>`
        //        /*+ `<td>${data.body}</td>`*/
        //        + '</tr>'
        //        + '<tr class=\'notes-row\'>'
        //        + `<td></td>`
        //        + `<td colspan='4'><div>Body : ${data.body}</div></td>`
        //        //+ `<td colspan='4'><div>Id : ${data.id}, UserID : ${data.userId}, Title : ${data.title}, Body : ${data.body}</div></td>`
        //        + '</tr>';

        //    container.append(markup);
        //},
        //rowTemplate: $('#gridRow'),
        onToolbarPreparing: function (e) {
            console.log("Toolsbar",e);
            let toolbarItems = e.toolbarOptions.items;
            console.log("ToolbarItems",toolbarItems);
            toolbarItems.push({
                widget: "dxButton",
                options: {
                    icon: "user",
                    onClick: function () {
                        DevExpress.ui.notify('User button clicked', "success", 500);
                    }
                },
                location: "after"
            });
            toolbarItems.push({
                widget: "dxButton",
                options: {
                    icon: "palette",
                    onClick: function () {
                        switchTheme();
                    }
                },
                location: "before"
            });
        },

        summary: {
            groupItems: [{
                summaryType: "count"
            }]
        },


        export: {
            enabled: true,
            allowExportSelectedData: true,
        },
        onExporting(e) {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Posts');

            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet,
                autoFilterEnabled: true,
            }).then(() => {
                workbook.xlsx.writeBuffer().then((buffer) => {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Posts.xlsx');
                });
            });
        },

        searchPanel: { visible: true },
        focusedRowEnabled: true,
        focusStateEnabled: false,
        loadPanel: {
            indicatorSrc: "https://cdn.dribbble.com/users/2973561/screenshots/5757826/loading__.gif",
            shading: true,
            shadingColor: "rgba(0,0,0,0.5)",
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
        onAdaptiveDetailRowPreparing: function (e) {
            console.log("AdaptiveDetailRowPreparing", e);
        },
        onCellClick: function (e) {
            console.log("Cell clicked", e);
        },
        //onRowPrepared: function (e) {
        //    if (e.rowType !== "data")
        //        return
        //    if ((e.dataIndex % 2) == 0) {
        //        e.rowElement.find("td").css('background', "#ddd");
        //        e.rowElement.find("td").css('color', "black");
        //    }
        //    else {
        //        e.rowElement.find("td").css('background', "white");
        //        e.rowElement.find("td").css('color', "black");
        //    }
        //}
    }).dxDataGrid("instance");


});



//onExporting(e) {
//    const workbook = new ExcelJS.Workbook();
//    const worksheet = workbook.addWorksheet('Posts');

//    DevExpress.excelExporter.exportDataGrid({
//        component: e.component,
//        worksheet,
//        topLeftCell: { row: 2, column: 2 },
//        autoFilterEnabled: true,
//        customizeCell(options) {
//            const { gridCell, excelCell } = options;

//            if (gridCell.rowType === 'data' && gridCell.column.caption === 'Image' && gridCell.value) {
//                excelCell.value = undefined; // Clear the cell value for the image

//                // Ensure that gridCell.value is a string before calling replace
//                const imageBase64 = (typeof gridCell.value === 'string') ? gridCell.value.replace(/^data:image\/(png|jpg|jpeg);base64,/, '') : '';

//                if (imageBase64) {
//                    const image = workbook.addImage({
//                        base64: imageBase64,
//                        extension: 'png',
//                    });

//                    worksheet.getRow(excelCell.row).height = 90;
//                    worksheet.addImage(image, {
//                        tl: { col: excelCell.col - 1, row: excelCell.row - 1 },
//                        br: { col: excelCell.col, row: excelCell.row },
//                    });
//                }
//            }
//        },
//    }).then(() => {
//        workbook.xlsx.writeBuffer().then((buffer) => {
//            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Posts.xlsx');
//        });
//    });
//},