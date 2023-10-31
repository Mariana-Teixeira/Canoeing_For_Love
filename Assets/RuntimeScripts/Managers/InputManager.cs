    using DialogueTree;
using UnityEngine;
public class InputManager : MonoBehaviour, INodeSubscriber
{
    NodePublisher publisher;
    InputInvoker invoker;
    DialogueManager treeManager;
    ICommand command;

    private ChoicePanel cp;


    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    private void Awake()
    {
        publisher = GetComponent<NodePublisher>();
        invoker = new InputInvoker();
        treeManager = GetComponent<DialogueManager>();
    }
    private void Start()
    {
        command = new StartDialogueCommand(treeManager);
        invoker.AddCommand(command);
        cp = ChoicePanel.instace;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            invoker.AddCommand(command);
        }
        
    }

    public void OnNotifyNPC(NPCNode node)
    {
        command = new NextNodeCommand(treeManager);
    }

    public void OnNotifyPlayer(PlayerNode node)
    {
        command = new ChooseNodeCommand(treeManager);
    }

    public void OnNotifyEndConversation()
    {

    }
}