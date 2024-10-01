document.addEventListener('DOMContentLoaded', function () {
    initValidation();
})

function initValidation() {
    let forms = document.querySelectorAll('form');

    forms.forEach(form => {
        let inputs = form.querySelectorAll('input, textarea, select');

        inputs.forEach(input => {
            if (input.dataset.val === 'true') {
                applyInitialValidationClasses(input); 
                attachValidationHandler(input);     
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


const textValidator = (element, minLength = 1) => {
    const isValid = element.value.length >= minLength;
    formErrorHandler(element, isValid);
}

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