using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

	public int mapX;
	public int mapY;

	public MapTile[,] mapArray;


  
	public void ReadMap (string filePath)
	{
		// Declare an array to store just the descriptions, and initialize mapArray to store the whole maptiles

		string[,] mapDescriptions = new string[mapX, mapY];
		mapArray = new MapTile[mapX, mapY];

		// Start a streamreader to read each line and split it into pieces, then store each piece in the description array

		StreamReader streamReader = new StreamReader (filePath);
		int row = 0;
		while (!streamReader.EndOfStream) {
			string[] line = streamReader.ReadLine ().Split ('|');
			for (int i = 0; i < mapX; i++) {
				Debug.Log (line [i]);
				mapDescriptions [i, row] = line [i];
			}
			row++;
		}

		// Go through the array of descriptions and copy each into the same index of the mapTile array

		for (int x = 0; x < mapX; x++) {
			for (int y = 0; y < mapY; y++) {
				mapArray[x, y] = new MapTile();
				mapArray[x, y].Description = mapDescriptions[x, y];
			}
		}

		// For testing

		if (mapArray[0,0].Characters == null)
		{
			mapArray[0, 0].Characters = new List<Character>();
		}
		mapArray[0,0].Characters.Add((Character)AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/Data/Characters/Sundar/character.asset"));
	}
}

public class MapTile {
	
	private string description;
	private List<Character> characters;

	public string Description { get; set; }
	public List<Character> Characters { get; set; } 
}
	