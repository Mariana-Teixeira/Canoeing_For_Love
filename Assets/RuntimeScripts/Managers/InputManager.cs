using DialogueTree;
using UnityEngine;

public class InputManager : MonoBehaviour, INodeSubscriber
{
    DialogueManager treeManager;
    InputInvoker invoker;
    ICommand command;

    ICommand comms;

    VisualManager vm;

    #region Node Publisher
    NodePublisher publisher;

    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    private void Awake()
    {
        publisher = GetComponent<NodePublisher>();
        treeManager = GetComponent<DialogueManager>();
        invoker = new InputInvoker();
    }
    private void Start()
    {
        comms = new LoadFileCommand(treeManager);
        invoker.AddCommand(comms);
        command = new StartDialogueCommand(treeManager);
        invoker.AddCommand(command);
        vm = VisualManager.instanceVisual;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) ){
            if(vm.lineFinish){
                invoker.AddCommand(command);
            }
            else  {
                vm.FinishLine();
            }
        }
        
    }

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        command = new NextNodeCommand(treeManager);
    }

    void OnApplicationQuit(){
        command = new SaveFileCommand(treeManager);
        invoker.AddCommand(command);
    }
}