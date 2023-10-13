using DialogueTree;

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
