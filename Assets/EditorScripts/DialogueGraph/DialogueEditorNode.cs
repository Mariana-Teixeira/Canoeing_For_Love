using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DialogueEditor
{
    public class DialogueEditorNode : Node
    {
        public Guid Guid;

        public DialogueEditorNode() => Guid = Guid.NewGuid();

        public virtual void Draw()
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            inputContainer.Add(inputPort);
            inputContainer.Add(outputPort);
        }
    }

    public class ChoiceNode : DialogueEditorNode
    {
        string[] ChoiceDialogues;

        public override void Draw()
        {
            base.Draw();

            RefreshExpandedState();
        }
    }

    public class LinearNode : DialogueEditorNode
    {
        string Dialogue = "Default";

        public override void Draw()
        {
            base.Draw();

            TextField dialogueField = new TextField() { value = Dialogue };
            dialogueField.RegisterValueChangedCallback(evt => this.Dialogue = evt.newValue);
            extensionContainer.Add(dialogueField);

            RefreshExpandedState();
        }
    }
}