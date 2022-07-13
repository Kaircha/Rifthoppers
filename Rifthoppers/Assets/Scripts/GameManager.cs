using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StateMachine))]
public class GameManager : Singleton<GameManager> {
  public StateMachine Machine;
  public Volume PostProcessingVolume;
  public Wave CurrentWave;

  private string Scene;
  private Coroutine GameplayLoop;

  // TEMPORARY
  public UpgradeObject DebugUpgrade;

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
    LobbyManager.Instance.Players.ForEach(player => player.Brain.EnterInteractState());

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

    // For testing purposes
    LobbyManager.Instance.Players[0].Brain.Upgrades.Add(DebugUpgrade.Upgrade());

    foreach (Wave wave in RiftManager.Instance.Rift) {
      CurrentWave = wave;
      yield return EncounterRoutine(wave.Encounter);
      yield return RewardRoutine(wave.Reward);
    }

    Machine.ChangeState(new State()); // Create a new State to Reset values; safety fallback
    ResetPlayers();
    GameplayLoop = StartCoroutine(LaboratoryRoutine(true));
  }


  public void RiftDefeat() {
    StopCoroutine(GameplayLoop);
    Machine.ChangeState(new State());
    ResetPlayers();
    GameplayLoop = StartCoroutine(LaboratoryRoutine());
  }

  public void RiftRestart() {
    StopCoroutine(GameplayLoop);
    Machine.ChangeState(new State());
    ResetPlayers();
    GameplayLoop = StartCoroutine(RiftRoutine());
  }

  public void ResetPlayers() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Brain.Upgrades.Clear();
      player.Brain.Entity.RemoveEffects();
    }
  }

  public IEnumerator EncounterRoutine(Encounter encounter) {
    Machine.ChangeState(encounter);
    yield return new WaitUntil(() => encounter.IsFinished);
  }

  public IEnumerator RewardRoutine(Reward reward) {
    Machine.ChangeState(reward);
    yield return new WaitForSeconds(reward.ExecutionTime);
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