let editEl = document.getElementById('edit')

let editButtonEl = document.getElementById('btn-edit')
let saveButtonEl = document.getElementById('save')
let closeButtonEl = document.getElementById('close')


editButtonEl.addEventListener('click' , enableForm)
closeButtonEl.addEventListener('click' , disableForm)

function enableForm(e) {

    editButtonEl.classList.add('d-none');
    closeButtonEl.classList.remove('d-none');
    saveButtonEl.classList.remove('d-none');

    editEl.removeAttribute('disabled');

    e.preventDefault();
}

function disableForm(e) {

    location.reload()

    e.preventDefault();
}