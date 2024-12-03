using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTriggerByIndex : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex = 4; 
    [SerializeField] private string triggeringTag = "Car"; 

    private void OnTriggerEnter(Collider other)
    {
       print(other.tag);
        if (other.CompareTag(triggeringTag))
        {
            Debug.Log($"Changement vers la scène avec l'index : {targetSceneIndex}");
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
