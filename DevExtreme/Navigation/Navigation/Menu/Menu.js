import { cars } from "./Data.js";

$(() => {


    const menu = $("#menu").dxMenu({
        items: cars,
        displayExpr: "name",

        accessKey: "M",
        activeStateEnabled: false,
        hoverStateEnabled: true,
        focusStateEnabled: false,
        adaptivityEnabled: true,
        animation: {
            show: { type: 'fadeIn', from: 0, to: 1, duration: 700 },
        },
        disabledExpr: "disabled",
        hideSubmenuOnMouseLeave: true,
        hint: "Select car type",
        itemsExpr: "subCars",
        //orientation: "vertical", // 'horizontal' | 'vertical' Default:"horizontal"
        rtlEnabled: false,
        //selectByClick: true,
        selecteditem: cars[0].subCars[0],
        selectionMode: "single",
        showFirstSubmenuMode: "onHover", // 'onClick' | 'onHover' Default:'onClick'
        showSubmenuMode: "onClick", // 'onClick' | 'onHover' Default:'onHover'
        submenuDirection: 'leftOrTop',

        onContentReady: function (e) {
            console.log("Menu content is ready");
        },
        onDisposing: function (e) {
            console.log("Menu is being disposed");
        },
        onInitialized: function (e) {
            console.log("Menu is initialized");
        },
        onItemClick: function (e) {
            if (!e.itemData.disabled) {
                // Handle item click
                console.log("Item clicked: " + e.itemData.name, e);
            }
        },
        onItemContextMenu: function (e) {
            console.log("Item context menu triggered: " + e.itemData.name);
        },
        onItemRendered: function (e) {
            console.log("Item rendered: " + e.itemData.name);
        },
        onOptionChanged: function (e) {
            console.log("Option changed: " + e.name + ", new value: " + e.value);
        },
        onSelectionChanged: function (e) {
            console.log("Selection changed");
        },
        onSubmenuHidden: function (e) {
            console.log("Submenu hidden");
        },
        onSubmenuHiding: function (e) {
            console.log("Submenu hiding");
        },
        onSubmenuShowing: function (e) {
            console.log("Submenu showing");
        },
        onSubmenuShown: function (e) {
            console.log("Submenu shown");
        },

        //itemTemplate: function (itemData) {
        //    const name = `<span>${itemData.name}</span>`;
        //    if (itemData.icon) {
        //        const img = `<img src="${itemData.icon}" style="width: 15px; height: auto; margin-right: 10px;">`;
        //        return `${img} | ${name}`;
        //    }
        //    const img = `<span class="dx-icon-chevronright"></span>`;
        //    return `${name} ${img}`;
        //},

    }).dxMenu("instance");


});
