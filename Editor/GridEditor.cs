using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyGrid))]
public class NewGrid : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        MyGrid grid = (MyGrid)target;

        if (GUILayout.Button("Create Grid")) { 
            grid.createGrid(grid.gridWidth, grid.gridHeight);
        }

        if (GUILayout.Button("Bake Neighbours")) {
            grid.updateGrid();
        }
    }
}
