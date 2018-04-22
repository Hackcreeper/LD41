using UnityEngine;

public class Car : MonoBehaviour
{
    public const float MaxFuel = 80f;

    [SerializeField] private float _speed = 1000f;
    [SerializeField] private float _rotationSpeed = 80f;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private Transform _saw;
    [SerializeField] private Transform _saw1;
    [SerializeField] private Transform _saw2;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private float _fuel = MaxFuel;
    private float _power;
    private float _sawTimer;
    private float _sawMaxTime = 5.5f;
    private bool _enableSaw;
    private float _nitroTimer;
    private float _nitroMaxTime = 5.5f;
    private bool _enableNitro;
    private float _originalSpeed;

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

        if (_fuel <= 0f)
        {
            ZombieManager.Instance.GameOver();
            return;
        }

        HandleSaw();
        HandleNitro();

        _power = Input.GetAxis("Vertical") * _speed;
        _velocity = transform.forward * _power;

        if (_velocity.magnitude <= 0f) return;

        _fuel -= Time.deltaTime;
        RotatePlayer();
    }

    private void HandleSaw()
    {
        if (!_enableSaw)
        {
            _saw1.transform.localEulerAngles = Vector3.Lerp(
                _saw1.transform.localEulerAngles,
                new Vector3(0, 0, 0),
                5f * Time.deltaTime
            );

            _saw2.transform.localEulerAngles = Vector3.Lerp(
                _saw2.transform.localEulerAngles,
                new Vector3(0, 0, 0),
                5f * Time.deltaTime
            );

            return;
        }

        _saw1.transform.localEulerAngles = Vector3.Lerp(
            _saw1.transform.localEulerAngles,
            new Vector3(0, 270, 0),
            5f * Time.deltaTime
        );

        _saw2.transform.localEulerAngles = Vector3.Lerp(
            _saw2.transform.localEulerAngles,
            new Vector3(0, 90, 0),
            5f * Time.deltaTime
        );

        _sawTimer -= Time.deltaTime;
        if (_sawTimer <= 0)
        {
            _enableSaw = false;
        }
    }

    private void HandleNitro()
    {
        if (!_enableNitro)
        {
            _originalSpeed = _speed;
            return;
        }

        _speed = _originalSpeed * 2.5f;

        _nitroTimer -= Time.deltaTime;
        if (_nitroTimer <= 0)
        {
            _speed = _originalSpeed;
            _enableNitro = false;
        }
    }

    private void RotatePlayer()
    {
        var f = _power >= 0 ? 1 : -1;
        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime * f, 0);

        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(new Vector3(0, 1, 0), _velocity.normalized.magnitude * 10);
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

    public void AddSaw()
    {
        _saw.gameObject.SetActive(true);
    }

    public void StartSaw()
    {
        _enableSaw = true;
        _sawTimer = _sawMaxTime;
    }
    
    public void ImproveSaw()
    {
        _sawMaxTime += 2f;
    }

    public void Nitro()
    {
        _enableNitro = true;
        _nitroTimer = _nitroMaxTime;
    }

    public void ImproveNitro()
    {
        _nitroMaxTime += 2f;
    }
}