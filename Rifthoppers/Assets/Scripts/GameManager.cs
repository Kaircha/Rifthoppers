using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StateMachine))]
public class GameManager : Singleton<GameManager> {
  [HideInInspector] public StateMachine Machine;
  public Volume PostProcessingVolume;

  public LaboratoryState LaboratoryState = new();
  public RiftState RiftState = new();

  private string Scene;
  private Coroutine GameplayLoop;

  // TEMPORARY
  public Upgrade DebugUpgrade;

  public override void Awake() {
    base.Awake();
    Machine = GetComponent<StateMachine>();
  }

  public void Start() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Brain.Entity.transform.position = new Vector3(0, -30);
    }

    GameplayLoop = StartCoroutine(LaboratoryRoutine());
  }


  public IEnumerator LaboratoryRoutine(bool fromPortal = false) {
    yield return StartCoroutine(LoadScene("Laboratory"));
    Machine.ChangeState(LaboratoryState);

    if (fromPortal) {
      foreach (Player player in LobbyManager.Instance.Players) {
        player.Brain.Entity.transform.position = Vector3.zero;
        player.Brain.Entity.Rigidbody.AddForce(100f * Vector2.down, ForceMode2D.Impulse);
      }
    }

    yield return new WaitUntil(() => LaboratoryManager.Instance.PortalEntered);
    LaboratoryManager.Instance.PortalEntered = false;

    GameplayLoop = StartCoroutine(RiftRoutine());
  }

  public IEnumerator RiftRoutine() {
    yield return StartCoroutine(LoadScene("Rift"));
    Machine.ChangeState(RiftState);
    RiftManager.Instance.Index = 0;

    LobbyManager.Instance.Players[0].Brain.Upgrades.Add(DebugUpgrade);

    foreach (Wave wave in RiftManager.Instance.Rift) {
      RiftManager.Instance.StartEncounter();
      yield return new WaitUntil(() => wave.Encounter.IsFinished);
      RiftManager.Instance.EndEncounter();

      if (wave.Index < RiftManager.Instance.Rift.Count - 1) {
        RiftManager.Instance.StartReward();
        yield return new WaitUntil(() => wave.Reward.IsFinished);
        RiftManager.Instance.EndReward();
      }

      RiftManager.Instance.Index++;
    }

    GameplayLoop = StartCoroutine(LaboratoryRoutine(true));
  }


  public void RiftDefeat() {
    StopCoroutine(GameplayLoop);
    GameplayLoop = StartCoroutine(LaboratoryRoutine());
  }

  public void RiftRestart() {
    StopCoroutine(GameplayLoop);
    GameplayLoop = StartCoroutine(RiftRoutine());
  }


  public IEnumerator LoadScene(string scene) {
    if (Scene != null) {
      AsyncOperation asyncSceneUnload = SceneManager.UnloadSceneAsync(Scene);
      yield return new WaitUntil(() => asyncSceneUnload.isDone);    
    }

    Scene = scene;
    AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive);
    yield return new WaitUntil(() => asyncSceneLoad.isDone);
  }
}