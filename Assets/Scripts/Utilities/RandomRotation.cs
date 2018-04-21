using UnityEngine;

namespace Utilities
{
    public class RandomRotation : MonoBehaviour
    {
        private void Start()
        {
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                Random.Range(0, 360),
                transform.localEulerAngles.z
            );
        }
    }
}