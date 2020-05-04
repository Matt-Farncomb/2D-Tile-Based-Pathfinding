using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public float distancetoTarget; // xy distance to target
    public float stepsFromOrigin; // how many steps from origin


    public GameObject satOnBy;

    public bool walkable = true;

    public bool isTarget = false;

    public bool isOrigin = false;

    public List<GameObject> neighbours;

    private RaycastHit2D hit;

    public Tile parent;

    private Vector2[] points = { 
        new Vector2( -1,  0),
        new Vector2(  0, -1),
        new Vector2(  0,  1),
        new Vector2(  1,  0)
     };


    public void addNeighbours() {
        neighbours = new List<GameObject>();
        foreach (Vector2 point in points) {
            findNeighbour(point);
        }
    }

    private void findNeighbour(Vector2 point) {
        float xpos = point.x + transform.position.x;
        float ypos = point.y + transform.position.y;
        
		hit = Physics2D.Raycast(new Vector2(xpos, ypos), Vector2.zero);
        if (hit.collider != null && hit.collider.GetComponent<Tile>().walkable) {
            neighbours.Add(hit.collider.gameObject);
        }
    }

    public void moveTile(float x, float y) {
        transform.position = new Vector2(x,y);
    }



}

