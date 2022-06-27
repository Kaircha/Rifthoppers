using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rubbershot", menuName = "Upgrades/Rubbershot")]
public class RubbershotUpgrade : Upgrade {
  public override void Add() {
    Brain.AmmoStats.AmmoReflects += 1;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.AmmoStats.AmmoReflects -= 1;
  }
}