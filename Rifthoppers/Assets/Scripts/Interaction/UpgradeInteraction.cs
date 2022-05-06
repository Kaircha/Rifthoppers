using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeInteraction : MonoBehaviour, IInteractable {
  private UpgradeOld Upgrade;
  private SpriteRenderer Renderer;
  public TextMeshProUGUI TempText;
  public GameObject TempTutorial;

  private void Awake() {
    Renderer = GetComponent<SpriteRenderer>();
  }

  // Ideally set externally in future
  private void OnEnable() => SetUpgrade(RiftManager.Instance.Upgrades[Random.Range(0, RiftManager.Instance.Upgrades.Count)]);

  public void SetUpgrade(UpgradeOld upgrade) {
    Upgrade = upgrade;
    Renderer.sprite = Upgrade.Sprite;
    TempText.text = upgrade.name;
    if (StatManager.Instance.Upgrades.Count > 0) TempTutorial.SetActive(false);
  }

  public void ShowHighlight() {
    Renderer.color = Color.gray;
  }

  public void HideHighlight() {
    Renderer.color = Color.white;
  }

  public void Interact(PlayerEntity interactor) {
    Upgrade.Apply();
    RiftManager.Instance.NextWave();
  }
}