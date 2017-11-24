using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "character", menuName = "Character")]
public class Character : ScriptableObject {
	
	public string characterName;
	public List<DialogueNode> conversation = new List<DialogueNode>();
}
