using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeInteraction : MonoBehaviour, IInteractable {
  public Reward Reward;
  private Upgrade Upgrade;
  public SpriteRenderer Renderer;
  public TextMeshProUGUI TempText;
  public GameObject TempTutorial;
  public ItemIndicatorUI ItemIndicator;

  // Ideally set externally in future
  private void OnEnable(){
    SetUpgrade(UpgradeWeaponManager.Instance.Upgrades[4]);
    //SetUpgrade(UpgradeWeaponManager.Instance.Upgrades[Random.Range(0, UpgradeWeaponManager.Instance.Upgrades.Count)]);
  }
  public void SetUpgrade(Upgrade upgrade) {
    Upgrade = upgrade;
    Renderer.sprite = Upgrade.Sprite;
    TempText.text = upgrade.name;
  }

  public void ShowHighlight() {
    ItemIndicator.ShowItemIndicator(Upgrade);
    Renderer.color = Color.gray;
  }

  public void HideHighlight() {
    ItemIndicator.HideItemIndicator();
    Renderer.color = Color.white;
  }

  public void Interact(PlayerBrain interactor) {
    interactor.Upgrades.Add(Upgrade);
    Reward.IsFinished = true;
    UpgradeWeaponManager.Instance.TakeUpgrade(Upgrade);
  }
}