using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// Requires JSONSaving component, while JsonSaving requires this one. (TODO)
/// </summary>
public class JSONSaving : MonoBehaviour
{

    private string path;
    private string persistentPath;

    public TMPro.TMP_Text LogText;

    public ItemService itemService;
    public ItemPresenter itemPresenter;

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
        /// Should call itemService instead
        itemPresenter.RetrieveItemsParams();
        print("saving data");
        string savePath = path;

        Debug.Log("saving Data at " + savePath);
        string json = JsonUtility.ToJson(itemService.GetItems());
        Debug.Log(json);
        LogText.text = json;

        using StreamWriter writer = new(savePath);
        writer.Write(json);
    }

    private void LoadData()
    {
        using StreamReader reader = new(path);
        string json = reader.ReadToEnd();
        itemService.SetItems(JsonUtility.FromJson<ItemsData>(json));
        Debug.Log(itemService.GetItems().ToString());
        LogText.text = itemService.GetItems().ToString();
    }

    public void InstantiateData()
    {
        LoadData();
        itemPresenter.AddItemsToScene();
    }
}
