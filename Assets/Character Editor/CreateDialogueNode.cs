using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateDialogueNode {
	public static DialogueNode Create(string name, string dialogue)
	{
		DialogueNode asset = ScriptableObject.CreateInstance<DialogueNode>();

		asset.dialogueText = dialogue;

		AssetDatabase.CreateAsset(asset, "Assets/Characters/" + name + "/node.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}
