using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clover", menuName = "Upgrades/Clover")]
public class CloverUpgrade : Upgrade {
  public int Luck = 2;

  public override void Add() {
    Brain.PlayerStats.Luck += Luck;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.PlayerStats.Luck -= Luck;
  }
}