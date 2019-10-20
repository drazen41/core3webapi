const uri = 'api/naselje';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPbrTextbox = document.getElementById('add-pbr');
    const addDrzavaDdl = document.getElementById('ddlDrzave');
    const item = {
        Naziv: addNameTextbox.value.trim(),
        PostanskiBroj: addPbrTextbox.value.trim(),
        DrzavaId: parseInt(addDrzavaDdl.value)
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = naselja.find(item => item.id === id);

    document.getElementById('edit-name').value = item.naziv;
    document.getElementById('edit-pbr').value = item.postanskiBroj;
    document.getElementById('edit-id').value = item.id;

    document.getElementById('edit-drzave').value = item.drzavaId;
      
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        Naziv: document.getElementById('edit-name').value.trim(),
        PostanskiBroj: document.getElementById('edit-pbr').value.trim(),
        DrzavaId: parseInt(document.getElementById('edit-drzave').value,10)
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'naselje' : 'naselja';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('naselja');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        //let isCompleteCheckbox = document.createElement('input');
        //isCompleteCheckbox.type = 'checkbox';
        //isCompleteCheckbox.disabled = true;
        //isCompleteCheckbox.checked = item.isComplete;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Ažuriraj';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Obriši';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        //let td1 = tr.insertCell(0);
        //td1.appendChild(isCompleteCheckbox);

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.naziv);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode1 = document.createTextNode(item.postanskiBroj);
        td2.appendChild(textNode1);

        let td3 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.drzava.naziv);
        td3.appendChild(textNode2);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    naselja = data;
}
function getDrzave(select) {
    fetch('api/drzava')
        .then(response => response.json())
        .then(data => displayDrzave(data,select))
        .catch(error => console.error('Unable to get items.', error));
}
function displayDrzave(data,select) {
    var ddl = document.getElementById(select);
    var opt = document.createElement('option');
    opt.value = "-1";
    opt.innerHTML = "--Odaberite--";
    //opt.setAttribute('selected', 'selected');
    ddl.appendChild(opt);
    data.forEach(item => {
        opt = document.createElement('option');
        opt.value = item.id;
        opt.innerHTML = item.naziv;
        ddl.appendChild(opt);
    });

}