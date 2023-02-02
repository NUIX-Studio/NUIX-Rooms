using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

/// <summary>
/// Used to Serialize/deserialize item data and action data
/// </summary>
public class JSONSaving : MonoBehaviour
{

    bool isSavingData = false;
    bool isLoadingData = false;
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
        path = Path.Combine(Application.dataPath, SceneManager.GetActiveScene().name + "-Data.json");
        persistentPath = Path.Combine(Application.persistentDataPath, SceneManager.GetActiveScene().name + "-Data.json");
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Serialize Data in ItemService. The created JSON is stored in the Assets folder
    /// </summary>
    public async Task SaveDataAsync()
    {
        

        while (isLoadingData)
        {
            await Task.Delay(25);
        }
        isSavingData = true;
        /// Should call itemService instead
        itemPresenter.RetrieveItemsParams();
        print("saving data");
        string savePath = path;

        Debug.Log("saving Data at " + savePath);
        if (LogText) LogText.text = "saving Data at " + savePath;
        string json = JsonUtility.ToJson(itemService.GetItems());
        Debug.Log(json);
        if (LogText) LogText.text += json;

        File.WriteAllText(savePath, json);
        //using StreamWriter writer = new(savePath);
        //writer.Write(json);
        isSavingData = false;
    }

    /// <summary>
    /// Deserialize data from JSON into cached data in ItemService
    /// </summary>
    private void LoadData()
    {
        //using StreamReader reader = new(path);
        //string json = reader.ReadToEnd();
        if (LogText) LogText.text = "Loading data from " + path;
        string json = File.ReadAllText(path);
        
        itemService.SetItems(JsonUtility.FromJson<ItemsData>(json));
        Debug.Log(itemService.GetItems().ToString());
        if (LogText) LogText.text += itemService.GetItems().ToString();
    }

    /// <summary>
    /// Deserialize data (into ItemService cache) and Add the stored items into the scene
    /// </summary>
    public void InstantiateData()
    {
        InvokeRepeating(nameof(InstantiateAndUpdateDataAsync), 0f, 1f);
        InvokeRepeating(nameof(SaveDataAsync), 0f, 0.05f);
    }

    private async Task InstantiateAndUpdateDataAsync()
    {
        while (isSavingData)
        {
            await Task.Delay(25);
        }
        isLoadingData = true;
        LoadData();
        isLoadingData = false;
        itemPresenter.AddItemsToScene();
    }
}
