namespace Nara.Core.Architecture
{
    public interface IModel
    {
        bool Initialized { get; set; }
        void Init();
        void Terminate();
    }
    public abstract class AbstractModel : IModel
    {
        public bool Initialized { get; set;}

        public void Init() => OnInit();
        public void Terminate() => OnTerminate();

        protected abstract void OnInit();
        protected virtual void OnTerminate() { }

    }
}

