using UnityEngine;

namespace Utilities
{
    public class Hover : MonoBehaviour
    {
        [SerializeField] private float _factor = 0.008f;
        [SerializeField] private float _speed = 2;

        private Vector3 _basePosition;

        private void Start()
        {
            _basePosition = transform.localPosition;
        }

        private void Update()
        {
            var y = Mathf.Lerp(transform.localPosition.y, _basePosition.y + Mathf.Sin(Time.time * _speed), _factor);
            
            transform.localPosition = new Vector3(
                _basePosition.x,
                y,
                _basePosition.z
            );
        }
    }
}