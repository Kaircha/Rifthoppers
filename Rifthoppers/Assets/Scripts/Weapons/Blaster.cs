using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public PlayerBrain Brain;
  public Barrel Barrel;
  public SpriteRenderer Sprite;

  public List<Weapon> Weapons = new();
  public bool IsShooting => Brain.Input.Shoot;
  public bool IsMoving => Brain.Entity.Direction.magnitude > 0;
  private bool CanShoot = true;

  public event Action OnShootingStarted;
  public event Action OnShootingUpdated;
  public event Action OnShootingStopped;

  public void ShootingStarted() => OnShootingStarted?.Invoke();
  public void ShootingUpdated() => OnShootingUpdated?.Invoke();
  public void ShootingStopped() => OnShootingStopped?.Invoke();


  private void OnEnable() {
    if (Weapons.Count == 0) AddWeapon(GetWeapon(Brain.Stats.WeaponType));
  }
  private void OnDisable() {
    StopAllCoroutines();
    if (IsShooting) ShootingStopped();
  }

  public IEnumerator BlasterRoutine() {
    CanShoot = true;
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      ShootingStarted();
      while (IsShooting) {
        if (CanShoot) {
          ShootingUpdated();
          StartCoroutine(FirerateRoutine());
        }
        yield return null;
      }
      ShootingStopped();
    }
  }

  public IEnumerator FirerateRoutine() {
    float firerate = (IsMoving ? 1 : 1.5f) * Brain.Stats.Firerate;
    CanShoot = false;
    yield return new WaitForSeconds(1 / firerate);
    CanShoot = true;
  }

  #region These don't actually work for weapons beyond the first
  public void AddWeapon(Weapon weapon) {
    // Technically can't be set like this; Entity/Barrel might be different; weapon is a scriptable object
    weapon.Brain = Brain;
    weapon.Barrel = Barrel;

    Weapons.Add(weapon);
    OnShootingStarted += weapon.ShootingStarted;
    OnShootingUpdated += weapon.ShootingUpdated;
    OnShootingStopped += weapon.ShootingStopped;
  }

  public void RemoveWeapon(Weapon weapon) {
    if (Weapons.Remove(weapon)) {
      OnShootingStarted -= weapon.ShootingStarted;
      OnShootingUpdated -= weapon.ShootingUpdated;
      OnShootingStopped -= weapon.ShootingStopped;
    }
  }
  #endregion

  public void ReplaceWeapons(WeaponType type) {
    Weapon weapon = UpgradeWeaponManager.Instance.Weapons[(int)type];
    // Not good; weapon is a scriptable object
    weapon.Brain = Brain;
    weapon.Barrel = Barrel;

    for (int i = 0; i < Weapons.Count; ++i) {
      // Replacement exception
      if (true) {
        OnShootingStarted -= Weapons[i].ShootingStarted;
        OnShootingUpdated -= Weapons[i].ShootingUpdated;
        OnShootingStopped -= Weapons[i].ShootingStopped;

        Weapons[i] = weapon;

        OnShootingStarted += weapon.ShootingStarted;
        OnShootingUpdated += weapon.ShootingUpdated;
        OnShootingStopped += weapon.ShootingStopped;
      }
    }
  }
  public Weapon GetWeapon(WeaponType type) => UpgradeWeaponManager.Instance.Weapons[(int)type];
}