using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    public Grid_<GridObject> grid;
    [SerializeField] private Transform transform;

    private void Awake()
    {
        int width = 11;
        int height = 10;
        float cellSize = 10f;
        grid = new Grid_<GridObject>(width, height, cellSize, new Vector3(0, 0, 0), (Grid_<GridObject> g, int x, int z) => new GridObject(g, x, z));

        Input.multiTouchEnabled = false;
    }

    public Vector3 GridPosition(int x, int z)
    {
        return grid.GetWorldPosition(x/10, z/10);
    }

    public Transform buildGround(int x, int z)
    {
       return Instantiate(transform, grid.GetWorldPosition(x, z), Quaternion.identity);
    }

    public Transform buildWall(int x, int z, Transform y)
    {
       return Instantiate(y, grid.GetWorldPosition(x, z), Quaternion.identity);
    }


    public class GridObject
    {
        private Grid_<GridObject> grid;
        private int x;
        private int z;

        public GridObject(Grid_<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return x + ", " + z;
        }
    }





}
