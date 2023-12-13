using DialogueTree;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, INodeSubscriber
{
    VisualManager vm;

    UIGameManager ui;
    DialogueManager treeManager;
    InputInvoker invoker;
    ICommand command;
    ICommand comms;

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
        comms = new NewFileCommand(treeManager);
        invoker.AddCommand(comms);
        comms = new LoadFileCommand(treeManager);
        invoker.AddCommand(comms);
        command = new StartDialogueCommand(treeManager);
        invoker.AddCommand(command);
        vm = VisualManager.instanceVisual;
        ui = UIGameManager.instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject.name == "ButtonPrefab(Clone)") && !ui.waitingOnUser){
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