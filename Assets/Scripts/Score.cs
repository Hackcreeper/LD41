using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    private int _value;
    private float _timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (ZombieManager.Instance.IsGameOver()) return;

        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        _value++;
        _timer = 1f;
    }

    public void Increase(int amount = 5)
    {
        _value += amount;
    }

    public int Get => _value;
}