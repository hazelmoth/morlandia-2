using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour {

	public GameController gameController;

	public void ActivateDialogue(Character characterEnteringDialogue)
	{
		gameController.currentState = "dialogue";

		gameController.mainUI.SetActive(false);
		gameController.dialogueUI.SetActive(true);

		gameController.currentCharacterInDialogue = characterEnteringDialogue;
		gameController.currentDialogueNode = characterEnteringDialogue.conversation[0];

		UpdateDialogue();
	}

	public void ExitDialogue()
	{
		gameController.currentState = "main";

		gameController.mainUI.SetActive(true);
		gameController.dialogueUI.SetActive(false);

		gameController.currentCharacterInDialogue = null;
		gameController.currentDialogueNode = null;
	}

	public void UpdateDialogue()
	{
		gameController.characterNameText.text = gameController.currentCharacterInDialogue.characterName;
		gameController.characterDialogueText.text = gameController.currentDialogueNode.dialogueText;


		// Set buttons as active and set their text depending on how many dialogue responses are available.
		// (Hopefully never more than 3; there are only 3 buttons.)


		if (gameController.currentDialogueNode.dialogueResponses.Count == 3)
			gameController.dialogueButtonText1.text = gameController.currentDialogueNode.dialogueResponses[0].responseText;

		if (gameController.currentDialogueNode.dialogueResponses.Count >= 2)
			gameController.dialogueButtonText2.text = gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 2].responseText;
		
		if (gameController.currentDialogueNode.dialogueResponses.Count >= 1)
			gameController.dialogueButtonText3.text = gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 1].responseText;


		gameController.dialogueButton1.gameObject.SetActive(gameController.currentDialogueNode.dialogueResponses.Count == 3);
		gameController.dialogueButton2.gameObject.SetActive(gameController.currentDialogueNode.dialogueResponses.Count >= 2);
		gameController.dialogueButton3.gameObject.SetActive(gameController.currentDialogueNode.dialogueResponses.Count >= 1);


	}


	public void onDialogueButton1()
	{
		EventSystem.current.SetSelectedGameObject(null); // Stop the button from being highlighted after it is pressed

		if (gameController.currentDialogueNode.dialogueResponses[0].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			gameController.currentDialogueNode = gameController.currentDialogueNode.dialogueResponses[0].dialogueLink;
			UpdateDialogue();
		}
	}

	public void onDialogueButton2()
	{
		EventSystem.current.SetSelectedGameObject(null);

		if (gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 2].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			gameController.currentDialogueNode = gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 2].dialogueLink;
			UpdateDialogue();
		}
	}

	public void onDialogueButton3()
	{
		EventSystem.current.SetSelectedGameObject(null);

		if (gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 1].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			gameController.currentDialogueNode = gameController.currentDialogueNode.dialogueResponses[gameController.currentDialogueNode.dialogueResponses.Count - 1].dialogueLink;
			UpdateDialogue();
		}
	}
}
