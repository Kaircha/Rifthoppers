using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {
  [HideInInspector] public PlayerBrain Brain;
  private List<Upgrade> UpgradeList = new();

  public void Add(Upgrade upgrade) {
    //DataManager.Instance.Set($"{ID}TimesObtained", DataManager.Instance.Get<int>($"{ID}TimesObtained") + 1);
    Debug.Log("Added upgrade: " + upgrade);
    upgrade.Brain = Brain;
    upgrade.Add();
    upgrade.Coroutine = StartCoroutine(upgrade.UpgradeRoutine());
    UpgradeList.Add(upgrade);
  }

  public void Remove(Upgrade upgrade) {
    upgrade.Remove();
    StopCoroutine(upgrade.Coroutine);

    foreach(Upgrade up in UpgradeList)
      if(up.GetType().Name == upgrade.GetType().Name){
        UpgradeList.Remove(up);
        break;
      }
  }

  public List<Upgrade> Get() => UpgradeList;

  public void Clear() {
    foreach (Upgrade upgrade in UpgradeList) {
      upgrade.Remove();
      Debug.Log("Removed Upgrade: " + upgrade);
      StopCoroutine(upgrade.Coroutine);
    }
    UpgradeList = new();
  }
}