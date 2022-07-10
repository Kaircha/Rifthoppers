using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager> {
  public List<UpgradeObject> Upgrades = new();
  public UpgradeInteractable UpgradeTemplate;

  public List<GameObject> CurrentUpgrades = new();
  public bool upgrading;

  public IEnumerator RewardRoutine() {

    upgrading = true;

    foreach (Player player in LobbyManager.Instance.Players)
      player.Brain.Entity.transform.position = new(0, 4.5f, 0);

    for(int i = 0; i < 3; ++i){
      UpgradeInteractable upgrade = Instantiate(UpgradeTemplate, transform);
      upgrade.transform.position = new Vector3(12 * (i-1), 12 * (i%2), 0);
      upgrade.Initialize(GetUpgrade());
      CurrentUpgrades.Add(upgrade.gameObject);
    }


    #region oldIdea
    // Time slows down.
    // Upgrades appear on - screen.
    // Energy begins draining, and the Rift closes(old animation).
    // Background is now black, with only the character at the center, behind the upgrades.
    // Energy fills back up, maybe in a different color?
    // Energy is full, and the selected / random upgrade is picked.
    // Unchosen upgrades disappear; picked upgrade shrinks.
    // Little upgrade flies toward the character, and hits it with a little happy dinging noise.
    // Music intro of the next area.
    // New area opens very quickly, with some cool transition effect, like screen-tearing, static, whatever.
    #endregion

    while (upgrading) yield return null;

    foreach (GameObject obj in CurrentUpgrades) Destroy(obj);
    CurrentUpgrades.Clear();
  }

  public UpgradeObject GetUpgrade() => Upgrades[Random.Range(0, Upgrades.Count)]; // will [probably] want to filter in the future
}