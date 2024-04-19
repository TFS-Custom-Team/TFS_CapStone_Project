using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

public class TilemapLayoutEditor : MonoBehaviour
{
    public Tilemap tilemap;
    public string level_name = "";
    public TextAsset m_level;
    private TextAsset m_current_level;
	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A) && Application.isEditor) Savelevel();

        if (m_current_level != m_level)
        {
            loadLayout_in_Editor(m_level);
        }
    }
    

    void loadLayout_in_Editor(TextAsset layout_to_load) {
		LevelData data = JsonUtility.FromJson<LevelData>(layout_to_load.text);
		tilemap.ClearAllTiles();

		for (int i = 0; i < data.poses.Count; i++)
		{
			tilemap.SetTile(data.poses[i], data.tiles[i]);
		}
        m_current_level = m_level;
	}

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
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));

                if (temp != null) {
                    levelData.tiles.Add(temp); 
                    levelData.poses.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData,true);
        File.WriteAllText(Application.dataPath + "/LevelJSON/"+GetVaildLevelName(), json);
    }

    private string GetVaildLevelName() {
		var info = new DirectoryInfo(Application.dataPath + "/LevelJSON");
        var fileInfo = info.GetFiles().ToArray();
        int depth = 1;
        while (true) {
            string temp = level_name+'-'+depth.ToString()+".json";
            if (File.Exists(Application.dataPath + "/LevelJSON/" + temp)) {
                depth++;
            }
            else {
                return temp;
            }
        }
	}
    
}

public class LevelData
{
    public List<TileBase> tiles = new List<TileBase>();
    public List<Vector3Int> poses = new List<Vector3Int>();
}