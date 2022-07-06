using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeObject : ScriptableObject {
  public Sprite Sprite;
  public string Quote;
  public List<Modifier> Modifiers;
  public int Weight;

  public abstract Upgrade Upgrade();
  // public bool IsUnlocked => DataManager.Instance.Get<bool>($"{ID}IsUnlocked");
  // public int TimesObtained => DataManager.Instance.Get<int>($"{ID}TimesObtained");
  // Some sort of Unlock Condition

  // public void Unlock() => DataManager.Instance.Set($"{ID}IsUnlocked", true);
}

public abstract class Upgrade {
  [HideInInspector] public UpgradeObject Object;
  [HideInInspector] public PlayerBrain Brain;
  [HideInInspector] public Coroutine Coroutine;

  public abstract void Add();
  public abstract IEnumerator UpgradeRoutine();
  public abstract void Remove();
}