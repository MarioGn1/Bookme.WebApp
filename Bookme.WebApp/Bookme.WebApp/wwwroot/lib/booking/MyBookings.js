let buttonSubbmitEl = document.getElementById('search');
let previousePageEl = document.getElementById('previouse');
let nextPageEl = document.getElementById('next');

let dateElement = document.getElementById('date');

let daysPerPage = 6;

previousePageEl.addEventListener('click', previouse);
nextPageEl.addEventListener('click', next);

function previouse(e) {
    let date = new Date(dateElement.value);
    date = date.addDays(-daysPerPage);
    dateString = `${date.getFullYear()}-${(date.getMonth() + 1).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}-${date.getDate().toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}`;
    dateElement.value = dateString

    buttonSubbmitEl.click();
}

function next(e) {
    let date = new Date(dateElement.value);
    date = date.addDays(daysPerPage);
    dateString = `${date.getFullYear()}-${(date.getMonth() + 1).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}-${date.getDate().toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}`;
    dateElement.value = dateString

    buttonSubbmitEl.click();
}


Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}