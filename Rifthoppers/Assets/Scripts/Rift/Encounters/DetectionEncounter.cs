using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEncounter : Encounter {
  public Surface Surface;
  public override bool IsFinished { get; set; }
    //Progress >= 1f && Surface.Entities.Count == LobbyManager.Instance.Players.Count;
  public override float Progress => Mathf.Clamp01(Amount / Required);
  public float Amount = 0f;
  public float Required = 100f;

  public override void Enter() {
    base.Enter();
  }
  public override IEnumerator Execute() {
    while (true) {
      if (Progress >= 1f && Surface.Entities.Count != LobbyManager.Instance.Players.Count) {
        // Highlight that all players need to be on the surface
      }
      yield return null;
    }
  }
  public override void Exit() {
    base.Exit();
  }
}