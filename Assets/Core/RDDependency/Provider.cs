using UnityEngine;

namespace RDDependency
{
    public class Provider : MonoBehaviour
    {
        Factory _factory = new Factory();
        
        public T ProvideService<T>() where T : ISetup, new()
        {
            var newObj = new T();
            newObj.Setup();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab) where T : MonoBehaviour, ISetup
        {
            var newObj = _factory.Create(prefab).GetComponent<T>();
            newObj.Setup();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position) where T : MonoBehaviour, ISetup
        {
            var newObj = _factory.Create(prefab, position).GetComponent<T>();
            newObj.Setup();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position, Quaternion quaternion) where T : MonoBehaviour, ISetup
        {
            var newObj = _factory.Create(prefab, position, quaternion).GetComponent<T>();
            newObj.Setup();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour, ISetup
        {
            var newObj = _factory.Create(prefab, position, rotation, parent);
            newObj.Setup();
            return newObj;
        }
    }
}