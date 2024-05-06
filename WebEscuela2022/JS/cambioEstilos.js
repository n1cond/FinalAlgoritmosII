let B = true;
let modificarLink = function () {
    if (B) {
        //Acá están cargados:
        //css1
        //nav
        //nav1
        //Hay que agregar css2 y quitar nav1
        let L = document.createElement("link");
        L.id = "css2";
        L.href = "CSS/General2.css";
        L.rel = "stylesheet";
        document.head.appendChild(L);
        document.head.removeChild(nav1);
        B = false;
    } else {
        //Acá están cargados:
        //css1
        //css2
        //nav
        //Hay que agregar nav1 y quitar css2
        document.head.removeChild(css2);
        let L2 = document.createElement("link");
        document.head.appendChild(L2);
        L2.id = "nav1";
        L2.href = "CSS/Nav1.css";
        L2.rel = "stylesheet";
        B = true;
    }
}