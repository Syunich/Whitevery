using System;
using UnityEngine;
using SyunichTool;


public class GameManager : SingletonMonovehavior<GameManager>
{
    protected override bool IsDestroyOnLoad{ get => true; }
    
    /// <summary>
    /// ゲーム中かどうか(触れるかどうか)
    /// </summary>
    public bool CanTouch { get; set; }
    public int NumOfPossibleReturn { get; set; }

    [SerializeField] private GameClearEffect gameClearEffect;

    protected override void Awake()
    {
        CanTouch = true;
    }

    public void GameClear()
    {
        CanTouch = false;
        Info.ClearStageNum.Add(Info.StageNum);
        AudioManager.Instance.PlaySE(3);
        StartCoroutine(gameClearEffect.GameClearUIMoving());
    }

    private void Update()
    {
        if (Info.IsSceneChanging || !CanTouch)
        {
            return;
        }

        if (TilesManager.Instance.IsAnyMoving())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneChanger.Instance.ChangeScene("GameScene" , 0);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneChanger.Instance.ChangeScene("StageSelectScene" , 0);
        }
    }
}
