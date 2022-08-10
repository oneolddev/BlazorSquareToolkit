console.log("Loading Square Payment Input Component")

// scoped to this module
let card;

export function initialize(environment, applicationId, locationId, dotnetRef) {
    const sdk = environment == 'Production' ?
        'https://web.squarecdn.com/v1/square.js' : 'https://sandbox.web.squarecdn.com/v1/square.js';
    import(sdk).then(
        exports => {
            console.log('  Square SDK loaded - ' + '\'' + environment + '\'');

            card = new Card('card-container');
            card.initialize(applicationId, locationId);

            DotNetRef.setReference(dotnetRef);
        }
    );
}

export async function tokenize() {
    return card.tokenize();
}

class Card {
    constructor(elementId) {
        this.cardContainer = document.getElementById(elementId);
    }

    //
    controlState = new ControlState();

    //
    async initialize(applicationId, locationId) {
        window.payments = Square.payments(applicationId, locationId);

        this.instance = await window.payments.card();
        await this.instance.attach(this.cardContainer);

        //
        await this.instance.addEventListener("focusClassRemoved",
            (event) => {
                const isValid = this.controlState.update(event.detail);
                DotNetRef.OnChange(isValid);
            });
    }

    async tokenize() {
        const response = await this.instance.tokenize();
        console.log(response);
        return JSON.stringify(response);
    }
}

class ControlState {
    cardNumberValid = false;
    expirationDateValid = false;
    cvvValid = false;
    postalCodeValid = false;

    // returns true if all entered values are valid for processing
    update = (detail) => {
        switch (detail.field) {
            case 'cardNumber':
                this.cardNumberValid = detail.currentState.isCompletelyValid;
                break;
            case 'expirationDate':
                this.expirationDateValid = detail.currentState.isCompletelyValid;
                break;
            case 'cvv':
                this.cvvValid = detail.currentState.isCompletelyValid;
                break;
            case 'postalCode':
                this.postalCodeValid = detail.currentState.isCompletelyValid;
                break;
            default:
        }

        return this.cardNumberValid
            && this.expirationDateValid
            && this.cvvValid
            && (!detail.hasOwnProperty('postalCodeValue')
                || (detail.hasOwnProperty('postalCodeValue') && this.postalCodeValid));
    }
}

class DotNetRef {
    static value;

    static setReference(value) {
        DotNetRef.value = value;
    }

    static async OnChange(status) {
        await DotNetRef.value.invokeMethodAsync('OnChange', status);
    }
}
