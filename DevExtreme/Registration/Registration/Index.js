import { genders, countries } from "./Data.js";


$(() => {

    let group = "passowrdValidation";
    let eyeOpen = 'data:image/png;base64,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';
    let eyeClose = 'data:image/png;base64,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';
    let age;
    let entry = 1;

    const changePasswordMode = function (name, button) {
        const editor = $(name).dxTextBox('instance');
        editor.option('mode', editor.option('mode') === 'text' ? 'password' : 'text');
        const btn = $(button).dxButton('instance');
        btn.option('icon', btn.option("icon") === eyeOpen ? eyeClose : eyeOpen);
    };

    const preventEvents = function (e) {
        e.event.preventDefault();
        alert(`Password can't be ${e.event.type}!'`);
    };


    const name = $("#name").dxTextBox({
        onValueChanged: function (e) {
            revalidate();
            var value = e.value.trim();
            name.option("value", value);
            //console.log(value, name.option("value"));
        }
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'pattern',
            pattern: /^[a-zA-Z ]*$/,
            reevaluate: true,
            message: 'Name must contain alphabates only!'
        }, {
            type: 'required',
            message: 'Name is required!'
        }]
    }).dxTextBox("instance");


    const gender = $("#gender").dxRadioGroup({
        items: genders,
        value: genders[0],
        layout: "horizontal",
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: 'Gender is not selected yet!'
        }]
    }).dxRadioGroup("instance");


    const birthDate = $("#birthDate").dxDateBox({
        type: "date",
        pickerType: "calendar",
        value: new Date(1999, 0, 1),
        max: new Date(),
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: "custom",
            validationCallback: function (options) {
                var value = new Date(options.value); // Ensure value is a Date object
                var today = new Date();

                // Calculate the year difference
                age = today.getFullYear() - value.getFullYear();

                // Adjust the year difference if the birthdate hasn't occurred yet this year
                var monthDifference = today.getMonth() - value.getMonth();
                if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < value.getDate())) {
                    age--;
                }

                // Check if the age is 18 or older
                return age >= 18;
            },
            message: "Eligable age is 18 years!"
        }, {
            type: "required",
            message: "Birthdate is not selected yet!"
        }]
    }).dxDateBox("instance");


    const country = $("#country").dxDropDownBox({
        dataSource: countries,
        displayExpr: 'name',
        displayValueFormatter: function (value) {
            return value;
        },
        valueExpr: 'name',
        contentTemplate: function (e) {
            var $list = $("<div>").dxList({

                searchEditorOptions: {
                    placeholder: 'Search country',
                    showClearButton: true,
                    inputAttr: { 'aria-label': 'Search' },
                },
                searchExpr: ["name"],
                searchEnabled: true,

                dataSource: e.component.getDataSource(),

                showSelectionControls: true,
                selectionMode: 'single',

                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.name);
                },

                onItemClick: function (selectedItems) {
                    country.option("value", selectedItems.itemData.name);
                    country.close();
                }
            });
            return $list;
        },
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: 'Country is not selected yet!',
        }],
    }).dxDropDownBox("instance");


    const countryCode = $("#countryCode").dxSelectBox({
        dataSource: countries,
        displayExpr: 'dialCode',
        valueExpr: 'dialCode',
        value: "+91",
        itemTemplate: function (itemData) {
            return $("<div>").text(itemData.dialCode + " " + itemData.name);
        },
        dropDownOptions: {
            width: "20vw",
        },
        wrapItemText: true,
        acceptCustomValue: true,
        searchEnabled: true,
        searchExpr: ["dialCode", "name"],
        searchMode: "startswith",
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: 'Country code is not selected yet!',
        }],
    }).dxSelectBox("instance");


    const contactNumber = $("#contactNumber").dxNumberBox({
        mode: "tel",
        value: "",
        placeholder: "Enter your 10 digit mobile number",
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'custom',
            validationCallback: function (options) {
                // Get the selected country code value
                const selectedCountryCode = countryCode.option("value");

                // Find the index of the country with the selected country code
                const index = countries.findIndex(country => country.dialCode === selectedCountryCode);
                
                // If the country code is found
                if (index !== -1) {
                    // Get the pattern corresponding to the found country
                    const pattern = countries[index].pattern;
                    
                    // Perform the validation using the pattern and the provided value
                    return pattern.test(options.value);
                }

                // Return false if the country code is not found
                return false;
            },
            message: "Enter valid mobile number according to selected country code",
        }]
    }).dxNumberBox("instance");


    const address = $("#address").dxTextArea({
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: 'Address is not specified yet!',
        }],
    }).dxTextArea("instance");


    const email = $("#email").dxTextBox({
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'email',
            message: 'Invalid email!'
        }]
    }).dxTextBox("instance");


    const password = $("#password").dxTextBox({
        mode: 'password',
        inputAttr: { 'aria-label': 'Password' },
        buttons: [{
            name: 'password',
            location: 'after',
            options: {
                icon: eyeOpen,
                stylingMode: 'text',
                elementAttr: {
                    "id": "showButton",
                },
                onClick: () => changePasswordMode('#password', "#showButton"),
            },
        }],
        onValueChanged: revalidate,
        onCut: preventEvents,
        onCopy: preventEvents,
        onPaste: preventEvents,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: 'Password is required',
        },
        {
            type: 'pattern',
            pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/,
            message: 'Must contain at least a number, a uppercase, a lowercase letter, and at least 8 characters',
        }],
    }).dxTextBox("instance");


    const confirmPassword = $("#confirmPassword").dxTextBox({
        mode: 'password',
        inputAttr: { 'aria-label': 'Password' },
        buttons: [{
            name: 'confirmPassword',
            location: 'after',
            options: {
                //icon: 'add',
                icon: eyeOpen,
                stylingMode: 'text',
                elementAttr: {
                    "id": "showButtonCpassword",
                },
                onClick: () => changePasswordMode('#confirmPassword', "#showButtonCpassword"),
            },
        }],
        onValueChanged: revalidate,
        onCut: preventEvents,
        onCopy: preventEvents,
        onPaste: preventEvents,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'compare',
            comparisonTarget() {
                const val = password.option('value');
                return val;
            },
            comparisonType: "===",
            message: "'Password' and 'Confirm Password' do not match.",
        }],
    }).dxTextBox("instance");


    const picture = $("#picture").dxFileUploader({
        accept: 'image/*',
        uploadMode: 'useForm',
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
        allowedFileExtensions: ['.jpg', '.jpeg', '.png'],
        invalidFileExtensionMessage: "File is not in proper file format (allowed extentions : .jpg, .jpeg, .png)",
        maxFileSize: 100000,
        invalidMaxFileSizeMessage: "Max file size is 100kb",
    }).dxFileUploader("instance");


    const terms = $("#terms").dxCheckBox({
        text: "I read all terms and conditions.",
        value: true,
        onValueChanged: revalidate,
    }).dxValidator({
        validationGroup: group,
        validationRules: [{
            type: 'required',
            message: "Terms and conditions aren't agreed yet!",
        }],
    }).dxCheckBox("instance");

    var callbacks = [];

    var revalidate = function () {
        callbacks.forEach(func => {
            func();
        });
    };


    $("#validator").dxValidator({
        validationGroup: group,
        adapter: {
            getValue: function () {
                return phone.option("value") || email.option("value");
            },
            applyValidationResults: function (e) {
                $("#contacts").css({ "border": e.isValid ? "none" : "1px solid red" });

            },
            validationRequestsCallbacks: callbacks
        }
    });


    const submit = $("#submit").dxButton({
        text: "Submit",
        stylingMode: "contained",
        type: "default",
        //useSubmitBehavior:true,
        onClick: function () {
            const { isValid } = DevExpress.validationEngine.validateGroup(group);
            if (isValid) {

                var information = [{
                    "Name": name.option("value"),
                    "Gender": gender.option("value"),
                    "BirthDate": birthDate.option("value"),
                    "Age": age,
                    "Country": country.option("value"),
                    "Contact number": countryCode.option("value") + contactNumber.option("value"),
                    "Address": address.option("value"),
                    "Email": email.option("value"),
                    "Password": password.option("value"),
                    "Profile picture": picture.option("value"),
                }];

                sessionStorage.setItem(`Entry${entry}`, JSON.stringify(information));

                // Retrieve and parse the information from session storage
                var retrievedInformation = JSON.parse(sessionStorage.getItem(`Entry${entry}`));

                console.log(retrievedInformation);

                alert("Registration completed successfully.");

                picture.option("value", "");
                document.getElementById("registratiopnForm").reset();
                entry++;
            }
            else {
                alert("Invalid details are found, please enter valid details.");
            }
        }
    }).dxButton("instance");


    //$("#summary").dxValidationSummary({
    //    validationGroup: group
    //});

});
