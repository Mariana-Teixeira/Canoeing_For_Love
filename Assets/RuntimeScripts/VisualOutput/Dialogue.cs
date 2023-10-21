using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Dialogue : MonoBehaviour
{   
    // shows text on dialog window
    public TextMeshProUGUI textComponent;

    // Shows who is speaking
    public TextMeshProUGUI nameComponent;
    
    // lines that will be represented
    public List<string> lines = new();

    // speed at which the text is written in the dialog window
    public float textSpeed;

    // used to iterate over the lines, might not be needed in the future
    private int index;

    private GameObject character;

    private GameObject nameHolder;
    // Start is called before the first frame update
    void Start()
    {
        // get image for ken, idk if this is how we gonna do it but oh well...
        character = GameObject.FindWithTag("Ken");
        nameHolder = GameObject.FindWithTag("nameHolder");
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        lines = FileManager.ReadTextFile("testfile.txt");
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {   
        // Updates text
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

    // types out a line
    IEnumerator TypeLine(){
        CheckForCharacters();
        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Go to next line
    void NextLine(){
        if (index < lines.Count - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.SetActive(false);
            nameHolder.SetActive(false);
        }
    }

    // Checks if there are characters speaking or if it is the narrator
    // Only working for ken and needs to be changed
    void CheckForCharacters()
    {
        if (lines[index].Contains("Ken")){
            character.SetActive(true);
            lines[index] = lines[index].Split(":")[1];
            nameComponent.text = "Ken";
        }
        else{
            character.SetActive(false);
            nameComponent.text = "Narrator";
        }
    }
}
