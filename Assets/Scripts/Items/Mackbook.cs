using UnityEngine;

namespace Items
{
    public class Mackbook : GrabbableItem
    {
        public Mackbook() : base(Quaternion.Euler(0, 107.799995f, 0))
        {
            ActionDictionary.Add(KeyCode.A, () => { print("Hello World!"); });
        }
    }
}