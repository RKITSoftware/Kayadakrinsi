$(() => {

    const button = $('#button').dxButton({
        text: 'Greet',
        height: 40,
        width: 180,
        onClick(data) {
            popup.show();
        },
    }).dxButton("instance");


    const popup = $("#popup").dxPopup({
        accessKey: "P",
        animation: {
            show: { type: 'fadeIn', from: 0, to: 1, duration: 700 },
        },
        closeOnOutsideClick: true,
        //container:"#button",
        contentTemplate: function () {
            const template = $('<div>').append(`
                        <div style="text-align: center; padding: 20px;">
                            <h2>Welcome!</h2>
                            <p>We are glad to have you here.</p>
                            <p>Have a great day!</p>
                            <img style="height:50%;width:50%;" src="https://media1.giphy.com/media/Xb7VYGVSzYMHV4pkHE/giphy.gif?cid=6c09b952j0sl9esy9fcajks1gawecrxw4talt9s25bljjbhl&ep=v1_internal_gif_by_id&rid=giphy.gif&ct=g"/>
                        </div>
                    `);

            template.dxScrollView({
                width: '100%',
                height: '100%',
            });

            return template;
        },
        deferRendering: true, // Default
        disabled: false, // Default
        dragEnabled: true,
        focusStateEnabled: true, // Default
        fullScreen: false, // Default
        height: "50vh",
        hint: "Greetings Popup",
        hoverStateEnabled: true,
        resizeEnabled: true,
        position: 'top',
        rtlEnabled: false,
        shadingColor: "rgba(0,0,100,0.1)",
        showCloseButton: true, // Default
        showTitle: true, // Default
        title: "Greetings...",
        toolbarItems: [{
            widget: 'dxButton',
            toolbar: 'bottom',
            location: 'after',
            options: {
                icon: 'email',
                stylingMode: 'outlined',
                text: 'Share',
                onClick() {
                    DevExpress.ui.notify("Greetings has been shared", 'success', 3000);
                    popup.hide();
                },
            },
        },],
        width: "30vw",

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
        onResize: function (e) {
            console.log("Resize", e);
        },
        onResizeEnd: function (e) {
            console.log("Resize End", e);
        },
        onResizeStart: function (e) {
            console.log("Resize Start", e);
        },
        onShowing: function (e) {
            console.log("Showing", e);
        },
        onShown: function (e) {
            console.log("Shown", e);
        },
        onTitleRendered: function (e) {
            console.log("Title Rendered", e);
        }

    }).dxPopup("instance");


});