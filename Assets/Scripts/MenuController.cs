using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	[SerializeField] private AudioSource player;
	[SerializeField] private AudioClip goHomeClip;
	[SerializeField] private AudioClip golfShot;

	[Space]

	[SerializeField] private RectTransform ball;
	private float interval = 5.0f;

	private float shotClipSize;
	private float homeClipSize;

	[SerializeField] private GameObject startButton;
	// Use this for initialization
	void Start () {
		Debug.Log("Menu Script Started");
		
		StartCoroutine("PlayIntro");
	}
	
	IEnumerator PlayIntro(){
		Debug.Log("Intro co routine started");
		player.clip = golfShot;
		player.Play();
		yield return new WaitForSeconds(1);
		StartCoroutine("AnimateBall");
		yield return new WaitForSeconds(golfShot.length - 2.5f);
		player.clip = goHomeClip;
		player.Play();
		yield return new WaitForSeconds(goHomeClip.length);
		startButton.SetActive(true);
	}

     IEnumerator AnimateBall()
     {
		 Vector2 targetPos = new Vector2(324, 143);
         float step = 0;
         while (step < 1)
         {
             ball.offsetMin = Vector2.Lerp(ball.offsetMin, targetPos, step += Time.deltaTime);
             ball.offsetMax = Vector2.Lerp(ball.offsetMax, targetPos, step += Time.deltaTime);
             yield return new WaitForSeconds(0.1f);
         }
     }

	public void StartGame(){
		SceneManager.LoadScene("SampleScene");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
