using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGenerator : MonoBehaviour
{
    // Grid and map
    public GameObject grid;
    public Tilemap tilemap;

    // Tiles
    public Tile ground;
    public Tile ceiling;

    // Locations for adding and removing tiles dynamically
    float cell = -.5f;
    int begin = 11;
    int end = -10;

    // Game variables
    public float speed = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grid.transform.position += new Vector3(-speed, 0);
        if(grid.transform.position.x <= cell)
        {
            for(int i = -5; i <= 4; i++)
            {
                tilemap.SetTile(new Vector3Int(end, i, 0), null);
            }
            tilemap.SetTile(new Vector3Int(begin, -5, 0), ground);
            tilemap.SetTile(new Vector3Int(begin, 4, 0), ceiling);
            cell -= 1;
            begin += 1;
            end += 1;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if(collision.gameObject.tag == "Bound")
        {
            
            
        }
    }
}
