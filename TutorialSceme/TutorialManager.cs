using System.Collections;
using System.Collections.Generic;
using SyunichTool;
using UnityEngine;

public class TutorialManager : SingletonMonovehavior<TutorialManager>
{
  /// <summary>
  /// チュートリアル用マップを101、102、103とするためここで番号を管理しておく
  /// </summary>
  public static int TutorialNumber = 101;
  protected override bool IsDestroyOnLoad{ get => true; }
    
  /// <summary>
  /// ゲーム中かどうか(触れるかどうか)
  /// </summary>
  public bool CanTouch { get; set; }
  [SerializeField] private TutorialUIManager tutorialUI;

  protected override void Awake()
  {
    CanTouch = true;
    Info.IsTutorial = true;
  }

  public void GameClear()
  {
    CanTouch = false;
    AudioManager.Instance.PlaySE(3);
    TutorialNumber++;
    StartCoroutine(tutorialUI.TutorialClearUIMoving());
  }

  private void Update()
  {
    if (Info.IsSceneChanging || !CanTouch)
    {
      return;
    }

    if (TutorialTileManager.Instance.IsAnyMoving())
    {
      return;
    }
    
    if (Input.GetKeyDown(KeyCode.T))
    {
      SceneChanger.Instance.ChangeScene("TitleScene" , 0);
    }
  }
}
