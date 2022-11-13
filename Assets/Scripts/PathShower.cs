using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PathShower : MonoBehaviour
{
    
  [SerializeField]  private LineRenderer LR;
  [SerializeField] private TMP_Text PathNumber;
  
  float maxy = 2f;
  
  public void ShowLines(List<List<Cell>> paths)
  {
      PathNumber.text = paths.Count.ToString();
      
      float yStep = maxy / paths.Count;
      
      foreach (Transform t in transform)
      {
          Destroy(t.gameObject);
      }

      List<Vector3> PathPos;
      for (int i = 0; i < paths.Count; i++)
      {
          PathPos = new List<Vector3>();
          LineRenderer LRnew = Instantiate(LR, transform).GetComponent<LineRenderer>();
         
          for (int j = 0; j < paths[i].Count; j++)
          {
              PathPos.Add(  new Vector3(  paths[i][j].transform.position.x, i*yStep ,paths[i][j].transform.position.z    )); 
          }

          LRnew.positionCount = PathPos.Count;
          LRnew.SetPositions(PathPos.ToArray());
          LRnew.startColor = Random.ColorHSV();
          LRnew.endColor = LRnew.startColor;
      }
      
      
      
      
      
  }
  
  
  
  
  
}
