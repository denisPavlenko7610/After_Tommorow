using AfterTomorrow.Core.RDDependency;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RDDependency
{
    public class Provider : DIMonoBehaviour
    {
        Factory _factory = new Factory();

        IoCContainer _container;
        public void Init(IoCContainer container)
        {
            _container = container;
        }

        public override void OnAwake()
        {
            base.OnAwake();
            ProcessData(monoBehaviour => monoBehaviour.OnAwake());
        }

        public override void OnStart()
        {
            base.OnStart();
            ProcessData(monoBehaviour => monoBehaviour.OnStart());
        }

        public T ProvideService<T>() where T : DIMonoBehaviour, new()
        {
            var newObj = gameObject.AddComponent<T>();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab) where T : DIMonoBehaviour
        {
            var newObj = _factory.Create(prefab).GetComponent<T>();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position) where T : DIMonoBehaviour
        {
            var newObj = _factory.Create(prefab, position).GetComponent<T>();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position, Quaternion quaternion) where T : DIMonoBehaviour
        {
            var newObj = _factory.Create(prefab, position, quaternion).GetComponent<T>();
            return newObj;
        }
        
        public T ProvideFactory<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : DIMonoBehaviour
        {
            var newObj = _factory.Create(prefab, position, rotation, parent);
            return newObj;
        }

        public void Setup<T>(T instance)
        {
            _container.Register(instance);
        }
        
        void ProcessData(Action<DIMonoBehaviour> action)
        {
            var data = _container.getData();
            foreach (KeyValuePair<Type, object> item in data)
            {
                DIMonoBehaviour monoBehaviour = item.Value as DIMonoBehaviour;
                if (monoBehaviour)
                {
                    action(monoBehaviour);
                }
            }
        }
    }
}