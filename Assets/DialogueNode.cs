using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "node", menuName = "Dialogue Node")]
public class DialogueNode : ScriptableObject {

	public bool isExitDialogue;
	public string dialogueText;
	public List<DialogueResponse> dialogueResponses;

}
