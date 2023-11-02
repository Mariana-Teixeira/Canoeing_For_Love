using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    public TextMeshProUGUI dialogueComponent;
    public TextMeshProUGUI nameComponent;
    public Image characterPortrait;
    public float textSpeed;

    [SerializeField] private CanvasGroup dialogueCanvas;
    [SerializeField] private CanvasGroup choiceCanvas;


    List<Guid> guids = new List<Guid>();
    private ChoicesPanel choicePanel;

    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    private void Start()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;

        choicePanel = ChoicesPanel.instance;
        choiceCanvas.enabled = false;
    }

    public void OnNotifyNPC(NPCNode node)
    {
        if (node == null)
        {
            EndDialogue();
            return;
        }

        ToggleChoiceCanvas(false);

        dialogueComponent.text = string.Empty;
        nameComponent.text = node.DisplayName;
        Sprite characterSprite = Resources.Load(node.ImagePath) as Sprite;
        characterPortrait.sprite = characterSprite;
        StartCoroutine(TypeLine(node.CharacterDialogue));
    }

    public void OnNotifyPlayer(PlayerNode node)
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
}
