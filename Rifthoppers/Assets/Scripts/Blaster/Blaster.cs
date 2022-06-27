using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {
  public PlayerBrain Brain;
  public Barrel Barrel;
  public SpriteRenderer Sprite;

  private Weapon _weapon;
  public Weapon Weapon {
    get => _weapon;
    set {
      if (_weapon != null) {
        OnShootingStarted -= _weapon.ShootingStarted;
        OnShootingUpdated -= _weapon.ShootingUpdated;
        OnShootingStopped -= _weapon.ShootingStopped;
      }
      _weapon = value;
      OnShootingStarted += _weapon.ShootingStarted;
      OnShootingUpdated += _weapon.ShootingUpdated;
      OnShootingStopped += _weapon.ShootingStopped;
    }
  }
  public bool IsShooting => Brain.Input.Shoot;
  public bool IsMoving => Brain.Entity.Direction.magnitude > 0;
  private bool CanShoot = true;

  public event Action<Entity, AmmoStats, Barrel> OnShootingStarted;
  public event Action<Entity, AmmoStats, Barrel> OnShootingUpdated;
  public event Action<Entity, AmmoStats, Barrel> OnShootingStopped;
  public void ShootingStarted(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingStarted?.Invoke(entity, stats, barrel);
  public void ShootingUpdated(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingUpdated?.Invoke(entity, stats, barrel);
  public void ShootingStopped(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingStopped?.Invoke(entity, stats, barrel);


  private void OnEnable() {
    Weapon = GetWeapon(Brain.PlayerStats.AmmoStats.WeaponType);
  }
  private void OnDisable() {
    StopAllCoroutines();
    if (IsShooting) ShootingStopped(Brain.Entity, Brain.PlayerStats.AmmoStats, Barrel);
  }

  public IEnumerator BlasterRoutine() {
    CanShoot = true;
    while (true) {
      yield return new WaitUntil(() => IsShooting);
      ShootingStarted(Brain.Entity, Brain.PlayerStats.AmmoStats, Barrel);
      while (IsShooting) {
        if (CanShoot) {
          ShootingUpdated(Brain.Entity, Brain.PlayerStats.AmmoStats, Barrel);
          StartCoroutine(FirerateRoutine());
        }
        yield return null;
      }
      ShootingStopped(Brain.Entity, Brain.PlayerStats.AmmoStats, Barrel);
    }
  }

  public IEnumerator FirerateRoutine() {
    float firerate = (IsMoving ? 1 : 1.5f) * Brain.PlayerStats.Firerate;
    CanShoot = false;
    yield return new WaitForSeconds(1 / firerate);
    CanShoot = true;
  }

  public Weapon GetWeapon(WeaponType type) => UpgradeWeaponManager.Instance.Weapons[(int)type];
}