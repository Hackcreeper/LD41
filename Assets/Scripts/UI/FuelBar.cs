using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelBar : MonoBehaviour
    {
        [SerializeField] private Car _player;
        [SerializeField] private Text _text;
        [SerializeField] private RectTransform _bar;

        private float _maxWidth;

        private void Start()
        {
            _maxWidth = _bar.sizeDelta.x;
        }

        private void Update()
        {
            var percentage = Mathf.RoundToInt(100f / Car.MaxFuel * _player.GetFuel());
            _text.text = $"{percentage}% Fuel";

            _bar.sizeDelta = new Vector2(
                _maxWidth / 100f * percentage,
                _bar.sizeDelta.y
            );
        }
    }
}