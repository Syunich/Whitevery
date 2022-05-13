using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SyunichTool;

public class TilesManager : SingletonMonovehavior<TilesManager> , IReverser
{
    protected override bool IsDestroyOnLoad
    {
        get => true;
    }

    //タイル全般を扱う
    [SerializeField] MapCreator _mapcreator;
    [SerializeField] private RemainTextManager RTM;
    private TilePresenter[,] presenters;
    private float lengthBetweenTile = 0.8f;
    private int canreturnnum;
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
        presenters = _mapcreator.CreateMap();
        canreturnnum = Info.CanReturnNum;
        RTM.IndicateRemain(canreturnnum);
    }
    
    public IEnumerator Reverse(TilePresenter presenter, ReverseType type)
    {
        if (IsAnyMoving() || !GameManager.Instance.CanTouch)
        {
            yield break;
        }

        //回数上限時の処理
        if (canreturnnum == 0)
        {
            yield break;
        }   
        
            if (!CheckPresenterInPresenters(presenter))
        {
            Debug.LogError("Cant Find" + presenter + "in Array");
        }

            canreturnnum -= 1;
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
        RTM.IndicateRemain(canreturnnum);
        switch (type)
        {
            case ReverseType.One :AudioManager.Instance.PlaySE(0); Debug.Log("called1");
                break;
            case  ReverseType.Cross: AudioManager.Instance.PlaySE(1);Debug.Log("called2");
                break;
            case ReverseType.Square: AudioManager.Instance.PlaySE(2);Debug.Log("called3");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        if (!IsAnyBlack())
        {
            GameManager.Instance.GameClear();
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

static class TileSelecter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="presenters">presenter配列</param>
    /// <param name="i">中心presenterのx番号</param>
    /// <param name="j">中心presenterのy番号</param>
    /// <returns></returns>
     public static TilePresenter[] SelectCross(TilePresenter[,] presenters, int i, int j)
    {
        var result = new List<TilePresenter>();
       var xlength = presenters.GetLength(1);
       var ylength = presenters.GetLength(0);
       
       //上方向追加
       if (j > 0)
       {
           result.Add(presenters[i,j - 1]);
       }
       //下方向追加
       if (j < xlength - 1)
       {
           result.Add(presenters[i,j + 1]);
       }
       //左方向追加
       if (i > 0)
       {
           result.Add(presenters[i - 1,j]);
       }
       //右方向追加
       if(i < ylength - 1)
       {
           result.Add(presenters[i + 1,j]);
       }
       result.Add(presenters[i,j]);

       return result.ToArray();
    }

    public static TilePresenter[] SelectSquare(TilePresenter[,] presenters, int i, int j)
    {
        var result = new List<TilePresenter>();
        var xlength = presenters.GetLength(1);
        var ylength = presenters.GetLength(0);
        //左上追加
        if (j > 0 && i > 0)
        {
            result.Add(presenters[i - 1, j - 1]);
        }
        //右上追加
        if (j > 0 && i < ylength - 1)
        {
            result.Add(presenters[i + 1, j - 1]);
        }
        //左下追加
        if (j < xlength - 1 && i > 0)
        {
            result.Add(presenters[i - 1, j + 1]);
        }
        //右下追加
        if (j < xlength - 1 && i < ylength - 1)
        {
            result.Add(presenters[i + 1, j + 1]);
        }

        foreach (var VARIABLE in SelectCross(presenters , i , j))
        {
            result.Add(VARIABLE);
        }

        return result.ToArray();
    }

}
    
