using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocationTrigger : MonoBehaviour {
    public bool trigger;
    public TextMesh text;
    public GameObject player;

    public void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateText(string txt)
    {
        text.text = txt;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger = true;
        }
    }
}
