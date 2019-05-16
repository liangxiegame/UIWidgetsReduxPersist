using System.Collections.Generic;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
// 1.import namespace
using QFramework.UIWidgets.ReduxPersist;

namespace QF.UIWidgets.ReduxPersist.Example
{
    // 2.declare state
    class ExampleState : AbstractPersistState<ExampleState>
    {
        public int Count = 0;
    }

    public class Example : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            var store = new Store<ExampleState>((state, action) =>
                {
                    switch (action)
                    {
                        case IncreaseCountAction _:
                            return new ExampleState
                            {
                                Count = state.Count + 1
                            };

                        case DecreaseCountAction _:
                            return new ExampleState
                            {
                                Count = state.Count - 1
                            };
                    }

                    return state;
                },
                // 3.call load
                ExampleState.Load(),
                // 4.add middleware
                ReduxPersistMiddleware.create<ExampleState>());
            return new StoreProvider<ExampleState>(store,
                child: new StoreConnector<ExampleState, int>(
                    converter: state => state.Count,
                    builder: (context, model, dispatcher) =>
                    {
                        return new Row(
                            children: new List<Widget>()
                            {
                                new FlatButton(
                                    child: new Text("-"),
                                    onPressed: () => { dispatcher.dispatch(new DecreaseCountAction()); }
                                ),
                                new Text(model.ToString()),
                                new FlatButton(
                                    child: new Text("+"),
                                    onPressed: () => { dispatcher.dispatch(new IncreaseCountAction()); }
                                )
                            }
                        );
                    }
                )
            );
        }
    }

    class IncreaseCountAction
    {

    }

    class DecreaseCountAction
    {

    }
}