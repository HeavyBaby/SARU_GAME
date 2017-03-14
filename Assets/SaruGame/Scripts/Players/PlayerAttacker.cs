using UnityEngine;
using System.Collections;
using UniRx;
using Saru.Weapons;
using Saru.Damages;
using System;

namespace Saru.Players
{
    public class PlayerAttacker : MonoBehaviour, IAttacker
    {
        [SerializeField]
        private int _playerId = 1;

        [SerializeField]
        private WeaponTypeEnum DefaultWeapon;

        private ReactiveProperty<WeaponBase> _currentWeapon = new ReactiveProperty<WeaponBase>();

        private IObservable<bool> AttackObservable;



        public int OwnerId { get { return _playerId; } }

        private GameObject DefaultWeaponPrefab
        {
            get { return WeaponProvider.Instance.GetWeapons(DefaultWeapon); }
        }

        void ChangeWeapon(GameObject weaponObject)
        {
            if(_currentWeapon.Value != null && _currentWeapon.Value.gameObject != null)
            {
                Destroy(_currentWeapon.Value.gameObject);

            }

            var wo = Instantiate(weaponObject, transform.position, Quaternion.identity) as GameObject;
            EquipeWeapon(wo.GetComponent<WeaponBase>());

        }

        void EquipeWeapon(WeaponBase weapon)
        {
            weapon.InitWeapon(this, AttackObservable);
            weapon.transform.SetParent(transform, true);
            weapon.transform.localPosition = new Vector3(0.25f, transform.localPosition.y, 1.0f);

            _currentWeapon.Value = weapon;
        }



        void Start()
        {
            var input = GetComponent<IPlayerInput>();
            var core = GetComponent<PlayerCore>();

            AttackObservable = input.OnAttackButtonObservable.TakeUntil(core.OnPlayerDeadObservable).Where(_ => core.IsPlayerControllable.Value);
            ChangeWeapon(DefaultWeaponPrefab);

        }
    }
}

