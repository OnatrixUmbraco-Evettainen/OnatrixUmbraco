//document.addEventListener('DOMContentLoaded', function () {
//    const emailInput = document.getElementById('emailInput');
//    const form = document.getElementById('supportForm');
//    const emailRegex = /^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;



//    emailInput.addEventListener('keyup', function () {
//        let emailValue = emailInput.value;

//        if (emailRegex.test(emailValue)) {
//            emailInput.classList.remove('is-invalid');
//            emailInput.classList.add('is-valid');
//        } else {
//            emailInput.classList.remove('is-valid');
//            emailInput.classList.add('is-invalid');
//        }
//    });
//});

//const formErrorHandler = (element, validationResult, customMessage = "") => {

//    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

//    if (validationResult) {
//        element.classList.remove('input-validation-error')

//        element.classList.remove('is-invalid')
//        element.classList.add('is-valid')

//        spanElement.classList.remove('field-validation-error')
//        spanElement.classList.add('field-validation-valid')
//        spanElement.innerHTML = ''

//    }
//    else {
//        element.classList.add('input-validation-error')
//        element.classList.add('is-invalid')
//        element.classList.remove('is-valid')

//        spanElement.classList.add('field-validation-error')
//        spanElement.classList.remove('field-validation-valid')
//        spanElement.innerHTML = customMessage || element.dataset.valRequired;
//    }
//}



//const textValidator = (element, minLength = 1) => {

//    if (element.value.length >= minLength) {

//        formErrorHandler(element, true)
//    }
//    else {
//        formErrorHandler(element, false)
//    }
//}


//const emailValidator = (element) => {
//    const isEmpty = !element.value;
//    const isValidEmail = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(element.value)

//    if (isEmpty) {
//        formErrorHandler(element, false, element.dataset.valRequired)
//    }
//    else if (!isValidEmail) {
//        formErrorHandler(element, false, element.dataset.valRegex)
//    }
//    else {
//        formErrorHandler(element, true)
//    }
//}




//const passwordValidator = (element) => {
//    if (element.dataset.valEqualtoOther !== undefined) {

//        const targetName = element.dataset.valEqualtoOther.replace('*.', '')
//        const targetElement = document.querySelector(`input[name$="${targetName}"]`);
//        const targetPassword = targetElement ? targetElement.value : null;
//        const isEmpty = !element.value
//        const isMatch = element.value === targetPassword

//        if (isEmpty) {
//            formErrorHandler(element, false, element.dataset.valRequired)
//        }
//        else if (!isMatch) {
//            formErrorHandler(element, false, element.dataset.valEqualto)
//        }
//        else {
//            formErrorHandler(element, true)
//        }
//    }
//    else {

//        const isEmpty = !element.value
//        const isValidPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(element.value)

//        if (isEmpty) {
//            formErrorHandler(element, false, element.dataset.valRequired)
//        }
//        else if (!isValidPassword) {
//            formErrorHandler(element, false, element.dataset.valRegex)
//        }
//        else {
//            formErrorHandler(element, true)
//        }
//    }
//}


//const checkboxValidator = (element) => {
//    if (element.checked) {
//        formErrorHandler(element, true)
//    }
//    else {
//        formErrorHandler(element, false)
//    }
//}



//document.addEventListener('DOMContentLoaded', function () {
//    initValidation();
//})


//function initValidation() {
//    let forms = document.querySelectorAll('form')
//    forms.forEach(form => {
//        let inputs = form.querySelectorAll('input')
//        let textareas = form.querySelectorAll('textarea')


//        inputs.forEach(input => {
//            if (input.dataset.val === 'true') {
//                if (input.classList.contains('input-validation-error')) {
//                    input.classList.add('is-invalid');
//                }
//                validateInput(input);
//            }
//        })


//        textareas.forEach(textarea => {
//            if (textarea.dataset.val === 'true') {
//                if (textarea.classList.contains('input-validation-error'))
//                    textarea.classList.add('is-invalid');

//                textarea.addEventListener('keyup', (e) => {
//                    textValidator(e.target)
//                })
//            }
//        })

//    })
//}

//function validateInput(input) {

//    if (input.dataset.validation === 'password') {
//        input.addEventListener('keyup', (e) => textValidator(e.target))
//    } else if (input.type === 'checkbox') {
//        input.addEventListener('change', (e) => checkboxValidator(e.target))
//    } else {
//        input.addEventListener('keyup', (e) => {
//            switch (e.target.type) {
//                case 'text':
//                    textValidator(e.target);
//                    break;
//                case 'email':
//                    emailValidator(e.target);
//                    break;
//                case 'password':
//                    passwordValidator(e.target);
//                    break;
//            }
//        })
//    }
//}


document.addEventListener('DOMContentLoaded', function () {
    initValidation();
})

function initValidation() {
    let forms = document.querySelectorAll('form');

    forms.forEach(form => {
        let inputs = form.querySelectorAll('input, textarea, select');

        inputs.forEach(input => {
            if (input.dataset.val === 'true') {
                applyInitialValidationClasses(input);  // Tillämpa initiala klasser
                attachValidationHandler(input);        // Fäst valideringshändelser
            }
        });
    });
}

function applyInitialValidationClasses(element) {
    if (element.classList.contains('input-validation-error')) {
        element.classList.add('is-invalid');
    }
}

function attachValidationHandler(element) {
    if (element.dataset.validation === 'password') {
        element.addEventListener('keyup', (e) => passwordValidator(e.target));
    }
    else if (element.type === 'checkbox') {
        element.addEventListener('change', (e) => checkboxValidator(e.target));
    }
    else if (element.tagName === 'SELECT') {
        element.addEventListener('change', (e) => selectValidator(e.target));
    }
    else {
        element.addEventListener('keyup', (e) => validateByType(e.target));
    }
}

function validateByType(element) {
    switch (element.type) {
        case 'text':
            textValidator(element);
            break;
        case 'email':
            emailValidator(element);
            break;
        case 'password':
            passwordValidator(element);
            break;
        default:
            textValidator(element);
    }
}


// Validerar textfält
const textValidator = (element, minLength = 1) => {
    const isValid = element.value.length >= minLength;
    formErrorHandler(element, isValid);
}

// Validerar e-postfält
const emailValidator = (element) => {
    const isEmpty = !element.value;
    const isValidEmail = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(element.value);

    if (isEmpty) {
        formErrorHandler(element, false, element.dataset.valRequired);
    } else if (!isValidEmail) {
        formErrorHandler(element, false, element.dataset.valRegex);
    } else {
        formErrorHandler(element, true);
    }
}

// Validerar lösenordsfält
const passwordValidator = (element) => {
    const targetName = element.dataset.valEqualtoOther?.replace('*.', '');
    const targetElement = document.querySelector(`input[name$="${targetName}"]`);
    const isEmpty = !element.value;
    const isMatch = targetElement ? element.value === targetElement.value : false;
    const isValidPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(element.value);

    if (isEmpty) {
        formErrorHandler(element, false, element.dataset.valRequired);
    } else if (targetElement && !isMatch) {
        formErrorHandler(element, false, element.dataset.valEqualto);
    } else if (!isValidPassword) {
        formErrorHandler(element, false, element.dataset.valRegex);
    } else {
        formErrorHandler(element, true);
    }
}


const checkboxValidator = (element) => {
    formErrorHandler(element, element.checked);
}

const selectValidator = (element) => {
    const isValid = element.value !== '';
    formErrorHandler(element, isValid)
}


const formErrorHandler = (element, isValid, customMessage = "") => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

    if (isValid) {
        updateValidationClasses(element, 'valid');
        spanElement.innerHTML = '';
    } else {
        updateValidationClasses(element, 'invalid');
        spanElement.innerHTML = customMessage || element.dataset.valRequired;
    }
}

// Uppdaterar valideringsklasser baserat på giltighet
const updateValidationClasses = (element, status) => {
    const isValid = (status === 'valid');
    element.classList.toggle('is-valid', isValid);
    element.classList.toggle('is-invalid', !isValid);
    element.classList.toggle('input-validation-error', !isValid);

    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);
    spanElement.classList.toggle('field-validation-error', !isValid);
    spanElement.classList.toggle('field-validation-valid', isValid);

    let formGroup = element.closest('.form-group');
    if (formGroup) {
        formGroup.classList.toggle('is-valid', isValid);
        formGroup.classList.toggle('is-invalid', !isValid);

    }
}