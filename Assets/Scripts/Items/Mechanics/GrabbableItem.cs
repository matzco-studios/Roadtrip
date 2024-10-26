using System.Collections.Generic;
using UnityEngine;

namespace Items.Mechanics
{
    public abstract class GrabbableItem : MonoBehaviour
    {
        public Quaternion Rotation;
        public string Name;

        public delegate void KeyAction();

        public Dictionary<KeyCode, KeyAction> ActionDictionary = new();

        public GrabbableItem(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public GrabbableItem()
        {
        }
    }
}