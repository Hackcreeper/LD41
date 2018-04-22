using UnityEngine;

public class Zombie : MonoBehaviour
{
    public bool IsElite;
    
    [SerializeField] private float _speed = 200f;

    private Rigidbody _rigidbody;
    private bool _killed;
    private float _damageTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_killed) return;

        _damageTimer -= Time.deltaTime;
        var lookPos = Level.Instance.GetPlayer().position - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        var velocity = transform.forward * _speed * Time.deltaTime * (IsElite ? 3 : 1);
        if (_killed)
        {
            velocity = Vector3.zero;
        }

        _rigidbody.velocity = new Vector3(
            velocity.x,
            _rigidbody.velocity.y,
            velocity.z
        );
    }

    public void Kill()
    {
        _killed = true;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        transform.localEulerAngles = new Vector3(
            270,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
        );
    }

    public bool IsDead() => _killed;

    private void OnCollisionStay(Collision other)
    {
        if (_killed) return;
        if (!other.gameObject.CompareTag("Player")) return;

        if (_damageTimer <= 0f)
        {
            other.gameObject.GetComponent<Car>().Damage();
            _damageTimer = 2f;
        }
    }
}