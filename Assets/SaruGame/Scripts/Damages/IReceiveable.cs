using UnityEngine;
using System.Collections;

namespace Saru.Damages
{
    public interface IReceiveable
    {
        void ApplyDamage(DamageInfo damage);
    }
}