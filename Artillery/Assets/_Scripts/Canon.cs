using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public static bool Bloqueado;

    public AudioClip clipDisparo;
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo;

    [SerializeField] private GameObject BalaPrefab;
    public GameObject ParticulaDisparo;
    private GameObject puntaCanon;
    private float rotacion;

    [SerializeField] public LineRenderer lineRenderer;

    private void Start()
    {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.Space)&&!Bloqueado)
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
        SeguirCamara.objetivo = temp;
        Vector3 direccionDisparo = transform.rotation.eulerAngles;
        direccionDisparo.y = 90 - direccionDisparo.x;
        Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
        GameObject Particulas = Instantiate
            (ParticulaDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);

        // Ajustado a tu versión de Unity (linearVelocity o velocity)
        tempRB.linearVelocity = transform.up * AdministradorJuego.SingletonAdministradorJuego.VelocidadBola;
        //SourceDisparo.PlayOneShot(clipDisparo);
        SourceDisparo.Play();
        Bloqueado = true;
    }
}
