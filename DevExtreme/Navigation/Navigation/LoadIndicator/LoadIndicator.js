$(() => {

    DevExpress.ui.dxLoadIndicator.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            height: 30,
            width: 30,
        }
    });


    const indicator = $("#indicator").dxLoadIndicator({

    }).dxLoadIndicator("instance");


    const normalIndicator = $("#normalIndicator").dxLoadIndicator({
        indicatorSrc: "https://www.icegif.com/wp-content/uploads/2023/07/icegif-1262.gif",
        width: 70,
        rtlEnabled: true,
    }).dxLoadIndicator("instance");


    const button = $('#button').dxButton({
        text: 'Send',
        height: 40,
        width: 180,
        template(data, container) {
            $(`<div class='button-indicator'></div><span class='dx-button-text'>${data.text}</span>`).appendTo(container);
            buttonIndicator = container.find('.button-indicator').dxLoadIndicator({
                visible: false,
            }).dxLoadIndicator('instance');
        },
        onClick(data) {
            data.component.option('text', 'Sending');
            buttonIndicator.option('visible', true);
            setTimeout(() => {
                buttonIndicator.option('visible', false);
                data.component.option('text', 'Send');
            }, 2000);
        },
    }).dxButton("instance");


    const heightBox = $("#heightBox").dxNumberBox({
        min: normalIndicator.option("height"),
        max: 50,
        value: normalIndicator.option("height"),
        showSpinButtons: true,
        onValueChanged(e) {
            normalIndicator.option("height", e.value);
        },
    }).dxNumberBox("instance");


    const widthBox = $("#widthBox").dxNumberBox({
        min: normalIndicator.option("width"),
        max: 150,
        value: normalIndicator.option("width"),
        showSpinButtons: true,
        onValueChanged(e) {
            normalIndicator.option("width", e.value);
        },
    }).dxNumberBox("instance");


});