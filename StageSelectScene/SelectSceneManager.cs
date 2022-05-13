using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneManager : MonoBehaviour
{
  [SerializeField] private GameObject[] StageSelectPanels;
  [SerializeField] private GameObject[] BackElements;
  public bool IsAllCleared()
  {
    foreach (var VARIABLE in StageSelectPanels)
    {
      if (VARIABLE.activeSelf)
        return false;
    }

    return true;
  }
  
  private void Awake()
  {
    foreach (var num in Info.ClearStageNum)
    {
      StageSelectPanels[num - 1].SetActive(false);
    }

    if (Info.IsAllCleared())
    {
      foreach (var VARIABLE in BackElements)
      {
      VARIABLE.SetActive(false);
      }
    }
  }
}
