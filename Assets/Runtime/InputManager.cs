using DialogueTree;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    DialogueManager treeManager;
    InputInvoker invoker;

    private void Awake()
    {
        treeManager = GetComponent<DialogueManager>();
        invoker = new InputInvoker();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ICommand nextNode = new NextNodeCommand(treeManager);
            invoker.AddCommand(nextNode);
        }
    } 
}