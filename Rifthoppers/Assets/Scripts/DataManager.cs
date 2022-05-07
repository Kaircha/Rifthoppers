using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager> {
  public GameObject Icon;
  public bool UseSave;
  public Dictionary<string, dynamic> Data = new();

  public override void Awake() {
    base.Awake();
    LoadData();
  }

  void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
  void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

  void OnSceneLoaded(Scene scene, LoadSceneMode mode) => SaveData();
  void OnApplicationQuit() => SaveData();

  public bool Has(string id) => Data.ContainsKey(id);
  public T Get<T>(string id) => Data.TryGetValue(id, out dynamic value) ? value : default(T); 
  public void Set<T>(string id, T value) => Data[id] = value;

  [ContextMenu("DeleteData")]
  public void DeleteData() => Data = new Dictionary<string, dynamic>();

  [ContextMenu("SaveData")]
  public void SaveData() => SaveData($"{Application.persistentDataPath}/Save.dat");
  private void SaveData(string path) {
    if (!UseSave) return;
    using (FileStream stream = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
      new BinaryFormatter().Serialize(stream, Data);
    }
    // Display saving icon
  }

  [ContextMenu("Load Data")]
  public void LoadData() => LoadData($"{Application.persistentDataPath}/Save.dat");
  private void LoadData(string path) {
    if (!UseSave || !File.Exists(path)) return;
    using (FileStream stream = new(path, FileMode.Open, FileAccess.Read)) {
      Data = new BinaryFormatter().Deserialize(stream) as Dictionary<string, dynamic>;
    }
  }
}