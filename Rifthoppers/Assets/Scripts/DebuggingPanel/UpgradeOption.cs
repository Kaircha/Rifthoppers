using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeOption : MonoBehaviour
{
  private int Index = 0;
  public Button Button;
  public TextMeshProUGUI text;
  private Action<int> OnClick;

  public void Initialize(int index, Action<int> onClick){
    Upgrade up = RewardManager.Instance.Upgrades[index].Upgrade();
    text.text = up.GetType().Name;
    Index = index;
    OnClick = onClick;
    Button.interactable = true;
  }

  public void OnBtnClick() => OnClick.Invoke(Index);
}
