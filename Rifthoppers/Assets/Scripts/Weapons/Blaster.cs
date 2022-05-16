using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public Entity Entity;
  public Barrel Barrel;
  public SpriteRenderer Sprite;

  public List<Weapon> Weapons = new();
  public bool IsShooting => Entity.Input.Shoot;
  public bool IsMoving => Entity.Direction.magnitude > 0;

  public event Action OnShootingStarted;
  public event Action OnShootingUpdated;
  public event Action OnShootingStopped;

  public void ShootingStarted() => OnShootingStarted?.Invoke();
  public void ShootingUpdated() => OnShootingUpdated?.Invoke();
  public void ShootingStopped() => OnShootingStopped?.Invoke();


  private void OnEnable() {
    // Shouldn't add a Default Weapon this way; Scriptable Object
    if (Weapons.Count == 0) AddWeapon(GetWeapon(Entity.Stats.WeaponType));
  }
  private void OnDisable() => StopAllCoroutines();

  public IEnumerator BlasterRoutine() {
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      ShootingStarted();
      Coroutine coroutine = StartCoroutine(ShootRoutine());
      yield return new WaitUntil(() => !IsShooting);
      ShootingStopped();
      StopCoroutine(coroutine);
    }
  }

  public IEnumerator ShootRoutine() {
    while (true) {
      ShootingUpdated();
      float firerate = (IsMoving ? 1 : 1.5f) * Entity.Stats.PlayerFirerate;
      yield return new WaitForSeconds(1 / firerate);
    }
  }

  #region These don't actually work for weapons beyond the first
  public void AddWeapon(Weapon weapon) {
    // Technically can't be set like this; Entity/Barrel might be different
    weapon.Entity = Entity;
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
    // Not good
    weapon.Entity = Entity;
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