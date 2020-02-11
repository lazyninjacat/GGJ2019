using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour {

	enum Events{
		LAUNCH_FORWARD,
		LAUNCH_BACKWARDS,
	}

	[SerializeField] private AudioClip theme;

	private SpriteRenderer ball;

	[HideInInspector] public bool facingRight = true;

    public float maxDb = 90f;
    public float minDb = 45f;
	public float moveForce = 365f;//365f;
	public float maxSpeed = 40f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
    public float currentSpeed = 0;

	private bool grounded = false;
	private Rigidbody2D rb2d;

	private VolumeCapture micScript;
	public bool micStarted = false;
	private bool p1Disabled = false;
	private bool p2Disabled = false;

	private BigHeadController headController;

	//private float yPosMin = 1.0f;

	[SerializeField] private Text t;
	// Use this for initialization
	void Start () {


		rb2d = GetComponent<Rigidbody2D>();
		micScript = GetComponent<VolumeCapture>();
		micScript.Init();
		ball = GetComponent<SpriteRenderer>();
		headController = GameObject.FindGameObjectWithTag("head").GetComponent<BigHeadController>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		

		if (micStarted){
			//update UI
		}

		if (Input.GetKeyDown(KeyCode.G)){
			//TriggerEvent(Events.LAUNCH_BACKWARDS, 25.0f, 15.0f, 5.0f);
			//TriggerEvent(Events.LAUNCH_BACKWARDS, 2.0f, 5.0f, 2.0f);
		}

		if (Input.GetKeyUp(KeyCode.G)){
			p1Disabled = false;
			p2Disabled = false;
		}

		if (Input.GetKeyDown(KeyCode.S) && !p1Disabled){
			Player1MicOn();
		}

		if ((Input.GetKeyUp(KeyCode.S) && micStarted)  && (p1Disabled != true)){
			Player1MicOff();
		}


		if (Input.GetKeyDown(KeyCode.K) && !p2Disabled){
			Player2MicOn();
		}

		if ((Input.GetKeyUp(KeyCode.K) && micStarted) && (p2Disabled != true)){
			Player2MicOff();
		}



	}

	void Player1MicOn(){
		Debug.Log("Player 1 using mic");
		p2Disabled = true;
		micStarted = true;

		micScript.StartMic();
		headController.ShowHead();
	}

	void Player1MicOff(){
		Debug.Log("Player 1 stopping mic");
		p2Disabled = false;
		micScript.StopMic();
		micScript.DbValue = 0f;
		micStarted = false;
		headController.HideHead();
	}

	void Player2MicOn(){
		Debug.Log("Player 2 using mic");
		p1Disabled = true;
		micStarted = true;
		micScript.StartMic();
	}

	void Player2MicOff(){
		Debug.Log("Player 2 stopping mic");
		p1Disabled = false;
		micScript.StopMic();
		micScript.DbValue = 0f;
		micStarted = false;
	}

	void TriggerEvent(Events e, float mSpd, float str, float jump){
		if (micStarted){
			micScript.StopMic();
			micStarted = false;
			micScript.DbValue = 0.0f;
		}
		p1Disabled = true;
		p2Disabled = true;

		CallEvent(e,mSpd,str,jump);

		p1Disabled = false;
		p2Disabled = false;
	}

	void CallEvent(Events e, float mSpd, float str, float jump){
		switch(e){
			case Events.LAUNCH_FORWARD:
				LaunchBall(mSpd, Math.Abs(str), jump);
				break;
			
			case Events.LAUNCH_BACKWARDS:
				float d = str;
				if (d > 0){
					d *= -1;
				}
				LaunchBall(mSpd, d, jump);
				break;
			
			default:
				break;
		}
	}

	///<summary> Launches the ball forward.true YOu can control the speed, direction + strength, jump height.
	/// Different configs I have tried:
	/// spd / dir / jump
	/// 25.0 / 15.0 / 5.0 - like a lob, far distance
	///</summary>
	///<param name="mSpeed (float)"> THe max speed player controller can move</param>
	///<param name="directionStrength (float)">The direction + strength of which way to go. Negative moves back, positive moves forwards </param>
	///<param name="jumpHeight (float)">The y velocity to give to the ball to jump</param>
	public void LaunchBall(float mSpeed, float directionStrength, float jumpHeight){
		//maxSpeed = mSpeed;
		rb2d.AddForce(Vector2.right * directionStrength * moveForce);
		rb2d.velocity = new Vector2((rb2d.velocity.x * -1.0f), jumpHeight);
		//maxSpeed = 5.0f;
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private float GetPowa(float input)
    {
        float adjustedDbs = input + maxDb;
        float percent = (((adjustedDbs - minDb)) / (maxDb - minDb));
        Debug.Log("PERCENT IS: " + percent.ToString());

        if (percent > 1f)
        {
            return maxSpeed;
        }
        else if (percent <= 0f)
        {
            return 0f;
        }
        else
        {
            return percent * maxSpeed;
        }
    }

    void FixedUpdate()
    {
        float h = 0f;

        h = GetPowa(micScript.DbValue);
        currentSpeed = h;
        Debug.Log("H: " + h.ToString());

        if(micStarted)
        rb2d.AddForce(Vector2.right * h);
    }

}