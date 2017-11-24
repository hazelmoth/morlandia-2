using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	[SerializeField] private GameObject mainUI;
	[SerializeField] private GameObject dialogueUI;

	[SerializeField] private Text descriptionText;
	[SerializeField] private InputField inputField;

	[SerializeField] private Text characterNameText;
	[SerializeField] private Text characterDialogueText;
	[SerializeField] private Button dialogueButton1;
	[SerializeField] private Button dialogueButton2;
	[SerializeField] private Button dialogueButton3;
	[SerializeField] private Text dialogueButtonText1;
	[SerializeField] private Text dialogueButtonText2;
	[SerializeField] private Text dialogueButtonText3;

	private UIState currentUIState = UIState.Main;

	public enum UIState
	{
		Main,
		Dialogue
	}

	void Start () {
		instance = this;
	}
	
	public void SwitchToMainUI () 
	{
		currentUIState = UIState.Main;

		mainUI.SetActive(true);
		dialogueUI.SetActive(false);
	}

	public void SwitchToDialogueUI () 
	{
		currentUIState = UIState.Dialogue;

		mainUI.SetActive(false);
		dialogueUI.SetActive(true);
	}

	public UIState CurrentUIState 
	{
		get { return currentUIState; }
	}
}

