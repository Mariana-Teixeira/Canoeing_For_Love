using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataToSave{

    public int node;

    public DataToSave(){
        this.node = 1;
    }

    public void setNode(int n){
        this.node = n;
    }

    public int getNode(){
        return this.node;
    }
}