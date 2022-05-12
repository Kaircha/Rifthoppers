using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
  //public virtual bool Persistent => true;
  private static T _instance;
  public static T Instance => _instance ?? Initialize();

  private static T Initialize() {
    _instance = FindObjectOfType<T>();
    _instance ??= new GameObject(typeof(T).Name).AddComponent<T>();
    return _instance;
  }

  public virtual void Awake() {
    if (_instance != null && _instance != this) Destroy(this.gameObject);
    else { 
      Initialize();
      // Singletons are instead loaded in their own dedicated scene.
      //if (Persistent) DontDestroyOnLoad(this.gameObject);
    }
  }
}