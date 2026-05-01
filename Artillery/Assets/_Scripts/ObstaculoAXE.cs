using UnityEngine;

public class ObstaculoAXE : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion") Destroy(this.gameObject);
    }
}
