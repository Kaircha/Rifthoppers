using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEncounter : Encounter {
  public Surface Surface;
  public override bool IsFinished => Progress >= 1f && Surface.Entities.Count == LobbyManager.Instance.Players.Count;
  public override float Progress => Mathf.Clamp01(Amount / Required);
  public float Amount = 0f;
  public float Required = 100f;

  public override void EncounterStart() {
    base.EncounterStart();
  }
  public override IEnumerator EncounterRoutine() {
    while (true) {
      if (Progress >= 1f && Surface.Entities.Count != LobbyManager.Instance.Players.Count) {
        // Highlight that all players need to be on the surface
      }
      yield return null;
    }
  }
  public override void EncounterEnd() {
    base.EncounterEnd();
  }
}