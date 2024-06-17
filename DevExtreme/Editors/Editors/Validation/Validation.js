import { disposeHandler, initializedHandler, optionChangedHandler } from "../event.js";


$(() => {

    let group = "validationGroup";

    let isCancelled = false;

    const asyncValidation = $("#asyncValidation").dxFileUploader({
        chunkSize: 1000,
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
        multiple: true,
        abortUpload: function () {
            isCancelled = true;
        },
    }).dxValidator({
        elementAttr: {
            'area-label': "async-validation-element",
        },
        name: "async-validation-name",
        //height: 100,
        //width:100,
        validationRules: [{
            type: 'async',
            message: 'File upload cancelled',
            validationCallback(params) {
                const d = $.Deferred();
                setTimeout(() => {
                    if (isCancelled) {
                        d.reject();
                    } else {
                        d.resolve("File uploaded");
                    }
                    isCancelled = false;
                }, 500);
                return d.promise();
            },
        }],
        onValidated(e) {
            console.log(e);
            console.log("Broken rule : ", e.brokenRule);
            console.log("Broken rules : ", e.brokenRules);
            console.log("Complete : ", e.complete);
            console.log("IsValid : ", e.isValid);
            console.log("Pending rules : ", e.pendingRules);
            console.log("Status : ", e.status);
            console.log("Validation rules : ", e.validationRules);
            console.log("Value : ", e.value);
            console.log("Validation error : ", asyncValidation.option("validationError"));
            console.log("Validation errors : ", asyncValidation.option("validationErrors"));
        }
    }).dxFileUploader("instance");

    //const asyncValidation = $("#asyncValidation").dxTextBox({
    //    //chunkSize: 1000,
    //    //uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
    //    //multiple: true,
    //    //abortUpload: function () {
    //    //    isCancelled = true;
    //    //},
    //    placeholder:"Enter name here...",
    //}).dxValidator({
    //    elementAttr: {
    //        'area-label': "async-validation-element",
    //    },
    //    name: "async-validation-name",
    //    //height: 100,
    //    //width:100,   
    //    validationRules: [{
    //        type: 'async',
    //        message: 'Hello is not allowed',
    //        validationCallback(params) {
    //            const d = $.Deferred();
    //            setTimeout(() => {
    //                d.reject(params.value === "Hello");
    //            }, 500);
    //            return d.promise();
    //        },
    //    }],
    //    onValidated(e) {
    //        console.log(e);
    //        console.log("Broken rule : ", e.brokenRule);
    //        console.log("Broken rules : ", e.brokenRules);
    //        console.log("Complete : ", e.complete);
    //        console.log("IsValid : ", e.isValid);
    //        console.log("Pending rules : ", e.pendingRules);
    //        console.log("Status : ", e.status);
    //        console.log("Validation rules : ", e.validationRules);
    //        console.log("Value : ", e.value);
    //        console.log("Validation error : ", asyncValidation.option("validationError"));
    //        console.log("Validation errors : ", asyncValidation.option("validationErrors"));
    //    }
    //}).dxTextBox("instance");


    const customValidation = $("#customValidation").dxTextArea({
        placeholder: "Enter address here...",
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: "custom",
            validationCallback: function (options) {
                var value = options.value;
                // This pattern allows alphanumeric characters, spaces, commas, periods, hyphens, and apostrophes
                var addressPattern = /^[a-zA-Z0-9\s,'-\.#]*$/;
                return addressPattern.test(value);
            },
            message: "Please enter a valid address.Allowed characters are alphanumeric and ,'-\.#"
        }],
    }).dxTextArea("instance");


    const emailValidation = $("#emailValidation").dxTextBox({
        placeholder: "Enter email here...",
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: "email",
            message: "Invalid email",
        }, {
            type: "required",
            message: "Email is required",
        }],
    }).dxTextBox("instance");


    const numericValidation = $("#numericValidation").dxTextBox({
        onValueChanged: function (e) {
            console.log(e);
            var result = DevExpress.validationEngine.validateGroup(group);
            console.log(result);
        },
    }).dxValidator({
        validationRules: [{
            type: 'numeric',
            message: "Not a number",
        }],
    }).dxTextBox("instance");


    const compareValidation = $("#compareValidation").dxTextBox({

    }).dxValidator({
        validationRules: [{
            type: 'compare',
            comparisonTarget() {
                const numericValue = numericValidation.option("value");
                const compareValue = compareValidation.option("value");

                // Log the values for debugging
                console.log("Numeric Value: ", numericValue);
                console.log("Compare Value: ", compareValue);

                // Return the value to compare against
                return numericValue;
            },
            comparisonType: "===",
            message: "Value not matching",
        }],
    }).dxTextBox("instance");


    const patternValidation = $("#patternValidation").dxTextBox({
        placeholder: "Enter name here..."
    }).dxValidator({
        validationRules: [{
            type: 'pattern',
            pattern: /^[a-zA-Z]*$/,
            message: 'Name must contain alphabates only'
        }]
    }).dxTextBox("instance");


    const rangeValidation = $("#rangeValidation").dxNumberBox({
        showSpinButtons: true,
        //max:20,
    }).dxValidator({
        validationRules: [{
            type: 'range',
            min: 21,
            max: 100,
            message: 'Valid range is between 21-100',
        }],
    }).dxNumberBox("instance");


    const stringLengthValidation = $("#stringLengthValidation").dxTextBox({
        //maxLength:1,
        buttons: [{
            name: "infoButton",
            location: "after",
            options: {
                icon: "more",
                onClick(e) {
                    var validator = e.validationGroup.validators[6].instance();
                    var result = validator.validate();
                    console.log("Broken rule : ", result.brokenRule);
                    console.log("Broken rules : ", result.brokenRules);
                    console.log("Complete : ", result.complete);
                    console.log("IsValid : ", result.isValid);
                    console.log("Pending rules : ", result.pendingRules);
                    console.log("Status : ", result.status);
                    console.log("Validation rules : ", result.validationRules);
                    console.log("Value : ", result.value);

                }
            }
        }, {
            name: "disposeButton",
            location: "after",
            options: {
                icon: "trash",
                onClick() {
                    stringLengthValidation.dispose();
                }
            }
        }]
    }).dxValidator({
        onInitialized: initializedHandler,
        validationRules: [{
            type: 'stringLength',
            min: 2,
            message: 'Enter at least two symbols',
        }],
        onDisposing: function () {
            var element = this.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxValidator.getInstance(element));
            console.log("Instance : ", this.instance());
            var result = this.validate();
            console.log("Value ", result.value);
            disposeHandler();
        },
        onOptionChanged: optionChangedHandler,
    }).dxTextBox("instance");


    const requiredValidation = $("#requiredValidation").dxCheckBox({

    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'This field is required',
        }],
    }).dxCheckBox("instance");

    var callbacks = [];
    let group2 = "contactsValidation";
    var revalidate = function () {
        callbacks.forEach(func => {
            func();
        });
    };

    var phone = $("#phone").dxTextBox({
        placeholder: "Phone",
        onValueChanged: revalidate,
    }).dxTextBox("instance");

    var email = $("#email").dxTextBox({
        type: "email",
        placeholder: "Email",
        onValueChanged: revalidate,
    }).dxTextBox("instance");

    $("#validator").dxValidator({
        validationRules: [{
            type: "required",
            message: "Specify your phone or email."
        }],
        validationGroup: group2,
        adapter: {
            getValue: function () {
                return phone.option("value") || email.option("value");
            },
            applyValidationResults: function (e) {
                console.log(e);
                $("#contacts").css({ "border": e.isValid ? "none" : "1px solid red" });

            },
            validationRequestsCallbacks: callbacks
        }
    });

    $("#button").dxButton({
        text: "Contact me",
        onClick: function () {
            const { isValid } = DevExpress.validationEngine.validateGroup(group2);
            if (isValid) {
                // Submit values to the server
            }
        }
    });

    $("#summary").dxValidationSummary({
        validationGroup: group2
    });



});