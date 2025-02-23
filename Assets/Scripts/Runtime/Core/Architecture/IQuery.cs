namespace Nara.Core.Architecture
{
    public interface IQuery<TResult>
    {
        TResult Do();
    }
    public abstract class AbstractQuery<TResult> : IQuery<TResult>
    {
        public TResult Do() => OnDo();

        protected abstract TResult OnDo();


    }
}

