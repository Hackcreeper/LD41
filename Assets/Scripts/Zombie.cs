using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _speed = 200f;

    private Rigidbody _rigidbody;
    private bool _killed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_killed) return;

        var lookPos = Level.Instance.GetPlayer().position - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        var velocity = transform.forward * _speed * Time.deltaTime;
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
}