using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
public class Dialogue : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    private GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Ken");
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){
            if(textComponent.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        if (lines[index].Contains("Ken")){
            character.SetActive(true);
        }
        else{
            character.SetActive(false);
        }
        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if (index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.SetActive(false);
        }
    }
}
