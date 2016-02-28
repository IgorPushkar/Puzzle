using UnityEngine;
using UnityEngine;
using System.Collections;

public static class PlayerPrefsController{

	private const string LEVEL_1_KEY = "LEVEL_1";
	private const string LEVEL_2_KEY = "LEVEL_2";
	private const string LEVEL_3_KEY = "LEVEL_3";
	private const string LEVEL_4_KEY = "LEVEL_4";

	private const string LEVEL_1_STAR_KEY = "LEVEL_1_STAR";
	private const string LEVEL_2_STAR_KEY = "LEVEL_2_STAR";
	private const string LEVEL_3_STAR_KEY = "LEVEL_3_STAR";
	private const string LEVEL_4_STAR_KEY = "LEVEL_4_STAR";

	private const string MUSIC_KEY = "MUSIC";

	#region LEVELS
	public static void SetLevel(int level){
		switch(level){
		case 0:
			PlayerPrefs.SetInt (LEVEL_1_KEY, 1);
			break;
		case 1:
			PlayerPrefs.SetInt (LEVEL_2_KEY, 1);
			break;
		case 2:
			PlayerPrefs.SetInt (LEVEL_3_KEY, 1);
			break;
		case 3:
			PlayerPrefs.SetInt (LEVEL_4_KEY, 1);
			break;
		default:
			Debug.LogWarning ("Level index in wrong format or invalid");
			break;
		}
	}

	public static bool GetLevel(int level){
		switch(level){
		case 0:
			if(!PlayerPrefs.HasKey(LEVEL_1_KEY)){
				SetLevel (0);
			}
			return true;
		case 1:
			if(!PlayerPrefs.HasKey(LEVEL_2_KEY)){
				return false;
			} else {
				return true;
			}
		case 2:
			if(!PlayerPrefs.HasKey(LEVEL_3_KEY)){
				return false;
			} else {
				return true;
			}
		case 3:
			if(!PlayerPrefs.HasKey(LEVEL_4_KEY)){
				return false;
			} else {
				return true;
			}
		default:
			Debug.LogWarning ("Level index in wrong format or invalid");
			break;
		}
		return false;
	}
	#endregion

	#region STARS
	public static void SetStars(int level, int stars){
		switch(level){
		case 0:
			PlayerPrefs.SetInt (LEVEL_1_STAR_KEY, stars);
			break;
		case 1:
			PlayerPrefs.SetInt (LEVEL_2_STAR_KEY, stars);
			break;
		case 2:
			PlayerPrefs.SetInt (LEVEL_3_STAR_KEY, stars);
			break;
		case 3:
			PlayerPrefs.SetInt (LEVEL_4_STAR_KEY, stars);
			break;
		default:
			Debug.LogWarning ("Level index in wrong format or invalid");
			break;
		}
	}

	public static int GetStars(int level){
		switch(level){
		case 0:
			if(PlayerPrefs.HasKey(LEVEL_1_STAR_KEY)){
				return PlayerPrefs.GetInt (LEVEL_1_STAR_KEY);
			} else {
				return 0;
			}
		case 1:
			if(PlayerPrefs.HasKey(LEVEL_2_STAR_KEY)){
				return PlayerPrefs.GetInt (LEVEL_2_STAR_KEY);
			} else {
				return 0;
			}
		case 2:
			if(PlayerPrefs.HasKey(LEVEL_3_STAR_KEY)){
				return PlayerPrefs.GetInt (LEVEL_3_STAR_KEY);
			} else {
				return 0;
			}
		case 3:
			if(PlayerPrefs.HasKey(LEVEL_4_STAR_KEY)){
				return PlayerPrefs.GetInt (LEVEL_4_STAR_KEY);
			} else {
				return 0;
			}
		default:
			Debug.LogWarning ("Level index in wrong format or invalid");
			break;
		}
		return 0;
	}
	#endregion

	#region MUSIC
	public static void SetMusic(float music){
		if(music >=0 || music <= 1){
			PlayerPrefs.SetFloat (MUSIC_KEY, music);
		} else {
			Debug.LogWarning ("Music volume in wrong format or invalid");
		}
	}

	public static float GetMusic(){
		if(PlayerPrefs.HasKey(MUSIC_KEY)){
			return PlayerPrefs.GetFloat (MUSIC_KEY);
		} else {
			Debug.LogWarning ("Music volume in wrong format or invalid");
			return 0.7f;
		}
	}
	#endregion
}
