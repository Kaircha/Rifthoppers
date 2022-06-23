using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEncounter : Encounter {
  public GameObject Worm;
  public override float Progress => throw new System.NotImplementedException();

  public override IEnumerator EncounterRoutine() {
    // Begin screen shake
    // Play spooky rumbling noise
    // Wait
    // Zoom out camera
    // Wait
    // Show boss healthbar
    // Wait
    // Spawn Boss
    yield return null;
  }
}