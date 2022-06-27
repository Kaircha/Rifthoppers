using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDeathState : State {
  public WormBrain Brain;

  public WormDeathState(WormBrain brain) {
    Brain = brain;
  }

  public override void Enter() {
    Object.Destroy(Brain.gameObject);
  }
  public override IEnumerator Execute() {
    yield return null;
  }
  public override void Exit() { }
}