using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButtonController : MonoBehaviour {

	private int level;
	[SerializeField]
	private GameObject[] stars;
	[SerializeField]
	private GameObject starsHolder, padlock;
	[SerializeField]
	private Button button;

	void Start () {
		level = int.Parse(gameObject.name);
		InitializeButton ();
	}

	void InitializeButton(){
		button.interactable = false;
		stars [0].SetActive (false);
		stars [1].SetActive (false);
		stars [2].SetActive (false);
		starsHolder.SetActive (false);
		padlock.SetActive (true);
		if (PlayerPrefsController.GetLevel (level)) {
			button.interactable = PlayerPrefsController.GetLevel (level);
			starsHolder.SetActive (button.interactable);
			padlock.SetActive (!button.interactable);
			switch (PlayerPrefsController.GetStars (level)) {
			case 1:
				stars [0].SetActive (true);
				break;
			case 2:
				stars [0].SetActive (true);
				stars [1].SetActive (true);
				break;
			case 3:
				stars [0].SetActive (true);
				stars [1].SetActive (true);
				stars [2].SetActive (true);
				break;
			}
		}
	}

	public void ReinitializeButton(){
		InitializeButton ();
	}
}
