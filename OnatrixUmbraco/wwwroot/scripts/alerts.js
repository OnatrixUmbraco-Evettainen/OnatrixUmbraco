function showAlert(message, iconType) {
    Swal.fire({
        title: 'Alert',
        text: message,
        icon: iconType,
        confirmButtonText: 'OK'
    });
}