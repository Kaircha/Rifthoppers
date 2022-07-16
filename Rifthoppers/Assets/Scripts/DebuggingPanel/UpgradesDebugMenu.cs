using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UpgradesDebugMenu : MonoBehaviour
{
  public GameObject EmptyMenu;
  public GameObject UpgradeBtn;

  public void AddUpgrade(int index) {
    LobbyManager.Instance.Players[0].Brain.Upgrades.Add(RewardManager.Instance.Upgrades[index].Upgrade());
    CloseMenu(0);
  }
  public void RemoveUpgrade(int index) {
    LobbyManager.Instance.Players[0].Brain.Upgrades.Remove(RewardManager.Instance.Upgrades[index].Upgrade());
    CloseMenu(0);
  }

  private GameObject menu;

  public void OpenMenu(){
    menu = Instantiate(EmptyMenu, transform.parent);
    transform.GetChild(0).gameObject.SetActive(false);
    GetComponent<Image>().enabled = false;

    GameObject a = Instantiate(UpgradeBtn, menu.transform.GetChild(0));
    a.GetComponent<UpgradeOption>().Initialize(0, CloseMenu);
    a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Back <";
  }

  public void InitializeAddUpgrades()
  {
    OpenMenu(); 
    for (int i = 0; i < RewardManager.Instance.Upgrades.Count; i++)
    {
      GameObject a = Instantiate(UpgradeBtn, menu.transform.GetChild(0));
      a.GetComponent<UpgradeOption>().Initialize(i, AddUpgrade);
    }
  }
  public void InitializeRemoveUpgrades()
  {
    OpenMenu();
    for (int i = 0; i < RewardManager.Instance.Upgrades.Count; ++i)
    {
      string Name = RewardManager.Instance.Upgrades[i].Upgrade().GetType().Name;

      bool found = false;

      foreach (Upgrade up in LobbyManager.Instance.Players[0].Brain.Upgrades.Get())
        if (up.GetType().Name == Name){
          found = true; break;
        }

      if (!found) continue;

      GameObject a = Instantiate(UpgradeBtn, menu.transform.GetChild(0));
      a.GetComponent<UpgradeOption>().Initialize(i, RemoveUpgrade);
    }
  }
  public void CloseMenu(int index){
    Destroy(menu);
    transform.GetChild(0).gameObject.SetActive(true);
    GetComponent<Image>().enabled = true;
  }
}
