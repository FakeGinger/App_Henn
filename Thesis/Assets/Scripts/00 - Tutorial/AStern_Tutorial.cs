using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStern_Tutorial : MonoBehaviour
{
    class Tile
    {
        public int X { get; set; }
        public int Z { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile Parent { get; set; }

        public void SetDistance(int targetX, int targetZ)
        {
            this.Distance = Mathf.Abs(targetX - X) + Mathf.Abs(targetZ - Z);
        }
    }


    public GameObject[] tiles;
    public GameObject startP;
    public GameObject finishP;
    public GameObject taskDone;
    public GameObject taskNotDone;
    public GameObject RoomUI;
    public GameObject database;

    [SerializeField] private GameObject roomCollector;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private Transform wallsParent;
    [SerializeField] private GameObject[] rooms;

    public void checkForPath()
    {
            if (Globals.objectID != 0)
            {
                var x = GameObject.Find(Globals.objectID.ToString());
                x.GetComponent<Room_Building>().placeObject();
            }

            tiles = GameObject.FindGameObjectsWithTag("Floor");
            var existingTiles = new List<Tile>();
            var path = new List<Tile>();
            var visited = new List<Tile>();

            foreach (GameObject target in tiles)
            {
                var placeholder = new Tile();
                placeholder.X = Mathf.RoundToInt(target.transform.position.x);
                placeholder.Z = Mathf.RoundToInt(target.transform.position.z);
                existingTiles.Add(placeholder);
            }

            var start = new Tile();
            start.X = Mathf.FloorToInt(startP.transform.position.x);
            start.Z = Mathf.FloorToInt(startP.transform.position.z);
            path.Add(start);

            var finish = new Tile();
            finish.X = Mathf.FloorToInt(finishP.transform.position.x);
            finish.Z = Mathf.FloorToInt(finishP.transform.position.z) - 10;
            start.SetDistance(finish.X, finish.Z);

            while (path.Count != 0)
            {
                var checkTile = path.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Z == finish.Z)
                {

                //Globals.buildMode = false;
                //DoorWindowUI.SetActive(true);
                //database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Der Hausbau wurde abgeschlossen. Die Platzierung von Türen wird gestartet.");
                //setCamera.GetComponent<PerspectivePan>().setCamera();
                //Globals.placeRoom = false;

                database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Der Hausbau des Tutorials wurde abgeschlossen. Die Platzierung von Türen wird gestartet.");
                taskDone.SetActive(true);
                //Globals.notification = true;
                RoomUI.SetActive(false);
                deleteWalls();

                rooms = GameObject.FindGameObjectsWithTag("Room");
                    foreach (GameObject room in rooms)
                    {
                        room.GetComponent<BoxCollider>().enabled = false;
                        Destroy(room.GetComponent<Room_Building>());
                    }

                    return;
                }
                visited.Add(checkTile);
                path.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(existingTiles, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    if (visited.Any(x => x.X == walkableTile.X && x.Z == walkableTile.Z))
                        continue;

                    if (path.Any(x => x.X == walkableTile.X && x.Z == walkableTile.Z))
                    {
                        var existingTile = path.First(x => x.X == walkableTile.X && x.Z == walkableTile.Z);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            path.Remove(existingTile);
                            path.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        path.Add(walkableTile);
                    }
                }
            }
        taskNotDone.SetActive(true);
        //Globals.notification = true;
        //database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Nutzer versucht, Hausbau zu beenden. Es wurden noch nicht genug Räume platziert.");
    }

    private static List<Tile> GetWalkableTiles(List<Tile> map, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>();
        var foundTiles = new List<Tile>();

        var test = new Tile { X = currentTile.X, Z = currentTile.Z - 10, Parent = currentTile, Cost = currentTile.Cost + 1 };
        possibleTiles.Add(test);
        var test_zwei = new Tile { X = currentTile.X, Z = currentTile.Z + 10, Parent = currentTile, Cost = currentTile.Cost + 1 };
        possibleTiles.Add(test_zwei);
        var test_drei = new Tile { X = currentTile.X - 10, Z = currentTile.Z, Parent = currentTile, Cost = currentTile.Cost + 1 };
        possibleTiles.Add(test_drei);
        var test_vier = new Tile { X = currentTile.X + 10, Z = currentTile.Z, Parent = currentTile, Cost = currentTile.Cost + 1 };
        possibleTiles.Add(test_vier);

        foreach (Tile tile in possibleTiles)
        {
            Vector3 placeholder = new Vector3(tile.X, 0, tile.Z);

            for (int i = 0; i < map.Count; i++)
            {
                Vector3 placeholder_second = new Vector3(map[i].X, 0, map[i].Z);

                if (placeholder == placeholder_second)
                {
                    tile.SetDistance(targetTile.X, targetTile.Z);
                    foundTiles.Add(tile);
                }
            }
        }
        return foundTiles;
    }

    //public void reset()
    //{
    //    UI_Alert.SetActive(false);
    //    tiles = null;
    //    Globals.notification = false;
    //}

    public void deleteWalls()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");

        for (int i = 0; i < walls.Length; i++)
        {
            string x = Mathf.RoundToInt(walls[i].transform.position.x).ToString();
            string z = Mathf.RoundToInt(walls[i].transform.position.z).ToString();
            walls[i].name = x + z;
        }

        var existingWalls = new List<GameObject>();

        foreach (GameObject target in walls)
        {
            existingWalls.Add(target);
        }

        existingWalls = existingWalls.OrderBy(x => x.name).ToList();

        for (int i = 0; i < existingWalls.Count; i++)
        {
            if (i + 1 < existingWalls.Count)
            {
                if (existingWalls[i].name == existingWalls[i + 1].name)
                {
                    existingWalls[i].SetActive(false);
                }

                if (existingWalls[i].name == "15100")
                {
                    existingWalls[i].SetActive(false);
                }
            }
        }
    }

}
