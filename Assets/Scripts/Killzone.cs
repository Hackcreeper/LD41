using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private bool _front;
    [SerializeField] private bool _static;
    [SerializeField] private Car _player;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (other.GetComponent<Zombie>().IsDead()) return;

        if ((_front && _player.GetPower() > 0) || _static)
        {
            ZombieManager.Instance.Kill(other.transform);
            return;
        }

        if (!_front && _player.GetPower() < 0)
        {
            ZombieManager.Instance.Kill(other.transform);
        }
    }
}