using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Opciones", menuName = "Herramientas/Opciones", order = 1)]

public class Opciones : ScorePersistente
{
    public int disparosPorJuego = 8;
    public dificultad NivelDificultad = dificultad.facil;

    [Serializable]
    public class RecordsNivel
    {
        public int mejorFacil;
        public int mejorNormal;
        public int mejorDificil;
    }

    public RecordsNivel nivel1 = new RecordsNivel();
    public RecordsNivel nivel2 = new RecordsNivel();
    public RecordsNivel nivel3 = new RecordsNivel();

    private void OnEnable()
    {
        string ruta = ObtenerRuta();
        if (System.IO.File.Exists(ruta))
        {
            Cargar();
        }
    }

    public enum dificultad
    {
        facil,
        normal,
        dificil
    }

    public void CambiarDificultad(int nuevaDificultad)
    {
        NivelDificultad = (dificultad)nuevaDificultad;

        switch (NivelDificultad)
        {
            case dificultad.facil:
                disparosPorJuego = 8;
                break;
            case dificultad.normal:
                disparosPorJuego = 5;
                break;
            case dificultad.dificil:
                disparosPorJuego = 3;
                break;
        }

        Guardar();
    }

    public void RegistrarRecord(int nivel, int usadas)
    {
        RecordsNivel nivelActual = (nivel == 1) ? nivel1 : (nivel == 2) ? nivel2 : nivel3;

        switch (NivelDificultad)
        {
            case dificultad.facil:
                if (nivelActual.mejorFacil == 0 || usadas < nivelActual.mejorFacil) nivelActual.mejorFacil = usadas;
                break;
            case dificultad.normal:
                if (nivelActual.mejorNormal == 0 || usadas < nivelActual.mejorNormal) nivelActual.mejorNormal = usadas;
                break;
            case dificultad.dificil:
                if (nivelActual.mejorDificil == 0 || usadas < nivelActual.mejorDificil) nivelActual.mejorDificil = usadas;
                break;
        }
        Guardar();
    }
}
