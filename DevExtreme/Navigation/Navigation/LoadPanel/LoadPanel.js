$(() => {

    const states = [
        "Andhra Pradesh",
        "Arunachal Pradesh",
        "Assam",
        "Bihar",
        "Chhattisgarh",
        "Goa",
        "Gujarat",
        "Haryana",
        "Himachal Pradesh",
        "Jharkhand",
        "Karnataka",
        "Kerala",
        "Madhya Pradesh",
        "Maharashtra",
        "Manipur",
        "Meghalaya",
        "Mizoram",
        "Nagaland",
        "Odisha",
        "Punjab",
        "Rajasthan",
        "Sikkim",
        "Tamil Nadu",
        "Telangana",
        "Tripura",
        "Uttar Pradesh",
        "Uttarakhand",
        "West Bengal"
    ];


    const selectBox = $("#selectBox").dxSelectBox({
        onOpened(e) {
            panel.show();
            /*panel.toggle();*/
        },
        onClosed(e) {
            selectBox.option("dataSource", 0);
        },
    }).dxSelectBox("instance");


    const panel = $("#loadPanel").dxLoadPanel({
        animation: {
            show: { type: 'fadeIn', from: 0, to: 1, duration: 700 },
        },
        //container:"#selectBox",
        closeOnOutsideClick: true,
        focusStateEnabled: true,
        hoverStateEnabled: true,
        height: 50,
        hint: "Please wait while loading",
        indicatorSrc: "https://www.icegif.com/wp-content/uploads/2023/07/icegif-1262.gif",
        minHeight: 30,
        minWidth: 30,
        maxHeight: "100vh",
        maxWidth: "100vw",
        position: { of: '.dx-popup-content' },
        shadingColor: 'rgba(0,0,0,0.4)',
        visible: false,
        showIndicator: true,
        showPane: true,
        shading: true,

        onContentReady(e) {
            console.log("Load panel content is ready",e);
        },
        onHidden(e) {
            console.log("Load panel is hidden",e);
        },
        onHiding(e) {
            console.log("Load panel is hiding",e);
        },
        onInitialized(e) {
            console.log("Load panel is initialized",e);
        },
        onOptionChanged(e) {
            console.log("Load panel option changed",e);
        },
        onShowing(e) {
            console.log("Load panel is showing",e);
        },
        onShown() {
            setTimeout(() => {
                selectBox.option("dataSource", states);
                panel.hide();
            }, 2000);
        },
        onOptionChanged(e) {
            console.log(e);
        }
    }).dxLoadPanel("instance");

});