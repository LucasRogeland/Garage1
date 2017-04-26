function search(str) {
    var xhttp;
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    } else {
        // code for IE6, IE5
        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            document.getElementById("autocomplete").innerHTML = xhttp.responseText;
        }
    };

    if (str.length !== 0) {
        console.log("Get");
        xhttp.open("GET", "/ParkedVehicles/SearchAj?License=" + str);
        xhttp.send();
    } else {
        console.log("Empty Get");
        xhttp.open("GET", "/ParkedVehicles/SearchAjAll");
        xhttp.send();
    }
}

function AdvacedSearch() {
    var inputs = document.getElementsByClassName("advanced-search-input");
    var Manufacturer = "";
    var Model = "";
    var Color = "";
    var vType = "";
    var time = "";

    for (var i = 0; i < inputs.length; i++){
        var attr = inputs[i].getAttribute("name");
        if (attr === "manuf") {
            Manufacturer = inputs[i].value;
        } else if (attr === "model") {
            Model = inputs[i].value;
        } else if (attr === "color") {
            Color = inputs[i].value;
        } else if (attr === "vType") {
            vType = inputs[i].value;
        } else if (attr === "time") {
            time = inputs[i].value;
        }
    }

    var xhttp;
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    } else {
        // code for IE6, IE5
        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            document.getElementById("autocomplete").innerHTML = xhttp.responseText;
        }
    };

    if (!(vType === "" && Manufacturer === "" && Model === "" && Color === "")) {
        xhttp.open("GET", "/ParkedVehicles/SearchAj?VehicleType=" + vType + "&Manufacturer=" + Manufacturer + "&VModel=" + Model + "&Color=" + Color);
        xhttp.send();
    } else {
        xhttp.open("GET", "/ParkedVehicles/SearchAjAll");
        xhttp.send();
    }

    


}

function toggleAdvancedSearch() {

    var advSearch = document.getElementById("advanced-search-cont");

    advSearch.classList.toggle("hidden");

    console.log("asdas");

    event.preventDefault();
}

function sort(val) {

    var inner = val.innerHTML.trim();

    var xhttp;
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    } else {
        // code for IE6, IE5
        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            document.getElementById("autocomplete").innerHTML = xhttp.responseText;
        }
    };

    if (inner.length !== 0) {

        if (val.className.indexOf("desc") !== -1) {
            console.log("Desc");
            xhttp.open("GET", "/ParkedVehicles/SortAj?SortBy=" + inner + "&Desc=true");
        } else {
            console.log("Not Desc");
            xhttp.open("GET", "/ParkedVehicles/SortAj?SortBy=" + inner + "&Desc=false");
        }
        
        xhttp.send();
    }
    
}