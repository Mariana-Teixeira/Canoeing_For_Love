using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    public TextMeshProUGUI dialogueComponent;
    public TextMeshProUGUI nameComponent;
    public Image characterPortrait;
    public float textSpeed;

    private GameObject choiceCanvas;

    private GameObject button1;

    private DialogueData dd;

    [SerializeField] private CanvasGroup dialogueCanvas;


    List<Guid> guids = new List<Guid>();
    private ChoicePanel cp;

    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    private void Start()
    {
        dialogueComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        choiceCanvas = GameObject.Find("ChoiceCanvas");
        button1 = GameObject.Find("Button");
        cp = ChoicePanel.instace;
        button1.SetActive(false);
        dd = new DialogueData();
    }

    public void OnNotifyNPC(NPCNode node)
    {
        dialogueCanvas.interactable = true;
        dialogueCanvas.blocksRaycasts = true;
        choiceCanvas.SetActive(false);
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
        dialogueCanvas.interactable = false;
        dialogueCanvas.blocksRaycasts = false;
        List<string> choices = new List<string>();
        
        choiceCanvas.SetActive(true);
        foreach(var dialogue in node.Choices){
            print(dialogue.NextNodeGUID);
            guids.Add(dialogue.NextNodeGUID);
            choices.Add(dialogue.choiceDialogue); 
        }
        string[] choicesArray = choices.ToArray();
        cp.Show(choicesArray);
        // have to get a way to wait for the input before calling this function
        StartCoroutine(CheckHasAnswer());
    }

    public IEnumerator CheckHasAnswer(){
        yield return new WaitUntil(()=>cp.AcceptAnswer(index: cp.getAnswer()));
        cp.Hide();
        choiceCanvas.SetActive(false);        
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
