using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataToSave{

    public int node;

    public int kenScore;

    public int allenScore;

    public DataToSave(int KenScore = 0, int AllenScore = 0){
        this.node = 1;
        this.allenScore = AllenScore;
        this.kenScore = KenScore;
    }

    public void setNode(int n){
        this.node = n;
    }

    public int getNode(){
        return this.node;
    }
}