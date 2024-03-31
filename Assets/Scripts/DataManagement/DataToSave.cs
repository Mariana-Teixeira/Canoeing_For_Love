[System.Serializable]
public class DataToSave{

    public int node;

    public int kenScore;

    public int allenScore;

    public string img;

    public DataToSave(int KenScore = 0, int AllenScore = 0, string image = ""){
        this.node = 1;
        this.allenScore = AllenScore;
        this.kenScore = KenScore;
        this.img = image;
    }

    public void setNode(int n){
        this.node = n;
    }

    public int getNode(){
        return this.node;
    }
}