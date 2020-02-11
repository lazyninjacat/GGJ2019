using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuoteGeneratorController : MonoBehaviour {

	[SerializeField] private GameObject p1Bubble;
	[SerializeField] private RawImage p1Image;
	[SerializeField] private Text p1Text;
	[SerializeField] private GameObject p2Bubble;
	[SerializeField] private RawImage p2Image;
	[SerializeField] private Text p2Text;

	public enum Players{
		ONE,
		TWO
	}

	public enum QuoteEvent{
		HAPPY,
		CHUBBS,
		GRANDMA,
		GIRLFRIEND,
		LARSON,
		POTTER,
		SHOOTER,
		JACKASS,
		BOB,
		NURSE,
		NOEVENT
	}

	private List<string> chubbsImages = new List<string>{"chubbs1", "chubbs2"};
	private List<string> grandmaImages = new List<string>{"grandma1", "girlfriend2"};
	private List<string> girlfriendImages = new List<string>{"girlfriend1", "girlfriend2"};
	private List<string> larsonImages = new List<string>{"larson1","larson2","larson3"};
	private List<string> potterImages = new List<string>{"potter1"};
	private List<string> shooterImages = new List<string>{"shooter1","shooter2","shooter4","shooter5"};
	private List<string> jackassImages = new List<string>{"jackass1","jackass2","jackass4"};
	private List<string> bobImages = new List<string>{"bob1", "bob2"};
	private List<string> nurseImages = new List<string>{"nurse1", "nurse2"};

	public List<string> happyQuotes = new List<string>{
		"Why you don't you just go HOME?",
		"That's your HOME!",
		"Are you too good for your HOME?",
		"ANSWER ME!",
		"Tap it in, just tap it in",
		"Give it a little tappy, tap tap tap-a-roo",
		"I wanna kiss you all over, and over and again",
		"364 days until next year's hockey tryouts, I have to toughen up.",
		"I'm stupid. You're smart. I was wrong. You were right. You're the best. I'm the worst. You're very good-looking. I'm not attractive.",
		"You eat pieces of S%*t for breakfast?"
	};

	private List<string> shooterQuotes = new List<string>{
		"I eat pieces of S%*t like you for breakfast!",
		"Damn you people. This is golf! Not a rock concert.",
		"Stay out of my way, or you'll pay.",
		"This is Shooter's tour!",
		"I saw two big fat bikers having sex in the woods....how am i supposed to CHIP with that going on?!",
		"Stop fraternizing with the help Gilmore. Just hit your ball... if you can find it.",
		"Damn you people. Go back to your shanties."
	};

	private List<string> jackassQuotes = new List<string>{
		"The club went further than the ball",
		"You're gonna need a blanket and suntan lotion, cause you're never gonna get off that beach, just like the way you never got into the NHL, ya JACKASS.",
		"Hey Gilmore - you suck!",
		"You will not make this putt - ya Jackass"
	};

	private List<string> bobQuotes = new List<string>{
		"I don't want a piece of you - I want the whole thing!",
		"This guy sucks",
		"I think you've had enough, No?",
		"There is no way that you could have been as bad at hockey as you are at golf."
	};

	private List<string> nurseQuotes = new List<string>{
		"You're in my world now, Grandma",
		"Oh, well, now your back's gonna hurt, 'cause you just pulled landscaping duty",
		"Now you will go to sleep or I will put you to sleep."
	};

	private List<string> chubbsQuotes = new List<string>{
		"Just easing the tension, baby. Just easing the tension.",
		"You win the Open tomorrow, and you're automatically on the Pro Tour.",
		"It's all in the hips, It's all in the hips, It's all in the hips!"
	};

	private List<string> grandmaQuotes = new List<string>{
		"Can I trouble you for a glass of warm milk? It helps put me to sleep.",
		"What happened to that nice girlfriend of yours?",
		"I just want you to be happy"
	};

	private List<string> girlfriendQuotes = new List<string>{
		"Did you see that? He just got a hole-in-one on a par four!",
		"Oh, I hope he wins. He's a publicist's dream!",
		"Do you always carry a puck with you?",
		"You can do it Happy!"
	};

	private List<string> potterQuotes = new List<string>{
		"Harness... energy... block... bad"
	};

	private List<string> larsonQuotes = new List<string>{
		"YOU can count on ME, waiting for YOU in the parking lot!",
		"Hey, I believe that's Mr. Gilmore's!",
		"Trying to reach the green from here, Shooter?",
		"Happy Gilmore accomplished that feat no more than an hour ago."
	};


	// Use this for initialization
	void Start () {
		QuoteBubbleState(QuoteEvent.NOEVENT, Players.ONE, false);
		QuoteBubbleState(QuoteEvent.NOEVENT, Players.TWO, false);
	}
	
	// Update is called once per frame
	void Update () {

		//Example on how to call an event
		if (Input.GetKeyUp(KeyCode.O)){
			QuoteBubbleState(QuoteEvent.SHOOTER, Players.TWO, true);
		}
	}

	///<summary>
	/// Call this to toggle the quote bubble of the Players enum passed on or off depending on the state passed. True is on, false is off.
	/// ie. QuoteBubble(Players.ONE, true) would turn Player1 bubble on.
	///</summary>
	///<param name="p (Players)">An enum declaring which player's bubble you are manipulating</param>
	///<param name="state (bool)">The state of the bubble - on (true) or off (false)</param>
	public void QuoteBubbleState(QuoteEvent q, Players p, bool state){
		string quote = "";
		string image = "grandma1";
		switch(q){
			case QuoteEvent.HAPPY:
				quote = happyQuotes[Random.Range(0,happyQuotes.Count)];
				image = grandmaImages[Random.Range(0,grandmaImages.Count)];
				break;
			case QuoteEvent.CHUBBS:
				quote = chubbsQuotes[Random.Range(0,chubbsQuotes.Count)];
				image = chubbsImages[Random.Range(0,chubbsImages.Count)];
				break;
			case QuoteEvent.GRANDMA:
				quote = grandmaQuotes[Random.Range(0,grandmaQuotes.Count)];
				image = grandmaImages[Random.Range(0,grandmaImages.Count)];
				break;
			case QuoteEvent.GIRLFRIEND:
				quote = girlfriendQuotes[Random.Range(0,girlfriendQuotes.Count)];
				image = girlfriendImages[Random.Range(0,girlfriendImages.Count)];
				break;
			case QuoteEvent.LARSON:
				quote = larsonQuotes[Random.Range(0,larsonQuotes.Count)];
				image = larsonImages[Random.Range(0,larsonImages.Count)];
				break;
			case QuoteEvent.POTTER:
				quote = potterQuotes[Random.Range(0,potterQuotes.Count)];
				image = potterImages[Random.Range(0,potterImages.Count)];
				break;
			case QuoteEvent.SHOOTER:
				quote = shooterQuotes[Random.Range(0,shooterQuotes.Count)];
				image = shooterImages[Random.Range(0,shooterImages.Count)];
				break;
			case QuoteEvent.JACKASS:
				quote = jackassQuotes[Random.Range(0,jackassQuotes.Count)];
				image = jackassImages[Random.Range(0,jackassImages.Count)];
				break;
			case QuoteEvent.BOB:
				quote = bobQuotes[Random.Range(0,bobQuotes.Count)];
				image = bobImages[Random.Range(0,bobImages.Count)];
				break;
			case QuoteEvent.NURSE:
				quote = nurseQuotes[Random.Range(0,nurseQuotes.Count)];
				image = nurseImages[Random.Range(0,nurseImages.Count)];
				break;
			case QuoteEvent.NOEVENT:
			default:
				break;
		}

		switch(p){
			case Players.ONE:
				p1Bubble.SetActive(state);
				p1Image.texture = (Texture)Resources.Load(("characters/" + image));
				p1Text.text = quote;
				break;
			case Players.TWO:
				p2Bubble.SetActive(state);
				p2Image.texture = (Texture)Resources.Load(("characters/" + image));
				p2Text.text = quote;
				break;
			default:
				Debug.Log("Something wrong with ShowBubble in QuoteGeneratorController");
				break;
		}	
	}///<summary>
	/// Call this to toggle the quote bubble of the Players enum passed on or off depending on the state passed. True is on, false is off.
	/// ie. QuoteBubble(Players.ONE, true) would turn Player1 bubble on.
	///</summary>
	///<param name="p (Players)">An enum declaring which player's bubble you are manipulating</param>
	///<param name="state (bool)">The state of the bubble - on (true) or off (false)</param>
	public void QuoteBubbleStateByNum(int rando, Players p, bool state){
		string quote = "";
		string image = "grandma1";
		switch(rando){
			case 0:
				quote = happyQuotes[Random.Range(0,happyQuotes.Count)];
				image = grandmaImages[Random.Range(0,grandmaImages.Count)];
				break;
			case 1:
				quote = chubbsQuotes[Random.Range(0,chubbsQuotes.Count)];
				image = chubbsImages[Random.Range(0,chubbsImages.Count)];
				break;
			case 2:
				quote = grandmaQuotes[Random.Range(0,grandmaQuotes.Count)];
				image = grandmaImages[Random.Range(0,grandmaImages.Count)];
				break;
			case 3:
				quote = girlfriendQuotes[Random.Range(0,girlfriendQuotes.Count)];
				image = girlfriendImages[Random.Range(0,girlfriendImages.Count)];
				break;
			case 4:
				quote = larsonQuotes[Random.Range(0,larsonQuotes.Count)];
				image = larsonImages[Random.Range(0,larsonImages.Count)];
				break;
			case 5:
				quote = potterQuotes[Random.Range(0,potterQuotes.Count)];
				image = potterImages[Random.Range(0,potterImages.Count)];
				break;
			case 6:
				quote = shooterQuotes[Random.Range(0,shooterQuotes.Count)];
				image = shooterImages[Random.Range(0,shooterImages.Count)];
				break;
			case 7:
				quote = jackassQuotes[Random.Range(0,jackassQuotes.Count)];
				image = jackassImages[Random.Range(0,jackassImages.Count)];
				break;
			case 8:
				quote = bobQuotes[Random.Range(0,bobQuotes.Count)];
				image = bobImages[Random.Range(0,bobImages.Count)];
				break;
			case 9:
				quote = nurseQuotes[Random.Range(0,nurseQuotes.Count)];
				image = nurseImages[Random.Range(0,nurseImages.Count)];
				break;
			case 10:
			default:
				break;
		}

		switch(p){
			case Players.ONE:
				p1Bubble.SetActive(state);
				p1Image.texture = (Texture)Resources.Load(("characters/" + image));
				p1Text.text = quote;
				break;
			case Players.TWO:
				p2Bubble.SetActive(state);
				p2Image.texture = (Texture)Resources.Load(("characters/" + image));
				p2Text.text = quote;
				break;
			default:
				Debug.Log("Something wrong with ShowBubble in QuoteGeneratorController");
				break;
		}	
	}
}
