using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

	[SerializeField]
	private AudioClip clickSound;
	[SerializeField]
	private AudioSource source;
	[SerializeField]
	private Slider volumeSlider;

	void Awake(){
		MakeSingleton ();
	}

	void MakeSingleton(){
		if(instance != null){
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
		
	void Start () {
		source.volume = PlayerPrefsController.GetMusic ();
		source.Play ();
	}

	public void UpdateVolume () {
		source.volume = volumeSlider.value;
	}

	public void UpdateSlider (){
		volumeSlider.value = source.volume;
	}

	public void SaveVolume(){
		PlayerPrefsController.SetMusic (volumeSlider.value);
	}

	public void PlayClickSound(){
		source.PlayOneShot (clickSound);
	}
}
