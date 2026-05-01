using NUnit.Framework.Internal;
using UnityEngine;
using TMPro;
using static Opciones;

public class AdministradorJuego : MonoBehaviour
{

    public static AdministradorJuego SingletonAdministradorJuego;
    public Opciones opciones;
    public bool juegoTerminado = false;
    private int velocidadBola = 30;
    private int disparosPorJuego = 2;
    private float velocidadRotacion = 1;

    public GameObject CanvasGanar;
    public GameObject CanvasPerder;

    public int numeroDeNivel;

    public int objetivosRestantes;

    public int VelocidadBola { get => velocidadBola; set => velocidadBola = value; }
    public int DisparosPorJuego { get => disparosPorJuego; set => disparosPorJuego = value; }
    public float VelocidadRotacion { get => velocidadRotacion; set => velocidadRotacion = value; }

    [Header("Referencias de Interfaz")]
    public TMP_Text textoBalasRestantes; // El "x 0"
    public TMP_Text textoBalasUsadas;    // El "Nº B. Usadas: 0"
    public TMP_Text textoMejorScore;


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

    private void Start()
    {
        if (opciones != null)
        {
            // 1. Balas según dificultad
            DisparosPorJuego = opciones.disparosPorJuego;

            // 2. Tamaño del objetivo
            float escala = 1f;
            switch (opciones.NivelDificultad)
            {
                case dificultad.facil: escala = 2.0f; break;
                case dificultad.normal: escala = 1.2f; break;
                case dificultad.dificil: escala = 0.6f; break;
            }
            // Asumiendo que tienes la referencia al GameObject 'objetivo'
            GameObject[] todosLosObjetivos = GameObject.FindGameObjectsWithTag("Objetivo");
            foreach (GameObject objetivoIndividual in todosLosObjetivos)
            {
                objetivoIndividual.transform.localScale = new Vector3(escala, escala, escala);
            }

            objetivosRestantes = todosLosObjetivos.Length;
            ActualizarInterfaz();
        }
    }

    void Update()
    {
        ActualizarInterfaz();

        int balasActivas = GameObject.FindGameObjectsWithTag("Bala").Length;

        if (balasActivas == 0)
        {
            Canon.Bloqueado = false;
        }
    }
    public void VerificarFinDeJuego()
    {
        if (juegoTerminado) return;
        Invoke("ChequeoFinal", 0.1f);
    }

    private void ChequeoFinal()
    {
        if (juegoTerminado) return; // Si en esos 0.1s ganaste, esto ya no se ejecuta

        int balasEnEscena = GameObject.FindGameObjectsWithTag("Bala").Length;
        if (DisparosPorJuego <= 0 && balasEnEscena <= 0) // Usamos 0 porque tras 0.1s ya se destruyó
        {
            PerderJuego();
        }
    }
    public void GanarJuego() 
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        CanvasGanar.SetActive(true);

        int balasUsadas = opciones.disparosPorJuego - DisparosPorJuego;
        opciones.RegistrarRecord(numeroDeNivel, balasUsadas);
    }

    public void PerderJuego() 
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        CanvasPerder.SetActive(true);
    }

    public void ActualizarInterfaz()
    {
        // 1. Balas que quedan por disparar
        textoBalasRestantes.text = "x " + DisparosPorJuego;

        // 2. Balas que ya se dispararon en esta partida
        int usadas = opciones.disparosPorJuego - DisparosPorJuego;
        textoBalasUsadas.text = "Nº B. Usadas: " + usadas;

        Opciones.RecordsNivel datosNivel = (numeroDeNivel == 1) ? opciones.nivel1 :
                                      (numeroDeNivel == 2) ? opciones.nivel2 : opciones.nivel3;

        // 3. Mostrar el mejor score guardado de la dificultad actual
        int mejor = 0;
        switch (opciones.NivelDificultad)
        {
            case Opciones.dificultad.facil: mejor = datosNivel.mejorFacil; break;
            case Opciones.dificultad.normal: mejor = datosNivel.mejorNormal; break;
            case Opciones.dificultad.dificil: mejor = datosNivel.mejorDificil; break;
        }
        textoMejorScore.text = "Mejor: " + mejor;
    }

    public void ObjetivoDestruido()
    {
        objetivosRestantes--;

        // Si ya no quedan más, disparamos la victoria
        if (objetivosRestantes <= 0)
        {
            GanarJuego();
        }
    }   
}
