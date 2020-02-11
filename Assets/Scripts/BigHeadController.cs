using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigHeadController : MonoBehaviour {

	[SerializeField] private GameObject bighead;

	[SerializeField] private RawImage headImg;

	[SerializeField] private float xOffset = -150.0f;
	[Space]
	[SerializeField] private GameObject quoteBubble;
	[SerializeField] private Text bubbleText;
	[SerializeField] private QuoteGeneratorController quoteGenerator;
	[SerializeField] private float changeQuoteSeconds;

	private const int intervals = 15;

	private Transform originalPosition;

	private bool hidden = true;

	// Use this for initialization
	void Start () {
		originalPosition = this.transform;
		hidden = true;
	}
	
	public void ShowHead(){
		if (hidden == true){
			for (int i = 0; i < intervals; i++){
				this.transform.localPosition = new Vector3((this.transform.localPosition.x + Math.Abs(intervals)), this.transform.localPosition.y, this.transform.localPosition.z);
			}
			hidden = false;

			quoteBubble.SetActive(true);
			StartCoroutine("RotateQuotes");
		}
	}

	public void HideHead(){
		if (hidden == false){
			for (int i = 0; i < intervals; i++){
				this.transform.localPosition = new Vector3((this.transform.localPosition.x + (intervals * -1)), this.transform.localPosition.y, this.transform.localPosition.z);
			}
			hidden = true;
			StopCoroutine("RotateQuotes");
			quoteBubble.SetActive(false);

		}
	}

	IEnumerator RotateQuotes(){
		while(true){
			bubbleText.text = quoteGenerator.happyQuotes[UnityEngine.Random.Range(0, quoteGenerator.happyQuotes.Count)];
			yield return new WaitForSeconds(changeQuoteSeconds);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H)){
			ShowHead();
		}
		if (Input.GetKeyDown(KeyCode.J)){
			HideHead();
		}
	}
}
