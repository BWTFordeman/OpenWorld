using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitScript : MonoBehaviour {
    public AudioClip clip;
    public bool played = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!played)
            {
                SoundManager.instance.playEfx(clip, true);
                played = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (played)
            {
                SoundManager.instance.loopingEfxSource.Stop();
                SoundManager.instance.loopingEfxSource.clip = null;
                played = false;
            }
        }
    }
}
