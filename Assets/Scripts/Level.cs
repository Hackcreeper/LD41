using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance;

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private GameObject _canisterPrefab;
    [SerializeField] private GameObject _rampPrefab;
    [SerializeField] private GameObject _repairSetPrefab;
    [SerializeField] private GameObject[] _upgradePrefabs;
    [SerializeField] private int _chunkScale = 32;

    private int _currentX = -1, _currentZ = -1;
    private Dictionary<string, GameObject> _chunks = new Dictionary<string, GameObject>();
    private List<string> _whitelisted = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var cx = Mathf.RoundToInt(_player.position.x / _chunkScale);
        var cz = Mathf.RoundToInt(_player.position.z / _chunkScale);

        if (_currentX != cx || _currentZ != cz)
        {
            _currentX = cx;
            _currentZ = cz;

            _whitelisted.Clear();
            GenerateChunks();
            RemoveOldChunks();
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
        _whitelisted.Add($"{x}_{z}");
        if (_chunks.ContainsKey($"{x}_{z}"))
        {
            return;
        }
        
        var chunk = Instantiate(_chunkPrefab, new Vector3(x * _chunkScale, -1, z * _chunkScale), Quaternion.identity);
        chunk.transform.Find("GFX").localScale = new Vector3(_chunkScale/2f, _chunkScale/2f, 0.1f);
        chunk.transform.SetParent(transform);
        _chunks.Add($"{x}_{z}", chunk);

        PlaceObjects(3, 20, _treePrefab, chunk);
        PlaceObjects(1, 10, _stonePrefab, chunk);
        PlaceObjects(0, 2, _canisterPrefab, chunk);
        PlaceObjects(0, 2, _repairSetPrefab, chunk);
        PlaceObjects(0, 2, _rampPrefab, chunk);
        PlaceObjects(0, 200, _upgradePrefabs[Random.Range(0, _upgradePrefabs.Length)], chunk);
    }

    private void RemoveOldChunks()
    {
        var toRemove = new List<string>();
        foreach (var keyValuePair in _chunks)
        {
            if (!_whitelisted.Contains(keyValuePair.Key))
            {
                toRemove.Add(keyValuePair.Key);
            }
        }

        toRemove.ForEach(key =>
        {
            Destroy(_chunks[key]);
            _chunks.Remove(key);
        });
    }

    private void PlaceObjects(int min, int max, GameObject prefab, GameObject chunk)
    {
        var amount = Random.Range(min, max);
        for (var i = 0; i < amount; i++)
        {
            var tree = Instantiate(prefab);
            tree.transform.SetParent(chunk.transform);
            tree.transform.localPosition = new Vector3(
                Random.Range(0, _chunkScale),
                0,
                Random.Range(0, _chunkScale)
            );
        }
    }
    
    public Transform GetPlayer() => _player;
}