namespace task18;

public interface ICommand
{
    void Execute();
}


public interface IScheduler
{
    bool HasCommand();
    ICommand Select();
    void Add(ICommand cmd);
}

public class Scheduler : IScheduler
{
    public Queue<ICommand> Commands = new Queue<ICommand>();

    public bool HasCommand()
    {
        return Commands.Count > 0;
    }

    public ICommand Select()
    {
        if (HasCommand())
        {
            ICommand cmd = Commands.Dequeue();
            Commands.Enqueue(cmd);
            return cmd;
        }

        return null;
    }

    public void Add(ICommand cmd)
    {
        if(cmd != null)
        {
            Commands.Enqueue(cmd);
        }
    
    }
}
