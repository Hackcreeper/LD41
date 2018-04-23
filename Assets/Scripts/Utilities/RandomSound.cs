using UnityEngine;

namespace Utilities
{
    public class RandomSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _audioClips;
        private float _delay = 0.5f;

        private AudioSource _audioSource;
        private float _timer = 4f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (ZombieManager.Instance.IsGameOver()) return;
            
            _timer -= Time.deltaTime;

            if (_timer > 0f || _audioSource.isPlaying) return;

            var clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _audioSource.clip = clip;
            _audioSource.Play();
            _timer = clip.length + _delay;
        }
    }
}