public class NextNodeCommand : ICommand
{
    DialogueManager treeManager;

    public NextNodeCommand(DialogueManager manager)
    {
        this.treeManager = manager;
    }

    public void Execute()
    {
        treeManager.PublishNextNode();
    }
}
public class ChooseNodeCommand : ICommand
{
    DialogueManager treeManager;

    public ChooseNodeCommand(DialogueManager manager)
    {
        this.treeManager = manager;
    }

    public void Execute()
    {
        treeManager.PublishChosenNode();
    }
}
