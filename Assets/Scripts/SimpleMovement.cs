using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(0, 0, 12 * Time.deltaTime);
    }
}