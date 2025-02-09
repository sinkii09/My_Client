using Nara.Patterns;
using System;
using System.Linq;
using UnityEngine;

namespace Nara.Core.Architecture
{
    public interface IApplication
    {
        void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem;
        void RegisterModel<TModel>(TModel model) where TModel : IModel;
        void RegisterController<TController>(TController controller) where TController : IController;
        void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility;

        TSystem GetSystem<TSystem>() where TSystem : class, ISystem;
        TModel GetModel<TModel>() where TModel : class, IModel;
        TUtility GetUtility<TUtility>() where TUtility : class, IUtility;
    
        void SendCommand<T>(T command) where T : ICommand;
        TResult SendCommand<TResult>(ICommand<TResult> command);

    }
    public abstract class App<T> : Singleton<T>, IApplication where T : App<T>
    {
        private bool initialized;
        private ServiceLocator serviceLocator = new ServiceLocator();
        public static IApplication Inteface
        {
            get
            {
                return Instance;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            if (!initialized)
            {
                Init();
                foreach (var model in serviceLocator.GetServicesByType<IModel>().Where(model => !model.Initialized))
                {
                    model.Init();
                    model.Initialized = true;
                }

                foreach (var system in serviceLocator.GetServicesByType<ISystem>().Where(s => !s.Initialized))
                {
                    system.Init();
                    system.Initialized = true;
                }

                initialized = true;
            }
        }
        private void OnDestroy()
        {
            Terminate();
        }
        protected abstract void Init();
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
        protected virtual void OnTerminate()
        {

        }
        public void RegisterController<TController>(TController controller) where TController : IController
        {
            serviceLocator.Register<TController>(controller);
            if (initialized)
            {
                
            }
        }

        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            serviceLocator.Register<TModel>(model);
            if (initialized)
            {
                model.Init();
                model.Initialized = true;
            }
        }

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem
        {
            serviceLocator.Register<TSystem>(system);
            if (initialized)
            {
                system.Init();
                system.Initialized = true;
            }
        }

        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility
        {
            serviceLocator.Register<TUtility>(utility);
        }

        TModel IApplication.GetModel<TModel>()
        {
            return serviceLocator.Get<TModel>();
        }

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
    }
}

