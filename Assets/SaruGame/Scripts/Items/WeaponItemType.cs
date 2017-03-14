using UnityEngine;
using System.Collections;
using Saru.Weapons;

namespace Saru.Items
{
    public class WeaponItemType : ItemType
    {
        private WeaponTypeEnum _weaponType;
        public WeaponTypeEnum WeaponType { get { return _weaponType; } }

        public WeaponItemType(WeaponTypeEnum weaponType)
        {
            _weaponType = weaponType;
        }
    }
}
