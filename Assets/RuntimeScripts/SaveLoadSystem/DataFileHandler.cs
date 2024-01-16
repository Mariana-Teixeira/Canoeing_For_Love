using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using DialogueTree;
using UnityEngine;


public class DataFileHandler 
{

    public void NewGame(){
        try{
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            if(jsonObj["active"]==10){
                int i = 0;
                foreach(var save in jsonObj["loaders"]){
                    if(save["node"] == 0){
                        jsonObj["active"] = i;
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.json"), output);
                        break;
                    }
                    i++;
                }
            }   
        }
        catch(Exception e){
            Debug.LogError(e);
        }
    }


    public int LoadGame(){
        int headNode = 0;
        try{
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
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

     public void SaveGame(DialogueRuntimeTree tree, Camera cam, int KenScore, int AllenScore, List<string> itemsChosen, bool close){
        // maybe refactor in the future, maybe not
        DataToSave d = new DataToSave();
        int kenneth = KenScore;
        int allenboy = AllenScore;
        if (tree.CurrentNode.DialogueEvents.ContainsKey(DialogueEvents.ADD_SCORE))
        {
            var name = (string)tree.CurrentNode.DialogueEvents[DialogueEvents.ADD_SCORE];
            if(name == "ken"){
                kenneth--;
            }
            if(name == "allen"){
                allenboy--;
            }
            
            
        }
        if (tree.CurrentNode.DialogueEvents.ContainsKey(DialogueEvents.REMOVE_SCORE))
        {
            var name = (string)tree.CurrentNode.DialogueEvents[DialogueEvents.REMOVE_SCORE];
            if(name == "ken"){
                kenneth++;
            }
            if(name == "allen"){
                allenboy++;
            }
            
            
        }
        if (tree.CurrentNode.DialogueEvents.ContainsKey(DialogueEvents.SHOW_CHOICESPANEL))
        {
            d.setNode(tree.data.guids.FirstOrDefault(x => x.Value == tree.CurrentNode.Guid).Key-1);
            
        }
        else{
            d.setNode(tree.data.guids.FirstOrDefault(x => x.Value == tree.CurrentNode.Guid).Key);
        }
        string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        
        int active = jsonObj["active"];
        // render and save screenshot
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        var a = cam.targetTexture;
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "image" + active + ".png"), byteArray);
        cam.targetTexture = a;
        // save game data: node and image
        jsonObj["loaders"][active]["node"] = d.getNode();
        jsonObj["loaders"][active]["image"] = "image" + active;
        jsonObj["loaders"][active]["kenScore"] = kenneth;
        jsonObj["loaders"][active]["allenScore"] = allenboy;
        string itemsSaved = jsonObj["loaders"][active]["items"];
        int count = itemsSaved.Split("_").Length;
        Debug.Log("Saved items: " + count);
        Debug.Log("Items To save: " + itemsChosen.Count);
        while(count<itemsChosen.Count){
            if(itemsChosen[count].Length>2){
                itemsSaved = itemsSaved + itemsChosen[count] + "_";
            }
            count++;
        }
        jsonObj["loaders"][active]["items"] = itemsSaved;
        if(close){
            jsonObj["active"] = 10;
        }
        jsonObj["loaders"][active]["savedate"] = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.json"), output);
    }

    // public void EliminateSave(int i){
    //     string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
    //     dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
    //     jsonObj["loaders"][i]["node"] = 0;
    //     jsonObj["loaders"][i]["image"] = "";
    //     jsonObj["loaders"][i]["kenScore"] = 0;
    //     jsonObj["loaders"][i]["allenScore"] = 0;
    //     jsonObj["loaders"][i]["items"] = "";
    //     string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
    //     File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.json"), output);
    // }
}
