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
    public float textSpeed = 0.03f;

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
        if (hash.ContainsKey(DialogueEvents.OPEN_CHOICES_PANEL))
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.OPEN_CHOICES_PANEL]);
        else if (hash.ContainsKey(DialogueEvents.DISPLAY_DIALOGUE))
            DisplayDialogue((string)hash[DialogueEvents.DISPLAY_DIALOGUE]);
        else
            ClearDialogueBox();

        if (hash.ContainsKey(DialogueEvents.DISPLAY_CHARACTER))
            DisplayCharacter((Character)hash[DialogueEvents.DISPLAY_CHARACTER]);
    }

    void DisplayDialogue(string dialogue)
    {
        StopAllCoroutines();
        ToggleChoiceCanvas(false);
        dialogueComponent.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));
    }

    void DisplayCharacter(Character character)
    {
        nameComponent.text = character.Name;
        Sprite characterSprite = Resources.Load(character.PortraitPath) as Sprite;
        characterPortrait.sprite = characterSprite;
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
