using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningSequenceScript : MonoBehaviour {
	List<string> dialog;
	List<string> speaker;

	public Sprite dialogBoxOrange;
	public Sprite dialogBoxGreen;

	Sprite showingBox;

	public Sprite sequenceImage1;
	public Sprite sequenceImage2;

	Sprite currentImage;

	Image render;

	Text textThing;

	public enum sequenceStep
	{
		image1,
		image2,
		dialog
	}

	GameObject openingText;

	sequenceStep currentStep;


	float timer;

	int currentDialog = 0;

	// Use this for initialization
	void Start () {
		render = GetComponent<Image>();


		openingText = GameObject.Find("OpeningText");
		textThing = openingText.GetComponent<Text>();
		textThing.text = "";

		dialog = new List<string>();
		speaker = new List<string>();

		currentStep = sequenceStep.image1;

		currentImage = sequenceImage1;

		render.sprite = currentImage;

		addDialog("What was that!?", "other");
		addDialog("I don't know","player");
		addDialog("Looks like we've lost pressure","other");
		addDialog("You'll need to get to the shuttle ","other");
        addDialog("in the rear of the ship.", "other");
        addDialog("And come pick me up","other");
		addDialog("Why me?","player");
		addDialog("I'm just an accountant","player");
		addDialog("You're the only one who can","other");
		addDialog("survive the vacuum of space","other");
		addDialog("You can use a magnetic boot","other");
		addDialog("to get around","other");
		addDialog("Press Left and Right to rotate your body","other");
		addDialog("and UP to jump","other");
		addDialog("Press A and D to rotate your arms","other");
		addDialog("and W to push yourself off of ","other");
        addDialog("anything you're touching.", "other");
        addDialog("But you really ought to know","other");
		addDialog("how to work your arms by now","other");
        addDialog("And remember, if something goes wrong", "other");
        addDialog("you can always press ESC to respawn", "other");
        addDialog("at your most recent checkpoint.", "other");
        addDialog("Good Luck!","other");

		timer = 0;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		switch (currentStep)
		{
			case sequenceStep.image1:
				if (timer >= 5)
				{
					currentImage = sequenceImage2;
					currentStep = sequenceStep.image2;
					timer = 0;
				}
				break;
			case sequenceStep.image2:
				if (timer >= 5)
				{
					currentImage = dialogBoxOrange;
					currentStep = sequenceStep.dialog;
					timer = 0;
					displayDialog(currentDialog);
				}
				break;
			case sequenceStep.dialog:
			if (timer >= 3)
			{
				timer = 0;
				currentDialog++;
				Debug.Log("dialog count: " + dialog.Count);
				if (currentDialog > dialog.Count - 1)
				{
					quitOpening();
					break;
				}
				displayDialog(currentDialog);

			}

				break;
		}
		render.sprite = currentImage;
	}

	void addDialog(string words, string talker)
	{
		dialog.Add(words);
		speaker.Add(talker);
	}

	void displayDialog(int index)
	{
		string textToShow = dialog[index];
		if (speaker[index] == "other")
		{
			showingBox = dialogBoxGreen;
		} else {
			showingBox = dialogBoxOrange;
		}
		currentImage = showingBox;
		textThing.text = textToShow;

	}

	void quitOpening()
	{
        SceneManager.LoadScene(2);
	}


}
