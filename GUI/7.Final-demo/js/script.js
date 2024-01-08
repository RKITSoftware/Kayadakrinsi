import User from "./user.js";
import { $accountNo, $customerNameTxt, $date, $tradePartnerNameTxt, $actionScl, $amountAmnt, $remarkTxt, maxDate, isValidAccountNo, isValidCustomerName, isValidTrader } from './validation.js';

let dateArray = []
let debitArray = [];
let creditArray = [];

const URL = "https://jsonplaceholder.typicode.com/users";

const urls = ['https://kit.fontawesome.com/b5b5cd7730.js', 'https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js', 'https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js'];

const $customersTbl = $("#customersTbl");
const $togglerBtn = $("#togglerBtn");
const $submitBtn = $("#submitBtn");
const $resetBtn = $('#resetBtn');
const $validationMessage1 = $("#validationMessage1");
const $validationMessage2 = $("#validationMessage2");
const $validationMessage3 = $("#validationMessage3");
const $validationMessage4 = $("#validationMessage4");
const $validationMessage6 = $("#validationMessage6");
const $validationMessages = $(".validationMessages");
const $viewRecords = $("#viewRecords");
const $showData = $(".showData");
const $recordsTbl = $("#recordsTbl");

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
    var records = JSON.parse(localStorage.getItem('records') || '[]');
    records.push(user);
    localStorage.setItem('records', JSON.stringify(records));

    $.each(records, function () {
        dateArray.push(user.date);

        if (user.actionScl === "Debit") {
            debitArray.push(user.amountAmnt);
        }
        else {
            creditArray.push(user.amountAmnt);
        }
    })
}

// Function for insert data into records table of banking section
function appendRecords() {
    var records = JSON.parse(localStorage.getItem('records'));

    $.each(records, function (i, val) {
        $recordsTbl.append(`<tr class="showData">
        <td class="recordFeild">${val.accountNo}</td>
        <td class="recordFeild">${val.customerNameTxt}</td>
        <td class="recordFeild">${val.date}</td>
        <td class="recordFeild">${val.tradePartnerNameTxt}</td>
        <td class="recordFeild">${val.actionScl}</td>
        <td class="recordFeild">${val.amountAmnt}</td>
        <td class="recordFeild">${val.remarkTxt}</td>
    </tr>`)
    });
}

$(document).ready(function () {
    console.log(maxDate);

    // Toggler for menu button
    $togglerBtn.click(function () {
        $("#sideBar").toggle(750);
    })

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
        console.log('hello')
        if (!isValidAccountNo()) {
            $accountNo.addClass('error');
            $validationMessage1.show();
        }
        else {
            $accountNo.removeClass("error");
            $validationMessage1.hide();
        }
    })

    // To focusin/out on invalid customer name
    $customerNameTxt.keyup(function () {
        if (!isValidCustomerName()) {
            $customerNameTxt.addClass('error');
            $validationMessage2.show();
        }
        else {
            $customerNameTxt.removeClass('error');
            $validationMessage2.hide();
        }
    })

    // To focusin/out on invalid trader name
    $tradePartnerNameTxt.keyup(function () {
        if (!isValidTrader()) {
            $tradePartnerNameTxt.addClass('error');
            $validationMessage4.show();
        }
        else {
            $tradePartnerNameTxt.removeClass('error');
            $validationMessage4.hide();
        }
    })

    // Event handler for submit button
    $submitBtn.click(function (event) {
        event.preventDefault();

        if ($date.val() > maxDate) {
            $date.addClass('error');
            $validationMessage3.show();
        }
        else {
            $date.removeClass('error');
            $validationMessage3.hide();
            if ($amountAmnt.val() > 1000000 || $amountAmnt.val() <= 0) {
                $amountAmnt.addClass('error');
                $validationMessage6.show();
            }
            else {
                $amountAmnt.removeClass('error');
                $validationMessage6.hide();

                alert('Your data is inserted successfully 🎉');

                insertData();

                // Creates charts based on data stored in records (local storage variable)
                new Chart("myChart", {
                    type: "line",
                    data: {
                        labels: dateArray,
                        datasets: [{
                            data: debitArray,
                            borderColor: "red",
                            fill: false
                        },
                        {
                            data: creditArray,
                            borderColor: "green",
                            fill: false
                        }]
                    },
                    options: {
                        legend: { display: false }
                    }
                });
            }
        }
    })

    // On cancle hide error messeges
    $resetBtn.click(function () {
        $validationMessages.hide();
        $(".error").removeClass("error");
    })

    // Function for show data or hide data of records table
    $viewRecords.click(function () {
        $showData.remove();
        appendRecords();
        $recordsTbl.toggle(10, function () {
            $viewRecords.text($viewRecords.text() == "View records" ? "Hide records" : "View records");
        });
    })
})

// Fetches data from URL
async function getCustomerData() {
    const response = await fetch(URL);

    if (!response.ok) {
        throw new Error("Something is wrong!!");
    }

    const data = await response.json();

    return data;
}

// Insert data into customers table
getCustomerData()
    .then(myData => {
        for (let i in myData) {
            const target = myData[i];
            $customersTbl.append(`<tr class="customerData">
                <td class="customerFeild">${target["username"]}</td>
                <td class="customerFeild">${target["id"]}</td>
                <td class="customerFeild">${target["name"]}</td>
                <td class="customerFeild">${target["email"]}</td>
            </tr>`)
        }
    })
    .catch(error => {
        console.log(error);
    })

// Caches important data here important urls
caches.open('imp-links').then((cache) => {
    cache.addAll(urls)
        .then(() => {
            console.log("Data cached");
        })
        .catch((error) => {
            console.log(error);
        })
})

