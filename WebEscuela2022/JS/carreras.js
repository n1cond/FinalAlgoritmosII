const CAREERDEFAULT = {
    ID: "",
    Sigla: "",
    Nombre: "",
    Titulo: "",
    Duracion: "",
    Estado: "",
    FotoURL: "",
    PdfURL: ""
};

let Careers;

const $$ClassCareer = function () { //Métodos para hacer ABMC de usuarios y login

    //FUNCIONES
    const Load = () => {
        $a.loadCareers();
    }

    const Clear = () => { Careers = null; }

    //METODOS
    this.add = function (career) {
        let data = "&Sigla" + career.Sigla +
            "&Nombre=" + career.Nombre +
            "&Titulo=" + career.Titulo +
            "&Duracion=" + career.Duracion +
            "&Estado=" + career.Estado;

        let respuesta = $a.consulta("accion=AGREGARCARRERA" + data);
        if (respuesta !== "OK") throw (respuesta);
    };

    this.erase = function (career) {
        $a.consulta("accion=ELIMINARCARRERA&ID=" + career.ID);
    };

    this.modify = function (career) {
        let data = "&ID=" + career.ID +
            "&Sigla=" + career.Sigla +
            "&Nombre=" + career.Nombre +
            "&Titulo=" + career.Titulo +
            "&Duracion=" + career.Duracion +
            "&Estado=" + career.Estado;

        let respuesta = $a.consulta("accion=MODIFICARCARRERA" + data);
        if (respuesta.startsWith("Error")) throw (respuesta);
    };

    this.disable = function (career) {
        career.Estado = "INACTIVO";
        $cc.modify(career);
    };

    this.listCareers = function () {
        Load();
        $tablas.listaCarreras(Careers);
        Clear();
    };
};
const $cc = new $$ClassCareer();