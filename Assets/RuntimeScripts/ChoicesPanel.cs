using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security;
using DialogueTree;

public class ChoicesPanel : MonoBehaviour
{
    public static ChoicesPanel instance {set; get;}

    private const float BUTTON_MIN_WIDTH = 50;
    private const float BUTTON_MAX_WIDTH = 500;
    private const float BUTTON_WIDTH_PADDING = 25;

    private const float BUTTON_HEIGHT_LINE = 50f;
    private const float BUTTON_HEIGHT_PADDING = 20;

    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private VerticalLayoutGroup buttonLayoutGroup;

    private List<ChoiceButton> buttons = new List<ChoiceButton>();
    	
    public ChoicePanelDecision decision {get; private set;} = null;

    void Awake() => instance = this;

    /// <summary>
    /// Generate and display buttons for each dialogue choice on screen.
    /// </summary>
    public IEnumerator GenerateChoices(DialogueChoices[] choices){
        decision = new ChoicePanelDecision(choices);
        float maxWidth = 0;
        
        // Iterating and creating buttons for each option.
        for(int i  = 0; i < choices.Length; i++){
            ChoiceButton choiceButton;
            if(i < buttons.Count)
            {
                choiceButton = buttons[i];
            }
            else
            {
                GameObject newButtonObject = Instantiate(choiceButtonPrefab, buttonLayoutGroup.transform);
                newButtonObject.SetActive(true);

                Button newButton = newButtonObject.GetComponent<Button>();
                TextMeshProUGUI newTitle = newButton.GetComponentInChildren<TextMeshProUGUI>();
                LayoutElement newLayout = newButton.GetComponent<LayoutElement>();

                choiceButton = new ChoiceButton {button=newButton, title=newTitle, layout=newLayout};
                buttons.Add(choiceButton);
            }

            choiceButton.button.onClick.RemoveAllListeners();
            int buttonIndex = i;
            choiceButton.button.onClick.AddListener(() => AcceptAnswer(buttonIndex));
            choiceButton.title.text = choices[i].choiceDialogue;

            float buttonWidth = Mathf.Clamp(BUTTON_WIDTH_PADDING + choiceButton.title.preferredWidth, BUTTON_MIN_WIDTH, BUTTON_MAX_WIDTH);
            maxWidth = Mathf.Max(maxWidth, buttonWidth);
        }

        foreach(var button in buttons)
        {
            button.layout.preferredWidth = maxWidth;
        }

        for(int i = 0; i < buttons.Count; i++)
        {
            bool show = i < choices.Length;
            buttons[i].button.gameObject.SetActive(show);

        }

        yield return new WaitForEndOfFrame();

        foreach(var button in buttons)
        {
            int lines = button.title.textInfo.lineCount;
            button.layout.preferredHeight = BUTTON_HEIGHT_PADDING + (BUTTON_HEIGHT_LINE*lines);
        }

    }

    /// <summary>
    /// Return true if the answer is valid.
    /// </summary>
    public bool AcceptAnswer(int index)
    {
        if(index < 0 || index > decision.choices.Length - 1)
        {
            return false;
        }
        else
        {
            decision.answerIndex = index;
            return true;
        }
        
    }

    public int GetAnswer(){
        return decision.answerIndex;
    }

    public class ChoicePanelDecision
    {
        public int answerIndex = -1;
        public DialogueChoices[] choices = new DialogueChoices[0];

        public ChoicePanelDecision(DialogueChoices[] choices)
        {
            answerIndex = -1;
            this.choices = choices;
        }
    }

    private struct ChoiceButton
    {
        public Button button;
        public TextMeshProUGUI title;
        public LayoutElement layout;
    }

}
