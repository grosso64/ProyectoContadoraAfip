function validacionRegistro() {
    var nombre = document.getElementById('txtNombre').value;
    var apellido = document.getElementById('txtApellido').value;
    var DNI = document.getElementById('txtDNI').value;
    var CUIT = document.getElementById('txtCUIT').value;
    var email = document.getElementById('txtEmail').value;
    var pass = document.getElementById('txtPass').value;
    

    // Validación del nombre
    if (nombre ==='') {
        alert('Por favor, introduce tu nombre.');
        return false;
    }
    if (apellido==='') {
        alert('Por favor, introduce tu apellido.');
        return false;
    }
    if (DNI === '') {
        alert('Por favor, introduce tu DNI.');
        return false;
    }
    if (!esNumerico(DNI)) {
        alert('Por favor, introduce solo digitos numericos en el DNI.');
        return false;
    }


    if (CUIT === '') {
        alert('Por favor, introduce tu CUIT.');
        return false;
    }
    if (CUIT.length <11) {
        alert('Por favor, introduce 11 digitos numericos en el CUIT.');
        return false;
    }

    if (!esNumerico(CUIT)) {
        alert('Por favor, introduce solo digitos numericos en el CUIT.');
        return false;
    }

    
    // Validación del correo electrónico
    var regexEmail = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
    if (!regexEmail.test(email)) {
        alert('Por favor, introduce un correo electrónico válido.');
        return false;
    }
    if (pass === '') {
        alert('Por favor, introduce tu Password.');
        return false;
    }




    // Si todo es válido, el formulario se enviará
    return true;
}

function esNumerico(str) {
    var expresionRegular = /^[0-9]+$/;
    return expresionRegular.test(str);
}