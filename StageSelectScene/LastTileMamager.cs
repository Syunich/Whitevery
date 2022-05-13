using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class LastTileMamager : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private bool IsClicked = false;
    [SerializeField] private TileView view;
    [SerializeField] private ParticleSystem miniParticle;
    [SerializeField] private AllClearUIMoving allclear;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Info.IsSceneChanging)
            return;
        
        if (!IsClicked)
        {
            IsClicked = true;
            StartCoroutine(Reverse());
        }
    }

    private IEnumerator Reverse()
    {
        yield return StartCoroutine(view.Reverse());
        view.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Instantiate(miniParticle, view.gameObject.transform.position , Quaternion.identity);
        if (Info.IsAllCleared())
        {
            AudioManager.Instance.PlaySE(0);
            AudioManager.Instance.PlaySE(3);
           StartCoroutine(allclear.Indicate());
        }
    }
}
