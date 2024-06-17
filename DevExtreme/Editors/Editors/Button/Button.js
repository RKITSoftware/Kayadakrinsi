import { initializedHandler, contentReadyHandler, disposeHandler, optionChangedHandler } from "../event.js";

$(() => {

    DevExpress.ui.dxButton.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            height: 40,
            width: 130,
            elementAttr: {
                'area-label': "ButtonElement",
            },
            onInitialized: initializedHandler,
            onContentReady: contentReadyHandler,

        }
    });


    const normalContainedButton = $("#normalContainedButton").dxButton({
        stylingMode: "contained",
        type: "normal",
        text: "Contained",
        tabIndex: 2,
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
    }).dxButton("instance");


    const normalOutlinedButton = $("#normalOutlinedButton").dxButton({
        stylingMode: "outlined",
        type: "normal",
        text: "Outlined",
        tabIndex: 1,
    }).dxButton("instance");


    const normalTextButton = $("#normalTextButton").dxButton({
        stylingMode: "text",
        type: "normal",
        text: "Text",
    }).dxButton("instance");


    const defualtContainedButton = $("#defualtContainedButton").dxButton({
        stylingMode: "contained",
        type: "default",
        text: "Contained",
    }).dxButton("instance");

    defualtContainedButton.on("click", function () {
        var element = defualtContainedButton.element();
        console.log("Element : ", element);
        console.log("Get instance : ", DevExpress.ui.dxButton.getInstance(element));
        console.log("Instance : ", defualtContainedButton.instance());
    });


    const defualtOutlinedButton = $("#defualtOutlinedButton").dxButton({
        stylingMode: "outlined",
        type: "default",
        text: "Outlined",
    }).dxButton("instance");


    const defualtTextButton = $("#defualtTextButton").dxButton({
        stylingMode: "text",
        type: "default",
        text: "Text",
        template: function (data, container) {
            var $icon = $("<i>").addClass("dx-icon-minus");
            var $text = $("<span>").text("Remove").addClass("button-text");
            container.append($icon).append($text);
        },
        onClick: function () {
            defualtTextButton.option("template", function (data, container) {
                var $text = $("<span>").text(" Remove").addClass("button-text");
                container.append($text);
            })
        }
    }).dxButton("instance");


    const successContainedButton = $("#successContainedButton").dxButton({
        stylingMode: "contained",
        type: "success",
        text: "Contained",
        icon: "trash",
    }).dxButton("instance");

    successContainedButton.option({
        onClick: function () {
            successContainedButton.dispose();
            setTimeout(() => {
                successContainedButton.repaint();
            }, 3000);
        },
        onDisposing: disposeHandler,
    });

    const successOutlinedButton = $("#successOutlinedButton").dxButton({
        stylingMode: "outlined",
        type: "success",
        text: "Outlined",
    }).dxButton("instance");


    const successTextButton = $("#successTextButton").dxButton({
        stylingMode: "text",
        type: "success",
        text: "Text",
    }).dxButton("instance");


    const dangerContainedButton = $("#dangerContainedButton").dxButton({
        stylingMode: "contained",
        type: "danger",
        text: "Contained",
    }).dxButton("instance");

    dangerContainedButton.on({
        "disposing": disposeHandler,
        "optionChanged": optionChangedHandler,
    });


    const dangerOutlinedButton = $("#dangerOutlinedButton").dxButton({
        stylingMode: "outlined",
        type: "danger",
        text: "Outlined",
        icon: "add",
        rtlEnabled: true,
    }).dxButton("instance");


    const dangerTextButton = $("#dangerTextButton").dxButton({
        stylingMode: "text",
        type: "danger",
        text: "Text",
        disabled: true,
    }).dxButton("instance");


    const backButton = $("#backButton").dxButton({
        stylingMode: "contained",
        type: "back",
        text: "Back",
        accessKey: "B",
        visible: true,
    }).dxButton("instance");

    backButton.on("click", function () {
        window.location.replace("https://localhost:44393/Index.html");
    });

});