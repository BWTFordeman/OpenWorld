using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public Shader minimapShader;

    private void Start()
    {
        gameObject.GetComponent<Camera>().SetReplacementShader(minimapShader, "");
    }

    // Update is called once per frame
    [ExecuteInEditMode]
	void FixedUpdate () {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Euler(90, player.transform.rotation.eulerAngles.y, 0);
	}
}
