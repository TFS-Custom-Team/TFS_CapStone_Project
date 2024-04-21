using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Unity.VisualScripting;
using System.Linq;
using Unity.Mathematics;
using System;
using System.Security.Cryptography;
using TreeEditor;
using UnityEditor;

public class TilemapLayoutEditor : MonoBehaviour
{
    public Tilemap tilemap;
    public string level_name = "";
    public bool allow_saving = false;
    public TileData tileData;
    public UDictionary<string, TileBase> replacements; //For numbered tiles. Easy way to change the TileBase for temporary/editor only tiles.
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
    
    public void Loadlevel() {
		LevelData data = JsonUtility.FromJson<LevelData>(m_level.text);
		tilemap.ClearAllTiles();
		int x = UnityEngine.Random.Range(1, 6);
		print(x);
        Dictionary<int, TileBase> dict = tileData.getOppositeDictionary();
		for (int i = 0; i < data.poses.Count; i++)
		{
			if ((data.tiles[i] > 0 && data.tiles[i] <= 6) && data.tiles[i] == x)
			{
				tilemap.SetTile(data.poses[i], replacements["wall"]);
			}
			else if ((data.tiles[i] > 0 && data.tiles[i] <= 6))
			{
				tilemap.SetTile(data.poses[i], replacements["floor"]);
			}
			else
			{
				tilemap.SetTile(data.poses[i], dict[data.tiles[i]]);
			}
		}
		m_current_level = m_level;
	}

    public void Loadlevel(TextAsset layout_to_load) { 
		LevelData data = JsonUtility.FromJson<LevelData>(layout_to_load.text);
		tilemap.ClearAllTiles();
        int x = UnityEngine.Random.Range(1,6);
        print(x);
		Dictionary<int, TileBase> dict = tileData.getOppositeDictionary();
		for (int i = 0; i < data.poses.Count; i++)
		{
            if ((data.tiles[i] > 0 && data.tiles[i] <= 6) && data.tiles[i] == x)
            {
                tilemap.SetTile(data.poses[i], replacements["wall"]);
            }
            else if ((data.tiles[i] > 0 && data.tiles[i] <= 6))
            {
                tilemap.SetTile(data.poses[i], replacements["floor"]);
            }
            else {
                tilemap.SetTile(data.poses[i], dict[data.tiles[i]]);
            }
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
                    levelData.tiles.Add(tileData.tiles[temp]); 
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
    public List<int> tiles = new List<int>(); //The tiles themselves.
    public List<Vector3Int> poses = new List<Vector3Int>(); //Positions of those tiles.
}


[CustomEditor(typeof(TilemapLayoutEditor))]
public class ObjectBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		TilemapLayoutEditor myScript = (TilemapLayoutEditor)target;
		if (GUILayout.Button("Load Layout"))
		{
			myScript.Loadlevel();
		}
	}
}
