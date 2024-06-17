

//$(() => {

//    const userStore = new DevExpress.data.CustomStore({
//        key: "id",
//        load: function (loadOptions) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/users",
//                method: "GET",
//                dataType: "json",
//                data: loadOptions
//            });
//        },
//        insert: function (values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/users",
//                method: "POST",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        update: function (key, values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/users/" + encodeURIComponent(key),
//                method: "PUT",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        remove: function (key) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/users/" + encodeURIComponent(key),
//                method: "DELETE"
//            });
//        }
//    });

//    const cartStore = new DevExpress.data.CustomStore({
//        key: "id",
//        load: function (loadOptions) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/carts",
//                method: "GET",
//                dataType: "json",
//                data: loadOptions
//            });
//        },
//        insert: function (values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/carts",
//                method: "POST",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        update: function (key, values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/carts/" + encodeURIComponent(key),
//                method: "PUT",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        remove: function (key) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/carts/" + encodeURIComponent(key),
//                method: "DELETE"
//            });
//        }
//    });

//    const productStore = new DevExpress.data.CustomStore({
//        key: "id",
//        load: function (loadOptions) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/products",
//                method: "GET",
//                dataType: "json",
//                data: loadOptions
//            });
//        },
//        byKey: function (key) {
//            var d = new $.Deferred();
//            $.get("https://fakestoreapi.com/products/" + key)
//                .done(function (dataItem) {
//                    d.resolve(dataItem);
//                });
//            return d.promise();
//        },
//        insert: function (values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/products",
//                method: "POST",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        update: function (key, values) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/products/" + encodeURIComponent(key),
//                method: "PUT",
//                contentType: "application/json",
//                data: JSON.stringify(values)
//            });
//        },
//        remove: function (key) {
//            return $.ajax({
//                url: "https://fakestoreapi.com/products/" + encodeURIComponent(key),
//                method: "DELETE"
//            });
//        }
//    });

//    const users = new DevExpress.data.DataSource({
//        store: userStore,
//        onChanged: function (e) {
//            console.log("On change", e);
//        },
//        onLoadError: function (error) {
//            console.log("On load error", error.message);
//        },
//        onLoadingChanged: function (isLoading) {
//            console.log("On loading changed", isLoading);
//        },
//        //paginate: true,
//        //pageSize: 5,
//        requireTotalCount: true,
//        reshapeOnPush: true,
//    });

//    const carts = new DevExpress.data.DataSource({
//        store: cartStore,
//        onChanged: function (e) {
//            console.log("On change", e);
//        },
//        onLoadError: function (error) {
//            console.log("On load error", error.message);
//        },
//        onLoadingChanged: function (isLoading) {
//            console.log("On loading changed", isLoading);
//        },
//        requireTotalCount: true,
//        reshapeOnPush: true,
//    });

//    //const products = new DevExpress.data.DataSource({
//    //    store: productStore,
//    //    onChanged: function (e) {
//    //        console.log("On change", e);
//    //    },
//    //    onLoadError: function (error) {
//    //        console.log("On load error", error.message);
//    //    },
//    //    onLoadingChanged: function (isLoading) {
//    //        console.log("On loading changed", isLoading);
//    //    },
//    //    requireTotalCount: true,
//    //    reshapeOnPush: true,
//    //});


//    async function calculate(rowData) {
//        let price, total;
//        await productStore.byKey(rowData.productId).done(function (dataItem) {
//            price = dataItem.price;
//            total = rowData.quantity * price;
//        });
//        return total;
//    }

//    console.log(carts, productStore);

//    const grid = $("#grid").dxDataGrid({

//        dataSource: users,

//        pager: {
//            allowedPageSizes: [5, 'all'],
//            //displayMode:"compact", //"compact"|"full"
//            infoText: "At page {0} of {1} ({2} items)",
//            showInfo: true,
//            showNavigationButtons: true,
//            showPageSizeSelector: true,
//            visible: true,
//        },
//        paging: {
//            //enabled:false,
//            pageSize: 10,
//            pageIndex: 0,
//        },

//        columns: [{
//            dataField: "id",
//            dataType: "number",
//            alignment: "center",
//            fixed: true,
//        },
//        {
//            caption: "Name",
//            calculateCellValue(rowData) {
//                return `${rowData.name.firstname} ${rowData.name.lastname}`;
//            },
//            //cellTemplate(container, options) {
//            //    $('<div>').append()
//            //}
//        },
//        {
//            dataField: "username",
//            alignment: "center",
//        },
//        {
//            dataField: "email",
//            alignment: "center"
//        }],

//        editing: {
//            mode: "form", // 'batch' | 'cell' | 'row' | 'form' | 'popup'
//            allowUpdating: true,
//            allowDeleting: true,
//            allowAdding: true,
//            confirmDelete: true, //Default:true
//            refreshMode: "full", //'full' | 'reshape' | 'repaint' Default:full
//            selectTextOnEditStart: true, //Default:false
//            startEditAction: "dblClick", //'click' | 'dblClick' Default:click
//        },

//        masterDetail: {
//            autoExpandAll: false, // Default : false
//            enabled: true,
//            template(container, options) {
//                const currentUserData = options.data;

//                $('<div>')
//                    .addClass('master-detail-caption')
//                    .text(`Cart of ${currentUserData.name.firstname} ${currentUserData.name.lastname}`)
//                    .appendTo(container);

//                $('<div>').dxDataGrid({
//                    dataSource: new DevExpress.data.DataSource({
//                        store: cartStore,
//                        filter: ['userId', '=', options.key],
//                    }),
//                    filter: ['userid', '=', options.key],
//                    columns: [{
//                        dataField: "id",
//                        alignment: "center",
//                    },
//                    {
//                        dataField: "date",
//                        alignment: "center",
//                        dataType: "date",
//                        }],
//                    editing: {
//                        mode: "form", // 'batch' | 'cell' | 'row' | 'form' | 'popup'
//                        allowUpdating: true,
//                        allowDeleting: true,
//                        allowAdding: true,
//                        confirmDelete: true, //Default:true
//                        refreshMode: "full", //'full' | 'reshape' | 'repaint' Default:full
//                        selectTextOnEditStart: true, //Default:false
//                        startEditAction: "dblClick", //'click' | 'dblClick' Default:click
//                    },
//                    dateSerializationFormat: "yyyy-MM-dd",
//                    masterDetail: {
//                        autoExpandAll: false, // Default : false
//                        enabled: true,
//                        template(container, options) {
//                            const currentCartData = options.data;

//                            $('<div>')
//                                .addClass('master-detail-caption')
//                                .text("Product Details")
//                                .appendTo(container);

//                            $('<div>').dxDataGrid({
//                                dataSource: currentCartData.products,
//                                filter: ['userid', '=', options.key],
//                                columns: [{
//                                    dataField: "productId",
//                                    caption: "Product",
//                                    alignment: "center",
//                                    lookup: {
//                                        dataSource: productStore,
//                                        valueExpr: "id",
//                                        displayExpr: "title",
//                                    },
//                                },
//                                {
//                                    dataField: "quantity",
//                                    alignment: "center",
//                                },
//                                {
//                                    caption: "Price", //Case sensitive
//                                    dataField: "productId",
//                                    alignment: "center",
//                                    lookup: {
//                                        dataSource: productStore,
//                                        valueExpr: "id",
//                                        displayExpr: "price",
//                                    },
//                                },
//                                {
//                                    caption: "Total",
//                                    dataType:"number",
//                                    cellTemplate(container,options) {
//                                        calculate(options.data).then(total => {
//                                            container.addClass('total');
//                                            $('<div />').append(total).appendTo(container);
//                                        });
//                                    } 
//                                    }],
//                                editing: {
//                                    mode: "form", // 'batch' | 'cell' | 'row' | 'form' | 'popup'
//                                    allowUpdating: true,
//                                    allowDeleting: true,
//                                    allowAdding: true,
//                                    confirmDelete: true, //Default:true
//                                    refreshMode: "full", //'full' | 'reshape' | 'repaint' Default:full
//                                    selectTextOnEditStart: true, //Default:false
//                                    startEditAction: "dblClick", //'click' | 'dblClick' Default:click
//                                },
//                            }).appendTo(container);
//                        },
//                    },
//                }).appendTo(container);
//            },
//        },

//    }).dxDataGrid("instance");

//});


$(() => {

    let billArray, productArray;

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

    const bills = new DevExpress.data.CustomStore({
        key: "id",
        load: function (loadOptions) {
            console.log(loadOptions);
            return $.ajax({
                url: "https://localhost:7149/api/Bills/bill",
                method: "GET",
                dataType: "json",
                data: loadOptions
            });
        },
        byKey: function (key) {
            var d = new $.Deferred();
            $.get("https://localhost:7149/api/Bills/bill/" + key)
                .done(function (dataItem) {
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        insert: function (values) {
            return $.ajax({
                url: "https://localhost:7149/api/Bills/bill",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        update: function (key, values) {
            return $.ajax({
                url: "https://localhost:7149/api/Bills/bill/" + key,
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        remove: function (key) {
            return $.ajax({
                url: "https://localhost:7149/api/Bills/bill/" + encodeURIComponent(key),
                method: "DELETE"
            });
        }
    });

    bills.load().then(function (data) {
        billArray = data;
        console.log(billArray);
    });

    const company = new DevExpress.data.CustomStore({
        key: "companyId",
        load: function (loadOptions) {
            return $.ajax({
                url: "https://localhost:7149/api/Company/company",
                method: "GET",
                dataType: "json",
                data: loadOptions
            });
        },
        byKey: function (key) {
            var d = new $.Deferred();
            $.get("https://localhost:7149/api/Company/company/" + key)
                .done(function (dataItem) {
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        insert: function (values) {
            return $.ajax({
                url: "https://localhost:7149/api/Company/company",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        update: function (key, values) {
            return $.ajax({
                url: "https://localhost:7149/api/Company/company/" + encodeURIComponent(key),
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        remove: function (key) {
            return $.ajax({
                url: "https://localhost:7149/api/Company/company/" + encodeURIComponent(key),
                method: "DELETE"
            });
        }
    });

    const product = new DevExpress.data.CustomStore({
        key: "productId",
        load: function (loadOptions) {
            return $.ajax({
                url: "https://localhost:7149/api/Product/product",
                method: "GET",
                dataType: "json",
                data: loadOptions
            });
        },
        byKey: function (key) {
            var d = new $.Deferred();
            $.get("https://localhost:7149/api/Product/product/" + key)
                .done(function (dataItem) {
                    d.resolve(dataItem);
                });
            return d.promise();
        },
        insert: function (values) {
            return $.ajax({
                url: "https://localhost:7149/api/Product/product",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        update: function (key, values) {
            return $.ajax({
                url: "https://localhost:7149/api/Product/product/" + encodeURIComponent(key),
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(values)
            });
        },
        remove: function (key) {
            return $.ajax({
                url: "https://localhost:7149/api/Product/product/" + encodeURIComponent(key),
                method: "DELETE"
            });
        }
    });

    product.load().then(function (data) {
        productArray = data;
    });


    const grid = $("#grid").dxDataGrid({

        onRowUpdating: function (e) {
            deepMerge(e.newData, e.oldData);
        },

        width: '100%',

        dataSource: bills,

        pager: {
            allowedPageSizes: [5, 'all'],
            infoText: "At page {0} of {1} ({2} items)",
            showInfo: true,
            showNavigationButtons: true,
            showPageSizeSelector: true,
            visible: true,
        },
        paging: {
            pageSize: 5,
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

        remoteOperations: {
            paging: true,
        },

        editing: {
            mode: "popup", // 'batch' | 'cell' | 'row' | 'form' | 'popup'
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            confirmDelete: true, //Default:true
            refreshMode: "full", //'full' | 'reshape' | 'repaint' Default:full
            selectTextOnEditStart: true, //Default:false
            startEditAction: "dblClick", //'click' | 'dblClick' Default:click
            form: {
                items: [{
                    dataField: "id",
                    editorType: "dxNumberBox",
                },
                {
                    dataField: "date",
                    editorType: "dxDateBox",
                },
                {
                    dataField: "sellerId",
                    editorType: "dxSelectBox",
                    editorOptions: {
                        dataSource: company, // Assuming company is your data source
                        displayExpr: "name", // Displayed field in the dropdown
                        valueExpr: "companyId", // Value field in the dropdown
                        searchEnabled: true // Enable search in the dropdown
                    }
                },
                {
                    dataField: "purchaserId",
                    editorType: "dxSelectBox",
                    editorOptions: {
                        dataSource: company, // Assuming company is your data source
                        displayExpr: "name", // Displayed field in the dropdown
                        valueExpr: "companyId", // Value field in the dropdown
                        searchEnabled: true // Enable search in the dropdown
                    }
                },
                {
                    dataField: "totalAmount",
                    editorType: "dxNumberBox",
                    editorOptions: {
                        format: {
                            type: "fixedPoint",
                            precision: 2
                        }
                    }
                }]
            },
            popup: {
                title: "Edit Bill",
                showTitle: true,
                width: 700,
                height: 525,
                position: {
                    my: "center",
                    at: "center",
                    of: window
                }
            }
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
        /*filterValue: ["id", "=", 1],*/
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
        /*selectionFilter: ["title", "startswith", "a"], // Works only when deffered selection is true*/

        allowColumnReordering: true,
        columnHidingEnabled: true, //Ignored if allowColumnResizing is true and columnResizingMode is "widget".
        allowColumnResizing: false, //If set to true adaptive column will not work
        columnMinWidth: 200,
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
        columns: [
            {
                dataField: "id",
                dataType: "number",
                alignment: "center",
                sortOrder: "desc",
                sortIndex: 0,
                fixed: true,
                validationRules: [
                    { type: "required", message: "Product ID is required" },
                    { type: "numeric", message: "Product ID must be a number" }
                ]
            },
            {
                dataField: "sellerId",
                caption: "Seller",
                groupIndex: 0,
                lookup: {
                    dataSource: company,
                    valueExpr: "companyId",
                    displayExpr: "name"
                },
                validationRules: [{ type: "required", message: "Seller is required" }]
            },
            {
                dataField: "purchaserId",
                caption: "Purchaser",
                lookup: {
                    dataSource: company,
                    valueExpr: "companyId",
                    displayExpr: "name"
                },
                validationRules: [{ type: "required", message: "Purchaser is required" }]
            },
            {
                dataField: "date",
                dataType: "date",
                alignment: "center",
                sortOrder: "asc",
                sortIndex: 1,
            },
            {
                dataField: "totalAmount",
                dataType: "number",
                format: {
                    type: "fixedPoint",
                    precision: 2
                },
                validationRules: [
                    { type: "required", message: "Total Amount is required" },
                    { type: "numeric", message: "Total Amount must be a number" }
                ]
            },
            {
                caption: "Other Information",
                isBand: true,
                alignment: "center",
                columns: [
                    {
                        hidingPriority: 1,
                        dataField: "sellerId",
                        caption: "Seller",
                        lookup: {
                            dataSource: company,
                            valueExpr: "companyId",
                            displayExpr: "gstNumber"
                        },
                        validationRules: [{ type: "required", message: "Seller is required" }]
                    },
                    {
                        hidingPriority: 0,
                        dataField: "purchaserId",
                        caption: "Purchaser",
                        lookup: {
                            dataSource: company,
                            valueExpr: "companyId",
                            displayExpr: "gstNumber"
                        },
                        validationRules: [{ type: "required", message: "Purchaser is required" }]
                    }
                ]
            }],

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
        rowAlternationEnabled: false,

        onToolbarPreparing: function (e) {
            console.log("Toolsbar", e);
            let toolbarItems = e.toolbarOptions.items;
            console.log("ToolbarItems", toolbarItems);
            toolbarItems.push({
                widget: "dxButton",
                options: {
                    icon: "palette",
                    onClick: function () {
                        switchTheme();
                    }
                },
                location: "after"
            });
        },

        summary: {
            groupItems: [{
                alignByColumn: true,
                column: "sellerId",
                summaryType: "count",
                showInGroupFooter: false,
                /*displayFormat: "This Sector contains : {0} {1}s.",*/
            }],
            recalculateWhileEditing: true,
            skipEmptyValues: true, //Default : true,
            texts: {
                avg: "Average is {0}",
                avgOtherColumn: "Average of {1} is {0}",
            },
            totalItems: [
            {
                column: "id",
                summaryType: "count",
                customizeText(itemInfo) {
                    return `Total Bills: ${itemInfo.value}`;
                    },
            },
            {
                name: 'SelectedRowsSummary',
                showInColumn: 'totalAmount',
                displayFormat: 'Total : {0}',
                valueFormat: {
                    type: "fixedPoint",
                    precision: 2
                },
                summaryType: 'custom',
            }],
            calculateCustomSummary(options) {
                if (options.name === 'SelectedRowsSummary') {
                    if (options.summaryProcess === 'start') {
                        options.totalValue = 0;
                    }
                    if (options.summaryProcess === 'calculate') {
                        if (options.component.isRowSelected(options.value.id)) {
                            options.totalValue += options.value.totalAmount;
                        }
                    }
                }
            },
        },
        selectedRowKeys: [1, 4, 7],
        sortByGroupSummaryInfo: [{
            summaryItem: "count",
            sortOrder: "desc",
            groupColumn: "sellerId",
        }],
        onSelectionChanged(e) {
            e.component.refresh(true);
        },

        searchPanel: { visible: true },

        masterDetail: {
            autoExpandAll: false,
            enabled: true,
            template(container, options) {
                const currentData = options.data.products;

                $('<div>')
                    .addClass('master-detail-caption')
                    .text("Product Details")
                    .appendTo(container);

                $('<div>').dxDataGrid({
                    dataSource: new DevExpress.data.DataSource({
                        store: product,
                        filter: function (dataItem) {
                            return currentData.some(item => item.id === dataItem.productId);
                        }
                    }),

                    columns: [
                        {
                            dataField: "name",
                            validationRules: [{ type: "required", message: "Name is required" }]
                        },
                        {
                            dataField: "quantity",
                            caption: "Quantity",
                            alignment: "center",
                            validationRules: [
                                { type: "required", message: "Quantity is required" },
                                { type: "range", message: "Quantity must be between 1 and 100", min: 1, max: 100 }
                            ],
                        },
                        {
                            dataField: "price",
                            validationRules: [{ type: "required", message: "Price is required" }]
                        },
                        {
                            caption: "Total",
                            alignment: "center",
                            calculateCellValue: function (rowData) {
                                const matchingProduct = currentData.find(product => product.id === rowData.productId);
                                return matchingProduct ? matchingProduct.quantity * rowData.price : 0;
                            },
                            format: {
                                type: "fixedPoint",
                                precision: 2,
                            },
                        }
                    ],

                    summary: {
                        totalItems: [{
                            column: "Total",
                            showInColumn: "Total",
                            summaryType: "sum",
                            valueFormat: {
                                type: "fixedPoint",
                                precision: 2
                            },
                        }]
                    },

                }).appendTo(container);
            },
        },

        export: {
            enabled: true,
            allowExportSelectedData: true,
        },
        onExporting: function (e) {
            var workbook = new ExcelJS.Workbook();
            var worksheet = workbook.addWorksheet('Bills');

            let masterRows = [];

            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet: worksheet,
                topLeftCell: { row: 2, column: 2 },
                customizeCell: function ({ gridCell, excelCell }) {
                    if (gridCell.column.index === 1 && gridCell.rowType === 'data') {
                        masterRows.push({ rowIndex: excelCell.fullAddress.row + 1, data: gridCell.data });
                    }
                }
            }).then((cellRange) => {
                const borderStyle = { style: "thin", color: { argb: "FF7E7E7E" } };
                let offset = 0;

                const insertRow = (index, offset, outlineLevel) => {
                    const currentIndex = index + offset;
                    const row = worksheet.insertRow(currentIndex, [], 'n');

                    for (var j = worksheet.rowCount + 1; j > currentIndex; j--) {
                        worksheet.getRow(j).outlineLevel = worksheet.getRow(j - 1).outlineLevel;
                    }

                    row.outlineLevel = outlineLevel;

                    return row;
                }

                for (var i = 0; i < masterRows.length; i++) {
                    let row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
                    let columnIndex = cellRange.from.column + 1;
                    row.height = 40;

                    let billsData = billArray.find((item) => item.id === masterRows[i].data.id);

                    Object.assign(row.getCell(columnIndex), {
                        value: getCaption(billsData),
                        fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } }
                    });
                    worksheet.mergeCells(row.number, columnIndex, row.number, 3);

                    const columns = ["name", "quantity","price"];

                    row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

                    columns.forEach((columnName, currentColumnIndex) => {
                        Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                            value: columnName,
                            fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                            font: { bold: true },
                            border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                        });
                    });

                    billsData.products = billsData.products.map(val => val.id);

                    getProducts(billsData.products).forEach((product, index) => {
                        row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

                        columns.forEach((columnName, currentColumnIndex) => {
                            Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                                value: product[columnName],
                                fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                                border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                            });
                        });
                    });
                    offset--;
                }
            }).then(function () {
                // https://github.com/exceljs/exceljs#writing-xlsx
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Bills.xlsx');
                });
            });
            e.cancel = true;
        },

    }).dxDataGrid("instance");


    const getCaption = ({ id }) => `Products of Bill Number ${id} : `;

    const getProducts = (ids) => productArray.filter((product) => ids.includes(product.productId));


    


});


function deepMerge(target, source) {
    for (let key in source) {
        if (source.hasOwnProperty(key)) {
            if (typeof source[key] === 'object' && source[key] !== null && !Array.isArray(source[key])) {
                if (!target[key]) {
                    target[key] = {};
                }
                deepMerge(target[key], source[key]);
            } else if (!target.hasOwnProperty(key)) {
                target[key] = source[key];
            }
        }
    }
}

