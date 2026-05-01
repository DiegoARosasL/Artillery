using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject menuOpciones;
    public GameObject menuControles;
    public AudioSource musicaFondo;
    public void MostrarMenuPausa()
    {
        Time.timeScale = 0f; // Congela el juego
        menuOpciones.SetActive(true);
        menuControles.SetActive(false);
        if (ControladorMusica.instancia != null)
        {
            ControladorMusica.instancia.GetComponent<AudioSource>().Pause();
        }
    }

    public void OcultarMenuPausa()
    {
        
        
        menuOpciones.SetActive(false);
        menuControles.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
        if (ControladorMusica.instancia != null)
        {
            ControladorMusica.instancia.GetComponent<AudioSource>().UnPause();
        }
    }

    public void RegresarAPantallaPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void MostrarMenuOpciones()
    {
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
        menuControles.SetActive(false);
    }

    public void MostrarMenuControles()
    {
        menuOpciones.SetActive(false);
        menuControles.SetActive(true);
    }

    
}
