
function addToCart() {
    var button = document.getElementById("AddToCart");
    //button.addEventListener("click", clickAdd(button.value))
}

function clickAdd(id) {
    var textbox = document.getElementById(id);
    textbox.value = Number(textbox.value) + Number(1);
}

function updateCart(id) {
    var textbox = document.getElementById(id);
    var amount = sessionStorage[id];
    if (amount == null)
        textbox.value = 0;
    else
        textbox.value = amount;
}


