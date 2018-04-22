using UnityEngine;

namespace Utilities
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;

        private void Update()
        {
            transform.Rotate(_rotation * Time.deltaTime);
        }
    }
}