import { WRender, ComponentsManager } from '../WDevCore/WModules/WComponentsTools.js';
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { Transactional_ConfiguracionesView } from './Transactional_ConfiguracionesView.js';
import { LogType, LogView } from './LogErrorView.js';

window.addEventListener("load", async () => {
    const DOMManager = new ComponentsManager({ MainContainer: Main, SPAManage: true });
    Aside.append(WRender.Create({ tagName: "h3", innerText: "Mantenimiento" }));
    Aside.append(new WAppNavigator({
        DarkMode: false,
        Direction: "column",
        SPAManage: true,
        Elements: [
            {
                name: "Configuraciones", action: () => {
                    DOMManager.NavigateFunction("Configuraciones", new Transactional_ConfiguracionesView());
                }
            }, {
                name: "Acciones", action: () => {
                    DOMManager.NavigateFunction("Acciones", new LogView({Type: LogType.ACTION}));
                }
            }, {
                name: "Errores", action: () => {
                    DOMManager.NavigateFunction("Errores", new LogView({Type: LogType.ACTION}));
                }
            }
        ]
    }));
});