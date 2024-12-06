using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    FadeInOut fade;
    private bool _triggered;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }	
    private IEnumerator LoadScene() {
        _triggered = true;
        var scene = SceneManager.LoadSceneAsync(3);
        scene.allowSceneActivation = false;

        yield return new WaitUntil(() => {
            print($"IS_DONE: {scene.isDone} AND Loading scene {scene.progress}...");
            return scene.progress < 0.9f;
        });
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        scene.allowSceneActivation = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Car") && !_triggered) {
            StartCoroutine(LoadScene());
        }
    }
}
