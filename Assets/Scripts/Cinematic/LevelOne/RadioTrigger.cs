using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    private AudioSource _radioSound;

    private void Start() {
        _radioSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Car")) {
            _radioSound.Play();
        }
    }
}
