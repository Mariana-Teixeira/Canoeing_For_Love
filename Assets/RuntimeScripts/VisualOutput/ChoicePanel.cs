using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security;
public class ChoicePanel : MonoBehaviour
{
    public static ChoicePanel instace {set; get;}

    private const float BUTTON_MIN_WIDTH = 50;
    private const float BUTTON_MAX_WIDTH = 500;
    private const float BUTTON_WIDTH_PADDING = 25;

    private const float BUTTON_HEIGHT_LINE = 50f;
    private const float BUTTON_HEIGHT_PADDING = 20;

    [SerializeField] private GameObject choiceButtonPrefab;

    [SerializeField] private VerticalLayoutGroup buttonLayoutGroup;

    [SerializeField] private CanvasGroup canvasGroup;

    private List<ChoiceButton> buttons = new List<ChoiceButton>();

    //boolean used to know if we are waiting for user choosing 
    public bool isWaitingUser {get; private set;} = false;
    	
    public ChoicePanelDecision decision {get; private set;} = null;

    void Awake(){
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // Used to show options on buttons
    public void Show(string[] choices){
        decision = new ChoicePanelDecision(choices);
        isWaitingUser = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        StartCoroutine(GenerateChoices(choices));
    }

    // Generates and displays choices and buttons on screen
    private IEnumerator GenerateChoices(string[] choices){
        float maxWidth = 0;
        // iterating and creating buttons for each option
        for(int i  = 0; i < choices.Length; i++){
            ChoiceButton choiceButton;
            if(i<buttons.Count){
                choiceButton = buttons[i];
            }
            else{
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
            choiceButton.title.text = choices[i];

            float buttonWidth = Mathf.Clamp(BUTTON_WIDTH_PADDING + choiceButton.title.preferredWidth, BUTTON_MIN_WIDTH, BUTTON_MAX_WIDTH);
            maxWidth = Mathf.Max(maxWidth, buttonWidth);
        }
        foreach(var button in buttons){
            button.layout.preferredWidth = maxWidth;
        }

        for(int i = 0; i < buttons.Count; i++){
            bool show = i < choices.Length;
            buttons[i].button.gameObject.SetActive(show);

        }
        yield return new WaitForEndOfFrame();

        foreach(var button in buttons){
            int lines = button.title.textInfo.lineCount;
            button.layout.preferredHeight = BUTTON_HEIGHT_PADDING + (BUTTON_HEIGHT_LINE*lines);
        }

    }

    // Hide canvas Group
    public void Hide(){
        isWaitingUser = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // True if there's a valid answer
    public bool AcceptAnswer(int index){

        if(index < 0 || index > decision.choices.Length - 1){
            return false;
        }
        else{
            decision.answerIndex = index;
            isWaitingUser = false;
            return true;
        }
        
    }

    // get the answer
    public int getAnswer(){
        return decision.answerIndex;
    }

    public class ChoicePanelDecision{
        public int answerIndex = -1;
        public string[] choices = new string [0];
        public ChoicePanelDecision(string[] choices){
            answerIndex = -1;
            this.choices = choices;
        }
    }

    private struct ChoiceButton{
        public Button button;
        public TextMeshProUGUI title;
        public LayoutElement layout;
    }

}
