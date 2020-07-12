using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KModkit;

public class colorNumberCode : MonoBehaviour 
{
	public KMBombInfo bomb;
	public KMAudio audio;
	
	public KMSelectable button1;
	public KMSelectable button2;
	public KMSelectable button3;
	public KMSelectable button4;
	
	public Material[] colorOptions;
	public Renderer led;
	private int ledIndex = 0;
	public Material off;
	
	static int moduleIdCounter = 1;
	int moduleId;
	private bool colorPicked = false;
	private bool moduleSolved; 
	
	void Awake()
	{
		moduleId = moduleIdCounter++;
		button1.OnInteract += delegate () { PressButton1(); return false; };
		button2.OnInteract += delegate () { PressButton2(); return false; };
		button3.OnInteract += delegate () { PressButton3(); return false; };
		button4.OnInteract += delegate () { PressButton4(); return false; };
	}

	void Start() 
	{
		if(colorPicked == false)
		{
			PickLEDColor();
			colorPicked = true;
		}
	}
	
	void PickLEDColor()
	{
		ledIndex = UnityEngine.Random.Range(0,4);
		led.material = colorOptions[ledIndex];
		Debug.LogFormat("[Color Numbers #{0}] The color of the LED is {1}", moduleId, colorOptions[ledIndex].name);
	}
	
	void PressButton1 ()
	{
		Debug.LogFormat("[Color Numbers #{0}] You pushed button one", moduleId);
		button1.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		//Execute if the LED is red
		if(ledIndex == 0)
		{
			Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
			moduleSolved = true;
			GetComponent<KMBombModule>().HandlePass();
			led.material = off;
		}
		//If it's not red, execute this
		else
		{
			Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
			GetComponent<KMBombModule>().HandleStrike();
		}
	}
	
	void PressButton2 ()
	{
		Debug.LogFormat("[Color Numbers #{0}] You pushed button two", moduleId);
		button2.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		//Execute if the LED is blue
		if(ledIndex == 1)
		{
			Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
			moduleSolved = true;
			GetComponent<KMBombModule>().HandlePass();
			led.material = off;
		}
		//If it's not blue, execute this
		else
		{
			Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
			GetComponent<KMBombModule>().HandleStrike();
		}
	}
	
	void PressButton3 ()
	{
		Debug.LogFormat("[Color Numbers #{0}] You pushed button three", moduleId);
		button3.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		//Execute if the LED is green
		if(ledIndex == 2)
		{
			Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
			moduleSolved = true;
			GetComponent<KMBombModule>().HandlePass();
			led.material = off;
		}
		//If it's not green, execute this
		else
		{
			Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
			GetComponent<KMBombModule>().HandleStrike();
		}
	}
	
	void PressButton4 ()
	{
		Debug.LogFormat("[Color Numbers #{0}] You pushed button four", moduleId);
		button4.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		//Execute if the LED is yellow
		if(ledIndex == 3)
		{
			Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
			moduleSolved = true;
			GetComponent<KMBombModule>().HandlePass();
			led.material = off;
		}
		//If it's not yellow, execute this
		else
		{
			Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
			GetComponent<KMBombModule>().HandleStrike();
		}
	}
	
}
