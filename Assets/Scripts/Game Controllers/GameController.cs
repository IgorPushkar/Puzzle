using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	private GameObject button;
	[SerializeField]
	private Sprite[] backside;
	[SerializeField]
	private GameObject blockPanel, endLevelPanel;
	[SerializeField]
	private Animator endLevelAnim, star1Anim, star2Anim, star3Anim, textAnim;
	[SerializeField]
	private GameObject[] levelPanels;
	private int buttonsCount;
	private List<Sprite[]> puzzleSprites = new List<Sprite[]> ();
	private List<GameObject> buttonsList = new List<GameObject> ();
	private List<Sprite> gameSprites = new List<Sprite>();
	private int level = 0, puzzleType = 0, starsEarned = 0;

	private int openedCount, puzzlesLeft, guessCount;
	private GameObject[] opened = new GameObject[2];

	void LoadSprites(){
		puzzleSprites.Add(Resources.LoadAll<Sprite>("Sprites/Candy"));
		puzzleSprites.Add(Resources.LoadAll<Sprite>("Sprites/Transport"));
		puzzleSprites.Add(Resources.LoadAll<Sprite>("Sprites/Fruits"));
	}

	void PrepareSprites(){
		gameSprites.Clear ();
		gameSprites = new List<Sprite> ();
		List<int> usedSprites = new List<int>();

		for(int i = 0; i < buttonsCount / 2; i++){
			int index = 0;
			while(usedSprites.Contains(index)){
				index = Random.Range (0, puzzleSprites [puzzleType].Length);
			}
			usedSprites.Add(index);
			gameSprites.Add (puzzleSprites[puzzleType][index]);
			gameSprites.Add (puzzleSprites[puzzleType][index]);
		}

		Shuffle ();
	}

	void Shuffle(){
		for(int i = 0; i < gameSprites.Count; i++){
			Sprite temp = gameSprites [i];
			int index = Random.Range (0, gameSprites.Count);
			gameSprites [i] = gameSprites [index];
			gameSprites [index] = temp;
		}
	}

	public void StartLevel(int level){
		this.level = level;
		switch(level){
		case 0:
			buttonsCount = 6;
			break;
		case 1:
			buttonsCount = 12;
			break;
		case 2:
			buttonsCount = 20;
			break;
		case 3:
			buttonsCount = 30;
			break;
		}

		LoadSprites ();
		switch(SelectPuzzle.selectedPuzzle){
		case "Candy Puzzle":
			puzzleType = 0;
			break;
		case "Transport Puzzle":
			puzzleType = 1;
			break;
		case "Fruit Puzzle":
			puzzleType = 2;
			break;
		}
		PrepareSprites ();
		CreateButtons ();
		openedCount = 0;
		puzzlesLeft = buttonsCount;
		opened = new GameObject[2];
		guessCount = 0;
		starsEarned = 0;
	}

	public void EndLevel(){
		HideEndLevelPanel ();
	}

	void CreateButtons(){
		ClearButtons ();
		for(int i = 0; i < buttonsCount; i++){
			GameObject btn = (GameObject)Instantiate (button);
			btn.gameObject.name = "" + i;
			btn.transform.SetParent (levelPanels[level].transform.FindChild ("Buttons Holder").transform, false);
			btn.GetComponent<Image>().sprite = backside [puzzleType];
			btn.GetComponent<Button>().onClick.AddListener (() => Click ());
			buttonsList.Add (btn);
		}
	}

	void ClearButtons(){
		buttonsList.Clear ();
		foreach(Transform button in levelPanels[level].transform.FindChild ("Buttons Holder").transform){
			Destroy (button.gameObject);
		}
	}

	public void Click(){
		if(openedCount < 2){
			int index = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			opened [openedCount] = buttonsList [index];
			opened[openedCount].GetComponent<Animator> ().Play("TurnUp");
			StartCoroutine (TurnCard (opened [openedCount], index));
			openedCount++;
		}
		if (openedCount == 2){
			StartCoroutine (CompareCards ());
		}
	}

	IEnumerator TurnCard(GameObject button, int index){
		yield return new WaitForSeconds (0.5f);
		button.GetComponent<Image>().sprite = gameSprites[index];
	}

	IEnumerator CompareCards(){
		blockPanel.SetActive (true);
		yield return new WaitForSeconds (1f);
		if(opened[0].GetComponent<Image>().sprite.name.Equals(opened[1].GetComponent<Image>().sprite.name)){
			opened[0].GetComponent<Animator> ().Play("FadeOut");
			opened[1].GetComponent<Animator> ().Play("FadeOut");
			puzzlesLeft -= 2;
		} else {
			opened[0].GetComponent<Animator> ().Play("TurnBack");
			opened[1].GetComponent<Animator> ().Play("TurnBack");
			yield return new WaitForSeconds (0.5f);
			opened[0].GetComponent<Image>().sprite = backside [puzzleType];
			opened[1].GetComponent<Image>().sprite = backside [puzzleType];

		}
		opened = new GameObject[2];
		guessCount++;
		openedCount = 0;
		blockPanel.SetActive (false);
		if(puzzlesLeft <= 0){
			ShowEndLevelPanel ();
		}
	}

	void ShowEndLevelPanel(){
		CalculateStarsEarned ();
		UnlockNextLevel ();
		StartCoroutine (ShowPanel ());
	}

	void HideEndLevelPanel(){
		StartCoroutine (HidePanel ());
	}

	IEnumerator HidePanel(){
		endLevelAnim.Play ("FadeOut");
		yield return new WaitForSeconds (1f);
		star3Anim.Play ("FadeOut");
		star2Anim.Play ("FadeOut");
		star1Anim.Play ("FadeOut");
		textAnim.Play ("FadeOut");
		yield return new WaitForSeconds (1f);
		endLevelPanel.SetActive (false);
	}

	IEnumerator ShowPanel (){
		endLevelPanel.SetActive (true);
		endLevelAnim.Play ("FadeIn");
		yield return new WaitForSeconds (1f);
		textAnim.Play ("FadeIn");
		switch(starsEarned){
		case 3:
			star3Anim.Play ("FadeIn");
			star2Anim.Play ("FadeIn");
			star1Anim.Play ("FadeIn");
			break;
		case 2:
			star2Anim.Play ("FadeIn");
			star1Anim.Play ("FadeIn");
			break;
		case 1:
			star1Anim.Play ("FadeIn");
			break;
		}
	}

	void CalculateStarsEarned(){
		switch(level){
		case 0:
			if(guessCount <= 3){
				starsEarned = 3;
			} else if (guessCount <= 5) {
				starsEarned = 2;
			} else if (guessCount <= 7) {
				starsEarned = 1;
			} else {
				starsEarned = 0;
			}
			break;
		case 1:
			if(guessCount <= 9){
				starsEarned = 3;
			} else if (guessCount <= 13) {
				starsEarned = 2;
			} else if (guessCount <= 17) {
				starsEarned = 1;
			} else {
				starsEarned = 0;
			}
			break;
		case 2:
			if(guessCount <= 14){
				starsEarned = 3;
			} else if (guessCount <= 18) {
				starsEarned = 2;
			} else if (guessCount <= 22) {
				starsEarned = 1;
			} else {
				starsEarned = 0;
			}
			break;
		case 3:
			if(guessCount <= 20){
				starsEarned = 3;
			} else if (guessCount <= 25) {
				starsEarned = 2;
			} else if (guessCount <= 30) {
				starsEarned = 1;
			} else {
				starsEarned = 0;
			}
			break;
		}
		if (starsEarned > PlayerPrefsController.GetStars (level)) {
			PlayerPrefsController.SetStars (level, starsEarned);
		}
	}

	void UnlockNextLevel(){
		if(level < 3 && !PlayerPrefsController.GetLevel(level + 1)){
			PlayerPrefsController.SetLevel (level + 1);
		}
	}
}