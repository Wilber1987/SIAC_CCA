document.addEventListener("DOMContentLoaded", function () {
    const menuParentOptions = document.querySelectorAll(".menuParentOption");
    const subMenus = document.querySelectorAll(".sub-menu");
    const backButton = document.getElementById("backButton");
    const adminMenu = document.querySelector(".admin-menu");
    const pageContent = document.querySelector(".page-content");
    const homeMenuOption = document.getElementById("menuInicio"); // Usamos el ID directo

    
    function toggleAdminMenu() {
        const currentPath = window.location.pathname.toLowerCase();
        const isHome = currentPath === "/home" || currentPath === "/";
         
        if (isHome) {
            adminMenu.style.display = "block";
            backButton.style.display = "none";
            pageContent.style.paddingTop = "";

            // Mostrar todas las opciones
            menuParentOptions.forEach(opt => opt.style.display = "block");

            subMenus.forEach(sub => sub.style.display = "");
            //if (homeMenuOption) homeMenuOption.style.display = "block";
        } else {
            adminMenu.style.display = "block";
            backButton.style.display = "block";
            pageContent.style.paddingTop = "10px";

            menuParentOptions.forEach(opt => opt.style.display = "none");
            subMenus.forEach(sub => sub.style.display = "none");

            // Ocultar específicamente la opción de Inicio
            //if (homeMenuOption){} homeMenuOption.style.display = "none";
        }
    }

    function hideAllOptions(except) {
        menuParentOptions.forEach(opt => {
            if (opt !== except) {
                opt.style.display = "none"; 
                opt.classList.remove("selected");
            } else {
                opt.classList.add("selected");
            }
        });
    }

    function showSubMenu(subMenu) {
        subMenu.classList.add("show", "mm-collapsing", "mm-show");
        subMenu.style.height = "auto";
        subMenu.style.width = "100%";
    }

    function resetSubMenus() {
        subMenus.forEach(subMenu => {
            subMenu.classList.remove("show");
            subMenu.style.height = "0";
            subMenu.style.width = "";
        });
    }

    menuParentOptions.forEach(option => {
        option.addEventListener("click", function () {
            hideAllOptions(option);
            const link = option.querySelector("a");
            if (link) link.style.display = "none";

            const relatedSubMenu = option.querySelector(".sub-menu");
            if (relatedSubMenu) showSubMenu(relatedSubMenu);

            option.style.width = "100%";
            backButton.style.display = "block";
        });
    });

    subMenus.forEach(subMenu => {
        const links = subMenu.querySelectorAll("a");
        links.forEach(link => {
            link.addEventListener("click", function () {
                // lógica adicional si se requiere
            });
        });
    });

    backButton.addEventListener("click", function () {
        window.location.href = "/home";
    });

    toggleAdminMenu();
    homeMenuOption.style.display = "none";
});
