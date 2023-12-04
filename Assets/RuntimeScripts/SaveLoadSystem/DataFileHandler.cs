using System;
using System.IO;
using System.Linq;
using DialogueTree;
using UnityEngine;


public class DataFileHandler 
{


    public int LoadGame(){
        int headNode = 0;
        try{
            string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            int active = jsonObj["active"];
            headNode = jsonObj["loaders"][active]["node"];
            if (headNode==0){
                headNode = 1;
            }
        }
        catch(Exception e){
            Debug.LogError(e);
        }
        return headNode;
    }

     public void SaveGame(DialogueRuntimeTree tree, Camera cam){
        // maybe refactor in the future, maybe not
        DataToSave d = new DataToSave();
        d.setNode(tree.data.guids.FirstOrDefault(x => x.Value == tree.CurrentNode.Guid).Key);
        string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        int active = jsonObj["active"];
        // render and save screenshot
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        File.WriteAllBytes("Assets/Resources/screens/image" + active + ".png", byteArray);
        // save game data: node and image
        jsonObj["loaders"][active]["node"] = d.getNode();
        jsonObj["loaders"][active]["image"] = "image" + active;
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json", output);
    }
}
