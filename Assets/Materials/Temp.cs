using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Temp : MonoBehaviour {

    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {


        Graphics.Blit(source, destination, mat);
    }
}
