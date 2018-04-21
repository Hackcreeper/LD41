using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private int _chunkScale = 32;

    private int _currentX = -1, _currentZ = -1;
    private Dictionary<string, GameObject> _chunks = new Dictionary<string, GameObject>();

    private void Update()
    {
        var cx = Mathf.RoundToInt(_player.position.x / _chunkScale);
        var cz = Mathf.RoundToInt(_player.position.z / _chunkScale);

        if (_currentX != cx || _currentZ != cz)
        {
            _currentX = cx;
            _currentZ = cz;

            GenerateChunks();
        }
    }

    private void GenerateChunks()
    {
        Generate(_currentX, _currentZ);
        Generate(_currentX, _currentZ - 1);
        Generate(_currentX, _currentZ + 1);
        Generate(_currentX - 1, _currentZ);
        Generate(_currentX + 1, _currentZ);
        Generate(_currentX - 1, _currentZ - 1);
        Generate(_currentX + 1, _currentZ - 1);
        Generate(_currentX - 1, _currentZ + 1);
        Generate(_currentX + 1, _currentZ + 1);
    }

    private void Generate(int x, int z)
    {
        if (_chunks.ContainsKey($"{x}_{z}"))
        {
            return;
        }
        
        var chunk = Instantiate(_chunkPrefab, new Vector3(x*_chunkScale, -1, z*_chunkScale), Quaternion.identity);
        chunk.transform.localScale = new Vector3(_chunkScale, 1, _chunkScale);
        chunk.transform.SetParent(transform);
        _chunks.Add($"{x}_{z}", chunk);
    }
}