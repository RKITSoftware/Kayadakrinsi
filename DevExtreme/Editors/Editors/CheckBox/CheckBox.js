$(function () {

    try {

        // Sets defualtt options to all elements of specified type, order doesn't matter works same
        //DevExpress.ui.dxCheckBox.defaultOptions({
        //    width: "80vw",
        //    height: 30,
        //    visible: true,
        //});

        const checkBoxEnabled = $("#checkBoxEnabled").dxCheckBox({

            // Executed when element intialized using devExtreme
            onInitialized: function (e) {
                console.log("checkBoxEnabled initialized");
            },

            // Executes when HTML element's content is ready
            onContentReady() {
                console.log("checkBoxEnabled content ready");
            },
            
        }).dxCheckBox("instance");

        // Sets several properties
        checkBoxEnabled.option({

            // Shortcut key to access element overwrites if alredy exist special keys doesn't working for it
            accessKey: "C",  

            // sets height using number
            height: 30,

            value: true,

        });

        // Used for consistance view of UI to users without flickering,loades element onetime between beginUpdate and endUpdate which makes update procedure faster also useful while using asynchronous tasks

        // Indicates element beginUpdate     
        checkBoxEnabled.beginUpdate();

        checkBoxEnabled.option("visible", false);

        checkBoxEnabled.option("value", false);

        checkBoxEnabled.option("visible", true);

        // Indicates element is updated
        checkBoxEnabled.endUpdate();


        const checkBoxDisabled = $("#checkBoxDisabled").dxCheckBox({

            // sets width using function
            width: function () {
                return "80vw";
            },

            value: true,

            hint: "Checkbox Disabled",

            // disables element
            disabled: true,

            elementAttr: {
                'aria-label': 'Disabled',
            },

        }).dxCheckBox("instance");

        const checkBoxDisabled2 = $("#checkBoxDisabled").dxCheckBox({

            // sets width using function
            width: function () {
                return "80vw";
            },

            value: true,

            hint: "Checkbox Disabled",

            // disables element
            disabled: true,

            elementAttr: {
                'aria-label': 'Disabled2',
            },

        }).dxCheckBox("instance");


        // Sets defualtt options to all elements of specified type order doesn't matter works same
        DevExpress.ui.dxCheckBox.defaultOptions({
            width: "80vw",
            height: 30,
            visible: true,
        });


        const checkBoxChecked = $("#checkBoxChecked").dxCheckBox({

            //defualt true, specifies visibility of UI component
            visible: true,

            value: true,

            // Displays hint as tooltip
            hint: "Click to change value",

            // Executes on changing value
            onValueChanged() {
                // Repaints element's markup
                checkBoxChecked.repaint();
                console.log("checkBoxChecked value changed");
            }

        }).dxCheckBox("instance");

        // Gets HTML element on which we can perform DOM operation
        var elementCheckBoxChecked = checkBoxChecked.element();

        // Sets class of element retrived
        elementCheckBoxChecked.addClass("checkBox");

        // Sets style of retrived element
        elementCheckBoxChecked.css("border", "1px solid red");

        console.log(elementCheckBoxChecked);

        // Retrives instance of element
        var instance = DevExpress.ui.dxCheckBox.getInstance(elementCheckBoxChecked);

        console.log(instance.option("value"));


        const checkBoxUnchecked = $("#checkBoxUnchecked").dxCheckBox({

            value: false,

            name: "CheckBoxUnchecked",

            // defualtt true [removes active state effects by setting it false]
            activeStateEnabled: false,

            // defualtt true [disables hover effects by setting false]
            hoverStateEnabled: false,

            hint: "Checkbox will be dispose after 5sec by checking it",

            // defualtt true [doesn't focus on keyboard nevigation by setting it false]
            focusStateEnabled: false,

            // Executes before dispose
            onDisposing() {
                alert("Disposing checkBoxUnchecked");
            },

            // Executes on value change
            onValueChanged() {
                console.log("Disposing checkBoxUnchecked");
                setTimeout(function () {
                    // Disposes element
                    checkBoxUnchecked.dispose();
                }, 3000);
            }

        }).dxCheckBox("instance");


        const checkBoxTask1 = $("#checkBoxTask1").dxCheckBox({

            value: true,

            iconSize: 30,

            // Defualt false [feild's value can not be changed by user on setting true']
            readOnly: true,

            // Global attributes
            elementAttr: {
                'aria-label': 'Task two',
            },

        }).dxCheckBox("instance");

        // Though it's read only feild we can set properties value
        checkBoxTask1.option("value", null);


        const checkBoxTask2 = $("#checkBoxTask2").dxCheckBox({

            value: null,

            onValueChanged(d) {
                console.log("Label of checkBoxTask2's value using parameter passed after changing ", d.value);
                console.log("Label of checkBoxTask2's value using option after changing ",checkBoxTask2.option("text"));

                setTimeout(function () {
                    console.log(d);
                    checkBoxTask2.reset();
                    console.log("reset called",d);
                }, 2000);
                checkBoxTask2.off("focusIn");
            },

            // Shows validation status [Possible value : invalid,valid,pending] (case insensitive)
            validationStatus: "pending",

        }).dxCheckBox("instance");

        // Executes specified event and event handler
        checkBoxTask2.on("focusIn", function () {
            console.log("checkBoxTask2 focused in");
        });


        const checkBoxLabeled = $("#checkBoxLabeled").dxCheckBox({

            value: true,

            width: "98vw",

            text: "CheckBox",

            // Sets element's direction right to left defualtt false
            rtlEnabled: true,

            // Specifies the number of the element when the Tab key is used for navigating defualtt 0
            tabIndex: 2,

            onValueChanged() {
                setTimeout(function () {
                    console.log("checkBoxLabeled repainted");
                }, 3000);
            }

        }).dxCheckBox("instance");

        // Focuses on specified element
        checkBoxLabeled.focus();


        const checkBoxHandledOnChange = $("#checkBoxHandledOnChange").dxCheckBox({

            accessKey: "A",

            value: true,

            //  Performs operation specified when option's value is changed
            onOptionChanged: function (e) {
                console.log("Changed option :",e.name,", Changed value :",e.value);
            },

            // Performs operation specified when element's value is changed
            onValueChanged(data) {

                checkBoxLabeled.option("text", data.value);

                setTimeout(function () {
                    checkBoxLabeled.resetOption("text");
                }, 2000);

            },

            // Specifies the number of the element when the Tab key is used for navigating defualtt 0
            tabIndex: 1,

        }).dxCheckBox("instance");

        checkBoxHandledOnChange.option("width", "90vw");

        // Registers shortcut key to perform operations defined
        checkBoxHandledOnChange.registerKeyHandler("A", function () {
            checkBoxHandledOnChange.option("validationStatus", "Invalid");
            console.log("Key pressed : register key handler");
        });
    }
    catch (error) {
        throw error;
    }
});