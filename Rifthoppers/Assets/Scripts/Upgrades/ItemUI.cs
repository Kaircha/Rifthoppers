using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
  public Animator anim;
  public TextMeshProUGUI Name;
  public TextMeshProUGUI Quote;

  private void OnEnable() => UpgradeManager.Instance.OnUpgradeTaken += TriggerInfo;
  private void OnDisable() => UpgradeManager.Instance.OnUpgradeTaken -= TriggerInfo;

  public void TriggerInfo(Upgrade upgrade) {

    Name.text = upgrade.Name;
    Quote.text = upgrade.Quote;
    StartCoroutine(DoAnimation());
  }

  private IEnumerator DoAnimation()
  {
    anim.SetBool("UpgradeTaken", true);
    yield return new WaitForSeconds(5);
    anim.SetBool("UpgradeTaken", false);
  }
}
