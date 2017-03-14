using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

namespace Saru.Weapons
{
    public class WeaponProvider : SingletonMonoBehaviour<WeaponProvider>
    {
        private static Dictionary<WeaponTypeEnum, GameObject> _weaponList = null;

        private const string FolderPath = "Prefabs/Weapons/";

        void Awake()
        {
            _weaponList = new Dictionary<WeaponTypeEnum, GameObject>();
            _weaponList.Clear();

            StartCoroutine(LoadWeapons());
        }

        /// <summary>
        /// 武器をResourcesフォルダからロードする。
        /// </summary>
        public IEnumerator LoadWeapons()
        {
            _weaponList = 
                Enum.GetValues(typeof(WeaponTypeEnum))
                .Cast<WeaponTypeEnum>()
                .ToDictionary(x => x, x => Resources.Load(FolderPath + x.ToString()) as GameObject);
            yield return null;

        }

        /// <summary>
        /// 武器を取得する。
        /// </summary>
        /// <param name="weaponType">武器の種類</param>
        /// <returns>武器のGameObject</returns>
        public GameObject GetWeapons(WeaponTypeEnum weaponType)
        {
            if (!_weaponList.ContainsKey(weaponType)) return null;
            return _weaponList[weaponType];
        }
    }
}

