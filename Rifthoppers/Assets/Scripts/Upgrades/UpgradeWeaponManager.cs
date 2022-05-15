using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeWeaponManager : Singleton<UpgradeWeaponManager> {
  public List<Upgrade> Upgrades = new();
  public List<Weapon> Weapons = new();

  public event Action<Upgrade> OnUpgradeTaken;

  public void TakeUpgrade(Upgrade upgrade) {
    OnUpgradeTaken?.Invoke(upgrade);
  }
}