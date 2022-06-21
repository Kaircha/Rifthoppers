using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Upgrades/Laser")]
public class LaserUpgrade : Upgrade {
  public WeaponType WeaponType;

  public override void Add() {
    Brain.Stats.WeaponType |= WeaponType;
    Brain.Blaster.Weapon = Brain.Blaster.GetWeapon(Brain.Stats.WeaponType);
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.Stats.WeaponType &= ~WeaponType;
    Brain.Blaster.Weapon = Brain.Blaster.GetWeapon(Brain.Stats.WeaponType);
  }
}