using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTiles : MonoBehaviour {

    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Vector3 tileSize;

    [SerializeField]
    private int maxDistance;

    [SerializeField]
    private GameObject[] tiles;

	// Use this for initialization
	void Start () {
        this.tiles = GameObject.FindGameObjectsWithTag(tileTag);
        ToggleTiles();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ToggleTiles();
	}

    void ToggleTiles()
    {
        Vector3 playerPos = this.gameObject.transform.position;

        foreach (var tile in tiles)
        {
            Vector3 tilePos = tile.gameObject.transform.position + (tileSize / 2f);

            float xDistance = Mathf.Abs(tilePos.x - playerPos.x);
            float zDistance = Mathf.Abs(tilePos.z - playerPos.z);

            if (xDistance + zDistance > maxDistance)
            {
                tile.GetComponent<TileEnabling>().ToggleTile(false);
            }
            else
            {
                tile.GetComponent<TileEnabling>().ToggleTile(true);
            }

        }
    }
}
