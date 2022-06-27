using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Overpenetration", menuName = "Upgrades/Overpenetration")]
public class OverpenetrationUpgrade : Upgrade {
  public override void Add() {
    Brain.AmmoStats.AmmoPierces += 1;
    Brain.AmmoStats.AmmoSpeedMulti += 0.2f;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.AmmoStats.AmmoPierces -= 1;
    Brain.AmmoStats.AmmoSpeedMulti -= 0.2f;
  }
}