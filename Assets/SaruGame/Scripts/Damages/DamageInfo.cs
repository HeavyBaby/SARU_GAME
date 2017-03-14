using UnityEngine;
using System.Collections;

namespace Saru.Damages
{
    public struct DamageInfo
    {
        public int DamageValue { get; private set; }
        public IAttacker AttackerInfo { get; private set; }

        public DamageInfo(int value, IAttacker attacker) : this()
        {
            DamageValue = value;
            AttackerInfo = attacker;
        }
    }
}