using UnityEngine;
using System.Collections;

public class SelectPuzzle : MonoBehaviour {

	[SerializeField]
	private GameObject puzzleSelectPanel, levelSelectPanel, blockPanel;
	[SerializeField]
	private GameObject[] gameplayPanels;
	[SerializeField]
	private GameObject[] levelButtons;
	[SerializeField]
	private Animator[] gameplayAnimators;
	private int currentLevel;
	[SerializeField]
	private Animator puzzleSelectAnimator, levelSelectAnimator;
	public static string selectedPuzzle;

	public void ToLevelSelect(string puzzle){
		selectedPuzzle = puzzle;
		StartCoroutine (PuzzleToLevelSelect ());
	}

	public void ToPuzzleSelect(){
		StartCoroutine (LevelToPuzzleSelect ());
	}

	public void ToGameplay(int level){
		currentLevel = level;
		StartCoroutine (LevelToGameplay ());
	}

	public void BackToLevelSelect(){
		StartCoroutine (GameplayToLevel ());
	}

	IEnumerator PuzzleToLevelSelect(){
		blockPanel.SetActive (true);
		levelSelectPanel.SetActive (true);
		foreach(GameObject obj in levelButtons){
			obj.GetComponent<LevelButtonController> ().ReinitializeButton ();
		}
		puzzleSelectAnimator.Play ("SlideIn2");
		yield return new WaitForSeconds (0.5f);
		levelSelectAnimator.Play ("SlideIn");
		yield return new WaitForSeconds (0.5f);
		puzzleSelectPanel.SetActive (false);
		blockPanel.SetActive (false);
	}

	IEnumerator LevelToPuzzleSelect(){
		blockPanel.SetActive (true);
		puzzleSelectPanel.SetActive (true);
		levelSelectAnimator.Play ("SlideOut");
		yield return new WaitForSeconds (0.5f);
		puzzleSelectAnimator.Play ("SlideOut2");
		yield return new WaitForSeconds (0.5f);
		levelSelectPanel.SetActive (false);
		blockPanel.SetActive (false);
	}

	IEnumerator LevelToGameplay(){
		blockPanel.SetActive (true);
		gameplayPanels[currentLevel].SetActive (true);
		GameObject.Find("Game Controller").GetComponent<GameController> ().StartLevel (currentLevel);
		levelSelectAnimator.Play ("SlideIn2");
		yield return new WaitForSeconds (0.5f);
		gameplayAnimators[currentLevel].Play ("SlideIn");
		yield return new WaitForSeconds (0.5f);
		levelSelectPanel.SetActive (false);
		blockPanel.SetActive (false);
	}

	IEnumerator GameplayToLevel(){
		GameObject.Find("Game Controller").GetComponent<GameController> ().EndLevel();
		foreach(GameObject obj in levelButtons){
			obj.GetComponent<LevelButtonController> ().ReinitializeButton ();
		}
		blockPanel.SetActive (true);
		yield return new WaitForSeconds (0.25f);
		levelSelectPanel.SetActive (true);
		gameplayAnimators[currentLevel].Play ("SlideOut");
		yield return new WaitForSeconds (0.5f);
		levelSelectAnimator.Play ("SlideOut2");
		yield return new WaitForSeconds (0.5f);
		gameplayPanels[currentLevel].SetActive (false);
		blockPanel.SetActive (false);
	}
}
