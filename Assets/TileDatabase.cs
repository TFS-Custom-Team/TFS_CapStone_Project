using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* ----------------------------------------------------------------------------------------------------------------------
 * Tile Database
 * Created by: DrRetro
 * 
 * This script is the framework of how layouts are loaded and how random generation works.
 * It contains a dictionary (ignore the UDictionary part, it is just a normal dictionary. The UDictionary is used for the 
 * ability to edit the dictionary in the editor.)
 * 
 * If you need to change the tiles in the database, find "LayoutTileDatabase" in the files.
 * ----------------------------------------------------------------------------------------------------------------------
 */

[CreateAssetMenu]
public class TileDatabase : ScriptableObject
{
	public UDictionary<TileBase, int> tiles;

	public Dictionary<int,TileBase> getOppositeDictionary() {
		Dictionary<int, TileBase> dict= new Dictionary<int, TileBase>();
		foreach (TileBase x in tiles.Keys){
			dict[tiles[x]] = x;
		}
		return dict;
	}
}
