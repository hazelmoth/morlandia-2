using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow {

	string characterName;
	private string nodeText;
	private ScriptableObject loadedCharacter;

	[MenuItem("Window/Character Editor")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(CharacterEditor));
	}

	void OnGUI()
	{
		GUILayout.Label("Character Editor", EditorStyles.boldLabel);

		characterName = EditorGUILayout.TextField("Name", characterName);

		GUILayout.BeginHorizontal();

		if(GUILayout.Button("Load Character"))
		{
			// Check if the asset with the entered name exists
			if(AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/Characters/" + characterName + ".asset") != null)
			{
				loadedCharacter = AssetDatabase.LoadAssetAtPath<ScriptableObject>("Assets/Characters/" + characterName + ".asset");
				Debug.Log("\'" + characterName + ".asset\' loaded.");

				characterName = loadedCharacter.name;
			}
			else
			{
				Debug.Log("Asset not found!");
			}
		}

		if(GUILayout.Button("Save Character"))
		{
			CreateCharacter.Create(characterName);
		}

		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		GUILayout.Label("Conversation", EditorStyles.boldLabel);

		nodeText = EditorGUILayout.TextField("Text", nodeText);

		if (GUILayout.Button("Create Node"))
		{
			CreateDialogueNode.Create(characterName, nodeText);
		}
	}
}
