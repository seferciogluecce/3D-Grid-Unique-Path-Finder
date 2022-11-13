using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    private List<List<Cell>> Grid;
    private List<List<Cell>> CalculatedPaths;

    private int xSize = 10;
    private int ySize = 10;

    private PathShower PathShower;
    
    void Start()
    {
        PathShower = FindObjectOfType<PathShower>();
        
        Grid = new List<List<Cell>>();
        for (int i = 0; i < xSize; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < ySize; j++)
            {
                row.Add(transform.GetChild(i*xSize+j).GetComponent<Cell>());
                row[j].SetCord(j,i);
            }
            Grid.Add(row);
        }
        
        CalculatePaths();
    }

   
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            CalculatePaths();    
        }
    }


    void CalculatePaths()
    {
        CalculatedPaths = new List<List<Cell>>();
        FindPaths(Grid,Grid[0][0],Grid[xSize-1][ySize-1]);
    }
    

    void StorePath(List<Cell> path)
    {
        CalculatedPaths.Add(path);
    }
     
  
    static bool isNotVisited(Cell current, List<Cell> path)
    {
        int size = path.Count;
        for(int i = 0; i < size; i++)
            if (path[i] == current)
                return false;
        
        return true;
    }

     
 
    private   void FindPaths(List<List<Cell> > g,
        Cell src, Cell dst )
    {
        // Create a queue which stores
        // the paths
        Queue<List<Cell> > queue = new Queue<List<Cell>>();
     
        // Path vector to store the current path
        List<Cell> path = new List<Cell>();
        path.Add(src);
        queue.Enqueue(path);
         
        while (queue.Count!=0)
        {
            path = queue.Dequeue();
            Cell last = path[path.Count - 1];
     
            // If last vertex is the desired destination
            // then print the path
            if (last == dst)
            {
                StorePath(path);
            }
     
            // Traverse to all the nodes connected to
            // current vertex and push new path to queue
            List<Cell> lastNode =  GetNeighbours(last);
            for(int i = 0; i < lastNode.Count; i++)
            {
                if (isNotVisited(lastNode[i], path))
                {
                    List<Cell> newpath = new List<Cell>(path);
                    newpath.Add(lastNode[i]);
                    queue.Enqueue(newpath);
                }
            }
        }
        
        PathShower.ShowLines(CalculatedPaths);
    }

    List<Cell> GetNeighbours(Cell c)
    {
        List<Cell> neighbours = new List<Cell>();
        
//left
        if (c.cordX - 1 >= 0)
        {
            if(Grid[c.cordY][c.cordX-1].isOpen)
                neighbours.Add(Grid[c.cordY ][c.cordX-1]);
        }


//right
        if (c.cordX + 1 <xSize)
        {
            if(Grid[ c.cordY][c.cordX+1].isOpen)
                neighbours.Add(Grid[c.cordY][c.cordX+1]);
        }

//up
        if (c.cordY - 1 >= 0)
        {
            if(Grid[c.cordY-1][ c.cordX].isOpen)
                neighbours.Add(Grid[c.cordY-1][c.cordX ]);
        }
//down
        if (c.cordY + 1 < ySize)
        {
            if(Grid[c.cordY+1][c.cordX].isOpen)
                neighbours.Add(Grid[c.cordY+1][c.cordX]);
        }

        return neighbours;
    }
    
    
    
    
    
    
    
    
    
    
    
    
}