public class NewFileCommand : ICommand
{
    DialogueManager treeManager;

    public NewFileCommand(DialogueManager treeManager)
    {
        this.treeManager = treeManager;
    }

    public void Execute()
    {
        treeManager.NewGame();
    }
}