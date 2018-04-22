using UnityEngine;

namespace Utilities
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Upgrades.Type _type;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Upgrades.Instance.Add(_type);
            Destroy(gameObject);
        }
    }
}