using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectTileManager : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private bool IsClicked = false;
    
    [Header("行くステージの情報")]
    [SerializeField] private int StageNumber;
    [SerializeField] private int canReturnNum;
    
    [SerializeField] private TileView view;
    [SerializeField] private ParticleSystem miniParticle;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Info.IsSceneChanging)
            return;
        
        if (!IsClicked)
        {
            IsClicked = true;
            Info.IsSceneChanging = true;
            StartCoroutine(StageChange());
        }
        
    }
    
    private IEnumerator StageChange()
    {
        yield return StartCoroutine(view.Reverse());
        AudioManager.Instance.PlaySE(0);
        view.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Instantiate(miniParticle, view.gameObject.transform.position,Quaternion.identity);
        Info.StageNum = StageNumber;
        Info.CanReturnNum = canReturnNum;
        SyunichTool.SceneChanger.Instance.ChangeScene("GameScene", 0);
    }
}