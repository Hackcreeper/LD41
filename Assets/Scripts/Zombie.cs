using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float _speed = 200f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        var lookPos = Level.Instance.GetPlayer().position - transform.position;
        lookPos.y = 0;
        
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        var velocity = transform.forward * _speed * Time.deltaTime;
        
        _rigidbody.velocity = new Vector3(
            velocity.x,
            _rigidbody.velocity.y,
            velocity.z
        );
    }
}