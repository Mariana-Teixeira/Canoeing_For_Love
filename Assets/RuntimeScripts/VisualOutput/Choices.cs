using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    //4 buttons for 4 choices max, if there are less choices we can hide them
    private Button choiceOne;
    private Button choiceTwo;
    private Button choiceThree;
    private Button choiceFour;

    // Saves the choice that was made. Don't know if I save the button or only the text
    private Button choiceMade;


    // Start is called before the first frame update
    void Start()
    {   
        // Get the buttons and add onClick funcs to them
        choiceOne = GameObject.Find("Choice 1").GetComponent<Button>();
        choiceTwo = GameObject.Find("Choice 2").GetComponent<Button>();
        choiceThree = GameObject.Find("Choice 3").GetComponent<Button>();
        choiceFour = GameObject.Find("Choice 4").GetComponent<Button>();
        choiceOne.onClick.AddListener(() => TaskOnClick(1));
        choiceTwo.onClick.AddListener(() => TaskOnClick(2));
        choiceThree.onClick.AddListener(() => TaskOnClick(3));
        choiceFour.onClick.AddListener(() => TaskOnClick(4));
    }

    // Update is called once per frame
    void Update()
    {   
        // Inside Update we will get the text to put on each button, or maybe we can do this in start idk yet
        UpdateChoiceText(1, "oioi");
    }

    // onClick func, later will add something to do with choiceMade here
    void TaskOnClick(int n){
        if (n == 1){
            Debug.Log(choiceOne.GetComponentInChildren<Text>().text);
            choiceMade = choiceOne;
        }
        else if (n == 2){
            Debug.Log(choiceTwo.GetComponentInChildren<Text>().text);
            choiceMade = choiceTwo;
        }
        else if (n == 3){
            Debug.Log(choiceThree.GetComponentInChildren<Text>().text);
            choiceMade = choiceThree;
        }
        else if (n == 4){
            Debug.Log(choiceFour.GetComponentInChildren<Text>().text);
            choiceMade = choiceFour;
        }
        else{
            Debug.Log("No button was clicked");
        }
    }

    // Func to update the text on each button
    void UpdateChoiceText(int n, string t){
        if (n == 1){
            choiceOne.GetComponentInChildren<Text>().text = t;
        }
        else if (n == 2){
            choiceTwo.GetComponentInChildren<Text>().text = t;
        }
        else if (n == 3){
            choiceThree.GetComponentInChildren<Text>().text = t;
        }
        else if (n == 4){
            choiceFour.GetComponentInChildren<Text>().text = t;
        }
        else{
            Debug.Log("Button not found");
        }
    }

}
