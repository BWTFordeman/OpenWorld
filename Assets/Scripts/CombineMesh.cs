using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMesh : MonoBehaviour {

	// Use this for initialization
    [ContextMenu("Combine")]
	void Combine () {
        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;

        while (i < meshFilters.Length)
        {
            if (meshFilters[i].GetComponent<MeshCollider>())
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }
            i++;
        }
        transform.gameObject.AddComponent<MeshFilter>();
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);

        

        transform.gameObject.AddComponent<MeshCollider>();
        transform.GetComponent<MeshCollider>().sharedMesh = null;
        transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;

        i = 0;
        while (i < meshFilters.Length)
        {
            DestroyImmediate(meshFilters[i].gameObject.GetComponent<MeshCollider>());
            i++;
        }

        //transform.GetComponent<MeshCollider>().convex = true;
        transform.position = oldPosition;
        transform.rotation = oldRotation;
	}
	
}
