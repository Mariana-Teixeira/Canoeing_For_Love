using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set;}

    private DataToSave dataToSave;

    void Awake(){
        if(instance != null){
            Debug.LogError("Found more than one data persistance manager!");
        }
        instance = this;
    }

    private void Start(){
        LoadGame();
    }

    public void NewGame(){
        this.dataToSave = new DataToSave();
    }

    public void LoadGame(){
        if (this.dataToSave == null){
            NewGame();
        }
    }

    public void SaveGame(){

    }

    private void OnApplicationQuit(){
        SaveGame();
    }

}
