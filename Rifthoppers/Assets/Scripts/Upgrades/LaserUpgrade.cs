using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Upgrades/Laser")]
public class LaserUpgrade : Upgrade {
  public WeaponType WeaponType;

  public override void Add() {
    Brain.PlayerStats.AmmoStats.WeaponType |= WeaponType;
    Brain.Blaster.Weapon = Brain.Blaster.GetWeapon(Brain.PlayerStats.AmmoStats.WeaponType);
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Brain.PlayerStats.AmmoStats.WeaponType &= ~WeaponType;
    Brain.Blaster.Weapon = Brain.Blaster.GetWeapon(Brain.PlayerStats.AmmoStats.WeaponType);
  }
}