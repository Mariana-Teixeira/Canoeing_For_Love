using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    static string json;
    dynamic jsonObj;

    
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
        json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
        jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        if(jsonObj["loaders"][0]["image"]!="" && img1.sprite == null){
            var imgData1 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image0.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData1);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img1.sprite = sprite;
        }
        if(jsonObj["loaders"][1]["image"]!="" && img2.sprite == null){
            var imgData2 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image1.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData2);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img2.sprite = sprite;
        }
        if(jsonObj["loaders"][2]["image"]!="" && img3.sprite == null){
            var imgData3 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image2.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData3);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img3.sprite = sprite;
        }
        if(jsonObj["loaders"][3]["image"]!="" && img4.sprite == null){
            var imgData4 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image3.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData4);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img4.sprite = sprite;
        }
        if(jsonObj["loaders"][4]["image"]!="" && img5.sprite == null){
            var imgData5 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image4.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData5);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img5.sprite = sprite;
        }
        if(jsonObj["loaders"][5]["image"]!="" && img6.sprite == null){
            var imgData6 = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "image5.png"));
            Texture2D tex = new Texture2D(2, 2);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            tex.LoadImage(imgData6);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            img6.sprite = sprite;
        }
    }

    // Update is called once per frame
    public void ManageButtons(int i){
        // string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
        // dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        jsonObj["active"]= i;
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.json"), output);
        SceneManager.LoadScene(1);
    }
}
