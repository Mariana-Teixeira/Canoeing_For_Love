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
    public float textSpeed;


    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    private void Start()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;
    }

    public void OnNotifyNPC(NPCNode node)
    {
        dialogueComponent.text = string.Empty;
        if (node == null)
        {
            EndDialogue();
            return;
        }

        nameComponent.text = node.DisplayName;
        Sprite characterSprite = Resources.Load(node.ImagePath) as Sprite;
        characterPortrait.sprite = characterSprite;
        StartCoroutine(TypeLine(node.CharacterDialogue));
    }

    public void OnNotifyPlayer(PlayerNode node)
    {
        return;
    }

    public void EndDialogue()
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
}
