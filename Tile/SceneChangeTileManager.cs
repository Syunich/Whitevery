using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneChangeTileManager : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private bool IsClicked = false;
    [SerializeField] private string sceneName;
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
            StartCoroutine(SceneChange());
        }
    }

    private IEnumerator SceneChange()
    {
        yield return StartCoroutine(view.Reverse());
        AudioManager.Instance.PlaySE(0);
        view.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Instantiate(miniParticle, view.gameObject.transform.position,Quaternion.identity);
        SyunichTool.SceneChanger.Instance.ChangeScene(sceneName, 0);
    }
    
}
