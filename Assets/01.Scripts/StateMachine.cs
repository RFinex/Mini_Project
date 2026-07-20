using UnityEngine;

public interface IState<T>
{
    public void Enter(T obj);

    public void Exit(T obj);

    public void Update(T obj);
}

public class StateMachine<T>
{
    protected IState<T> currentState;

    private T obj;

    public void ChangeState(IState<T> state)
    {
        if (currentState != null)
            currentState.Exit(obj);

        currentState = state;
        currentState.Enter(obj);
    }

    public void Update()
    {
        currentState.Update(obj);
    }
}
