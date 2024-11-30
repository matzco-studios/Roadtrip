using System.Collections;
using Map;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private GameObject _brokenEffect;
        [SerializeField] private AudioSource _brokenSound;
        [SerializeField] private GameObject _toBeContinued;
        private Zone _zone;
        private Animator _animator;
        private bool _endRotation;
        private PlayerScript _player;

        public bool IsEndRotation() => _endRotation;

        public void PlayerZoom() {
            _animator.SetTrigger("PlayerZoom");
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
            _zone.Stop();
            _player.Stop();

            yield return new WaitForSeconds(1f);
            _toBeContinued.SetActive(true);

        }
    }
}