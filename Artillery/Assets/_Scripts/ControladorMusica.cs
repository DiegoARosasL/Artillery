using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    public static ControladorMusica instancia;

    void Awake()
    {
        // Si ya existe una instancia de música, destruye esta nueva
        if (instancia != null)
        {
            Destroy(gameObject);
            return;
        }

        // Si es la primera vez, se guarda como la instancia principal
        instancia = this;

        // Esto hace que el objeto NO se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }
}
