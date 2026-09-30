using System;
using System.Collections.Generic;
using UnityEngine;


namespace SGLib.Utility.Management.ServiceLocator
{
    [CreateAssetMenu(menuName = "SG Lib/Utility/Management/Service Locator")]
    public class ServiceLocator_So : ScriptableObject
    {
        [SerializeField] private ServiceBase_So[] services;

        private readonly Dictionary<Type, ServiceBase_So> registry = new();

        public bool Active { get; private set; } = false;

        public void SetActive(bool active) => Active = active;

        public void Install()
        {
            for (int i = 0; i < services.Length; i++)
            {
                Register(services[i]);
            }
        }

        public void Register<T>(T service) where T : ServiceBase_So
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            var type = service.GetType();

            if (registry.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"A service of type '{type.FullName}' is already registered. " +
                    "Duplicate service registration is not allowed."
                );
            }

            registry[type] = service;
        }

        public void Unregister<T>() where T : ServiceBase_So
        {
            registry.Remove(typeof(T));
        }

        public T Get<T>() where T : ServiceBase_So
        {
            if (!Active) throw new InvalidOperationException(
                "(disabled) The ServiceLocator is currently disabled. Call SetActive(true) before attempting to use it.");

            if (registry.TryGetValue(typeof(T), out var service))
                return service as T;

            return default;
        }

        public void Clear()
        {
            registry.Clear();
        }
    }
}

