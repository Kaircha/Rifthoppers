using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBlaster : MonoBehaviour {
  public Entity Entity;
  public List<Barrel> Barrels;
  public Weapon Weapon;
  public bool CanShoot = true;

  public event Action<Entity, AmmoStats, Barrel> OnShootingStarted;
  public event Action<Entity, AmmoStats, Barrel> OnShootingUpdated;
  public event Action<Entity, AmmoStats, Barrel> OnShootingStopped;

  public void ShootingStarted(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingStarted?.Invoke(entity, stats, barrel);
  public void ShootingUpdated(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingUpdated?.Invoke(entity, stats, barrel);
  public void ShootingStopped(Entity entity, AmmoStats stats, Barrel barrel) => OnShootingStopped?.Invoke(entity, stats, barrel);

  private void OnEnable() {
    OnShootingStarted += Weapon.ShootingStarted;
    OnShootingUpdated += Weapon.ShootingUpdated;
    OnShootingStopped += Weapon.ShootingStopped;
  }
  private void OnDisable() {
    OnShootingStarted -= Weapon.ShootingStarted;
    OnShootingUpdated -= Weapon.ShootingUpdated;
    OnShootingStopped -= Weapon.ShootingStopped;
  }
}