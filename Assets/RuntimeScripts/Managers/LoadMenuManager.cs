using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenuManager : MonoBehaviour
{
    [SerializeField] public Button button1;
    [SerializeField] public Button button2;

    [SerializeField] public Button button3;

    [SerializeField] public Button button4;

    [SerializeField] public Button button5;

    [SerializeField] public Button button6;

    [SerializeField] public Image img1;
    [SerializeField] public Image img2;
    [SerializeField] public Image img3;
    [SerializeField] public Image img4;
    [SerializeField] public Image img5;
    [SerializeField] public Image img6;

    static string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);


    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(() => ManageButtons(0));
        button2.onClick.AddListener(() => ManageButtons(1));
        button3.onClick.AddListener(() => ManageButtons(2));
        button4.onClick.AddListener(() => ManageButtons(3));
        button5.onClick.AddListener(() => ManageButtons(4));
        button6.onClick.AddListener(() => ManageButtons(5));   
    }

    void Update(){
        if(jsonObj["loaders"][0]["image"]!="" && img1.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][0]["image"]);
            img1.sprite = image;
        }
        if(jsonObj["loaders"][1]["image"]!="" && img2.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][1]["image"]);
            img2.sprite = image;
        }
        if(jsonObj["loaders"][2]["image"]!="" && img3.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][2]["image"]);
            img3.sprite = image;
        }
        if(jsonObj["loaders"][3]["image"]!="" && img4.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][3]["image"]);
            img4.sprite = image;
        }
        if(jsonObj["loaders"][4]["image"]!="" && img5.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][4]["image"]);
            img5.sprite = image;
        }
        if(jsonObj["loaders"][5]["image"]!="" && img6.sprite == null){
            Sprite image = Resources.Load<Sprite>("screens/" + jsonObj["loaders"][5]["image"]);
            img6.sprite = image;
        }
    }

    // Update is called once per frame
    public void ManageButtons(int i){
        // string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
        // dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        jsonObj["active"]= i;
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json", output);
        SceneManager.LoadScene(1);
    }
}
