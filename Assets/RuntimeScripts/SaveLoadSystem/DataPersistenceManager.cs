using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set;}

    private List<IDataPersistence> dataPersistences;
    private DataToSave dataToSave;

    void Awake(){
        if(instance != null){
            Debug.LogError("Found more than one data persistance manager!");
        }
        instance = this;
    }

    void Start(){
        dataPersistences = FindAllDataPersistences();
        LoadGame();
    }

    public void NewGame(){
        this.dataToSave = new DataToSave();
    }

    public void LoadGame(){
        if (this.dataToSave == null){
            NewGame();
        }
        foreach(IDataPersistence dataPersistenceobj in dataPersistences){
            dataPersistenceobj.LoadData(dataToSave);
        }
        Debug.Log("Initial node: " + dataToSave.node.ToString());
    }

    public void SaveGame(){
        foreach(IDataPersistence dataPersistenceobj in dataPersistences){
            dataPersistenceobj.SaveData(ref dataToSave);
        }
        Debug.Log(message: "Current node: " + dataToSave.node.ToString());

    }

    private void OnApplicationQuit(){
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistences(){
        IEnumerable <IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(collection: dataPersistences);
    }

}
