using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileEnabling : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private List<GameObject> objects = new List<GameObject>();
    private Vector3 tileSize = new Vector3(1000, 1000, 1000);
    private Vector3 myPos;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        this.gameObject.GetComponent<TerrainCollider>().enabled = false;

        this.myPos = this.transform.position + (tileSize / 2f);
        foreach (var rootObjs in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (Transform child in rootObjs.transform)
            {
                var obj = child.gameObject;
                if (obj.tag != "Player" && obj.tag != "MainCamera" && obj.tag != "AlwaysEnabled")
                {
                    float xDistance = obj.transform.position.x;
                    float zDistance = obj.transform.position.z;

                    if ((xDistance >= this.myPos.x - (tileSize.x / 2f) && zDistance >= this.myPos.z - (tileSize.z / 2f)) &&
                        (xDistance >= this.myPos.x - (tileSize.x / 2f) && zDistance <= this.myPos.z + (tileSize.z / 2f)) &&
                        (xDistance <= this.myPos.x + (tileSize.x / 2f) && zDistance >= this.myPos.z - (tileSize.z / 2f)) &&
                        (xDistance <= this.myPos.x + (tileSize.x / 2f) && zDistance <= this.myPos.z + (tileSize.z / 2f))
                        )
                        this.objects.Add(obj);
                }
            }
        }
	}

    public void ToggleTile(bool active)
    {
        this.gameObject.GetComponent<TerrainCollider>().enabled = active;
        this.gameObject.GetComponent<Terrain>().drawTreesAndFoliage = active;
        foreach (var obj in this.objects)
        {
            obj.SetActive(active);
        }
    }

    void Update()
    {/*
        if (Vector3.Distance(player.transform.position, this.myPos) <= 3000)
        //if ((player.transform.position.x >= this.myPos.x - (tileSize.x / 2f) && player.transform.position.z >= this.myPos.z - (tileSize.z / 2f)) &&
        //    (player.transform.position.x >= this.myPos.x - (tileSize.x / 2f) && player.transform.position.z <= this.myPos.z + (tileSize.z / 2f)) &&
        //    (player.transform.position.x <= this.myPos.x + (tileSize.x / 2f) && player.transform.position.z >= this.myPos.z - (tileSize.z / 2f)) &&
        //    (player.transform.position.x <= this.myPos.x + (tileSize.x / 2f) && player.transform.position.z <= this.myPos.z + (tileSize.z / 2f))
        //    )
            this.gameObject.GetComponent<TerrainCollider>().enabled = true;
        else
            this.gameObject.GetComponent<TerrainCollider>().enabled = false;*/
    }
}
