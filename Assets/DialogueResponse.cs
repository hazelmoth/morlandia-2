using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "response", menuName = "Dialogue Response")]
public class DialogueResponse : ScriptableObject {

	public bool isExitResponse;
	public string responseText;
	public DialogueNode dialogueLink;

}
