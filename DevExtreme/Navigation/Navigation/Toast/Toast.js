$(() => {

    const button = $("#button").dxButton({
        text: "Notify",
        onClick(e) {
            toast.show();
        },
    }).dxButton("instance");


    const toast = $("#toast").dxToast({
        accessKey: "T",
        animation: {
            show: { type: "fadeIn", from: 0, to: 1, duration: 100 }
        },
        closeOnClick: true,
        closeOnOutsideClick: true,
        closeOnSwipe: true,
        contentTemplate(e) {
            const message = toast.option("message");
            e[0].innerText = `✉ ${message}`;
        },
        deferRendering: true, // Default
        displayTime: 2000, // Default:4000(material), 2000
        focusStateEnabled: true,
        height: "8vh",
        hint: "Notification",
        message: "You just got a notification!",
        position: {
            of: ".container",
            at: { x: "right", y: "top" },
            offset: "-276 18",
        },
        rtlEnabled: false,
        shading: false,
        shadingColor: 'rgba(0,0,50,0.1)',
        type: 'custom', // 'info' | 'custom' | 'error' | 'success' | 'warning'

        onContentReady: function (e) {
            console.log("Content Ready", e);
        },
        onHidden: function (e) {
            console.log("Hidden", e);
        },
        onHiding: function (e) {
            console.log("Hiding", e);
        },
        onInitialized: function (e) {
            console.log("Initialized", e);
        },
        onOptionChanged: function (e) {
            console.log("Option Changed", e);
        },
        onShowing: function (e) {
            console.log("Showing", e);
        },
        onShown: function (e) {
            console.log("Shown", e);
        },


    }).dxToast("instance");


});