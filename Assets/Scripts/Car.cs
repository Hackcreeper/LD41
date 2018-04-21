using UnityEngine;

public class Car : MonoBehaviour
{
    public const float MaxFuel = 80f;

    [SerializeField] private float _speed = 1000f;
    [SerializeField] private float _rotationSpeed = 80f;
    [SerializeField] private Transform[] _wheels;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private float _fuel = MaxFuel;
    private float _power;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ZombieManager.Instance.IsGameOver())
        {
            _power = 0;
            _velocity = Vector3.zero;
            return;
        }

        _power = Input.GetAxis("Vertical") * _speed;
        _velocity = transform.forward * _power;

        if (_velocity.magnitude <= 0f) return;

        _fuel -= Time.deltaTime;

        var f = _power >= 0 ? 1 : -1;
        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime * f, 0);

        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(new Vector3(0, 1, 0), _velocity.normalized.magnitude * 10);
        }

        if (_fuel <= 0f)
        {
            ZombieManager.Instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(
            _velocity.x * Time.fixedDeltaTime,
            _rigidbody.velocity.y,
            _velocity.z * Time.fixedDeltaTime
        );
    }

    public float GetFuel() => _fuel;
    public float GetPower() => _power;

    public void Refuel(int amount = 16)
    {
        _fuel += amount;
        _fuel = Mathf.Clamp(_fuel, 0, MaxFuel);
    }
}