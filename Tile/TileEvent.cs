using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 受け取った入力をタイルマネージャーへ流す
/// </summary>
public class TileEvent : MonoBehaviour , IPointerClickHandler
{
    private TilePresenter presenter;
    private IReverser _reverser;
    private void Awake()
    {
        presenter = GetComponent<TilePresenter>();
        if (Info.IsTutorial)
        {
            _reverser = TutorialTileManager.Instance;
        }
        else
        {
            _reverser = TilesManager.Instance;
        }
    }

    //タイルがクリックされた時の動作
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Info.IsSceneChanging)
            return;
            
        //同時押しの場合は見過ごす
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X))
            return;
        
        if (Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(_reverser.Reverse(presenter , ReverseType.Cross));
            return;
        }

        if (Input.GetKey(KeyCode.X))
        {
            StartCoroutine(_reverser.Reverse(presenter, ReverseType.Square));
            return;
        }
        StartCoroutine(_reverser.Reverse(presenter, ReverseType.One));
    }
}

public enum ReverseType
{
    One,
    Cross,
    Square
}
