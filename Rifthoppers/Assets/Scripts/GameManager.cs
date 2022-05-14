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
  public RiftWaveState RiftWaveState = new();
  public RiftUpgradeState RiftUpgradeState = new();

  public override void Awake() {
    base.Awake();

    Machine = GetComponent<StateMachine>();
  }

  public void Start() {
    // Won't let us play from the Rift scene directly
    Machine.ChangeState(LaboratoryState);
  }


  // Temporary?
  public void WaveToUpgrade() => Machine.ChangeState(RiftUpgradeState);
  public void UpgradeToWave() => Machine.ChangeState(RiftWaveState);


  public IEnumerator LabToWave() {
    AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync("Rift", LoadSceneMode.Additive);
    yield return new WaitUntil(() => asyncSceneLoad.isDone);
    Machine.ChangeState(RiftWaveState);
    SceneManager.UnloadSceneAsync("Laboratory");
  }

  public IEnumerator WaveToLab() {
    AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync("Laboratory", LoadSceneMode.Additive);
    yield return new WaitUntil(() => asyncSceneLoad.isDone);
    Machine.ChangeState(LaboratoryState);
    SceneManager.UnloadSceneAsync("Rift");

    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.transform.position = Vector3.zero;
      // Way overkill. Maybe better to not use physics?
      player.Entity.Rigidbody.AddForce(700f * Vector2.down, ForceMode2D.Impulse);
    }
  }
}