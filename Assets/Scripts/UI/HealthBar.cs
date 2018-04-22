using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class HealthBar : MonoBehaviour
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
			var percentage = Mathf.RoundToInt(100f / Car.MaxHealth * _player.GetHealth());
			_text.text = $"{percentage}% Health";
    
			_bar.sizeDelta = new Vector2(
				_maxWidth / 100f * percentage,
				_bar.sizeDelta.y
			);
		}
	}
}