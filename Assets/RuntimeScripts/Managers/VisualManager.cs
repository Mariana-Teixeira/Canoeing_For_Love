using DialogueTree;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    public TextMeshProUGUI dialogueComponent;
    public TextMeshProUGUI nameComponent;
    public Image characterPortrait;
    public Image backgroundImage;
    float textSpeed = 0.03f;

    [SerializeField] private CanvasGroup dialogueCanvas;
    [SerializeField] private CanvasGroup choiceCanvas;

    private ChoicesPanel choicePanel;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
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

        if (hash.ContainsKey(DialogueEvents.GOTO_CHOICESPANEL))
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.GOTO_CHOICESPANEL]);

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
        StopAllCoroutines();
        ToggleChoiceCanvas(false);
        dialogueComponent.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));
    }

    void DisplayNameplate(string name)
    {
        if (name == "Narrator")
            characterPortrait.color = Color.gray;
        else
            characterPortrait.color = Color.white;

        nameComponent.text = name;
    }

    void DisplayCharacter(string characterPath)
    {
        Sprite characterSprite = Resources.Load(characterPath) as Sprite;
        characterPortrait.sprite = characterSprite;
    }

    void DisplayBackground(string backgroundPath)
    {
        if (backgroundPath == string.Empty)
            backgroundImage.color = Color.black;
        else
            backgroundImage.color = Color.white;

        characterPortrait.enabled = false;
        Sprite backgroundSprite = Resources.Load(backgroundPath) as Sprite;
        backgroundImage.sprite = backgroundSprite;
    }

    void DisplayChoicesPanel(DialogueChoices[] choices)
    {
        StopAllCoroutines();
        ToggleChoiceCanvas(true);
        StartCoroutine(choicePanel.GenerateChoices(choices));
    }

    void ToggleChoiceCanvas(bool boolean)
    {
        choiceCanvas.gameObject.SetActive(boolean);
        dialogueCanvas.interactable = !boolean;
        dialogueCanvas.blocksRaycasts = !boolean;
    }

    void ClearDialogueBox()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;
    }

    IEnumerator TypeLine(string dialogue)
    {
        foreach (char c in dialogue.ToCharArray())
        {
            dialogueComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
