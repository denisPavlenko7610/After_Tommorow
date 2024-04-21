using UnityEngine;

namespace RDDependency
{
    public class Factory
    {
        public T Create<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
        {
            T instance = Object.Instantiate(prefab, position, rotation, parent);
            return instance;
        }

        public T Create<T>(T prefab, Vector3 position) where T : MonoBehaviour
        {
            T instance = Object.Instantiate(prefab, position, Quaternion.identity,null);
            return instance;
        }
        
        public T Create<T>(T prefab, Vector3 position, Quaternion quaternion) where T : MonoBehaviour
        {
            T instance = Object.Instantiate(prefab, position, quaternion,null);
            return instance;
        }
        
        public T Create<T>(T prefab, Transform parent = null) where T : MonoBehaviour
        {
            T instance = Object.Instantiate(prefab, parent);
            return instance;
        }
    }
}