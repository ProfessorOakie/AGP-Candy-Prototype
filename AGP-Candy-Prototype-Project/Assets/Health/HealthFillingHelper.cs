using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFillingHelper : MonoBehaviour {

    enum Axis
    {
        positiveX,
        positiveY,
        positiveZ,
        negativeX,
        negativeY,
        negativeZ
    };


    Renderer[] renderers;

    [SerializeField]
    Axis mFillAxis;

    private void Start()
    {

        Debug.LogWarning("TODO: Make this work for multiple mesh filters");
        Mesh mesh = GetComponentInChildren<MeshFilter>().mesh;

        float low = Mathf.Infinity;
        float high = Mathf.NegativeInfinity;
        for(int i = 0; i<mesh.vertices.Length; i++)
        {
            if(mFillAxis == Axis.positiveX || mFillAxis == Axis.negativeX)
            {
                low = Mathf.Min(low, mesh.vertices[i].x);
                high = Mathf.Max(high, mesh.vertices[i].x);
            }
            else if (mFillAxis == Axis.positiveY || mFillAxis == Axis.negativeY)
            {
                low = Mathf.Min(low, mesh.vertices[i].y);
                high = Mathf.Max(high, mesh.vertices[i].y);
            }
            else if (mFillAxis == Axis.positiveZ || mFillAxis == Axis.negativeZ)
            {
                low = Mathf.Min(low, mesh.vertices[i].z);
                high = Mathf.Max(high, mesh.vertices[i].z);
            }
        }

        renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (var mat in renderers[i].materials)
            {
                mat.SetInt("_FillAxis", (int)mFillAxis);
                mat.SetFloat("_FillAxisLength", (high - low));
            }
        }
       
    }

    public void SetPercent(float percent)
    {
        Debug.LogError("Not implemmented");
    }

}
