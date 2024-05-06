const $$Tablas = function () {
    this.listaUsuarios = (usuarios) => {
        let header = ["FOTO", "NOMBRE", "DNI", "MAIL", "DIRECCION", "TELEFONO", "MODIFICAR", "ELIMINAR"];
        let armarRow = (row, user) => {
            let foto = row.childNodes[0];
            let fotoImg = $dc.img(foto, user.FotoURL);
            fotoImg.className = "tableThumbnail";
            let nombre = row.childNodes[1];
            nombre.innerText = user.Nombre;
            let dni = row.childNodes[2];
            dni.innerText = user.Dni;
            let mail = row.childNodes[3];
            mail.innerText = user.Mail;
            let direccion = row.childNodes[4];
            direccion.innerText = user.Direccion;
            let telefono = row.childNodes[5];
            telefono.innerText = user.Telefono;
            
            let modificar = row.childNodes[6];
            let aMod = $dc.a(modificar, "modificar");
            aMod.innerText = "Modificar";
            aMod.className = "btnTable Modificar"
            aMod.onclick = () => {
                cargarRoles(user);
                $uf.formModifyUser(user, $navegacion.abmLista);
                $tablas.listaUsuarios(usuarios);
            };

            let cargarRoles = (user) => {
                user.Roles = JSON.parse($a.consulta("accion=CARGARROLESUSUARIO&ID=" + user.ID));
            };

            //let eliminar = $dc.td(row);
            if (user.ID !== 1)
            {
                let eliminar = row.childNodes[7];
                let aDel = $dc.a(eliminar, "eliminar");
                aDel.innerText = "Eliminar";
                aDel.className = "btnTable Eliminar";
                aDel.onclick = () => { $cu.erase(user); $navegacion.abmLista(); }
            }
        };
        $dt.table(header, usuarios, 3, armarRow);
    };

    this.listaCarreras = (carreras) => {
        let header = ["FOTO", "NOMBRE", "SIGLA", "DESCRIPCIÓN", "PLAN DE ESTUDIOS", "MODIFICAR", "DESACTIVAR"];
        let armarRow = (row, career) => {
            let foto = row.childNodes[0];
            let fotoImg = $dc.img(foto, career.FotoURL);
            fotoImg.className = "tableThumbnail";
            let sigla = row.childNodes[1];
            sigla.innerText = career.Sigla;
            let nombre = row.childNodes[2];
            nombre.innerText = career.Nombre;
            let descripcion = row.childNodes[3];
            descripcion.innerHTML = "Título: " + career.Titulo +
                "<br>Duración: " + career.Duracion + " años";

            let planEstudios = row.childNodes[4];
            planEstudios.innerHTML = ("PLAN<br>" + career.Sigla).toUpperCase();
            planEstudios.className = "btnTable PlanEstudios";
            planEstudios.onclick = () => { $navegacion.planEstudios(career); };

            let modificar = row.childNodes[5];
            let aMod = $dc.a(modificar, "modificar");
            aMod.innerText = "Modificar";
            aMod.className = "btnTable Modificar";
            aMod.onclick = () => { $navegacion.modifyCareer(career, carreras); }

            //let eliminar = $dc.td(row);
            let eliminar = row.childNodes[6];
            let aDel = $dc.a(eliminar, "eliminar");
            aDel.innerText = "Desactivar";
            aDel.className = "btnTable Eliminar";
            aDel.onclick = () => {
                try {
                    $cc.disable(career);
                    $navegacion.abmCarreras();
                } catch (e) {
                    alert(e);
                }
            }
        };
        $dt.table(header, carreras, 3, armarRow);
    };
};
const $tablas = new $$Tablas();