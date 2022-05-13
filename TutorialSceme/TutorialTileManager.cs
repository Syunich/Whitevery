using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SyunichTool;

public class TutorialTileManager : SingletonMonovehavior<TutorialTileManager> , IReverser
{
    protected override bool IsDestroyOnLoad
    {
        get => true;
    }

    //タイル全般を扱う
    [SerializeField] MapCreator _mapcreator;
    private TilePresenter[,] presenters;
    private float lengthBetweenTile = 0.8f;
    public float LengthBetweenTile
    {
        get => lengthBetweenTile;
    }

    public bool IsAnyMoving()
    {
        foreach (var presenter in presenters)
        {
           if(presenter.GetModel().IsMoving) 
               return true;
        }
        return false;
    }

    public bool IsAnyBlack()
    {
        foreach (var presenter in presenters)
        {
            if(!presenter.GetModel().IsWhite) 
                return true;
        }
        return false;
    }

    public int xlength
    {
        get => presenters.GetLength(1);
    }
    public int ylength
    {
        get => presenters.GetLength(0);
    }

    protected override void Awake()
    {
        Info.StageNum = TutorialManager.TutorialNumber;
        presenters = _mapcreator.CreateMap();
    }
    
    public IEnumerator Reverse(TilePresenter presenter, ReverseType type)
    {
        if (IsAnyMoving() || !TutorialManager.Instance.CanTouch)
        {
            yield break;
        }

        if (!CheckPresenterInPresenters(presenter))
        {
            Debug.LogError("Cant Find" + presenter + "in Array");
        }
        
        var indexes = GetElementsFromPresenter(presenter);
        TilePresenter[] selected;
        switch (type)
        {
            case ReverseType.One: StartCoroutine(presenter.Reverse()); break;
            case ReverseType.Cross: selected = TileSelecter.SelectCross(presenters , indexes.i , indexes.j);
                foreach (var pre in selected)
                {
                   StartCoroutine(pre.Reverse());
                }
                break;
            case ReverseType.Square: selected = TileSelecter.SelectSquare(presenters , indexes.i , indexes.j);
                foreach (var pre in selected)
                {
                    StartCoroutine(pre.Reverse());
                }
                break;
        }
        //TODO::ここの待機時間雑
        yield return new WaitForSeconds(0.91f);
        switch (type)
        {
            case ReverseType.One :AudioManager.Instance.PlaySE(0);
                break;
            case  ReverseType.Cross: AudioManager.Instance.PlaySE(1);
                break;
            case ReverseType.Square: AudioManager.Instance.PlaySE(2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        Debug.Log(!IsAnyBlack());
        if (!IsAnyBlack())
        {
            TutorialManager.Instance.GameClear();
        }
        
    }

    private (int i , int j) GetElementsFromPresenter(TilePresenter presenter)
    {
        if (!CheckPresenterInPresenters(presenter))
        {
            Debug.LogError("Cant Find" + presenter + "in Array");
        }

        var x = presenter.transform.localPosition.x;
        var y = presenter.transform.localPosition.y;
        int i = (int)Math.Round(ylength / 2 - y / lengthBetweenTile - 0.5f * (1 - ylength % 2));
        int j = (int)Math.Round(x / lengthBetweenTile + xlength / 2 - 0.5f * (1 - xlength % 2));

        return (i, j);
    }

    private bool CheckPresenterInPresenters(TilePresenter presenter)
    {
        bool result = false;
        foreach (var pre in presenters)
        {
            if (pre == presenter)
            {
                result = true;
                break;
            }
        }
        
        return result;
    }
}

