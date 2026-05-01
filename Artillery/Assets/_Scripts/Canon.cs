using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public CanonControls canonControls;
    private InputAction apuntar;
    private InputAction modificarfuerza;
    private InputAction disparar;

    [SerializeField] private UnityEngine.UI.Slider sliderFuerza;

    private void Awake()
    {
        canonControls = new CanonControls();
    }

    private void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarfuerza = canonControls.Canon.ModificarFuerza;
        disparar = canonControls.Canon.Disparar;
        apuntar.Enable();
        modificarfuerza.Enable();
        disparar.Enable();
        disparar.performed += Disparar;
    }

    private void OnDisable()
    {
        // Esto desvincula el disparo para que no de error al cambiar de escena
        disparar.performed -= Disparar;

        apuntar.Disable();
        modificarfuerza.Disable();
        disparar.Disable();
    }

    private void Start()
    {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetComponent<AudioSource>();
        if (sliderFuerza == null)
        {
            sliderFuerza = FindFirstObjectByType<UnityEngine.UI.Slider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        float cambioFuerza = modificarfuerza.ReadValue<float>();
        if (sliderFuerza != null)
        {
            sliderFuerza.value -= cambioFuerza * Time.deltaTime * 20f;
        }

        rotacion += apuntar.ReadValue<float>() * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        
    }
    void Disparar(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0) return;
            
        if (Bloqueado) return;

        if (AdministradorJuego.SingletonAdministradorJuego.juegoTerminado) return;
        if (AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego > 0)
        {

            GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);

            Rigidbody tempRB = temp.GetComponent<Rigidbody>();

            float potencia = sliderFuerza.value;

            // Si transform.up no funciona, prueba con transform.forward o transform.right
            tempRB.linearVelocity = transform.up * potencia;

            Debug.Log("POTENCIA DEL DISPARO: " + potencia);

            SeguirCamara.objetivo = temp;
            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;
            Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
            GameObject Particulas = Instantiate
                (ParticulaDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);

            // Ajustado a tu versión de Unity (linearVelocity o velocity)
            //tempRB.linearVelocity = transform.up * AdministradorJuego.SingletonAdministradorJuego.VelocidadBola;
            //SourceDisparo.PlayOneShot(clipDisparo);
            SourceDisparo.Play();
            AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;
            Bloqueado = true;
            Debug.Log("Disparos restantes: " + AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego);
        }
        else
        {
            Debug.Log("¡Te quedaste sin munición!");
        }
    }
}
