using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    void LoadData(DataToSave dataToSave);

    void SaveData(ref DataToSave dataToSave);
}
