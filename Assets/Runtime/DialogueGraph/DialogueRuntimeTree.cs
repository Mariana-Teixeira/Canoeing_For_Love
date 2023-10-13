using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueTree
{
    public class DialogueRuntimeTree : MonoBehaviour, IObserver
    {
        DialogueData data;
        DialogueRuntimeNode currentNode;
        public Subject inputSubject;
        Invoker dialogueInvoker;

        private void OnEnable() => inputSubject.AddObserver(this);
        private void Awake() => LoadData();
        void LoadData()
        {
            data = new DialogueData();
            data.graph.TryGetValue(data.headNode, out currentNode);
        }


        private void Start()
        {
            dialogueInvoker = new Invoker();
            ExecuteCommand();
        }

        public void OnNotify()
        {
            GoToNextNode();
            ExecuteCommand();
        }
        void GoToNextNode()
        {
            NPCNode node = (NPCNode)currentNode;
            data.graph.TryGetValue(node.NextNodeGUID, out currentNode);
        }

        public void ExecuteCommand()
        {
            ICommand command = new DisplayDialogueCommand(currentNode.ToString());
            dialogueInvoker.AddCommand(command);
        }

        private void OnDisable() => inputSubject.RemoveObserver(this);
    }
}