import User from "./user.js";

const $accountNo = $("#accountNo");
const $customerNameTxt = $("#customerNameTxt");
const $date = $("#date");
const $tradePartnerNameTxt = $("#tradePartnerNameTxt");
const $actionScl = $("#actionScl");
const $amountAmnt = $("#amountAmnt");
const $remarkTxt = $("#remarkTxt");
const $validationMessage1 = $("#validationMessage1");
const $validationMessage2 = $("#validationMessage2");
const $validationMessage3 = $("#validationMessage3");
const $validationMessage4 = $("#validationMessage4");
const $validationMessage5 = $("#validationMessage5");
const $validationMessages = $(".validationMessages");
const $showData = $(".showData");
const $recordsTbl = $("#recordsTbl");
const $submitBtn=$("#submitBtn");

const d = new Date();
let date = d.getDate();
let month = d.getMonth() + 1;
let year = d.getUTCFullYear();
if (date < 10) {
    date = '0' + date;
}
if (month < 10) {
    month = '0' + month;
}
let maxDate = year + "-" + month + "-" + date;

// Validation function for account number
function isValidAccountNo() {
    let regex = /^[0-9]{11}$/;
    if ($accountNo.val().match(regex)) {
        return 1;
    }
    return 0;
}

// Validation function for customer name
function isValidCustomerName() {
    let regex = /^[a-zA-Z]{1,50}$/;
    if ($customerNameTxt.val().match(regex)){
        return 1;
    }
    return 0;
}

// Validation function for trader name
function isValidTrader() {
    let regex = /^[a-zA-Z]{1,50}$/;
    if ($tradePartnerNameTxt.val().match(regex)){
        return 1;
    }
    return 0;
}

// Function for inserting data into localstorage variable
function insertData() {

    const accountNo = $accountNo.val();
    const customerNameTxt = $customerNameTxt.val();
    const date = $date.val();
    const tradePartnerNameTxt = $tradePartnerNameTxt.val();
    const actionScl = $actionScl.val();
    const amountAmnt = $amountAmnt.val();
    const remarkTxt = $remarkTxt.val(); 

    const user = new User(accountNo, customerNameTxt, date, tradePartnerNameTxt, actionScl, amountAmnt, remarkTxt);
    var recordsForJqueryDemo = JSON.parse(localStorage.getItem('recordsForJqueryDemo') || '[]');
    recordsForJqueryDemo.push(user);
    console.log(recordsForJqueryDemo);

    localStorage.setItem('recordsForJqueryDemo', JSON.stringify(recordsForJqueryDemo));
}

// Function for insert data into records table
function appendRecords() {
    var recordsForJqueryDemo = JSON.parse(localStorage.getItem('recordsForJqueryDemo') || '[]');
    $.each(recordsForJqueryDemo, function (i, val) {
        $recordsTbl.append(`<tr class="row m-0 p-0" class="showData">
        <td class="col text-center">${val.accountNo}</td>
        <td class="col text-center">${val.customerNameTxt}</td>
        <td class="col text-center">${val.date}</td>
        <td class="col text-center">${val.tradePartnerNameTxt}</td>
        <td class="col text-center">${val.actionScl}</td>
        <td class="col text-center">${val.amountAmnt}</td>
        <td class="col text-center">${val.remarkTxt}</td>
    </tr>`)
    })
}

// To show inserted data into table on landing page
appendRecords();

$(document).ready(function () {

    // Function to restrict date picker to select past dates only 
    $date.click(function () {
        $date.attr("max", maxDate);
    })

    // Function to restrict amount limit
    $amountAmnt.click(function () {
        $amountAmnt.attr({ "min": 1, "max": 1000000 });
    })

    // To hide messages of error in begining
    $validationMessages.hide();

    // To focusin/out on invalid account number
    $accountNo.keyup(function () {
        if (!isValidAccountNo()) {
            $validationMessage1.show();
        }
        else {
            $validationMessage1.hide();
        }
    })

    // To focusin/out on invalid customer name
    $customerNameTxt.keyup(function () {
        if (!isValidCustomerName()) {
            $validationMessage2.show();
        }
        else {
            $validationMessage2.hide();
        }
    })

    // To focusin/out on invalid trader name
    $tradePartnerNameTxt.keyup(function () {
        if (!isValidTrader()) {
            $validationMessage4.show();
        }
        else {
            $validationMessage4.hide();
        }
    })

    // Event handler for submit button
    $submitBtn.click(function (event) {
        event.preventDefault();

        if ($date.val() > maxDate) {
            $validationMessage3.show();
        }
        else {
            $validationMessage3.hide();
            if ($amountAmnt.val() > 1000000 || $amountAmnt.val() <= 0) {
                $validationMessage5.show();
            }
            else {
                $validationMessage5.hide();

                alert('Your data is inserted successfully 🎉');
                insertData();
                $showData.remove();
                appendRecords();
            }
        }
    })
})
