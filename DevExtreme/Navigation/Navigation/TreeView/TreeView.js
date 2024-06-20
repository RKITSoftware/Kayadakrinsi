import { bikes, bikesH } from "./Data.js";

$(() => {

    DevExpress.ui.dxTreeView.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            height: "70vh",
            width: "40vw",
        }
    });


    const plainTree = $("#plainTree").dxTreeView({

        //items: bikes,
        dataStructure: "plain",
        parentIdExpr: "categoryId",
        displayExpr: "name",
        createChildren: function (parentNode) {
            let parentId = parentNode ? parentNode.itemData.id : null;
            var d = $.Deferred();
            let arr = bikes.filter(val => val.categoryId == parentId);
            return d.resolve(arr);
        },

        disabledExpr: "isAvailable",
        expandAllEnabled: true,
        keyExpr: "id",
        noDataText: "Something went wrong",
        rtlEnabled: true,
        scrollDirection: "both",
        searchEnabled: true,
        searchExpr: "name",
        searchMode: "contains", // 'contains' | 'startswith' | 'equals'
        searchTimeout: 500,
        selectByClick: true,
        selectedExpr: "price",
        selectionMode: "multiple", // 'multiple' | 'single'
        selectNodesRecursive: false,
        showCheckBoxesMode: "selectAll", // 'none' | 'normal' | 'selectAll'
        useNativeScrolling: false,

        onContentReady: function (e) {
            console.log("Plain tree content ready", e);
            console.log("Selected nodes:", e.component.getSelectedNodes());
            console.log("Plain nodes:", e.component.getNodes());
        },
        onInitialized: function (e) {
            console.log("Plain tree initialized", e);
        },

    }).dxTreeView("instance");

    plainTree.option({
        onItemClick: function (e) {
            console.log("Item clicked in plain tree", e);
            plainTree.selectItem(e.itemData);
        },
        onItemCollapsed: function (e) {
            console.log("Item collapsed in plain tree", e);
        },
        onItemContextMenu: function (e) {
            console.log("Context menu called on item in plain tree", e);
        },
        onItemExpanded: function (e) {
            console.log("Item expanded in plain tree", e);
        },
        onItemHold: function (e) {
            console.log("Item held in plain tree", e);
            plainTree.scrollToItem(e.itemData);
        },
        onItemRendered: function (e) {
            console.log("Item rendered in plain tree", e);
            plainTree.updateDimensions();
            plainTree.selectAll();
        },
        onItemSelectionChanged: function (e) {
            console.log("Item selection changed in plain tree", e);
            console.log("Plain selected nodes", plainTree.getSelectedNodes());
        },
        onSelectAllValueChanged: function (e) {
            console.log("Select all value changed in plain tree", e);
        },
        onSelectionChanged: function (e) {
            console.log("Selection changed in plain tree", e);
            //plainTree.unselectAll();
        }
    });


    const hierarchicalTree = $("#hierarchicalTree").dxTreeView({

        dataSource: bikesH,
        //dataStructure:"tree",
        displayExpr: "name",

        itemTemplate: function (itemData) {
            const name = `<span style="vertical-align: middle;">${itemData.name}</span>`;
            if (itemData.img) {
                return `<img src="${itemData.img}" style="width: 30px; height: auto; margin-right: 10px;">${name}`;
            }
            return `${name}`;
        },
        disabledExpr: "isAvailable",
        expandAllEnabled: true,
        //expandedExpr: "items",
        expandEvent: "click", // 'dblclick' | 'click' 
        expandNodesRecursive: true,
        scrollDirection: "both",
        searchEnabled: true,
        searchExpr: ["name", "price"],
        searchMode: "startswith", // 'contains' | 'startswith' | 'equals'
        searchTimeout: 700,
        selectByClick: true,
        selectedExpr: "price",
        selectionMode: "multiple", // 'multiple' | 'single'
        selectNodesRecursive: false,
        showCheckBoxesMode: "selectAll", // 'none' | 'normal' | 'selectAll'

        onContentReady: function (e) {
            console.log("Hierarchical tree content ready", e);
            console.log("Selected node keys:", e.component.getSelectedNodeKeys());
            console.log("Hierarchical nodes:", e.component.getNodes());
        },
        onInitialized: function (e) {
            console.log("Hierarchical tree initialized", e);
        },
       
    }).dxTreeView("instance");

    hierarchicalTree.option({
        onItemClick: function (e) {
            console.log("Item clicked in hierarchical tree", e);
            hierarchicalTree.selectItem(e.itemData);
        },
        onItemCollapsed: function (e) {
            console.log("Item collapsed in hierarchical tree", e);
        },
        onItemContextMenu: function (e) {
            console.log("Context menu called on item in hierarchical tree", e);
            console.log("Hierarichal datasource",e.component.getDataSource());
        },
        onItemExpanded: function (e) {
            console.log("Item expanded in hierarchical tree", e);
        },
        onItemHold: function (e) {
            console.log("Item held in hierarchical tree", e);
            hierarchicalTree.scrollToItem(e.itemData);
        },
        onItemRendered: function (e) {
            console.log("Item rendered in hierarchical tree", e);
            hierarchicalTree.updateDimensions();
            hierarchicalTree.unselectAll();
        },
        onItemSelectionChanged: function (e) {
            console.log("Item selection changed in hierarchical tree", e);
            console.log("Hierarichal selected nodes",hierarchicalTree.getSelectedNodes());
        },
        onSelectAllValueChanged: function (e) {
            console.log("Select all value changed in hierarchical tree", e);
        },
        onSelectionChanged: function (e) {
            console.log("Selection changed in hierarchical tree", e);
        }

    });


});