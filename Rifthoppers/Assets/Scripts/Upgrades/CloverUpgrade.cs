using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clover", menuName = "Upgrades/Clover")]
public class CloverUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new Clover(this);

  public class Clover : Upgrade {
    public int Luck = 2;

    public Clover(UpgradeObject obj) {
      Object = obj;
    }

    public override void Add() => Brain.PlayerStats.Luck += Luck;
    public override IEnumerator UpgradeRoutine() { yield return null; }
    public override void Remove() => Brain.PlayerStats.Luck -= Luck;
  }
}