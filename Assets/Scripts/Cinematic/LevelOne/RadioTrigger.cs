using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    private AudioSource _radioSound;
    private bool hasPlayed = false;

    private void Start() {
        _radioSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Car") && !hasPlayed) {
            hasPlayed = true;
            _radioSound.Play();
        }
    }
}
