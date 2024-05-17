using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;
using UnityEditor;
/* ----------------------------------------------------------------------------------------------------------------------
 * Layout Loader and Editor
 * Created by: DrRetro
 * 
 * This script is the Editor and Loader for our game. It has two main fuctions, to create layouts and load them in-game.
 * To be honest, the only function that you really need to know is Loadlevel.
 * ----------------------------------------------------------------------------------------------------------------------
 */
public class TilemapLayoutEditor : MonoBehaviour
{
	[Tooltip("Set to the current tilemap.")]
	public Tilemap tilemap;
	[Tooltip("Database of Tile IDs (Does not need to be changed from \"LayoutTileData\")")]
	public TileDatabase tileDatabase;
    [Tooltip("Replacement tiles for random generation. Requires replacement called \"floor\" and \"wall\".")]
    public UDictionary<string, TileBase> replacements; //For numbered tiles. Easy way to change the TileBase for temporary/editor only tiles.
	public TextAsset layout; //Set this with the layout you want to load in when possible.
	public GameObject Player;
	public WaveManager WaveManager;
	private Vector3 player_spawn_pos;
	// Start is called before the first frame update
	void Start() {
		Loadlevel();
	}

    // Update is called once per frame
    private void Update() {
    }
    
	//Code for loading level.
    public void Loadlevel() {
		LevelData data = JsonUtility.FromJson<LevelData>(layout.text);
		tilemap.ClearAllTiles();
		int x = UnityEngine.Random.Range(1, 6);
		print(x);
        Dictionary<int, TileBase> dict = tileDatabase.getOppositeDictionary();
		for (int i = 0; i < data.poses.Count; i++) {
			if ((data.tiles[i] > 0 && data.tiles[i] <= 6) && data.tiles[i] == x) {
				tilemap.SetTile(data.poses[i], replacements["wall"]);
			}
			else if ((data.tiles[i] > 0 && data.tiles[i] <= 6)) {
				tilemap.SetTile(data.poses[i], replacements["floor"]);
			}
			else if (data.tiles[i] == 7) {
				player_spawn_pos = tilemap.GetCellCenterWorld(data.poses[i]);
				tilemap.SetTile(data.poses[i], replacements["floor"]);
			}
			else {
				tilemap.SetTile(data.poses[i], dict[data.tiles[i]]);
			}
			playerSpawn();
		}
	}

    public void Loadlevel(TextAsset layout_to_load) { 
		LevelData data = JsonUtility.FromJson<LevelData>(layout_to_load.text);
		tilemap.ClearAllTiles();
        int x = UnityEngine.Random.Range(1,7);
        print(x);
		Dictionary<int, TileBase> dict = new Dictionary<int, TileBase>();
		try {
			dict = tileDatabase.getOppositeDictionary();
		}
		catch (Exception e) { // Throws a custom error that says that the TileDatabase is missing.
			throw new MissingTileDatabase();
		}
		for (int i = 0; i < data.poses.Count; i++) {
			if ((data.tiles[i] > 0 && data.tiles[i] <= 6) && data.tiles[i] == x)
			{
				tilemap.SetTile(data.poses[i], replacements["wall"]);
			}
			else if ((data.tiles[i] > 0 && data.tiles[i] <= 6))
			{
				tilemap.SetTile(data.poses[i], replacements["floor"]);
			}
			else if (data.tiles[i] == 7) {
				player_spawn_pos = tilemap.GetCellCenterWorld(data.poses[i]);
				tilemap.SetTile(data.poses[i], replacements["floor"]);
			}
			else
			{
				tilemap.SetTile(data.poses[i], dict[data.tiles[i]]);
			}
		}
		playerSpawn();
	}

	public void playerSpawn() {
		Player.transform.position = transform.position;
	}
	public void clearTiles() {
		tilemap.ClearAllTiles();
	}
	#if UNITY_EDITOR
	public void LoadlevelwithEditorTiles() { //Load layout with all tiles that are excluive to the editor. ONLY AVAILABLE IN EDITOR.
		LevelData data = JsonUtility.FromJson<LevelData>(layout.text);
		tilemap.ClearAllTiles();
		int x = UnityEngine.Random.Range(1, 7);
		print(x);
		Dictionary<int, TileBase> dict = new Dictionary<int, TileBase>();
		try {
			dict = tileDatabase.getOppositeDictionary();
		}
		catch (Exception e) {
			throw new MissingTileDatabase();
		}
		for (int i = 0; i < data.poses.Count; i++) {
			tilemap.SetTile(data.poses[i], dict[data.tiles[i]]);
		}
	}
	public void Savelevel(string path) { //ONLY AVAILABLE IN EDITOR.
		BoundsInt bounds = tilemap.cellBounds; //How big is this level?

		LevelData levelData = new LevelData();

		for (int x = bounds.min.x; x < bounds.max.x; x++) { // For getting all tiles in the tilemap.
			for (int y = bounds.min.y; y < bounds.max.y; y++)
			{
				TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));

				if (temp != null)
				{
					levelData.tiles.Add(tileDatabase.tiles[temp]);
					levelData.poses.Add(new Vector3Int(x, y, 0));
				}
			}
		}
		Debug.Log(path);
		string json = JsonUtility.ToJson(levelData, true);
		File.WriteAllText(path, json);
		layout = Resources.Load<TextAsset>(path);
	}

	public void Savelevel() { //ONLY AVAILABLE IN EDITOR.
        BoundsInt bounds = tilemap.cellBounds; //How big is this level?

        LevelData levelData = new LevelData(); 
        
        for(int x = bounds.min.x; x < bounds.max.x; x++) {  //For getting all tiles in the tilemap.
			for (int y = bounds.min.y; y < bounds.max.y; y++) {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));

                if (temp != null) {
                    levelData.tiles.Add(tileDatabase.tiles[temp]); 
                    levelData.poses.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData,true);
		if (layout != null)
		{
			var popup = LayoutOverwriteConfirmation.init(400, 250);
			popup.editor = this;
		}
		else
		{
			var popup = LayoutSaveName.init(300, 250);
			popup.editor = this;
		}
	}

	public void overwriteLevel() {//ONLY AVAILABLE IN EDITOR.
		BoundsInt bounds = tilemap.cellBounds; //How big is this level?

		LevelData levelData = new LevelData();

		for (int x = bounds.min.x; x < bounds.max.x; x++) {//For getting all tiles in the tilemap.
			for (int y = bounds.min.y; y < bounds.max.y; y++)
			{
				TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));

				if (temp != null)
				{
					levelData.tiles.Add(tileDatabase.tiles[temp]);
					levelData.poses.Add(new Vector3Int(x, y, 0));
				}
			}
		}

		string json = JsonUtility.ToJson(levelData, true);
		File.WriteAllText(AssetDatabase.GetAssetPath(layout), json);
	}
	#endif
}

public class LevelData
{
    public List<int> tiles = new List<int>(); //The tiles themselves.
    public List<Vector3Int> poses = new List<Vector3Int>(); //Positions of those tiles.
}
#if UNITY_EDITOR
public class LayoutSaveName : EditorWindow //Do not worry about this code. There is nothing we need to modify here.
{
	public TilemapLayoutEditor editor;
	public string name_of_file = "";
	static public LayoutSaveName init(int width, int height) {
		LayoutSaveName window = ScriptableObject.CreateInstance<LayoutSaveName>();
		window.position = new Rect((Screen.width / 2)-(width/2), (Screen.height / 2)-(height/2), width , height);
		window.ShowPopup();
		return window;
	}

	void OnGUI() {
		GUILayout.Label("Save layout as:");
		name_of_file = EditorGUILayout.TextField(name_of_file);

		if (GUILayout.Button("Save!")) {
			checkInput();
		}
		if (GUILayout.Button("Cancel")) {
			Close();	
		}
	}
	
	public void checkInput() {
		Debug.Log(editor);
		Debug.Log(name_of_file);
		if (name_of_file.Length > 0) {
			editor.Savelevel(Application.dataPath + "/_LevelJSON/" + name_of_file + ".json");
			Close();
		}
	}
}
public class LayoutOverwriteConfirmation : EditorWindow //Do not worry about this code. There is nothing we need to modify here.
{
	public TilemapLayoutEditor editor;
	static public LayoutOverwriteConfirmation init(int width, int height)
	{
		LayoutOverwriteConfirmation window = ScriptableObject.CreateInstance<LayoutOverwriteConfirmation>();
		window.position = new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);
		window.ShowPopup();
		return window;
	}

	void OnGUI()
	{
		GUILayout.Label("Are you sure you want to overwrite the currently saved layout?");

		if (GUILayout.Button("Overwrite"))
		{
			editor.overwriteLevel();
			Close();
		}
		if (GUILayout.Button("Cancel"))
		{
			Close();
		}
	}
}
[CustomEditor(typeof(TilemapLayoutEditor))]
public class SaveloadLayout : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		TilemapLayoutEditor myScript = (TilemapLayoutEditor)target;
		if (GUILayout.Button("Load Layout")) {
			myScript.LoadlevelwithEditorTiles();
		}
		if (GUILayout.Button("Load Layout w/ Replacements")) {
			myScript.Loadlevel();
		}
		if (GUILayout.Button("Save Layout")) {
			myScript.Savelevel();
		}
		if (GUILayout.Button("Clear All Tiles")) {
			myScript.clearTiles();
		}
	}
}
#endif

public class MissingTileDatabase : Exception {
	public MissingTileDatabase() : base("LayoutEditor is missing a TileDatabase. Please set \"tileDatabase\" with \"LayoutTileData\". If \"LayoutTileData\" does not exist in files, please contact a Project Manager or Lead Developer.", null) {
		return;
	}
}

