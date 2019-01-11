using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruins : MonoBehaviour {
    public GameObject[] lights;
    public GameObject player;

	// Use this for initialization
	void Start () {
        lights = GameObject.FindGameObjectsWithTag("smallTorch");
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var light in lights)
        {
            if (Vector3.Distance(light.transform.position, player.transform.position) < 20f)
            {
                if (!light.GetComponentInChildren<Light>().enabled)
                {
                    light.GetComponentInChildren<ParticleSystem>().Play();
                    light.GetComponentInChildren<Light>().enabled = true;
                }
            }
            else
            {
                if (light.GetComponentInChildren<Light>().enabled)
                {
                    light.GetComponentInChildren<ParticleSystem>().Stop();
                    light.GetComponentInChildren<Light>().enabled = false;
                }
            }
        }
	}
}
