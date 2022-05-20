using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Upgrades/Laser")]
public class LaserUpgrade : Upgrade {
  public WeaponType WeaponType;

  public override void Add() {
    Entity.Stats.WeaponType |= WeaponType;
    Entity.Blaster.ReplaceWeapons(Entity.Stats.WeaponType);
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Entity.Stats.WeaponType &= ~WeaponType;
    Entity.Blaster.ReplaceWeapons(Entity.Stats.WeaponType);
  }
}