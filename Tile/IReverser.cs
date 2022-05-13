using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReverser
{
    public IEnumerator Reverse(TilePresenter presenter, ReverseType type);
}
