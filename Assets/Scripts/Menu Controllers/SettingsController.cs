using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsController : MonoBehaviour {

	[SerializeField]
	private GameObject settingsPanel;
	[SerializeField]
	private Animator settingsPanelAnimator;

	public void OpenSettingsPanel(){
		MusicManager.instance.UpdateSlider ();
		settingsPanel.SetActive (true);
		settingsPanelAnimator.Play ("SlideIn");
	}

	public void CloseSettingsPanel(){
		if(settingsPanel.activeInHierarchy){
			StartCoroutine (CloseSettings ());
		}
	}

	IEnumerator CloseSettings(){
		MusicManager.instance.SaveVolume ();
		settingsPanelAnimator.Play ("SlideOut");
		yield return new WaitForSeconds(1f);
		settingsPanel.SetActive (false);
	}

	public void Reset(){
		PlayerPrefsController.ResetAllKeys ();
	}
}
