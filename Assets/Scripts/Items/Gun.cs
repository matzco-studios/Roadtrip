using System;
using Items.Mechanics;
using UnityEngine;

namespace Items
{
    public class Gun : GrabbableItem
    {
        [SerializeField]
        private int _magSize = 7;
        [SerializeField]
        private float _damage = 13f;
    }
}