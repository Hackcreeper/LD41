using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Slots : MonoBehaviour
    {
        [SerializeField] private Car _player;

        [SerializeField] private RectTransform _slot1;
        [SerializeField] private RectTransform _slot2;
        [SerializeField] private RectTransform _slot3;

        [SerializeField] private Sprite _sawTexture;
        [SerializeField] private Sprite _nitroTexture;

        private float _cooldown1;
        private float _cooldown2;
        private float _cooldown3;

        private int _cachedLength;

        private void Update()
        {
            HandleKeys();
            HandleCooldowns();

            var slots = Upgrades.Instance.GetSlots();
            if (slots.Count == _cachedLength) return;
            _cachedLength = slots.Count;

            if (slots.Count >= 1) InitSlot(_slot1, slots[0]);
            if (slots.Count >= 2) InitSlot(_slot2, slots[1]);
            if (slots.Count >= 3) InitSlot(_slot3, slots[2]);
        }

        private void HandleKeys()
        {
            if (_cachedLength >= 1 && Input.GetKeyDown(KeyCode.Alpha1) && _cooldown1 <= 0)
            {
                _cooldown1 = Execute(Upgrades.Instance.GetSlots()[0]);
            }

            if (_cachedLength >= 2 && Input.GetKeyDown(KeyCode.Alpha2) && _cooldown2 <= 0)
            {
                _cooldown2 = Execute(Upgrades.Instance.GetSlots()[1]);
            }

            if (_cachedLength >= 3 && Input.GetKeyDown(KeyCode.Alpha3) && _cooldown3 <= 0)
            {
                _cooldown3 = Execute(Upgrades.Instance.GetSlots()[2]);
            }
        }

        private void HandleCooldowns()
        {
            if (_cooldown1 > 0) _cooldown1 -= Time.deltaTime;
            if (_cooldown2 > 0) _cooldown2 -= Time.deltaTime;
            if (_cooldown3 > 0) _cooldown3 -= Time.deltaTime;

            if (_cachedLength >= 1)
            {
                var cooldown1 = _slot1.Find("Cooldown");
                cooldown1.gameObject.SetActive(_cooldown1 > 0);
                cooldown1.Find("Cooldown_Text").GetComponent<Text>().text = Mathf.RoundToInt(_cooldown1).ToString();
            }

            if (_cachedLength >= 2)
            {
                var cooldown = _slot2.Find("Cooldown");
                cooldown.gameObject.SetActive(_cooldown2 > 0);
                cooldown.Find("Cooldown_Text").GetComponent<Text>().text = Mathf.RoundToInt(_cooldown2).ToString();
            }

            if (_cachedLength >= 3)
            {
                var cooldown = _slot3.Find("Cooldown");
                cooldown.gameObject.SetActive(_cooldown3 > 0);
                cooldown.Find("Cooldown_Text").GetComponent<Text>().text = Mathf.RoundToInt(_cooldown3).ToString();
            }
        }

        private float Execute(Upgrades.Type type)
        {
            switch (type)
            {
                case Upgrades.Type.Saw:
                    _player.StartSaw();
                    return 5;
                case Upgrades.Type.Nitro:
                    _player.Nitro();
                    return 30;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void InitSlot(Transform slot, Upgrades.Type type)
        {
            slot.Find("Number_BG").GetComponent<Image>().color = Color.white;

            var image = slot.Find("Image").GetComponent<Image>();
            image.color = Color.white;

            switch (type)
            {
                case Upgrades.Type.Saw:
                    image.sprite = _sawTexture;
                    break;
                case Upgrades.Type.Nitro:
                    image.sprite = _nitroTexture;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}