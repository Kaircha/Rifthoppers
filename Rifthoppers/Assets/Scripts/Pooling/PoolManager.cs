using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager> {
  public Pool Bullets;
  public Pool BulletImpact;
  public Pool SquidPool;
  public Pool SquidmotherPool;
  public Pool SquidlingPool;
}