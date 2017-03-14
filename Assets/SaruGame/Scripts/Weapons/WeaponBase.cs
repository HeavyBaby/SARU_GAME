using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using Saru.Damages;

namespace Saru.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        protected IAttacker _attacker = null;

        protected IObservable<bool> OnAttackObservable = null;

        private Transform _weaponTransform = null;


        /// <summary>
        /// 武器の種類
        /// </summary>
        public WeaponTypeEnum WeaponType { get; protected set; }

        protected Transform WeaponTransform
        {
            get
            {
                if (_weaponTransform != null) return _weaponTransform;

                var weapon = GetComponentInChildren<WeaponTransform>();
                _weaponTransform = weapon != null ? weapon.transform : transform;

                return _weaponTransform;
            }
        }

        /// <summary>
        /// 武器の初期化を行う。
        /// </summary>
        /// <param name="attacker">攻撃する者</param>
        /// <param name="attackObservable">攻撃イベント</param>
        public void InitWeapon(IAttacker attacker,IObservable<bool> attackObservable)
        {
            _attacker = attacker;
            OnAttackObservable = attackObservable.TakeUntil(this.OnDestroyAsObservable());

        }
    }
}

