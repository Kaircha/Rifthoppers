using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : Singleton<RewardManager> {
  public List<UpgradeObject> Upgrades = new();
  public UpgradeSurface UpgradeTemplate;

  private List<GameObject> CurrentUpgrades = new();
  public List<UpgradePair> UpgradePairs = new(); // Not sure what to name this

  public float TimeLimit = 5;

  private Energy Energy => RiftManager.Instance.Energy;

  public IEnumerator RewardRoutine() {
    foreach (Player player in LobbyManager.Instance.Players)
      player.Brain.Entity.transform.position = new(0, 4.5f, 0);

    for (float i = 0; i < 3; ++i) {
      UpgradeSurface upgrade = Instantiate(UpgradeTemplate, transform);
      float angle = (i / 3) * 2 * Mathf.PI;
      upgrade.transform.position = 8f * new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
      upgrade.Initialize(GetUpgrade());
      CurrentUpgrades.Add(upgrade.gameObject);
    }

    float timer = 0;
    while (timer < TimeLimit) {
      float percentage = (TimeLimit - timer) * 100 / TimeLimit;
      Energy.Dynamic = Energy.Static = percentage;

      timer += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }


    foreach (UpgradePair pair in UpgradePairs) pair.Upgrades.Add(pair.Upgrade);

    foreach (GameObject obj in CurrentUpgrades) Destroy(obj);
    CurrentUpgrades.Clear();
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