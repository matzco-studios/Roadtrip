using System.Collections;
using UnityEngine;


namespace Map
{
    public class PostLampScript : MonoBehaviour
    {
        float minFlickerSpeed = 0.01f;
        float maxFlickerSpeed = 0.8f;
        float minUpTime = 0.1f;
        float maxUpTime = 4f;
        public Light _spotlight;
        public Light _pointLight;
        void Start()
        {
            StartCoroutine(FlickerLight());
        }

        IEnumerator FlickerLight()
        {
            while (true)
            {
                Debug.Log("Flickering");
                _spotlight.enabled = true;
                _pointLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(minUpTime, maxUpTime));
                _spotlight.enabled = false;
                _pointLight.enabled = false;
                yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            }
        }
    }
}