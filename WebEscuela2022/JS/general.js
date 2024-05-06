const Home = function () {
    ClearSection();
    let sectionImg = $dc.img(Section, "IMAGENES/GENERAL/instituto.jpg");
    sectionImg.className = "sectionImg";
};

const ClearSection = () => { Section.innerHTML = ""; };

const AnularF5 = function () {
    document.onkeydown = () => {
        if (event.keyCode === 116) {
            event.preventDefault();
        };
    };
};

const $$Navegacion = function () {
    this.abmLista = () => {
        //Form abmUsuario, luego listaUsuarios
        $uf.formAddUser();
        $cu.listUsers();
    };

    this.abmCarreras = () => {
        $cf.formAddCareer();
        $cc.listCareers();
    };

    this.modifyCareer = (career, carreras) => {
        $cf.formModifyCareer(career);
        $cc.listCareers();
    }

    this.planEstudios = (career) => {
        ClearSection();
        let iframe = $d.ce("iframe");
        $d.ac(Section, iframe);
        iframe.className = "iframePdf";
        iframe.id = "iframe";
        iframe.src = career.PdfURL;
    }
};
const $navegacion = new $$Navegacion();
