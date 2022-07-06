using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rubbershot", menuName = "Upgrades/Rubbershot")]
public class RubbershotUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new Rubbershot(this);
}

public class Rubbershot : Upgrade {
  public Rubbershot(UpgradeObject obj) {
    Object = obj;
  }

  public override void Add() {
    Brain.PlayerStats.AmmoStats.AmmoReflects += 1;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.PlayerStats.AmmoStats.AmmoReflects -= 1;
  }
}