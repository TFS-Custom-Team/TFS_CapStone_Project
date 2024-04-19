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
    public bool allow_saving = false;
    public TextAsset m_level; //Set this with the layout you want to load in when possible.
    private TextAsset m_current_level; // The level currently loaded into the game.
	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A) && Application.isEditor) Savelevel();

        if (m_current_level != m_level)
        {
            Loadlevel(m_level);
        }
    }
    

    void Loadlevel(TextAsset layout_to_load) { 
		LevelData data = JsonUtility.FromJson<LevelData>(layout_to_load.text);
		tilemap.ClearAllTiles();

		for (int i = 0; i < data.poses.Count; i++)
		{
			tilemap.SetTile(data.poses[i], data.tiles[i]);
		}
        m_current_level = m_level;
	}

    void Savelevel() { //ONLY AVAILABLE IN EDITOR.
		if (!allow_saving) {
            return;
        }
        BoundsInt bounds = tilemap.cellBounds; //How big is this level?

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
        File.WriteAllText(Application.dataPath + "/LevelJSON/"+GetVaildLayoutName(), json);
    }

    private string GetVaildLayoutName() { //For saving. Makes sure that there is no layouts that exist with the same name, and if so, will be given an alternative name.
		var info = new DirectoryInfo(Application.dataPath + "/LevelJSON"); //Folder full of layout JSONs.
        var fileInfo = info.GetFiles().ToArray(); //JSON files.
        int depth = 1; //For making sure the new level has a vaild name, by adding this number at the end.
        while (true) {
            string temp = level_name+'-'+depth.ToString()+".json"; // "test_level-1.json"
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
    public List<TileBase> tiles = new List<TileBase>(); //The tiles themselves.
    public List<Vector3Int> poses = new List<Vector3Int>(); //Positions of those tiles.
}