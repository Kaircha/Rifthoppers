using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition {
  public abstract bool Satisfied { get; }
  public abstract void Initialize();
  public abstract void Reset();
  public abstract void Terminate();
}