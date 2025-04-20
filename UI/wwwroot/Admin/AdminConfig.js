import { WRender, ComponentsManager } from '../WDevCore/WModules/WComponentsTools.js';
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { Transactional_ConfiguracionesView } from './Transactional_ConfiguracionesView.js';
import { LogType, LogView } from './LogErrorView.js';
import { css } from '../WDevCore/WModules/WStyledRender.js';

window.addEventListener("load", async () => {
    const DOMManager = new ComponentsManager({ MainContainer: Main, SPAManage: true });
    Aside.append(WRender.Create({ tagName: "h3", innerText: "Mantenimiento" }));
    Aside.append(css`
        h3 {
            padding: 20px;
            margin: 0px;
        }
        w-app-navigator {
            margin:0px;
            color: var(--font-fourth-color);
            border-radius: 0.3cm;
            padding: 20px;
            display: block;
        }`);
    const navigator = new WAppNavigator({
        DarkMode: false,
        //Direction: "row",
        SPAManage: true,
        NavStyle: "tab",
        Inicialize: true,
        Elements: [
            {
                name: "Configuraciones", action: () => {
                    return new Transactional_ConfiguracionesView();
                }
            }, {
                name: "Acciones", action: () => {
                    return  new LogView({ Type: LogType.ACTION });
                }
            }, {
                name: "Errores", action: () => {
                    return  new LogView({ Type: LogType.ACTION });
                }
            }
        ]
    });
    Aside.append(navigator);
});