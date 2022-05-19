using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Overpenetration", menuName = "Upgrades/Overpenetration")]
public class OverpenetrationUpgrade : Upgrade {
  public override void Add() {
    Entity.Stats.ProjectilePierces += 1;
    Entity.Stats.ProjectileSpeedMulti += 0.2f;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Entity.Stats.ProjectilePierces -= 1;
    Entity.Stats.ProjectileSpeedMulti -= 0.2f;
  }
}