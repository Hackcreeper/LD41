using UnityEngine;

public class Canister : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Car>().Refuel();
        Destroy(gameObject);
    }
}