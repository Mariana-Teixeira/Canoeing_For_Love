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
        if (node.GetType() == typeof(PlayerNode))
        {
            PlayerNode pNode = (PlayerNode)node;
            UpdatePlayer(pNode);
        }
        else
        {
            NPCNode nNode = (NPCNode)node;
            UpdateNPC(nNode);
        }
    }

    public void UpdateNPC(NPCNode node)
    {
        if (node == null)
        {
            EndDialogue();
            return;
        }
        ToggleChoiceCanvas(false);
        ChangeCharacterPortrait(node.Character);
        dialogueComponent.text = string.Empty;
        StartCoroutine(TypeLine(node.Dialogue));
    }

    public void UpdatePlayer(PlayerNode node)
    {
        ToggleChoiceCanvas(true);
        StartCoroutine(choicePanel.GenerateChoices(node.Choices));
    }

    void ToggleChoiceCanvas(bool boolean)
    {
        choiceCanvas.gameObject.SetActive(boolean);
        dialogueCanvas.interactable = !boolean;
        dialogueCanvas.blocksRaycasts = !boolean;
    }

    void EndDialogue()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        StopAllCoroutines();
    }

    IEnumerator TypeLine(string dialogue)
    {
        foreach (char c in dialogue.ToCharArray())
        {
            dialogueComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void ChangeCharacterPortrait(Character character)
    {
        nameComponent.text = character.Name;
        Sprite characterSprite = Resources.Load(character.PortraitPath) as Sprite;
        characterPortrait.sprite = characterSprite;
    }
}
