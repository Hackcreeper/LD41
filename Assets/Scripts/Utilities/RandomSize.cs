using UnityEngine;

namespace Utilities
{
    public class RandomSize : MonoBehaviour
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        private void Start()
        {
            var size = Random.Range(_min, _max);
            transform.localScale = new Vector3(size, size, size);
        }
    }
}