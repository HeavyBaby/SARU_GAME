using UnityEngine;
using System.Collections;
using System;
using Saru.Weapons;

namespace Saru.Items
{
    public class WeaponItemObject : MonoBehaviour, IItemObject
    {
        [SerializeField]
        private WeaponTypeEnum _weapon;

        public ItemType ItemType
        {
            get { return new WeaponItemType(_weapon); }
        }

        public void PickupItem()
        {
            Destroy(gameObject);
        }
    }
}