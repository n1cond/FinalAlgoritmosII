namespace BASICA
{
    public interface IABMF : IID
    {
        /// <summary>
        /// Da persistencia al objeto que la implementa
        /// </summary>
        void Add();
        /// <summary>
        /// Modifica el objeto
        /// </summary>
        void Modify();//MODIFICA UN OBJETO
        /// <summary>
        /// Elimina un objeto
        /// </summary>
        void Erase();
        /// <summary>
        /// Carga el objeto en la persistencia en la clase que lo implementa
        /// </summary>
        /// <returns> Json (para cada javascrip</returns>
        string Find();

    }
}