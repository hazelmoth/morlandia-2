using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public GameObject mainUI;
	public GameObject dialogueUI;

	public DataController dataController;
	public DialogueController dialogueController;
	public Text descriptionText;
	public InputField inputField;

	public Text characterNameText;
	public Text characterDialogueText;
	public Button dialogueButton1;
	public Button dialogueButton2;
	public Button dialogueButton3;
	public Text dialogueButtonText1;
	public Text dialogueButtonText2;
	public Text dialogueButtonText3;

	private int x, y;

	string currentSecondaryText;
	string previousCommand;

	public Character currentCharacterInDialogue;
	public DialogueNode currentDialogueNode;

	public string currentState = "main";

	void Start () {
		instance = this;

		x = 0;
		y = 0;

		dataController.ReadMap("Assets/testmap.csv");

		dialogueController.ExitDialogue();     // To ensure that the proper canvas is active
		UpdateDescription();
	}


	void Update ()
	{
		if (currentState == "main")
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) == true)
			{
				inputField.text = previousCommand;
			}
			
			if (Input.GetKeyDown(KeyCode.Return) == true)
			{
				ActivateTurn();
			}
		}
		else if (currentState == "dialogue")
		{
			// We don't currently need to do anything in update if we're in dialogue; it's all handled by DialogueController
		}
		else 
		{
			Debug.Log("Current state string is invalid");
		}
	}

	void UpdateDescription () {
		descriptionText.text = dataController.mapArray[x, y].Description;
		if (dataController.mapArray[x,y].Characters != null)
		{
			foreach (Character c in dataController.mapArray[x,y].Characters)
			{
				descriptionText.text = descriptionText.text + " " + c.characterName + " is nearby.";
			}
		}

		if (currentSecondaryText != null) {
			descriptionText.text = descriptionText.text + "\n\n" + currentSecondaryText;
			currentSecondaryText = null;
		}
	}

	void ActivateTurn ()
	{
		inputField.text = inputField.text.Trim();
		string commandOriginal = inputField.text;
		string commandLowered = commandOriginal.ToLower();

		previousCommand = commandOriginal;

		if (commandLowered.StartsWith("talk to ") || 
			commandLowered.StartsWith("talk with ") || 
			commandLowered.StartsWith("speak to ") || 
			commandLowered.StartsWith("speak with "))
		{
			// Cut the name of the person out of the command
			string nameOfPersonBeingTalkedTo = "";

			if (commandLowered.StartsWith("talk to "))
				nameOfPersonBeingTalkedTo = commandOriginal.Substring(8);
			else if (commandLowered.StartsWith("talk with "))
				nameOfPersonBeingTalkedTo = commandOriginal.Substring(10);
			else if (commandLowered.StartsWith("speak to "))
				nameOfPersonBeingTalkedTo = commandOriginal.Substring(9);
			else if (commandLowered.StartsWith("speak with "))
				nameOfPersonBeingTalkedTo = commandOriginal.Substring(11);
			
			Debug.Log(nameOfPersonBeingTalkedTo);

			if (dataController.mapArray[x,y].Characters != null)
			{
				bool foundCharacter = false;
				if (dataController.mapArray[x, y].Characters.Count == 0)
				{
					currentSecondaryText = "No one named \"" + nameOfPersonBeingTalkedTo + "\" is around.";
					Debug.Log("List count is zero.");
				}
				else
				{
					foreach (Character characterInMaptile in dataController.mapArray[x,y].Characters)
					{
						if (nameOfPersonBeingTalkedTo.ToLower() == characterInMaptile.characterName.ToLower())
						{
							foundCharacter = true;
							Debug.Log("found character!");
							dialogueController.ActivateDialogue(characterInMaptile);
						}
						if (foundCharacter == true)
							break;
					}
					if (foundCharacter == false)
					{
						currentSecondaryText = "No one named \"" + nameOfPersonBeingTalkedTo + "\" is around.";
						Debug.Log("Character not found in list.");
					}
				}
			}
			else
			{
				currentSecondaryText = "No one named \"" + nameOfPersonBeingTalkedTo + "\" is around.";
				Debug.Log("Maptile character list is null");
			}
		}

		else if (commandLowered.StartsWith("say "))
		{
			string thingBeingSaid = commandOriginal.Substring(4);
			currentSecondaryText = "\"" + thingBeingSaid + ",\" you say loudly.";
		}

		else
		{
			switch (commandLowered)
			{

				case "":
					break;
				case "north":
				case "go north":
				case "n":
					if (y == 0)
					{
						currentSecondaryText = "You can travel no further north.";
						break;
					}
					y--;
					break;
				case "south":
				case "go south":
				case "s":
					if (y == dataController.mapY - 1)
					{
						currentSecondaryText = "You can travel no further south.";
						break;
					}
					y++;
					break;
				case "west":
				case "go west":
				case "w":
					if (x == 0)
					{
						currentSecondaryText = "You can travel no further west.";
						break;
					}
					x--;
					break;
				case "east":
				case "go east":
				case "e":
					if (x == dataController.mapX - 1)
					{
						currentSecondaryText = "You can travel no further east.";
						break;
					}
					x++;
					break;
				default:
					currentSecondaryText = "You don't know how to " + commandLowered + ".";
					break;
			}
		}

		UpdateDescription();
        inputField.text = "";
		inputField.ActivateInputField();
	}



}

