using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (other.GetComponent<Zombie>().IsDead()) return;
        
        ZombieManager.Instance.Kill(other.transform);
    }
}