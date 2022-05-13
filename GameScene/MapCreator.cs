using UnityEngine;

public class MapCreator : MonoBehaviour
{ 
   [SerializeField] private GameObject mapTileObj;
   [SerializeField] Transform TopWall;
   [SerializeField] Transform ButtomWall;
   [SerializeField] Transform LeftWall;
   [SerializeField] Transform RightWall;
   
   //タイル同士の間隔
   private float lengthBetweenTile = 0.8f;
   
   //タイル1つにつき壁が移動する量
   private int unitPerWallMoveAmout = 57;

   private string[] LoadMapFromResource(int mapnumber)
   {
      string path = "Maps/map" + mapnumber;
      TextAsset textAsset = (Resources.Load(path, typeof(TextAsset)) as TextAsset);
      string allLoadedText = textAsset.text;
      string[] splitTexts = allLoadedText.Split('\n');
      return splitTexts;
   }

   //縦横等間隔に生成
   public TilePresenter[,] CreateMap()
   {
      string[] MapLines = LoadMapFromResource(Info.StageNum);
      int ylength = MapLines.Length;
      int xlength = MapLines[0].Length - 1;
      Debug.Log(xlength);
      Debug.Log(ylength);
      float x, y;
      TilePresenter[ , ] result = new TilePresenter[ylength , xlength];
      
      SetWalls(xlength , ylength);
      for (int i = 0; i < ylength; i++)
      {   
         y = ((ylength / 2) - i) * lengthBetweenTile - (lengthBetweenTile / 2.0f) * (1 - ylength % 2);
         for (int j = 0; j < xlength; j++)
         {
            bool isWhite = CheckIsWhiteFromNum(MapLines[i][j]);
            x = (j -  (xlength / 2)) * lengthBetweenTile + (lengthBetweenTile / 2.0f) * (1 - xlength % 2);
            result[i ,j] = TilePreCreator.Instantiate(mapTileObj, new Vector3(x, y, mapTileObj.transform.position.z), isWhite);
         }
      }
      return result;
   }

   private bool CheckIsWhiteFromNum(char c)
   {
      if (c == '1') return true;
       return false;
   }

   private void SetWalls(int xLength , int yLength)
   {
      //隠し壁移動量
      float MoveXAmount = (xLength / 2) * unitPerWallMoveAmout - (57f / 2f) * (1 - xLength % 2);
      float MoveYAmount = (yLength / 2) * unitPerWallMoveAmout - (57f / 2f) * (1 - yLength % 2);
      TopWall.localPosition    = new Vector3(TopWall.localPosition.x, TopWall.localPosition.y + MoveYAmount, TopWall.localPosition.z);
      ButtomWall.localPosition = new Vector3(ButtomWall.localPosition.x, ButtomWall.localPosition.y - MoveYAmount, ButtomWall.localPosition.z);
      RightWall.localPosition  = new Vector3(RightWall.localPosition.x + MoveXAmount, RightWall.localPosition.y, RightWall.localPosition.z);
      LeftWall.localPosition   = new Vector3(LeftWall.localPosition.x - MoveXAmount, LeftWall.localPosition.y, LeftWall.localPosition.z);
   }


}
