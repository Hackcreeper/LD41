using UnityEngine;

namespace UI
{
	public class Pause : MonoBehaviour
	{
		public static Pause Instance;
		
		[SerializeField] private GameObject _pauseUi;
		
		private bool _paused;

		private void Awake()
		{
			Instance = this;
			Time.timeScale = 1f;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_paused = !_paused;
				_pauseUi.SetActive(_paused);
				if (_paused) Level.Instance.GetPlayer().GetComponent<AudioSource>().Pause();
				Time.timeScale = _paused ? 0 : 1;
			}
		}

		public bool isPaused => _paused;
	}
}
