const $$Ajax = function () {
    //Testing
    this.consulta = function (data) {
        return Post(data);
    };

    const Post = function (data) {
        const request = new XMLHttpRequest();
        let response;
        request.open("POST", "Default.aspx", false);
        request.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        request.onreadystatechange = function () {
            if (request.readyState === 4 && request.status === 200) {
                response = request.responseText;
            }
        }
        request.send(data);
        return response;
    };

    this.loadUsers = function () {
        let data = "accion=CARGARUSUARIOS";
        try {
            let respuesta = Post(data);
            if (respuesta.startsWith("Error")) {
                throw (respuesta);
                return;
            }
            Users = JSON.parse(respuesta);
        } catch (e) { alert(e); }
    };

    this.loadCareers = function () {
        let data = "accion=CARGARCARRERAS";
        try {
            let respuesta = Post(data);
            if (respuesta.startsWith("Error")) {
                throw (respuesta);
                return;
            }
            Careers = JSON.parse(respuesta);
        } catch (e) { alert(e); }
    };
}
const $a = new $$Ajax();