using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



public class DataToSave{
    public int node;

    public DataToSave(){
        this.node = 0;
    }

    public void setNode(int n){
        this.node = n;
    }

    public void getNode(int n){
        this.node = n;
    }
}