import { WForm } from "./WDevCore/WComponents/WForm.js";
import { WTableComponent } from "./WDevCore/WComponents/WTableComponent.js";
import { EntityClass } from "./WDevCore/WModules/EntityClass.js";
import { html } from "./WDevCore/WModules/WComponentsTools.js";
import { css } from "./WDevCore/WModules/WStyledRender.js";


class Component extends HTMLElement {
    constructor() {
        super();
        this.append(html`<h1 class='border w-100'>HOME</h1>`);
    }
}
customElements.define('w-component', Component);
export { Component }
window.onload = () => {
    Main.append(new Component())
}