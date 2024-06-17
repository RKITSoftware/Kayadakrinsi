import { initializedHandler, contentReadyHandler, disposeHandler, optionChangedHandler, valueChangedHandler } from "../event.js";

$(() => {

    DevExpress.ui.dxFileUploader.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            accessKey: "k",
            elementAttr: {
                'area-label': "fileElement",
            },
            inputAttr: {
                'area-label': "fileInput",
            },
            onInitialized: initializedHandler,
            onContentReady: contentReadyHandler,
        }
    });


    const simpleFileUploader = $("#simpleFileUploader").dxFileUploader({
        accept: 'image/*',
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
        //height:"20vh",
        //width: "80vw",
        //value:"hello world",
        visible: true,
        hint: "Upload image",
        allowCanceling: true,
        uploadMode: "useForm",
        uploadMode: "useButtons",
        allowedFileExtensions: ['.jpg', '.jpeg', '.png'],
        invalidFileExtensionMessage: "File is not in proper file format (allowed extentions : .jpg, .jpeg)",
        maxFileSize: 1000000,
        minFileSize: 50000,
        invalidMaxFileSizeMessage: "Max file size is 1000kb",
        invalidMinFileSizeMessage: "Min file size is 50kb",
        multiple: true,
        name: "SimpleFileUploaderName",
        labelText: "Upload Image Here...",
        readyToUploadMessage: "File is ready to upload",
        selectButtonText: "Choose file",
        //showFileList: false,
        uploadAbortedMessage: "Upload aborted",
        uploadButtonText: "Submit",
        uploadedMessage: "File uploaded successfully",
        uploadFailedMessage: "Something went wrong",
        uploadMethod: "POST",
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
    }).dxFileUploader("instance");

    simpleFileUploader.on("beforeSend", function () {
        alert("File will be send");
    });


    const readOnlyFileUploader = $("#readOnlyFileUploader").dxFileUploader({
        accept: "image/*,application/pdf",
        readOnly: true,
        //isValid: false,
        //validationStatus: "invalid",
    }).dxFileUploader("instance");


    const disabledFileUploader = $("#disabledFileUploader").dxFileUploader({
        disabled: true,
        accept: ".xlsx",
        rtlEnabled: true,
    }).dxFileUploader("instance");


    const eventsFileUploader = $("#eventsFileUploader").dxFileUploader({
        tabIndex: 1,
        maxFileSize: 1000000,
        minFileSize: 50000,
        multiple: true,
        chunkSize: 1000,
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
        allowCanceling: true,
        //uploadMode: "useForm",
        uploadMode: "useButtons",
        //dialogTrigger:$("#simpleFileUploader"),
        dropZone: $("#headding"),
        abortUpload: function () {
            alert("aborted");
        },
        uploadFile: function (file, progressCallback) {
            console.log("upload file", file, progressCallback);
        },
        uploadCustomData: {
            from: "eventsFileUploader",
        },
        uploadChunk: function (file, uploadInfo) {
            console.log(file, uploadInfo);

            let bytesUploaded = uploadInfo.bytesUploaded;
            let blobSize = uploadInfo.chunkBlob.size;


            //const formData = new FormData();
            //formData.append("chunk", chunk);
            //formData.append("fileName", file.name);
            //formData.append("offset", offset);
            //formData.append("totalChunks", totalChunkCount);
            //formData.append("token", customChunkUploadWidget.option("uploadCustomData").token);

            return fetch(eventsFileUploader.option("uploadUrl"), {
                method: "POST",
                body: {
                    "BytesUploaded": bytesUploaded,
                    "BlobSize": blobSize,
                    "From": eventsFileUploader.option("uploadCustomData").from
                },
            }).then((response) => {
                //console.log(response);
                if (response.ok) {
                    DevExpress.ui.notify("Chunk uploaded", "success", 1);
                }
                else {
                    DevExpress.ui.notify({
                        message: "Chunk failed to upload",
                        type: "error",
                    });
                }
            });
        },
    }).dxFileUploader("instance");

    eventsFileUploader.on({
        //"beforeSend": function () {
        //    alert("File will be send");
        //},
        "dropZoneEnter": function (e) {
            //$(".dx-fileuploader-input-wrapper").css("border", "3px solid red");
            //var ele = $(e.dropZoneElement);
            //ele.css("background-color", "lightblue");
            console.log("Drop zone entered", e, e.dropZoneElement);
        },
        "dropZoneLeave": function () {
            console.log("Drop zone leaved");
        },
        "disposing": function () {
            disposeHandler();
            setTimeout(() => {
                eventsFileUploader.repaint();
            }, 2000);
        },
        "optionChanged": optionChangedHandler,
        "progress": function (e) {
            var bytesLoaded = e.bytesLoaded / 1000;
            var total = e.bytesTotal / 1000;
            console.log(e, eventsFileUploader.option("progress"));
            let element = document.getElementById("progessAdded");// $("#progessAdded");
            if (element == null) {
                var span = `<span id="progessAdded">file</span>`;
                $("#eventsFileUploader").append(span);
            }
            var data = `${bytesLoaded}/${total}kb`;
            document.getElementById("progessAdded").innerText = data;
        },
        "filesUploaded": function (e) {
            alert("File(s) uploaded successfully");
            console.log(eventsFileUploader.option("value"));
            document.getElementById("progessAdded").innerText = "";
        },
        "uploadAborted": function (e) {
            alert("Upload aborted" + e.file.name);
            eventsFileUploader.dispose();
        },
        "uploaded": function (e) {
            alert(e.file.name + " Uploaded");
            eventsFileUploader.upload();
        },
        "uploadError": function (e) {
            alert("Error uploading " + e.file.name);
            eventsFileUploader.abortUpload();
        },
        "uploadStarted": function (e) {
            alert("Started upload of " + e.file.name);
        },
        "valueChanged": function (e) {
            valueChangedHandler(e);
            var element = eventsFileUploader.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxFileUploader.getInstance(element));
            console.log("Instance : ", eventsFileUploader.instance());
        }
    });

});