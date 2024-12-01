using System.Collections;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cinematic.EndScene
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private AudioSource _endSound;
        [SerializeField] private GameObject _brokenEffect;
        [SerializeField] private AudioSource _brokenSound;
        [SerializeField] private GameObject _multiColorBars;
        [SerializeField] private AudioSource _noSignalSound;
        [SerializeField] private GameObject _blackScreen;
        [SerializeField] private AudioSource _finalMessage;

        private Zone _zone;
        private Animator _animator;
        private bool _endRotation;
        private PlayerScript _player;

        public bool IsEndRotation() => _endRotation;

        public void PlayerZoom() {
            _animator.SetTrigger("PlayerZoom");
        }

        private void Awake() {
            _endSound.Play();
        }

        private void Start() {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
            _animator = GetComponent<Animator>();
            _zone = GameObject.FindWithTag("Zone").GetComponent<Zone>();
            _animator.SetTrigger("StartOutZoom");
        }

        public IEnumerator EndRotation()
        {
            _endRotation = true;
            _animator.SetTrigger("EndRotation");

            yield return new WaitUntil(() => transform.rotation.x >= 0.24f);
            _brokenSound.Play();
            _brokenEffect.SetActive(true);
            _player.Stop();

            yield return new WaitForSeconds(1.5f);
            _endSound.Stop();
            _multiColorBars.SetActive(true);
            _noSignalSound.Play();
            _brokenEffect.SetActive(false);
            _zone.Stop();

            yield return new WaitForSeconds(1f);
            _blackScreen.SetActive(true);
            _finalMessage.Play();
            _multiColorBars.SetActive(false);
            _noSignalSound.Stop();
            yield return new WaitWhile(() => _finalMessage.isPlaying);
            SceneManager.LoadScene(0);
        }
    }
}