"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cardNotificationHub").build();

connection.on("CardDetected", function (isValid) {
    // document.getElementById("status").innerText = cardId
    console.log("Card detected");
    if(isValid)
    {
        window.location.replace("/EnterPin");
    }
    else
    {
        document.getElementById("main-message").innerText = "Ha ocurrido un error al leer la tarjeta";
        document.getElementById("side-message").innerText = "Intente acercarla nuevamente";
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});