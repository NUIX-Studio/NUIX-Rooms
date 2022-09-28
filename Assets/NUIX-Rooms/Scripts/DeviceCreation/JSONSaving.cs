using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{

    private string path;
    private string persistentPath;

    // Start is called before the first frame update
    void Start()
    {
        SetPaths();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveData()
    {
        print("saving data");
        string savePath = path;

        Debug.Log("saving Data at " + savePath);
        string json = JsonUtility.ToJson(GetComponent<ItemsStorage>().itemsData);
        Debug.Log(json);

        using StreamWriter writer = new(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new(path);
        string json = reader.ReadToEnd();
        GetComponent<ItemsStorage>().itemsData = JsonUtility.FromJson<ItemsData>(json);
        Debug.Log(GetComponent<ItemsStorage>().itemsData.ToString());
    }

    public void InstantiateData()
    {
        LoadData();
        GetComponent<ItemsStorage>().AddItemsToScene();
    }
}
