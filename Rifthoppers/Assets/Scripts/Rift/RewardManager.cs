using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class RewardManager : Singleton<RewardManager> {
  public List<UpgradeObject> Upgrades = new();
  public UpgradeSurface UpgradeTemplate;
  public CinemachineVirtualCamera VirtualCamera;

  private List<GameObject> CurrentUpgrades = new();
  public List<UpgradePair> UpgradePairs = new(); // Not sure what to name this

  public float TimeLimit = 3;

  private Energy Energy => RiftManager.Instance.Energy;

  public void StartReward() {
    //foreach (Player player in LobbyManager.Instance.Players)
    //  player.Brain.Entity.transform.position = new(0, 0, 0);

    for (float i = 0; i < 3; ++i) {
      UpgradeSurface upgrade = Instantiate(UpgradeTemplate, transform);
      float angle = (i / 3) * 2 * Mathf.PI;
      upgrade.transform.position = 8f * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
      upgrade.Initialize(GetUpgrade());
      CurrentUpgrades.Add(upgrade.gameObject);
    }

    VirtualCamera.Priority = 20;
  }

  public IEnumerator RewardRoutine() {
    float timerA = 0f;
    float speed = Time.timeScale;
    while (timerA < 1) {
      Time.timeScale = Mathf.Lerp(speed, 1f, timerA);
      timerA += Time.deltaTime;
      yield return null;
    }

    float timerB = 0;
    while (timerB < TimeLimit) {
      float percentage = (TimeLimit - timerB) * 100 / TimeLimit;
      Energy.Dynamic = Energy.Static = percentage;

      timerB += Time.deltaTime;
      yield return null;
    }

    float timerC = 0f;
    float speedC = Time.timeScale;
    while (timerC < 1) {
      Time.timeScale = Mathf.Lerp(speedC, 0.5f, timerC);
      timerC += Time.deltaTime;
      yield return null;
    }
  }

  public void EndReward() {
    foreach (UpgradePair pair in UpgradePairs) pair.Upgrades.Add(pair.Upgrade);
    foreach (GameObject obj in CurrentUpgrades) Destroy(obj);
    CurrentUpgrades.Clear();

    VirtualCamera.Priority = 0;
  }

  public UpgradeObject GetUpgrade() => Upgrades[Random.Range(0, Upgrades.Count)]; // will [probably] want to filter in the future
}

public struct UpgradePair {
  public Upgrades Upgrades; // naming is awful, I know
  public Upgrade Upgrade;

  public UpgradePair(Upgrades upgrades, Upgrade upgrade) {
    Upgrades = upgrades;
    Upgrade = upgrade;
  }
}