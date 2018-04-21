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
        var power = Input.GetAxis("Vertical") * _speed;
        _velocity = transform.forward * power;

        if (_velocity.magnitude <= 0f) return;

        var f = power >= 0 ? 1 : -1;
        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime * f, 0);

        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(new Vector3(0, 1, 0), _velocity.normalized.magnitude * 10);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _velocity * Time.fixedDeltaTime;
    }
}