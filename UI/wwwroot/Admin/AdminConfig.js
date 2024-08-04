import { WRender, ComponentsManager } from '../WDevCore/WModules/WComponentsTools.js';
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { Transactional_ConfiguracionesView } from './Transactional_ConfiguracionesView.js';
const DOMManager = new ComponentsManager({ MainContainer: Main, SPAManage: true });
window.addEventListener("load", async () => {
    Aside.append(WRender.Create({ tagName: "h3", innerText: "Mantenimiento" }));
    Aside.append(new WAppNavigator({
        DarkMode: false,
        Direction: "column", 
        Elements: [
            {
                name: "Config", action: () => {
                    DOMManager.NavigateFunction("Transactional_Configuraciones", new Transactional_ConfiguracionesView());
                }
            }
        ]
    }));
});