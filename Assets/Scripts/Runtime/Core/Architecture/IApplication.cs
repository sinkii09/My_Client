using Nara.Patterns;
using System;
using System.Linq;
using UnityEngine;

namespace Nara.Core.Architecture
{
    public interface IApplication
    {
        void RegisterService(object service);
        void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem;
        void RegisterModel<TModel>(TModel model) where TModel : class, IModel;
        void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility;

        TSystem GetSystem<TSystem>() where TSystem : class, ISystem;
        TModel GetModel<TModel>() where TModel : class, IModel;
        TUtility GetUtility<TUtility>() where TUtility : class, IUtility;
    
        void SendCommand<T>(T command) where T : ICommand;
        TResult SendCommand<TResult>(ICommand<TResult> command);
        TResult SendQuery<TResult>(IQuery<TResult> query);

    }
    /// <summary>
    /// This class provides a centralized way to manage and access various systems, models, controllers, and utilities
    /// within the application. It leverages the Singleton pattern to ensure a single instance throughout the application
    /// lifecycle and uses a Service Locator to manage dependencies and service registrations.
    /// </summary>
    /// <typeparam name="T">The type of the derived application class.</typeparam>
    
    public abstract class App<T> : Singleton<T>, IApplication where T : App<T>
    {
        private ServiceLocator serviceLocator = new ServiceLocator();
        public static IApplication Inteface
        {
            get
            {
                return Instance;
            }
        }
        public bool Initialized { get; private set; }

        public event Action OnAppInit;

        private void OnDestroy()
        {
            Terminate();
        }


        public void StartGame(ServiceConfigSO config)
        {
            if (!Initialized)
            {
                RegisterServices(config);
                InitializeServices();
                Initialized = true;
                OnAppInit?.Invoke();
            }
        }
        private void RegisterServices(ServiceConfigSO config)
        {
            if (config == null)
            {
                Debug.LogWarning($"[GameApp] Service Config is null, no service can be registered");
                return;
            }

            config?.RegisterServices();
            Debug.Log($"[GameApp] all services has been Registered");
        }
        private void InitializeServices()
        {
            foreach (var model in serviceLocator.GetServicesByType<IModel>().Where(model => !model.Initialized))
            {
                model.Init();
                model.Initialized = true;
                Debug.Log($"[App] Model of type {model.GetType().FullName} initialized");
            }

            foreach (var system in serviceLocator.GetServicesByType<ISystem>().Where(s => !s.Initialized))
            {
                system.Init();
                system.Initialized = true;
                Debug.Log($"[App] System of type {system.GetType().FullName} initialized");
            }

        }
        public void Terminate()
        {
            OnTerminate();
            foreach (var system in serviceLocator.GetServicesByType<ISystem>().Where(s => s.Initialized))
            {
                system.Terminate();
            }
            foreach (var model in serviceLocator.GetServicesByType<IModel>().Where(m => m.Initialized))
            {
                model.Terminate();
            }
            serviceLocator.Clear();
        }
        protected virtual void OnTerminate() { }
        public void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            serviceLocator.Register<TModel>(model);
            if (Initialized)
            {
                model.Init();
                model.Initialized = true;
            }
        }
        public void RegisterService(object service)
        {
            serviceLocator.Register(service);
        }
        public void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            serviceLocator.Register(system);
            if (Initialized)
            {
                system.Init();
                system.Initialized = true;
            }
        }

        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            if (serviceLocator.TryGet<TUtility>(out var _))
            {
                Debug.LogWarning($"[App] RegisterUtility: Utility of type {typeof(TUtility).FullName} already registered");
                return;
            }
            serviceLocator.Register<TUtility>(utility);
        }

        TModel IApplication.GetModel<TModel>() => serviceLocator.Get<TModel>();

        TSystem IApplication.GetSystem<TSystem>()
        {
            return serviceLocator.Get<TSystem>();
        }

        TUtility IApplication.GetUtility<TUtility>()
        {
            return serviceLocator.Get<TUtility>();
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            ExecuteCommand(command);
        }

        protected virtual void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }

        public TResult SendCommand<TResult>(ICommand<TResult> command)
        {
            return command.Execute();
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            return query.Do();
        }
    }
}

