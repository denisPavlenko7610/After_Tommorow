using RDDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AfterTomorrow.Core.RDDependency
{
    public class IoCContainer
    {
        Dictionary<Type, object> _container = new Dictionary<Type, object>();

        public Dictionary<Type, object> getData() => _container;

        public void Register<TInterface, TConcrete>() where TConcrete : TInterface
        {
            var instance = Activator.CreateInstance<TConcrete>();
            _container[typeof(TInterface)] = instance;
            Inject(instance);
        }

        public void Register<T>(T instance)
        {
            _container[typeof(T)] = instance;
            Inject(instance);
        }
        
        public void Unregister<T>()
        {
            _container.Remove(typeof(T));
        }
        
        public List<MonoBehaviour> FindObjectsWithInjectAttribute()
        {
            List<MonoBehaviour> objectsWithInjectAttribute = new List<MonoBehaviour>();

            foreach (var monoBehaviour in Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
            {
                var injectMethods = monoBehaviour.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m => Attribute.IsDefined(m, typeof(InjectAttribute)));

                if (injectMethods.Any())
                {
                    objectsWithInjectAttribute.Add(monoBehaviour);
                }
            }

            return objectsWithInjectAttribute;
        }
        
        public void InjectOnScene(List<MonoBehaviour> objectsToInject)
        {
            foreach (var monoBehaviour in objectsToInject)
            {
                Inject(monoBehaviour);
            }
        }
       
        void Inject(object obj)
        {
            var injectMethods = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => Attribute.IsDefined(m, typeof(InjectAttribute)));

            foreach (var method in injectMethods)
            {
                var parameters = method.GetParameters()
                    .Select(p => _container[p.ParameterType]).ToArray();
                method.Invoke(obj, parameters);
            }
        }
    }
}