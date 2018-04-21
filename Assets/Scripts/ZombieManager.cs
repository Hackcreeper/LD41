using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _zombiePrefab;
    private int _maxZombies = 200;
    private int _spawnPerSecond = 10;
    private int _spawnRadius = 35;

    private readonly List<Transform> _zombies = new List<Transform>();
    private float _timer = 3f;

    private void Update()
    {
        SpawnZombies();
        // Remove old zombies
        // Make a distance check for each zombie
        // Destroy zombey object
        // Decrease zombie count
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
            
            var zombie = Instantiate(_zombiePrefab, new Vector3(x, 1, z), Quaternion.identity);
            _zombies.Add(zombie.transform);
        }
    }
}