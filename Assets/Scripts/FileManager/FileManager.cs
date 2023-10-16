using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JetBrains.Annotations;
using UnityEditor.Tilemaps;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    // path to where textfiles should be
    public static readonly string rootPath = $"{Application.dataPath}/gameData/"; 

    // reads text file and return lines off text
    public static List<string> ReadTextFile(string filePath, bool includeBlankLines = true){
        if (!filePath.StartsWith("/")){
            filePath = rootPath + filePath;
        }

        List
        <string> lines = new();
        try{
            using (StreamReader sr = new(filePath)){
                while(!sr.EndOfStream){
                    string line = sr.ReadLine();
                    if(includeBlankLines || !string.IsNullOrWhiteSpace(line)){
                        lines.Add(line);
                    }
                }
            }
        }
        catch (FileNotFoundException ex){
            Debug.LogError("File not found: " + ex.FileName);
        }


        return lines;
    }

}
