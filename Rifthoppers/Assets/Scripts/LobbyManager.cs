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

  private void Start() => AddPlayer();

  public void AddPlayer() {
    InputData input = Instantiate(PlayerPrefab, transform).GetComponent<InputData>();
    PlayerEntity entity = Instantiate(EntityPrefab, input.transform).GetComponent<PlayerEntity>();
    entity.Input = input;

    Players.Add(new Player(input, entity));

    if (Players.Count > 1) CoopVCam.Priority = 11; // Up for change later when cutscenes require more VCams.
  }

  public void RemovePlayer(int index) {
    Player player = Players[index];
    Players.Remove(player);
    Destroy(player.Input.gameObject);

    if (Players.Count <= 1) CoopVCam.Priority = 0; // Up for change later when cutscenes require more VCams.
  }
  
  public void Update() {
    if (Players.Count == 0) return;
    CameraTarget.position = Players.Count > 1 ? AvgPos() : AimPos();
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

    newEntity.transform.SetParent(transform);
    newEntity.Input = player.Input;
    newEntity.EnterHubState();

    oldEntity.EnterAIState();
    oldEntity.transform.SetParent(null);
    SceneManager.MoveGameObjectToScene(oldEntity.gameObject, SceneManager.GetActiveScene());
    // the old entity needs to exit Hub State first, which takes a frame?
    //oldEntity.Input = null;
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