using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateCharacter {
	public static Character Create(string name)
    {
        Character asset = ScriptableObject.CreateInstance<Character>();

		asset.characterName = name;

		AssetDatabase.CreateAsset(asset, "Assets/Characters/" + name + ".asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}