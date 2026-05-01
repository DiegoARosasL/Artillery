using UnityEngine;

public class ControlBordes : MonoBehaviour
{
    [Header("Configurar en el editor")]
    public float radio = 1f;
    public bool mantenerEnPantalla = false;

    [Header("Configuraciones dinamicas")]
    public bool estaEnPantalla = true;
    public float anchoCamara;
    public float altoCamara;
    public bool salioDerecha, salioIzquierda, salioArriba, salioAbajo;

    public void Awake()
    {
        altoCamara = Camera.main.orthographicSize;
        anchoCamara = Camera.main.aspect * altoCamara;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        estaEnPantalla = true;
        salioAbajo = salioArriba = salioDerecha = salioIzquierda = false;
        if (pos.x > anchoCamara - radio + 50)
        {
            pos.x = anchoCamara - radio + 50;
            salioDerecha = true;
        }
        if (pos.x < -anchoCamara + radio - 50)
        {
            pos.x = -anchoCamara + radio - 50;
            salioIzquierda = true;
        }
        if (pos.y > altoCamara - radio)
        {
            pos.y = altoCamara - radio;
            salioArriba = true;
        }
        if (pos.y < -altoCamara + radio)
        {
            pos.y = -altoCamara + radio;
            salioAbajo = true;
        }

        estaEnPantalla = !(salioAbajo || salioArriba || salioDerecha || salioIzquierda);
        if (transform.position.y < -30 || transform.position.y > 50 ||
        transform.position.x > 60 || transform.position.x < -60)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 tamanoBorde = new Vector3(anchoCamara * 2, altoCamara * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, tamanoBorde);
    }
}
