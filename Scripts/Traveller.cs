using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Traveller : MonoBehaviour
{
    Tile tile; // the tile traveller currently occupoies

    Stack<Tile> newPath = new Stack<Tile>();

    public List<GameObject> myNeighbours = new List<GameObject>();
    List<GameObject> past = new List<GameObject>();

    public List<Tile> path = new List<Tile>();
    public bool pathComplete = false;
    public GameObject grid;

    public int speed;
    public int index;
    Tile tempTile;
    public bool enablePathfinding = false;

    private void Start() {
        grid = GameObject.Find("Grid");
        tile = grid.GetComponent<MyGrid>().origin;
        transform.position = tile.transform.position;
        myNeighbours.Add(tile.gameObject);
        tempTile = tile;
    }
    private void Update() {
        if (enablePathfinding) {
            Debug.Log("Travel");
            travel();
            if (pathComplete) {
                //followPath();
                if (transform.position == tempTile.transform.position && newPath.Count != 0) tempTile = newPath.Pop();
                else transform.position = Vector2.MoveTowards(transform.position, tempTile.transform.position, speed * Time.deltaTime);

            }
            Debug.Log("Traveled");
        }
    }

    public bool checkIfArrived() {
        // if currently occupied tile is the target tile, than you have arrived
        if (tile.isTarget) {
            return true;
        } else {
            //if (myNeighbours.Count > 0) myNeighbours.RemoveAt(0);
            return false;
        }
    }
    // if not arrived, sort neighbours list and call addnegihbours, which adds the the nighbours first tile of sorted list
    public void travel() {
        if (!checkIfArrived()) {
            //myNeighbours = mySort();
             ///myNeighbours.OrderBy(x => x.GetComponent<Tile>().distancetoTarget +  x.GetComponent<Tile>().stepsFromOrigin);
            myNeighbours.Sort(delegate(GameObject a, GameObject b) {
                return (
                    a.GetComponent<Tile>().distancetoTarget +  a.GetComponent<Tile>().stepsFromOrigin) // add the abs value of diff between current and next coord
                    .CompareTo
                    (b.GetComponent<Tile>().distancetoTarget +  b.GetComponent<Tile>().stepsFromOrigin);
            });

            addNeighbours(myNeighbours);
        } else if (checkIfArrived() && !pathComplete) {
            Debug.Log("arrived!");
            makePath(tile);
            //path.Reverse();
            pathComplete = true;
            Debug.Log("path complete!");     
        }
    }

    public void makePath(Tile tile) {
        if (tile != null) {
            tile.GetComponent<SpriteRenderer>().color = Color.gray; // for testing
            //path.Add(tile);
            newPath.Push(tile);
            makePath(tile.parent);
        }     
    }

    public float distanceApart(Tile origin, Tile nextTile) {
        float xDist = Mathf.Abs(origin.transform.position.x - nextTile.transform.position.x);
        float yDist = Mathf.Abs(origin.transform.position.y - nextTile.transform.position.y);
        return xDist + yDist;
    }

    public void addNeighbours(List<GameObject> neighbours) {
        
            tile = neighbours[0].GetComponent<Tile>();
            tile.GetComponent<SpriteRenderer>().color = Color.black;
            foreach (GameObject obj in tile.neighbours) {
                
                if (!past.Contains(obj)) {
                    obj.GetComponent<Tile>().stepsFromOrigin = tile.stepsFromOrigin + 1;
                    obj.GetComponent<Tile>().parent = tile;
                    myNeighbours.Add(obj); 
                }
                if (!past.Contains(tile.gameObject)) past.Add(tile.gameObject);
                myNeighbours.Remove(tile.gameObject);        
            }   
    }

}
