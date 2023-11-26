using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave{
    public int node;

    public DataToSave(){
        this.node = 1;
    }

    public void setNode(int n){
        this.node = n;
    }

    public void getNode(int n){
        this.node = n;
    }
}