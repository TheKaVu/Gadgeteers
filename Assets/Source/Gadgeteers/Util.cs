using System;
using System.Collections.Generic;
using System.Reflection;
using Source.Gadgeteers.Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Gadgeteers
{
    public static class Util
    {
        public const float DamageReductionIndex = 50f;

        public static float Round(this float f, int index)
        {
            var p = Mathf.Pow(10, index);
            return Mathf.Round(f * p) / p;
        }

        public static T CreateFromPrefab<T>(string name = null) where T : MonoBehaviour
        {
            var prefabAttribute = typeof(T).GetCustomAttribute<PrefabAttribute>();
            if(prefabAttribute == null) return null;
            
            var prefab = Resources.Load<GameObject>($"Prefabs/{prefabAttribute.Id}");
            if(prefab == null) throw new PrefabNotFoundException($"There is no such prefab as: {prefabAttribute.Id}");
            var o = Object.Instantiate(prefab);
            o.name = name ?? typeof(T).Name;
            return o.GetComponent<T>();
        }
    }
}