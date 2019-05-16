using Unity.UIWidgets;

namespace QFramework.UIWidgets.ReduxPersist
{
    public class ReduxPersistMiddleware
    {
        public static Middleware<TState> create<TState>() where TState : AbstractPersistState<TState>, new()
        {
            return (store) => (next) => new DispatcherImpl((action) =>
            {
                var result = next.dispatch(action);
                var afterState = store.getState();
                afterState.Save();

                return result;
            });
        }
    }
}