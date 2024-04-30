using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
