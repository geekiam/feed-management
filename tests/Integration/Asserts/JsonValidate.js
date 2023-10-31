const Ajv = require('ajv');

function validate(jsonData, jsonSchema) {
    const ajv = new Ajv();

    const validate = ajv.compile(jsonSchema);
    const isValid = validate(jsonData);

    if (!isValid) {
        return {
            valid: false,
            errors: validate.errors
        };
    } else {
        return {
            valid: true,
            errors: null
        };
    }
}

module.exports = validate;
