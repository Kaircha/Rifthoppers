using System;
using UnityEngine;

[System.Serializable]
public class Experience {
  public int Level = 1;
  public float Current;
  public int Required => Level * (Level + 1) * 50;
  public int Previous => (Level - 1) * Level * 50;
  //public int Required => Level * (Level + 1);
  //public int Previous => (Level - 1) * Level;
  public float Progress => Mathf.Clamp01((float)(Current - Previous) / (float)(Required - Previous));
  public event Action OnLevelUp;
  public event Action OnLearn;

  public void Learn(float amount) {
    if (Current + amount >= Required) {
      Current = Required;
      Level++;
      OnLevelUp?.Invoke();
    } else {
      Current += amount;
      OnLearn?.Invoke();
    }
  }
  public void Learn() {
    Current = Required;
    Level++;
    OnLevelUp?.Invoke();
    OnLearn?.Invoke();
  }
}