using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeInteraction : MonoBehaviour, IInteractable {
  private Upgrade Upgrade;
  public SpriteRenderer Renderer;
  public TextMeshProUGUI TempText;
  public GameObject TempTutorial;
  public ItemIndicatorUI ItemIndicator;

  // Ideally set externally in future
  private void OnEnable(){
    SetUpgrade(UpgradeManager.Instance.Upgrades[Random.Range(0, UpgradeManager.Instance.Upgrades.Count)]);
  }
  public void SetUpgrade(Upgrade upgrade) {
    Upgrade = upgrade;
    Renderer.sprite = Upgrade.Sprite;
    TempText.text = upgrade.Name;
    //if (StatManager.Instance.Upgrades.Count > 0) TempTutorial.SetActive(false);
  }

  public void ShowHighlight() {
    ItemIndicator.ShowItemIndicator(Upgrade);
    Renderer.color = Color.gray;
  }

  public void HideHighlight() {
    ItemIndicator.HideItemIndicator();
    Renderer.color = Color.white;
  }

  public void Interact(PlayerEntity interactor) {
    Upgrade.Add(interactor);
    RiftManager.Instance.NextWave();
    UpgradeManager.Instance.TakeUpgrade(Upgrade);
  }
}