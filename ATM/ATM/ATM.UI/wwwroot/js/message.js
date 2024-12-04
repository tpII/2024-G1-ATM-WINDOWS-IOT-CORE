const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.on("NavigateToView", function(viewName) {
    if (viewName === "Error") {
        window.location.href = "/Error"; // Redirigir a la vista A
    } else if (viewName === "Privacy") {
        window.location.href = "/Privacy"; // Redirigir a la vista B
    }
});

connection.start().catch(function(err) {
    return console.error(err.toString());
});
