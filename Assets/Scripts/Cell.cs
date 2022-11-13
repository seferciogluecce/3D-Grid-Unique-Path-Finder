using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isOpen;

    public Material OpenMat;
    public Material CloseMat;

    private Renderer renderer;

    public int cordX;
    public int cordY;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        SetMaterial();
    }

    public void SetCord(int x,int y)
    {
        cordX = x;
        cordY = y;
        gameObject.name = "Cell " + x + " " + y;
    }

    public string GetName()
    {
        return "Cell " + cordX + " " + cordY;
    }
    
    void SetMaterial()
    {
        renderer.material = (isOpen) ? OpenMat : CloseMat;
    }
    
    private void OnMouseDown()
    {
        isOpen = !isOpen;
        SetMaterial();
    }

}
