using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
	public AudioSource exitAudio;
	public AudioSource caughtAudio;
	bool hasAudioPlayed;

	public float fadeDuration = 1.0f;
	public float displayImageDuration = 1.0f;

    public GameObject player;
	public CanvasGroup exitBackGroundImageCanvasGroup;
	public CanvasGroup caughtBackGroundImageCanvasGroup;

	bool isPlayerAtExit = false;
	bool isPlayerCaught = false;

	float timer = 0.0f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			isPlayerAtExit = true;
		}
	}

	private void Update()
	{
		if (isPlayerAtExit)
		{
			EndLevel(exitBackGroundImageCanvasGroup,false,exitAudio);	
		}
		else if(isPlayerCaught)
		{
			EndLevel(caughtBackGroundImageCanvasGroup,true,caughtAudio);
		}
	}

	/// <summary>
	/// Lanza imagen de fin de partida y finaliza el juego
	/// </summary>
	/// <param name="imageCanvasGroup">Imagen correspondiente</param>
	private void EndLevel(CanvasGroup imageCanvasGroup,bool doRestart,AudioSource audioSource)
	{
		if (!hasAudioPlayed)
		{
			audioSource.Play();
			hasAudioPlayed = true;
		}

		timer += Time.deltaTime;
		//si la transparencia es >1 no pasa nada , se queda en 1 para unity
		imageCanvasGroup.alpha += Mathf.Clamp(timer / fadeDuration, 0, 1);

		//un segundo a visible + 1s en pantalla
		if (timer > fadeDuration + displayImageDuration)
		{
			if (doRestart)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			else
			{
				//cierra el juego
				//solo funciona en builds
				//en el editor no pasará nada
				Debug.Log("Quit");
				Application.Quit();
			}
		}		
	}

	public void CatchPlayer()
	{
		isPlayerCaught = true;
	}
}
