using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Overpenetration", menuName = "Upgrades/Overpenetration")]
public class OverpenetrationUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new Overpenetration(this);

  public class Overpenetration : Upgrade {
    public Overpenetration(UpgradeObject obj) {
      Object = obj;
    }

    public override void Add() {
      Brain.PlayerStats.AmmoStats.AmmoPierces += 1;
      Brain.PlayerStats.AmmoStats.AmmoSpeedMulti += 0.2f;
    }

    public override IEnumerator UpgradeRoutine() { yield return null; }

    public override void Remove() {
      Brain.PlayerStats.AmmoStats.AmmoPierces -= 1;
      Brain.PlayerStats.AmmoStats.AmmoSpeedMulti -= 0.2f;
    }
  }
}