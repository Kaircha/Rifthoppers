using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEncounter : Encounter {
  public GameObject WormPrefab;
  [HideInInspector] public WormBrain Brain;
  public override float Progress => 1 - Brain.Health.Percentage;
  public override bool IsFinished => IsDead;
  public bool IsDead;

  public override void EncounterStart() {
    base.EncounterStart();
    IsDead = false;
    Brain = Instantiate(WormPrefab).GetComponent<WormBrain>();
  }

  public override IEnumerator EncounterRoutine() {
    Brain.Impulse.GenerateImpulse();
    // Play spooky rumbling noise
    // Wait
    LobbyManager.Instance.SoloVCam.m_Lens.OrthographicSize = 12f;
    // Wait
    // Show boss healthbar
    // Wait
    // Spawn Boss
    yield return new WaitUntil(() => Brain.Health.IsDead);
    // Wait
    IsDead = true;
  }

  public override void EncounterEnd() {
    base.EncounterEnd();
    LobbyManager.Instance.SoloVCam.m_Lens.OrthographicSize = 10f;
  }
}