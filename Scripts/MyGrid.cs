using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MyGrid : MonoBehaviour
{
   
   public Tile origin;
   public Tile target;
   public Tile baseTile;

   //public Tile[] tiles;
   public List<Tile> tiles = new List<Tile>();

    public int gridWidth;
    public int gridHeight;
   

    void Update() {
        if (Application.isPlaying) {
            // code executed in play mode
        } else {
            //changeColour(); //makes sure each tile is color coded correctly
        }
    }

    public void updateGrid() {
        foreach (Tile tile in tiles) {
            tile.addNeighbours();
            updateTargetAndOrigin(tile);
            colourTiles(tile);
            updateCost(tile);
        }
    }

    // if tile is target/origin, upgrade grids target/origin
    void updateTargetAndOrigin(Tile tile) {
        if (tile.isTarget) target = tile;
        else if (tile.isOrigin) origin = tile;
    }
   

    // color grids in tile to show what type they are - more for testing then actual gaming use
   public void colourTiles(Tile tile) {
        target.GetComponent<SpriteRenderer>().color = Color.blue;
        origin.GetComponent<SpriteRenderer>().color = Color.green;
        if (!tile.walkable) tile.GetComponent<SpriteRenderer>().color = Color.red;   
        else if (tile != origin && tile != target) tile.GetComponent<SpriteRenderer>().color = Color.white;
   }

    
   public void updateCost(Tile tile) {
        float xDist = Mathf.Abs(target.transform.position.x - tile.transform.position.x);
        float yDist = Mathf.Abs(target.transform.position.y - tile.transform.position.y);

        tile.distancetoTarget = xDist + yDist;
   }

   public void createGrid(int gridWidth, int gridHeight) {

        int gridSize = gridWidth * gridHeight;

        tiles = new List<Tile>();

        for (int i = 0; i < gridWidth * gridHeight; i++) {
            Tile newtile = Instantiate(baseTile, new Vector2(0,0), Quaternion.identity, transform);
            newtile.name = "Tile: " + i;
            tiles.Add(newtile);
        }

        int x = 0;
        int y = 0;

        foreach (Tile tile in tiles) {
            tile.moveTile(x, y);
            tile.name = "Tile: " + x + "," + y;
            x++;
            if (x >= gridWidth) {
                x = 0;
                y++;
            }  
        }
       
   }



}
