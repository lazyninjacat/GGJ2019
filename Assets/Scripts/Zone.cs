using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    [SerializeField] private QuoteGeneratorController generator;
    [SerializeField] private VolumeCapture volume;
    [SerializeField] private BallController balls;
    [SerializeField] private GameObject jackassMessage;
    [SerializeField] private GameObject UiCanvas;
    [SerializeField] private GameObject BarCanvas;
    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject LoseText;

    public float maxPower = 1;

    private const float SECONDS = 2f;
    private const float FLASH_SECONDS = 0.5f;
    private const float MODIFIER = 0.75f;
    private const float BACK = -5f;
    private const float JUMP = 20f;

    private bool jackassTriggered = false;
    //private bool inJackass = false;
    private int randoNum;
    private string toonName = "";
    private System.Random rand;

    private void Start()
    {
        rand = new System.Random();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rando")
        {
            randoNum = rand.Next(0, 5);
            generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.ONE, true);
        }
        else if (collision.gameObject.tag == "jackass")
        {
            StartCoroutine("FlashJackAss");
        }
        else if (collision.gameObject.tag == "winzone")
        {
            UiCanvas.SetActive(false);
            BarCanvas.SetActive(false);
            WinText.SetActive(true);
            WinCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jackass")
        {
            if (jackassTriggered)
            {
                //generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.TWO, false);
                StartCoroutine("HideJackAssAfterSeconds");
                jackassTriggered = false;
            }
            else
            {
                randoNum = rand.Next(0, 4);
                generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.ONE, true);
                StartCoroutine("HideBubbleAfterSeconds");
            }

            StopCoroutine("FlashJackAss");
            jackassMessage.SetActive(false);
        }
        else if (collision.gameObject.tag == "rando")
        {
            generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.ONE, false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jackass")
        {
            if (Input.GetKeyDown(KeyCode.K) && !jackassTriggered)
            //if (!jackassTriggered)
            {
                if (balls.currentSpeed >= maxPower)
                {
                    StopCoroutine("FlashJackAss");
                    jackassMessage.SetActive(false);
                    randoNum = rand.Next(6, 9);
                    generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.TWO, true);
                    jackassTriggered = true;
                    // FIRE BACKWARDS    
                    balls.LaunchBall(maxPower, BACK, JUMP);
                }
            }
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && inJackass)
        {
            randoNum = rand.Next(6, 9);
            generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.TWO, true);
            jackassTriggered = true;
            // FIRE BACKWARDS
        }
    */

    private IEnumerator HideBubbleAfterSeconds()
    {
        yield return new WaitForSeconds(SECONDS);
        generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.ONE, false);
        StopCoroutine("HideBubbleAfterSeconds");
    }

    private IEnumerator FlashJackAss()
    {
        while (true)
        {
            yield return new WaitForSeconds(FLASH_SECONDS);
            jackassMessage.SetActive(!jackassMessage.activeSelf);
        }
    }

    private IEnumerator HideJackAssAfterSeconds()
    {
        yield return new WaitForSeconds(SECONDS);
        generator.QuoteBubbleStateByNum(randoNum, QuoteGeneratorController.Players.TWO, false);
        //StopCoroutine("HideBubbleAfterSeconds");
    }
}
