using System.Data;
using System.Collections.Generic;

namespace BASICA
{
    public interface ISingleton
    {
        IImage IIm { get; }
        IHashing IHash { get; }
        IJson IJson { get; }
        string TableToJson<T>(DataTable dt, IGenericSingleton<T> IGS);
        IConnection IC { get; }
        List<T> TableToList<T>(DataTable dt, IGenericSingleton<T> IGS);
    }
}
