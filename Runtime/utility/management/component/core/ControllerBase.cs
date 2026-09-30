using System;
using System.Collections.Generic;
using SGLib.Utility.Generators;
using SGLib.Utility.Management.Component.Capabilities.Sync;
using SGLib.Utility.Patterns.Scene.Contexts;
using UnityEngine;

namespace SGLib.Utility.Management.Component
{
    public abstract class ControllerBase : ComponentBase, IRegistrable
    {
        [SerializeField] protected ComponentBase[] ComponentArray;
        protected Dictionary<Type, ComponentBase> ComponentDict;

        public int Id { get; private set; }

        public override void Initialize()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            ComponentDict = new Dictionary<Type, ComponentBase>(ComponentArray.Length);

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.Initialize();

                ComponentDict[component.GetType()] = component;
            }

            // set the unique Id for this controller
            Id = IdGenerator.GenerateId();
        }

        public override void BindContext(SceneCtx sceneCtx, AppCtx appCtx)
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            base.BindContext(sceneCtx, appCtx);

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.BindContext(sceneCtx, appCtx);
            }
        }

        public override void BindComponents()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.BindComponents();
            }
        }

        public virtual void Register() { }
        public virtual void UnRegister() { }

        public override void Configure()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.Configure();
            }
        }

        protected virtual void OnEnable()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.Activate();
            }
        }

        protected virtual void OnDisable()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.Deactivate();
            }
        }

        protected virtual void OnApplicationQuit()
        {
            if (ComponentArray == null) throw new InvalidOperationException(
                "(missing) components storage uninitialized.");

            for (int i = 0; i < ComponentArray.Length; i++)
            {
                var component = ComponentArray[i];
                if (component == null) continue;
                component.Deconfigure();
            }
        }

        public T GetSGComponent<T>() where T : ComponentBase
        {
            if (ComponentDict == null) throw new InvalidOperationException(
                    "(missing) components dictionary uninitialized.");

            if (ComponentDict.Count == 0) return null;

            return ComponentDict.TryGetValue(typeof(T), out var component)
                ? (T)component : null;
        }

        public T RequireSGComponent<T>() where T : ComponentBase
        {
            var component = GetSGComponent<T>();

            if (component == null) throw new InvalidOperationException(
                    "(missing) component not found.");

            return component;
        }
    }
}