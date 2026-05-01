using UnityEngine;

public class ObstaculoExplosivo : MonoBehaviour
{
    public GameObject particulasExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Objetivo" || collision.gameObject.tag == "ObstaculoExplosivo" || collision.gameObject.tag == "ObstaculoAXE") Explotar();
    }

    public void Explotar()
    {
        GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity) as GameObject;
        Canon.Bloqueado = false;
        SeguirCamara.objetivo = null;

        if (AdministradorJuego.SingletonAdministradorJuego != null)
        {
            AdministradorJuego.SingletonAdministradorJuego.VerificarFinDeJuego();
        }
        Destroy(this.gameObject);
    }
}
