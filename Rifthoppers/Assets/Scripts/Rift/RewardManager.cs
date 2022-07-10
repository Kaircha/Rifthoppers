using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : Singleton<RewardManager> {
  public List<UpgradeObject> Upgrades = new();
  public UpgradeSurface UpgradeTemplate;

  public List<GameObject> CurrentUpgrades = new();
  public Upgrade CurrentUpgrade = null;

  public Transform EnergyBar;
  public Material RiftWaveUI;

  private float TimeWindow = 3;

  private Energy Energy => RiftManager.Instance.Energy;

  public IEnumerator RewardRoutine() {

    EnableEnergyBar();

    foreach (Player player in LobbyManager.Instance.Players)
      player.Brain.Entity.transform.position = new(0, 4.5f, 0);

    for(int i = 0; i < 3; ++i){
      UpgradeSurface upgrade = Instantiate(UpgradeTemplate, transform);
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

    float time = 0; 

    while(time < TimeWindow){

      float percentage = (TimeWindow - time) * 100 / TimeWindow;
      Energy.Dynamic = Energy.Static = percentage;

      time += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    //yield return new WaitForSeconds(3);

    foreach (GameObject obj in CurrentUpgrades) Destroy(obj);
    CurrentUpgrades.Clear();
    DisableEnergyBar();

    if (CurrentUpgrade != null)
      CurrentUpgrade.Add();

    CurrentUpgrade = null;
  }

  public void EnableEnergyBar() => EnergyBar.GetComponent<Image>().material = EnergyBar.GetChild(0).GetComponent<Image>().material = EnergyBar.GetChild(1).GetComponent<Image>().material = null;
  public void DisableEnergyBar() => EnergyBar.GetComponent<Image>().material = EnergyBar.GetChild(0).GetComponent<Image>().material = EnergyBar.GetChild(1).GetComponent<Image>().material = RiftWaveUI;

  public UpgradeObject GetUpgrade() => Upgrades[Random.Range(0, Upgrades.Count)]; // will [probably] want to filter in the future
}