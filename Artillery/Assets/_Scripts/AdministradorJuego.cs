using NUnit.Framework.Internal;
using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{

    public static AdministradorJuego SingletonAdministradorJuego;
    private int velocidadBola = 30;
    private int disparosPorJuego = 10;
    private float velocidadRotacion = 1;

    public int VelocidadBola { get => velocidadBola; set => velocidadBola = value; }
    public int DisparosPorJuego { get => disparosPorJuego; set => disparosPorJuego = value; }
    public float VelocidadRotacion { get => velocidadRotacion; set => velocidadRotacion = value; }


    private void Awake()
    {
        if (SingletonAdministradorJuego == null) 
        {
            SingletonAdministradorJuego = this;
        }
        else 
        {
            Destroy(gameObject);
            Debug.LogError("Ya existe una instancia de esta clase");
        }
    }
}
