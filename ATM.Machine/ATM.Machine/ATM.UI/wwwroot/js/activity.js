let timeout;
const notification = document.getElementById("notification"); // Elemento de la alerta
const mainContent = document.getElementById("main-content"); // Contenedor principal de la página

function startInactivityTimer() {
    clearTimeout(timeout);
    timeout = setTimeout(async () => {
        // Notificar al backend sobre el timeout
        // await fetch('/api/session/timeout', { method: 'POST' });

        // // Ocultar el contenido principal
        // mainContent.classList.add("d-none");

        // // Mostrar la alerta
        // notification.classList.remove("d-none");
        // notification.textContent = "Por inactividad, se le redirigirá a la página de inicio.";
        
        clearTimeout(timeout); // Reinicia el temporizador

        // Muestra la notificación y redirige
        notification.classList.remove("d-none");
        notification.textContent = "Por inactividad, se le redirigirá a la página de inicio.";
        setTimeout(() => {
            window.location.href = '/Index'; // Cambiar por la ruta real de la página de inicio
        }, 3000); // Tiempo para mostrar la notificación
    }, 15000); // 15 segundos de inactividad
}

// Reiniciar el temporizador en caso de actividad
document.addEventListener("keypress", startInactivityTimer);
document.addEventListener("mousemove", startInactivityTimer);
document.addEventListener("click", startInactivityTimer);

startInactivityTimer();