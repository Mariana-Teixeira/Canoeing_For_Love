using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace DialogueEditor
{
    public class DialogueEditorWindow : EditorWindow
    {
        DialogueEditorGraph graphView;

        [MenuItem("DialogueTree/Editor")]
        public static void OpenDialogueWidndow()
        {
            GetWindow<DialogueEditorWindow>();
        }

        // Visually compose the editor window.
        private void OnEnable()
        {
            graphView = new DialogueEditorGraph();
            graphView.StretchToParentSize();

            Toolbar toolbar = new Toolbar();
            Button saveButton = new Button(() => SaveData()) { text = "Save" };
            toolbar.Add(saveButton);

            rootVisualElement.Add(toolbar);
            rootVisualElement.Add(graphView);
        }

        // Outputs a DialogueData file.
        private void SaveData()
        {
        }
    }
}