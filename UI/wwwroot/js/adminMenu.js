document.addEventListener("DOMContentLoaded", function () {
    const menuParentOptions = document.querySelectorAll(".menuParentOption");
    const subMenus = document.querySelectorAll(".sub-menu");
    const backButton = document.getElementById("backButton");

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

    // Manejar clics en las opciones del menú padre
    menuParentOptions.forEach(option => {
        option.addEventListener("click", function (event) {
            event.preventDefault(); // Evitar la acción predeterminada solo si es un padre
            hideAllOptions(option);

            const link = option.querySelector("a");
            if (link) {
                link.style.display = "none"; // Oculta el enlace padre
            }

            const relatedSubMenu = option.querySelector(".sub-menu");
            if (relatedSubMenu) {                
                showSubMenu(relatedSubMenu);
            }
            option.style.width = "100%";
            backButton.style.display = "block";
        });
    });

    // Manejar clics en los enlaces hijos
    subMenus.forEach(subMenu => {
        const links = subMenu.querySelectorAll("a");
        links.forEach(link => {
            link.addEventListener("click", function (event) {                
            });
        });
    });

    backButton.addEventListener("click", function () {
        resetSubMenus();
        
        menuParentOptions.forEach(opt => {
            opt.style.display = "block";
            opt.classList.remove("selected");
            opt.style.width = "";
            const link = opt.querySelector("a");
            if (link) {
                link.style.display = "block"; // Muestra el enlace padre
            }
        });

        backButton.style.display = "none"; // Oculta el botón de regreso
    });
});
