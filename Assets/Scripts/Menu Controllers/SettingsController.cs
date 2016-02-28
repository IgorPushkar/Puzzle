using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour {

	[SerializeField]
	private GameObject settingsPanel;
	[SerializeField]
	private Animator settingsPanelAnimator;

	public void OpenSettingsPanel(){
		settingsPanel.SetActive (true);
		settingsPanelAnimator.Play ("SlideIn");
	}

	public void CloseSettingsPanel(){
		if(settingsPanel.activeInHierarchy){
			StartCoroutine (CloseSettings ());
		}
	}

	IEnumerator CloseSettings(){
		settingsPanelAnimator.Play ("SlideOut");
		yield return new WaitForSeconds(1f);
		settingsPanel.SetActive (false);
	}
}
