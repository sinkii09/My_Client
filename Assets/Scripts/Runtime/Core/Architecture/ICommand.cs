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
    
}

