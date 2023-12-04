using DialogueTree;
using UnityEngine;

public class InputManager : MonoBehaviour, INodeSubscriber
{
    VisualManager vm;
    DialogueManager treeManager;
    InputInvoker invoker;
    ICommand command;


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
}