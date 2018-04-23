using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Transform _car;
        [SerializeField] private GameObject _light;
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _carSpeed = 12f;
        [SerializeField] private GameObject _menuObject;

        private int _step;
        private float _carTimer = 1f;

        private void Update()
        {
            if (_step == 1)
            {
                _menuObject.SetActive(false);
                _camera.position = Vector3.Lerp(_camera.position, _cameraTarget.position, _speed * Time.deltaTime);
                _camera.rotation = Quaternion.Lerp(_camera.rotation, _cameraTarget.rotation, _speed * Time.deltaTime);

                if (Vector3.Distance(_camera.position, _cameraTarget.position) <= 0.1f)
                {
                    _step = 2;
                }
            }

            if (_step == 2)
            {
                _camera.SetParent(_car);
                _car.Translate(new Vector3(0, 0, _carSpeed * Time.deltaTime), Space.Self);
                _carTimer -= Time.deltaTime;

                if (_carTimer <= 0) _step = 3;
            }

            if (_step == 3)
            {
                _car.Translate(new Vector3(0, 0, _carSpeed * Time.deltaTime), Space.Self);
                _light.SetActive(true);
                _carTimer = 1f;
                _step = 4;
            }

            if (_step == 4)
            {
                _car.Translate(new Vector3(0, 0, _carSpeed * Time.deltaTime), Space.Self);
                _carTimer -= Time.deltaTime;

                if (_carTimer <= 0)
                {
                    _carSpeed *= 2;
                    _step = 5;
                    _carTimer = 1f;
                }
            }

            if (_step == 5)
            {
                _fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(_fadeImage.color.a, 1f, _speed * Time.deltaTime));

                _car.Translate(new Vector3(0, 0, _carSpeed * Time.deltaTime), Space.Self);
                _carTimer -= Time.deltaTime;

                if (_carTimer <= 0)
                {
                    SceneManager.LoadScene("Game");
                }
            }

            if (Input.anyKeyDown && _step == 0)
            {
                _step = 1;
            }
        }
    }
}