using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Update()
        {
            _text.text = $"Score: {Score.Instance.Get}";
        }
    }
}