using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
  public Entity Entity;
  public Transform ShootOrigin;

  public abstract void Shoot();
  public abstract void ShootingStarted();
  public abstract void ShootingStopped();
}