"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cardNotificationHub").build();

const statusDiv = document.getElementById("status");

connection.on("CardDetected", function (isValid) {
    // if(isValid)
    // {
    //     window.location.replace("/");
    // }
    // else
    // {
    //     document.getElementById("main-message").innerText = "Ha ocurrido un error al leer la tarjeta";
    //     document.getElementById("side-message").innerText = "Intente acercarla nuevamente";
    // }
    if (isValid) {
        // Simula un registro exitoso
        statusDiv.className = "alert alert-success";
        statusDiv.textContent = "Tarjeta reconocida. Espere un momento...";
        setTimeout(() => {
            window.location.href = '/EnterPin'; // Cambiar a la página principal
        }, 2000);
    } else {
        // Simula un error
        statusDiv.className = "alert alert-danger";
        statusDiv.textContent = "Error: Tarjeta no reconocida. Por favor, intente nuevamente.";
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});