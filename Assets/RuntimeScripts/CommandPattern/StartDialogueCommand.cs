public class StartDialogueCommand : ICommand
{
    DialogueManager treeManager;

    public StartDialogueCommand(DialogueManager treeManager)
    {
        this.treeManager = treeManager;
    }

    public void Execute()
    {
        treeManager.StartDialogueTree();
    }
}