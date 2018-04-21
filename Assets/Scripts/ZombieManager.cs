using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;
    
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private int _maxZombies = 200;
    [SerializeField] private int _spawnPerSecond = 20;
    [SerializeField] private int _spawnRadius = 35;

    private readonly List<Transform> _zombies = new List<Transform>();
    private float _timer = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        SpawnZombies();
        RemoveOldZombies();
    }

    private void SpawnZombies()
    {
        _timer -= Time.deltaTime;

        if (_zombies.Count >= _maxZombies) return;
        if (_timer > 0f) return;

        _timer = 1f;
        for (var i = 0; i < _spawnPerSecond; i++)
        {
            var angle = Random.value * Mathf.PI * 2f;
            var x = _player.position.x + Mathf.Cos(angle) * _spawnRadius;
            var z = _player.position.z + Mathf.Sin(angle) * _spawnRadius;

            var zombie = Instantiate(_zombiePrefab, new Vector3(x, 0, z), Quaternion.identity);
            _zombies.Add(zombie.transform);
        }
    }

    private void RemoveOldZombies()
    {
        var toBeRemoved = new List<Transform>();
        _zombies.ForEach(zombie =>
        {
            if (Vector3.Distance(zombie.position, Level.Instance.GetPlayer().position) >= 50)
            {
                toBeRemoved.Add(zombie);
            }
        });

        toBeRemoved.ForEach(zombie =>
        {
            _zombies.Remove(zombie);
            Destroy(zombie.gameObject);
        });
    }

    public void Kill(Transform zombie)
    {
        _zombies.Remove(zombie);
        zombie.GetComponent<Zombie>().Kill();
    }
}