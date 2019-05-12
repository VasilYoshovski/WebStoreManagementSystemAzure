$(document).ready(function () {
    $("#createAddressBtn").click(function () {
        document.getElementById('labelNewAddressName').innerHTML = 'Enter address text:';
        document.getElementById('titleNewAddressName').innerHTML = 'Create address';

        document.getElementById('routeToAction').value = 'createAddress';
        document.getElementById('selectToAddTo').value = 'addressNameSelect';

        $('#myModal').modal('show')
    });

    $("#createCityBtn").click(function () {

        document.getElementById('labelNewAddressName').innerHTML = 'Enter city text:';
        document.getElementById('titleNewAddressName').innerHTML = 'Create city';

        document.getElementById('routeToAction').value = 'createCity';
        document.getElementById('selectToAddTo').value = 'cityNameSelect';

        $("#myModal").modal('show');
    });

    $("#createCountryBtn").click(function () {

        document.getElementById('labelNewAddressName').innerHTML = 'Enter country text:';
        document.getElementById('titleNewAddressName').innerHTML = 'Create country';

        document.getElementById('routeToAction').value = 'createCountry';
        document.getElementById('selectToAddTo').value = 'countryNameSelect';

        $("#myModal").modal('show');
    });
});

function addItemToSelect(target, text, value) {
    if (!target) {
        return;
    }
    else {

        select = document.getElementById(target);
        select.appendChild(new Option(text, value));
        select.value = value;
    }
}

function createFunction(target) {
    if (!target) {
        return;
    }
    else {

        var str = document.getElementById(target).value;

        var xhttp;
        if (str.length == 0) {
            return;
        }
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var selectToAdd = document.getElementById('selectToAddTo').value;
                addItemToSelect(selectToAdd, str, this.responseText);
            }
        };
        var route = document.getElementById('routeToAction').value;
        xhttp.open("GET", '../' + route + '/' + str, true);
        xhttp.send();
        $('#myModal').modal('hide');
    }
}