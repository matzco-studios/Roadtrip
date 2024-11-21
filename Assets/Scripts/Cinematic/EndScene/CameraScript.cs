using System;
using System.Collections;
using Map;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class CameraScript : MonoBehaviour
    {
        private float _rotation;
        private const float MaxRotation = -90f;
        private const float RotationDelta = 50f;
        [SerializeField] private GameObject _brokenEffect;
        [SerializeField] private AudioSource _brokenSound;
        [SerializeField] private Zone _zone;

        private void OnEnable()
        {
            transform.SetParent(null);
            StartCoroutine(ChangeRotation());
        }

        private IEnumerator ChangeRotation()
        {
            while (_rotation > MaxRotation)
            {
                _rotation = Mathf.MoveTowards(_rotation, MaxRotation, RotationDelta * Time.deltaTime);
                transform.rotation = Quaternion.Euler(_rotation, 180f, 0f);
                yield return null;
            }

            _brokenSound.Play();
            _brokenEffect.SetActive(true);
            _zone.StopZone();
            yield return null;
        }
    }
}