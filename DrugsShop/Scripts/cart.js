﻿$(document).ready(
    loadCart
);

function loadCart() {
    var products = document.getElementsByClassName("count");
    for (var p = 0; p < products.length; p++) {
        var textbox = products[p];
        var amount = localStorage[textbox.attributes["id"].value];

        if (amount != null)
            textbox.value = amount;
        else
            textbox.value = "0";
    }
}

function clickAdd(id) {
    var textbox = document.getElementById(id);
    textbox.value = +textbox.value + +1;
    localStorage[id] = textbox.value;
}

function getCartId() {
    var id = [];
    for (var i = 0; i < localStorage.length; i++)
    {
        if (i > 0) id.push(i);
    }
    return id;
}

function getCartAmount() {
    var amount = [];
    for (var i = 0; i < localStorage.length; i++) {
        if (i > 0) amount.push(localStorage[i]);
    }
    return amount;
}

//function openCart() {
//    var amount = [];
//    var id = [];

//    for (var i = 0; i < localStorage.length; i++) {
//        if (i > 0) id.push(localStorage.);
//        if (i > 0) amount.push(localStorage[i]);
//    }

//    var url = 'Cart/GetCart' + '?id=' + id + '&amount=' + amount;
//    window.location.replace(url);
//}

function getProducts() {
    var products = [];
    var product;
    var id;
    var amount;

    for (var key in localStorage) {
        id = key;
        if (id > 0) {
            amount = localStorage[key];
            product = { "Id": id, "Amount": amount };
            products.push(product);
        }
    }

    return products
}

function openCart() {
    var products = getProducts();

    $.ajax({
        url: 'Cart/Calculate',
        type: 'POST',
        data: JSON.stringify(products),
        contentType: "application/json;charset=utf-8"
    });
}

function checkout(status) {
    if (status == 2)
    {
        var products = getProducts();

        $.ajax({
            url: 'Cart/Checkout',
            type: 'POST',
            data: JSON.stringify(products),
            contentType: "application/json;charset=utf-8"
        });
    }
    else
    {
        var url = 'User/Login'
        window.location.replace(url);
    }
}