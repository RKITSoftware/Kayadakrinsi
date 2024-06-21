import { stocks, menu } from './Data.js';

$(() => {

    const menus = $("#menu").dxMenu({
        items: menu,
        displayExpr: "name",
        selectByClick: true,
        selectionMode: 'single',
        onSelectionChanged: function (e) {
            const selectedItem = e.addedItems[0];

            if (selectedItem.id === '3_1') {
                // Example using DevExpress.ui.dxPopover directly
                DevExpress.ui.dxPopover($("#menu"), {
                    target: e.component.element(),
                    position: { my: "right", at: "right", of: "#menu" },
                    title: "Username",
                    shading: false,
                    width: 200,
                    contentTemplate: function () {
                        return $("<div>").append(`
                        <div style="text-align: center;">
                            <p>TestUser</p>
                        </div>
                    `);
                    }
                }).dxPopover("show");
            }

            if (selectedItem.id === '3_2') {
                // Example using dxPopover component with an existing element
                const popover = $("#popover").dxPopover({
                    target: selectedItem,
                    title: "PAN",
                    shading: false,
                    width: 200,
                    contentTemplate: function () {
                        return $("<div>").append(`
                        <div style="text-align: center;">
                            <p>ASEWE2134A</p>
                        </div>
                    `);
                    }
                }).dxPopover("instance");

                popover.show();
            }
        }
    }).dxMenu("instance");



});