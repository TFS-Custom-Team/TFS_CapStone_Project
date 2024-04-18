using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Unity.VisualScripting;

public class TilemapLayoutEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S) && Application.isEditor) Savelevel();
    }
    public Tilemap tilemap;

    void Loadlevel() { //Loads level from a json file.
        string json = File.ReadAllText(Application.dataPath + "LevelJSON/test_level.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);
		tilemap.ClearAllTiles();

        for (int i = 0; i < data.poses.Count; i++) {
            tilemap.SetTile(data.poses[i], data.tiles[i]);
        }
	}

    void Savelevel() {
        BoundsInt bounds = tilemap.cellBounds;

        LevelData levelData = new LevelData();
        
        for(int x = bounds.min.x; x < bounds.max.x; x++) {
            for(int y = bounds.min.y; y < bounds.max.y; y++) {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y,0));

                if (temp != null) {
                    levelData.tiles.Add(temp);
                    levelData.poses.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData,true);
        File.WriteAllText(Application.dataPath + "LevelJSON/test_level.json",json);
    }
}

public class LevelData
{
    public List<TileBase> tiles;
    public List<Vector3Int> poses;
}