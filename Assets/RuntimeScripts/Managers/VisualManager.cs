using DialogueTree;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    public static VisualManager instanceVisual {get; set;}
    public TextMeshProUGUI dialogueComponent;
    public TextMeshProUGUI nameComponent;
    public Image characterPortrait;
    public Image backgroundImage;
    float textSpeed = 0.03f;

    [HideInInspector] public string dialogueChecker = "";
    [HideInInspector] public bool lineFinish = false;


    [SerializeField] private CanvasGroup dialogueCanvas;

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject attractionUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject charactersUI;
    [SerializeField] private CanvasGroup choiceCanvas;

    private ChoicesPanel choicePanel;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() {
        publisher = GetComponent<NodePublisher>();
        instanceVisual = this;
    } 
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    private void Start()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;

        choicePanel = ChoicesPanel.instance;
        choiceCanvas.enabled = false;
    }

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        var hash = node.DialogueEvents;

        if (hash.ContainsKey(DialogueEvents.SHOW_CHOICESPANEL))
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.SHOW_CHOICESPANEL]);

        if (hash.ContainsKey(DialogueEvents.SHOW_DIALOGUE))
            DisplayDialogue((string)hash[DialogueEvents.SHOW_DIALOGUE]);            

        if (hash.ContainsKey(DialogueEvents.DISPLAY_CHARACTER))
        {
            characterPortrait.enabled = true;
            characterPortrait.color = Color.white;
            DisplayCharacter((string)hash[DialogueEvents.DISPLAY_CHARACTER]);
        }

        if (hash.ContainsKey(DialogueEvents.DISPLAY_BACKGROUND))
            DisplayBackground((string)hash[DialogueEvents.DISPLAY_BACKGROUND]);

        if (hash.ContainsKey(DialogueEvents.SHOW_NAMEPLATE))
            DisplayNameplate((string)hash[DialogueEvents.SHOW_NAMEPLATE]);
    }

    void DisplayDialogue(string dialogue)
    {
        lineFinish = false;
        ToggleChoiceCanvas(false);
        StopAllCoroutines();
        dialogueComponent.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));  
    }

    void DisplayNameplate(string name)
    {
        if (name == "Narrator" || name == "Developers")
            characterPortrait.enabled = false;
        else{
            characterPortrait.enabled = true;
            characterPortrait.color = Color.white;
        }
        nameComponent.text = name;
    }

    void DisplayCharacter(string characterPath)
    {   
        if(characterPath=="ENDING"){
            dialogueUI.SetActive(false);
            attractionUI.SetActive(false);
            menuUI.SetActive(false);
            charactersUI.SetActive(false);
        }
        Sprite characterSprite = Resources.Load("characters/" + characterPath) as Sprite;
        characterPortrait.sprite = characterSprite;
    }

    void DisplayBackground(string backgroundPath)
    {
        if (backgroundPath == "black"){
            characterPortrait.enabled = false;
            backgroundImage.color = Color.black;
        }
        else{
            backgroundImage.color = Color.white;
            Sprite backgroundSprite = Resources.Load("backgrounds/" + backgroundPath) as Sprite;
            backgroundImage.sprite = backgroundSprite;
        }
        //characterPortrait.enabled = false;
        
    }

    void DisplayChoicesPanel(DialogueChoices[] choices)
    {
        StopAllCoroutines();
        ToggleChoiceCanvas(boolean: true);
        StartCoroutine(choicePanel.GenerateChoices(choices));
    }

    void ToggleChoiceCanvas(bool boolean)
    {
        choiceCanvas.gameObject.SetActive(boolean);
        dialogueCanvas.interactable = !boolean;
        dialogueCanvas.blocksRaycasts = !boolean;
    }

    public void FinishLine(){
        StopAllCoroutines();
        dialogueComponent.text = dialogueChecker;
        lineFinish = true;
    }

    IEnumerator TypeLine(string dialogue)
    {
        dialogueChecker = dialogue;
        foreach (char c in dialogue.ToCharArray())
        {
            dialogueComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        lineFinish = true;
    }
}
