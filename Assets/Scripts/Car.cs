using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 1000f;
    [SerializeField] private float _rotationSpeed = 80f;

    [SerializeField] private Transform[] _wheels;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _velocity = transform.forward * (Input.GetAxis("Vertical") * _speed);

        if (_velocity.magnitude > 0f)
        {
            var rotation = new Vector3(0,
                transform.localEulerAngles.y + Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime, 0);
            transform.localEulerAngles = rotation;

            // Rotate the wheels
            foreach (var wheel in _wheels)
            {
                wheel.transform.Rotate(new Vector3(0, 1, 0), _velocity.normalized.magnitude * 10);
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _velocity * Time.fixedDeltaTime;
    }
}