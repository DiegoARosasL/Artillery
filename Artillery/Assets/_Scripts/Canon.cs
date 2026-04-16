using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject BalaPrefab;
    private GameObject puntaCanon;
    private float rotacion;

    private void Start()
    {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Verificamos si quedan disparos en el Singleton
            if (AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego > 0)
            {
                Disparar();
                // Restamos uno al contador
                AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;
                Debug.Log("Disparos restantes: " + AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego);
            }
            else
            {
                Debug.Log("¡Te quedaste sin munición!");
            }
        }
    }
    void Disparar()
    {
        GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
        Rigidbody tempRB = temp.GetComponent<Rigidbody>();

        // Ajustado a tu versión de Unity (linearVelocity o velocity)
        tempRB.linearVelocity = transform.up * AdministradorJuego.SingletonAdministradorJuego.VelocidadBola;
    }
}
