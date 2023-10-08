using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DialogueEditor
{
    public class DialogueEditorGraph : GraphView
    {
        #region Adding Manipulators to the Graph Editor
        public DialogueEditorGraph()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(CreateRightClickMenu());
        }

        private IManipulator CreateRightClickMenu()
        {
            System.Action<ContextualMenuPopulateEvent> menuBuilder = menuEvent =>
            {
                menuEvent.menu.AppendAction("Create Linear Node", action => { AddElement(CreateLinearNode()); });
                menuEvent.menu.AppendAction("Create Choice Node", action => { AddElement(CreateChoiceNode()); });
            };
            ContextualMenuManipulator rightClickMenu = new ContextualMenuManipulator(menuBuilder);
            return rightClickMenu;
        }
        #endregion

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList();
        }

        ChoiceNode CreateChoiceNode()
        {
            ChoiceNode node = new ChoiceNode();
            node.Draw();
            return node;
        }

        LinearNode CreateLinearNode()
        {
            LinearNode node = new LinearNode();
            node.Draw();
            return node;
        }
    }
}