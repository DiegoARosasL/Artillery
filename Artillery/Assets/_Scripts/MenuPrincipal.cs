using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject MenuOpciones;
    public GameObject MenuInicial;
    public GameObject MenuControles;

    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
    }

    public void finalizarJuego()
    {
        Application.Quit();
    }

    public void MostrarOpciones()
    {
        MenuInicial.SetActive(false);
        MenuOpciones.SetActive(true);
        MenuControles.SetActive(false);
    }

    public void MostrarMenuInicial()
    {
        MenuOpciones.SetActive(false);
        MenuInicial.SetActive(true);
    }

    public void MostrarMenuControles() 
    {
        MenuOpciones.SetActive(false);
        MenuControles.SetActive(true);
    }
}
