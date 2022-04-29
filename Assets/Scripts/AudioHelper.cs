using System.Collections;
using UnityEngine;

public static class AudioHelper
{

	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, float volume)
	{
		float startVolume = audioSource.volume;
		while (audioSource.volume > volume)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float volume)
	{
		audioSource.Play();
		audioSource.volume = 0f;
		while (audioSource.volume < volume)
		{
			audioSource.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}

}