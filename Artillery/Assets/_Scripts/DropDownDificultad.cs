using TMPro;
using UnityEngine;

public class DropDownDificultad : MonoBehaviour
{
    public Opciones opciones;
    private TMP_Dropdown dificultad;

    private void Start()
    {
        dificultad = GetComponent<TMP_Dropdown>();

        if (dificultad != null && opciones != null)
        {

            dificultad.value = (int)opciones.NivelDificultad;
            dificultad.RefreshShownValue();
        }
        dificultad.onValueChanged.AddListener(delegate { opciones.CambiarDificultad(dificultad.value); });
    }
}
