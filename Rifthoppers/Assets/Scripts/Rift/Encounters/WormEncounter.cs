using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEncounter : Encounter {
  [HideInInspector] public WormBrain Brain;
  public override float Progress => 1f - Brain.Health.Percentage;
  public override bool IsFinished { get; set; }

  public override void Enter() {
    base.Enter();
    Brain = Object.Instantiate(RiftManager.Instance.WormBoss, Area.transform).GetComponent<WormBrain>();
    IsStarted = true;
    IsFinished = false;
  }

  public override IEnumerator Execute() {
    yield return new WaitForSeconds(0.5f);
    //Brain.Impulse.GenerateImpulse();
    // Play spooky rumbling noise
    // Wait
    LobbyManager.Instance.SoloVCam.m_Lens.OrthographicSize = 12f;
    // Wait
    // Show boss healthbar
    // Wait
    // Spawn Boss
    yield return new WaitUntil(() => Brain.Health.IsDead);
    // Wait
    IsFinished = true;
  }

  public override void Exit() {
    base.Exit();
    LobbyManager.Instance.SoloVCam.m_Lens.OrthographicSize = 10f;
  }
}