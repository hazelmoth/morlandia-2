using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
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
		for (int x = 0; x < mapX; x++) {
			for (int y = 0; y < mapY; y++) {
				mapArray[x, y] = new MapTile(); // Initialize every map tile in preparation for storing things in them
			}
		}

		// Start a streamreader to read each line and split it into pieces, then store each piece in the description array.
		// Each line in the map file is a row (y), and each section of the row, divided by '|', is an x.
		// We're using mapDescriptions to temporarily grab the descriptions from the map file, before we copy them into the final map array (mapArray).

		StreamReader streamReader = new StreamReader (filePath);
		int row = 0;
		while (!streamReader.EndOfStream) {
			string[] line = streamReader.ReadLine ().Split ('|');

			for (int i = 0; i < mapX; i++) {	// Loop through all the descriptions on a line; we're assuming there will be the same number as mapX
				Debug.Log (line [i]);

				if (mapArray[i, row].Characters == null) {
					mapArray[i, row].Characters = new List<Character>(); // Create a list of characters on the current tile if there isn't one
				}


				// Check if the current description contains the '@' character, and if it does, use Regex to find each name following an @ character.

				if (line[i].Contains('@'))
				{
					foreach (Match matchedCharacterName in Regex.Matches(line[i], "(?<=@)[\\w ]+"))
					{
						string nameString = matchedCharacterName.ToString();
						nameString.Trim();
						mapArray[i, row].Characters.Add((Character)AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/Data/Characters/" + nameString.Trim() + "/character.asset"));
					}
					mapDescriptions[i, row] = line[i].Substring(0, line[i].IndexOf('@')).Trim(); // Add the description, excluding the names, to the mapDescriptions array.
				}

				// If there are no @ characters, just copy the string into mapDescriptions.

				else 
				{
					mapDescriptions[i, row] = line[i].Trim();
				}
			}
			row++;
		}

		// Go through the array of descriptions and copy each into the same index of the mapTile array

		for (int x = 0; x < mapX; x++) {
			for (int y = 0; y < mapY; y++) {
				mapArray[x, y].Description = mapDescriptions[x, y];
			}
		}
	}
}

public class MapTile {
	
	private string description;
	private List<Character> characters;

	public string Description { get; set; }
	public List<Character> Characters { get; set; } 
}
	