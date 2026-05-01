using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public abstract class ScorePersistente : ScriptableObject
{
    public void Guardar(string NombreArchivo = null)
    {
        var bf = new BinaryFormatter();
        var file = File.Create(ObtenerRuta(NombreArchivo));
        var json = JsonUtility.ToJson(this);

        bf.Serialize(file, json);
        file.Close();
    }

    public virtual void Cargar(string nombreArchivo = null)
    {
        var ruta = ObtenerRuta(nombreArchivo);


        if (File.Exists(ruta))
        {
            var bf = new BinaryFormatter();
            var archivo = File.Open(ruta, FileMode.Open);
            try
            {
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(archivo), this);
            }
            finally
            {
                archivo.Close();
            }
        }
    }

    public string ObtenerRuta(string nombreArchivo = null)
    {
        var nombreArchivoCompleto = string.IsNullOrEmpty(nombreArchivo) ? name : nombreArchivo;
        return string.Format("{0}/{1}.darl", Application.persistentDataPath, nombreArchivoCompleto);
    }
}   

