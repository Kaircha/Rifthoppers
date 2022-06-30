using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager> {
  public GameObject Icon;
  public bool UseSave;
  public Dictionary<string, dynamic> Data = new();

  public override void Awake() {
    base.Awake();
    Load();
  }

  void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
  void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

  void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Save();
  void OnApplicationQuit() => Save();

  public bool Has(string key) => Data.ContainsKey(key);
  public T Get<T>(string key) => Data.TryGetValue(key, out dynamic value) ? value : default(T);
  public bool TryGet<T>(string key, out T value) {
    if (Data.TryGetValue(key, out dynamic v)) {
      value = v;
      return true;
    } else {
      value = default;
      return false;
    }
  }
  public void Set<T>(string id, T value) => Data[id] = value;

  [ContextMenu("Delete")]
  public void DeleteData() => Data = new Dictionary<string, dynamic>();

  [ContextMenu("Save")]
  public void Save() => Save($"{Application.persistentDataPath}/Save.xml");
  private void Save(string path) {
    if (!UseSave) return;
    Debug.Log(path);
    List<KeyValue<string, dynamic>> data = Data.Select(x => new KeyValue<string, dynamic> { Key = x.Key, Value = x.Value }).ToList();
    XmlSerializer serializer = new(typeof(List<KeyValue<string, dynamic>>));
    StreamWriter writer = new(path);
    serializer.Serialize(writer.BaseStream, data);
    writer.Close();
  }

  [ContextMenu("Load")]
  public void Load() => Load($"{Application.persistentDataPath}/Save.xml");
  private void Load(string path) {
    if (!UseSave || !File.Exists(path)) return;
    XmlSerializer serializer = new(typeof(List<KeyValue<string, dynamic>>));
    StreamReader reader = new(path);
    Data = ((List<KeyValue<string, dynamic>>)serializer.Deserialize(reader.BaseStream)).ToDictionary(x => x.Key, x => x.Value);
    reader.Close();
  }

  [XmlType(TypeName = "Entry")]
  public class KeyValue<K, V> {
    public K Key { get; set; }
    public V Value { get; set; }
  }
}