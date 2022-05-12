using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LobbyManager : Singleton<LobbyManager> {
  public List<Player> Players = new List<Player>();
  public int Maximum; // not yet enforced
  public GameObject PlayerPrefab;
  public GameObject EntityPrefab;
  public Transform CameraTarget;
  public CinemachineVirtualCamera SoloVCam;
  public CinemachineVirtualCamera CoopVCam;

  public override void Awake() {
    base.Awake();

    AddPlayer();
  }

  private void Update() {
    if (Players.Count == 0) return;
    CameraTarget.position = Players.Count > 1 ? AvgPos() : AimPos();
  } 


  private void OnEnable() => SceneManager.activeSceneChanged += OnSceneChanged;
  private void OnDisable() => SceneManager.activeSceneChanged -= OnSceneChanged;

  public void AddPlayer() {
    InputData input = Instantiate(PlayerPrefab, transform).GetComponent<InputData>();
    PlayerEntity entity = Instantiate(EntityPrefab, input.transform).GetComponent<PlayerEntity>();
    entity.Input = input;
    entity.transform.position = SceneManager.GetActiveScene().name == "Rift" ? Vector3.zero : new Vector3(0, -30);
    Players.Add(new Player(input, entity));

    UpdateAudioListeners();
    UpdateVirtualCameras();
  }

  public void RemovePlayer(int index) {
    Player player = Players[index];
    Players.Remove(player);
    Destroy(player.Input.gameObject);

    UpdateAudioListeners();
    UpdateVirtualCameras();
  }

  // Camera is not persistant between scenes
  public void OnSceneChanged(Scene a, Scene b) {
    UpdateAudioListeners();
    UpdateVirtualCameras();
  }

  public void UpdateAudioListeners() {
    // Turn off all audio listeners, then turn the correct one on, so only a single AudioListener is ever enabled.
    if (Players.Count > 1) {
      foreach (AudioListener listener in FindObjectsOfType<AudioListener>()) listener.enabled = false;
      Camera.main.GetComponentInChildren<AudioListener>().enabled = true;
    } else {
      foreach (AudioListener listener in FindObjectsOfType<AudioListener>()) listener.enabled = false;
      if (Players.Count == 1) Players[0].Entity.GetComponentInChildren<AudioListener>().enabled = true;
      // else No players active at all!
    }
  }
  public void UpdateVirtualCameras() {
    // Values are up for change; likely will be set by Enum once multiple VCams are needed for e.g. cutscenes
    if (Players.Count > 1) CoopVCam.Priority = 11;
    else CoopVCam.Priority = 0;
  }

  public Vector2 AvgPos() {
    if (Players.Count == 0) return Vector2.zero;
    Vector3 result = Vector3.zero;
    foreach (Player player in Players) {
      result += player.Entity.transform.position;
    }
    return result / Players.Count;
  }
  public Vector2 AimPos() {
    Vector2 A = Players[0].Entity.Target.position;
    Vector2 B = Players[0].Entity.transform.position;
    return 0.1f * A + 0.9f * B;
  }

  public void ChangeCharacter(PlayerEntity oldEntity, PlayerEntity newEntity) {
    Player player = Players.First(x => x.Entity == oldEntity);
    player.Entity = newEntity;

    newEntity.transform.SetParent(player.Input.transform);
    newEntity.Input = player.Input;
    newEntity.EnterLabState();

    oldEntity.EnterAIState();
    oldEntity.transform.SetParent(null);
    // Opposite of DontDestroyOnLoad
    SceneManager.MoveGameObjectToScene(oldEntity.gameObject, SceneManager.GetActiveScene());
    
    UpdateAudioListeners();
  }

  public PlayerEntity GetClosest(Vector3 position) {
    if (Players.Count == 0) return Players[0].Entity;
    else return Players.OrderBy(x => Vector3.Distance(x.Entity.transform.position, position)).First().Entity;
  }
  public float GetDistanceToClosest(Vector3 position) => Vector3.Distance(position, GetClosest(position).transform.position);

}

[System.Serializable]
public class Player {
  public InputData Input;
  public PlayerEntity Entity;

  public Player(InputData input, PlayerEntity entity) {
    Input = input;
    Entity = entity;
  }
}