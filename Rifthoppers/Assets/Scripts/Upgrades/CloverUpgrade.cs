using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clover", menuName = "Upgrades/Clover")]
public class CloverUpgrade : Upgrade {
  public int Luck = 2;

  public override void Add() {
    Entity.Stats.Luck += Luck;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Entity.Stats.Luck -= Luck;
  }
}