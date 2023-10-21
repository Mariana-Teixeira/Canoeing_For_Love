using System.Collections.Generic;

public class InputInvoker
{
    Stack<ICommand> commands;

    public InputInvoker() => commands = new Stack<ICommand>();

    public void AddCommand(ICommand command)
    {
        command.Execute();
        commands.Push(command);
    }
}
