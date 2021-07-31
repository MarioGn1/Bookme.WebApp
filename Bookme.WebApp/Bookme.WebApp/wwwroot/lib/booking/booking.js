let buttonEl = document.getElementById('Chek_button');
let serviceId = document.getElementById('booking').value;
let calendarElement = document.getElementsByClassName('form-control')[0];
let hoursEl = document.getElementById('hours');

calendarElement.addEventListener('click', enableCheck)

buttonEl.addEventListener('click', clicked)

function enableCheck() {
    buttonEl.classList.remove('d-none');
    hoursEl.innerHTML = '';
}

function clicked(e) {
    buttonEl.classList.add('d-none');

    let date = document.getElementById('date').value;
    
    hoursEl.innerHTML = '';    

    let url = `/api/bookings/${serviceId}/${date} `;
    const httpRequest = new XMLHttpRequest();

    httpRequest.addEventListener('loadend', loadInfo);

    function loadInfo(e) {
        if (httpRequest.status === 200) {
            let data = JSON.parse(httpRequest.responseText);
            if (data.bookedHours.length) {
                console.log('empty')
            }

            let shiftStart = data.ownerInfo.shiftStart.totalMinutes;
            let shiftEnd = `${data.ownerInfo.shiftEnd.hours}:${data.ownerInfo.shiftEnd.minutes}`;

            let workingDay = data.ownerInfo.shiftEnd.totalMinutes - data.ownerInfo.shiftStart.totalMinutes;
            let numberOfPossibleServices = Math.round(workingDay / 50)

            let ulEl = document.createElement('div');

            for (var i = 0; i < numberOfPossibleServices; i++) {
                let liEl = document.createElement('button');
                liEl.id = i;
                liEl.classList.add('btn');
                liEl.classList.add('m-3');
                liEl.classList.add('btn-success');
                liEl.classList.add('col-2');
                liEl.innerText = `${Math.floor(shiftStart / 60)}:${Math.round(((shiftStart / 60) - Math.floor(shiftStart / 60)) * 60)}`;
                shiftStart += data.ownerInfo.serviceInterval

                ulEl.appendChild(liEl);
            }
            hoursEl.appendChild(ulEl);

            console.log(numberOfPossibleServices)
            console.log(data.ownerInfo)
        }
        else {
            console.log('error')
        }
    }

    hoursEl.classList.remove('d-none');

    httpRequest.open('GET', url);
    httpRequest.send();
}