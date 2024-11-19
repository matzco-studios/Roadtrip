using System;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class CameraScript : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.SetParent(null);
            transform.Rotate(270, 0, 0);
        }
    }
}