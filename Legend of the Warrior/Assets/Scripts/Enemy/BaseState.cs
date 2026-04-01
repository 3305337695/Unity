public abstract class BaseState
{
    protected Enemy currentEnemy;

    public abstract void OnEnter(Enemy enemy);

    public abstract void Executing();

    public abstract void OnExit();

}
