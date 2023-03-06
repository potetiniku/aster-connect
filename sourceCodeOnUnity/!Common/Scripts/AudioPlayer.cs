using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Audio Player")]
public class AudioPlayer : ScriptableObject
{
	public void PlayOneShot(AudioClip clip) =>
		AudioSource?.PlayOneShot(clip);

	public AudioSource AudioSource { get; set; }
}