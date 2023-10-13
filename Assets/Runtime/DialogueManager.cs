using DialogueTree;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;

    private void Awake() => tree = new DialogueRuntimeTree();

    public void PublishNextNode()
    {
        tree.GoToNextNode();
        NotifyObserver(tree.CurrentNode);
    }
}