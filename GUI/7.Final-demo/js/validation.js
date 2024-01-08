export const $accountNo = $("#accountNo");
export const $customerNameTxt = $("#customerNameTxt");
export const $date = $("#date");
export const $tradePartnerNameTxt = $("#tradePartnerNameTxt");
export const $actionScl = $("#actionScl");
export const $amountAmnt = $("#amountAmnt");
export const $remarkTxt = $("#remarkTxt");

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
export let maxDate = year + "-" + month + "-" + date;

// Validation function for account number
export function isValidAccountNo() {
    let regex = /[0-9]{11}/;
    console.log($accountNo.val().match(regex));
    if ($accountNo.val().match(regex)) {
        return 1;
    }
    return 0;
}

// Validation function for customer name
export function isValidCustomerName() {
    let regex = /^[a-zA-Z]{1,50}$/;

    if ($customerNameTxt.val().match(regex)) {
        return 1;
    }

    return 0;
}

// Validation function for trader name
export function isValidTrader() {
    let regex = /[a-zA-Z]{1,50}/;

    if ($tradePartnerNameTxt.val().match(regex)) {
        return 1;
    }

    return 0;
}

export * from './validation.js'