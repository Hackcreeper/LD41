using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public static Upgrades Instance;

    [SerializeField] private Car _player;

    private readonly Dictionary<Type, int> _upgrades = new Dictionary<Type, int>();
    private readonly List<Type> _slots = new List<Type>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Type type)
    {
        if (_upgrades.ContainsKey(type))
        {
            Improve(type);
            _upgrades[type]++;
            return;
        }

        Initial(type);
        _upgrades.Add(type, 1);
    }

    private void Initial(Type type)
    {
        switch (type)
        {
            case Type.Saw:
                _player.AddSaw();
                _slots.Add(type);
                break;
            case Type.Nitro:
                _slots.Add(type);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void Improve(Type type)
    {
        switch (type)
        {
            case Type.Saw:
                _player.ImproveSaw();
                break;
            case Type.Nitro:
                _player.ImproveNitro();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public List<Type> GetSlots() => _slots;

    public enum Type
    {
        Saw, Nitro
    }
}