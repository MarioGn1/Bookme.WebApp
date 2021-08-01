let buttonEl = document.getElementById('Chek_button');
let closeButtonEl = document.getElementById('Close_button');
let serviceId = document.getElementById('booking').value;
let calendarElement = document.getElementsByClassName('form-control')[0];
let hoursEl = document.getElementById('hours');
let hoursLableEl = document.getElementById('hours-label');
let legendEl = document.getElementsByClassName('legend')[0];
let postFormEl = document.getElementsByClassName('post-form')[0];

console.log(calendarElement.innerHTML)

calendarElement.addEventListener('input', enableCheck);
buttonEl.addEventListener('click', clicked);
hoursEl.addEventListener('click', bookHour);
closeButtonEl.addEventListener('click', closeForm);

function closeForm(e) {
    postFormEl.classList.add('d-none')
    hoursEl.classList.remove('d-none')
    hoursLableEl.classList.remove('d-none')
}

function bookHour(e) {
    if (e.target.classList.contains('hour')) {
        let hourFieldEl = document.getElementById('hour-confirmation');
        let button = e.target;

        hourFieldEl.value = calendarElement.value + ' ' + button.innerHTML;

        postFormEl.classList.remove('d-none')
        hoursEl.classList.add('d-none');
        hoursLableEl.classList.add('d-none');
    }
}

function enableCheck() {
    buttonEl.classList.remove('d-none');
    legendEl.classList.add('d-none');
    postFormEl.classList.add('d-none');
    hoursEl.innerHTML = '';
}

function clicked(e) {
    buttonEl.classList.add('d-none');

    let date = document.getElementById('date').value;

    //let now = new Date(date)
    //let now2 = new Date();

    //console.log(date)
    //console.log(now.getMonth())
    //console.log(now.getDate())
    //console.log(now < now2)

    hoursEl.innerHTML = '';

    let url = `/api/bookings/${serviceId}/${date} `;
    const httpRequest = new XMLHttpRequest();

    httpRequest.addEventListener('loadend', loadInfo);

    function loadInfo(e) {
        if (httpRequest.status === 200) {
            let data = JSON.parse(httpRequest.responseText);

            let bookedHoursTotals = [];
            if (!data.bookedHours.length) {
                console.log('empty')
            }
            else {
                bookedHoursTotals = createBookedHoursArr(data.bookedHours)
                console.log(bookedHoursTotals);
            }

            let serviceStart = data.ownerInfo.shiftStart.totalMinutes;
            let shiftEnd = data.ownerInfo.shiftEnd.totalMinutes;
            let serviceInterval = data.ownerInfo.serviceInterval

            let workingDay = data.ownerInfo.shiftEnd.totalMinutes - data.ownerInfo.shiftStart.totalMinutes;
            let numberOfPossibleServices = Math.round(workingDay / 50)

            let ulEl = document.createElement('div');

            for (var i = 0; i < numberOfPossibleServices; i++) {

                for (var j = 0; j < data.ownerInfo.breaks.length; j++) {

                    let curBreakStart = data.ownerInfo.breaks[j].breakStart.totalMinutes;
                    let curBreakEnd = data.ownerInfo.breaks[j].breakEnd.totalMinutes;
                    //console.log(curBreakStart)
                    if (serviceStart + serviceInterval > curBreakStart && serviceStart <= curBreakEnd) {
                        serviceStart = curBreakEnd;

                        let liEl = document.createElement('button');
                        arrangeButton(j, liEl, curBreakStart, curBreakEnd)
                        serviceStart = curBreakEnd;

                        ulEl.appendChild(liEl);
                        isBreak = true;
                        break;
                    }
                }

                let isBooked = false;

                for (var k = 0; k < bookedHoursTotals.length; k++) {
                    let currBookedHour = bookedHoursTotals[k]

                    if (currBookedHour.totalMins === serviceStart) {

                        let liEl = document.createElement('button');
                        arrangeButton(k, liEl, serviceStart, undefined, true)
                        serviceStart += serviceInterval;
                        ulEl.appendChild(liEl);
                        isBooked = true;
                        break;

                    }
                }
                console.log(serviceStart)
                console.log(isBooked)

                if (isBooked) {
                    continue;
                }

                if (serviceStart + serviceInterval > shiftEnd) {
                    break;
                }

                let liEl = document.createElement('button');
                arrangeButton(i, liEl, serviceStart)
                serviceStart += serviceInterval

                ulEl.appendChild(liEl);
            }
            hoursEl.appendChild(ulEl);
        }
        else {
            console.log('error')
        }
    }



    hoursEl.classList.remove('d-none');
    hoursLableEl.classList.remove('d-none');
    legendEl.classList.remove('d-none');

    httpRequest.open('GET', url);
    httpRequest.send();
    e.preventDefault();
}

function createBookedHoursArr(boekedHours) {
    let bookedHoursTotal = [];
    for (var i = 0; i < boekedHours.length; i++) {
        let currHours = new Date(boekedHours[i].date).getHours();
        let currMins = new Date(boekedHours[i].date).getMinutes();
        let totalMins = currHours * 60 + currMins;
        let duration = boekedHours[i].duration;
        bookedHoursTotal.push({ totalMins: totalMins, duration: duration });
    }
    
    return bookedHoursTotal
}

function hourFormat(minutes) {
    return `${Math.floor(minutes / 60).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}:${Math.round(((minutes / 60) - Math.floor(minutes / 60)) * 60).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })}`
}

function arrangeButton(num, liEl, Start, End, isBooked) {
    liEl.classList.add('btn');
    liEl.classList.add('m-3');
    liEl.classList.add('col-3');
    if (isBooked) {
        liEl.id = 'busy-' + num;
        liEl.classList.add('btn-danger');
        liEl.innerText = hourFormat(Start);
    }
    else if (End === undefined) {
        liEl.id = num;
        liEl.classList.add('hour');
        liEl.classList.add('btn-success');
        liEl.innerText = hourFormat(Start);
    }
    else {
        liEl.id = 'break-' + num;
        liEl.classList.add('btn-warning');
        liEl.innerText = `${hourFormat(Start)} - ${hourFormat(End)}`;
    }
}