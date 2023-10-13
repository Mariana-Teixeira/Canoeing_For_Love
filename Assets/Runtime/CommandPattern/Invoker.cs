using System.Collections.Generic;
using System.Xml.Serialization;

public class Invoker
{
    Stack<ICommand> commands;

    public Invoker() => commands = new Stack<ICommand>();

    public void AddCommand(ICommand command)
    {
        command.Execute();
        //commands.Push(command);
    }
}
