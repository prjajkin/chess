using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static  class PrefabsUtility
    {
        public static T InstantiatePrefab<T>(GameObject prefab, Transform parent)
        {
            var go = Object.Instantiate(prefab, parent, false);
            go.SetActive(true);
            return go.GetComponent<T>();
        }
        public static T InstantiatePrefab<T>(GameObject prefab, Transform parent,Vector3 position)
        {
            var go = Object.Instantiate(prefab, position,Quaternion.identity, parent);
            go.SetActive(true);
            return go.GetComponent<T>();
        }
    }
}
