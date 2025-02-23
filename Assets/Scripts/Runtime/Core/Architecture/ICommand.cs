namespace Nara.Core.Architecture
{
    public interface ICommand
    {
        void Execute();
    }
    public interface ICommand<TResult>
    {
        TResult Execute();
    }
    
    public abstract class Command : ICommand
    {
        public void Execute() => OnExecute();
        protected abstract void OnExecute();
    }
    public abstract class Command<TResult> : ICommand<TResult>
    {
        public TResult Execute() => OnExecute();
        protected abstract TResult OnExecute();
    }
}

