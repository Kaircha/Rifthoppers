using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade {
  public abstract string Name { get; }
  public abstract int ID { get; }
  public abstract Sprite Sprite { get; }
  public abstract string Quote { get; }
  public abstract string Description { get; }
  public abstract int Weight { get; }
  public bool IsUnlocked => DataManager.Instance.Get<bool>($"{ID}IsUnlocked");
  public int TimesObtained => DataManager.Instance.Get<int>($"{ID}TimesObtained");
  // Some sort of Unlock Condition

  public void Unlock() => DataManager.Instance.Set($"{ID}IsUnlocked", true);
  public void Add() {
    DataManager.Instance.Set($"{ID}TimesObtained", DataManager.Instance.Get<int>($"{ID}TimesObtained") + 1);
    OnAdd();
  }
  public void Remove() {
    OnRemove();
  }

  public abstract void OnAdd();
  public abstract void OnRemove();
}