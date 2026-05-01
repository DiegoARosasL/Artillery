using UnityEngine;
using UnityEngine.Events;

public class Objetivo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            // 2. En lugar de GanarJuego directo, avisamos que este objetivo murió
            if (AdministradorJuego.SingletonAdministradorJuego != null)
            {
                AdministradorJuego.SingletonAdministradorJuego.ObjetivoDestruido();
            }

            // 3. Destruimos este objetivo
            Destroy(this.gameObject);
        }
    }
}
