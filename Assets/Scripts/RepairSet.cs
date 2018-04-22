using UnityEngine;

public class RepairSet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Car>().Heal();
        Destroy(gameObject);
    }
}