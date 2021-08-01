let buttonEl = document.getElementById('Chek_button');
let closeButtonEl = document.getElementById('Close_button');
let serviceId = document.getElementById('booking').value;
let calendarElement = document.getElementsByClassName('form-control')[0];
let hoursEl = document.getElementById('hours');
let legendEl = document.getElementsByClassName('legend')[0];
let postFormEl = document.getElementsByClassName('post-form')[0];

calendarElement.addEventListener('input', enableCheck);
buttonEl.addEventListener('click', clicked);
hoursEl.addEventListener('click', bookHour);
closeButtonEl.addEventListener('click', closeForm);

function closeForm(e) {
    postFormEl.classList.add('d-none')
    hoursEl.classList.remove('d-none')
}

function bookHour(e) {
    if (e.target.classList.contains('hour')) {
        let button = e.target;
        postFormEl.classList.remove('d-none')
        hoursEl.classList.add('d-none');
        button.classList.remove('btn-success');
        button.classList.add('btn-danger');
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
            if (!data.bookedHours.length) {
                console.log('empty')
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
                    console.log(curBreakStart)
                    if (serviceStart + serviceInterval > curBreakStart && serviceStart <= curBreakEnd) {
                        serviceStart = curBreakEnd;

                        let liEl = document.createElement('button');
                        arrangeButton(j, liEl, curBreakStart, curBreakEnd)
                        serviceStart = curBreakEnd;

                        ulEl.appendChild(liEl);

                        break;
                    }
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
    legendEl.classList.remove('d-none');

    httpRequest.open('GET', url);
    httpRequest.send();
    e.preventDefault();
}

function hourFormat(minutes) {
    return `${Math.floor(minutes / 60)}:${Math.round(((minutes / 60) - Math.floor(minutes / 60)) * 60)}`
}

function arrangeButton(num, liEl, Start, End) {
    liEl.classList.add('btn');
    liEl.classList.add('m-3');
    liEl.classList.add('col-3');
    if (End === undefined) {
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