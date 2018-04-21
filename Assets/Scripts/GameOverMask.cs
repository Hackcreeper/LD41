using UnityEngine;
using UnityEngine.UI;

public class GameOverMask : MonoBehaviour
{
    private float _speed = .5f;
    [SerializeField] private GameObject _textWrapper;
    [SerializeField] private Image _textFade;
    [SerializeField] private Text _infoText;

    private Image _image;
    private bool _faded;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!ZombieManager.Instance.IsGameOver()) return;

        var alpha = Mathf.Lerp(_image.color.a, 1f, _speed * Time.deltaTime);
        _image.color = new Color(
            0, 0, 0, alpha
        );

        if (alpha >= 0.9f) _faded = true;
        if (!_faded) return;

        _infoText.text = _infoText.text.Replace("{{score}}", Score.Instance.Get.ToString()); 
        
        _textWrapper.SetActive(true);
        var fadeAlpha = Mathf.Lerp(_textFade.color.a, 0f, _speed * 2f * Time.deltaTime);
        _textFade.color = new Color(
            0, 0, 0, fadeAlpha
        );
    }
}