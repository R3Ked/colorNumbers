using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KModkit;
using System.Text.RegularExpressions;

public class colorNumberCode : MonoBehaviour 
{
	public KMBombInfo bomb;
	public KMAudio audio;
    public KMColorblindMode colorblind;
    public KMRuleSeedable RuleSeedable;

    public KMSelectable button1;
	public KMSelectable button2;
	public KMSelectable button3;
	public KMSelectable button4;

    public TextMesh colorblindText;
	public Material[] colorOptions;
    private bool colorblindActive;
	public Renderer led;
    private int[] ledValues = new int[] { 0, 1, 2, 3 };
	private int ledIndex = 0;
	public Material off;
	
	static int moduleIdCounter = 1;
	int moduleId;
	private bool colorPicked = false;
	private bool moduleSolved; 
	
	void Awake()
	{
		moduleId = moduleIdCounter++;
        colorblindActive = colorblind.ColorblindModeActive;
		button1.OnInteract += delegate () { PressButton1(); return false; };
		button2.OnInteract += delegate () { PressButton2(); return false; };
		button3.OnInteract += delegate () { PressButton3(); return false; };
		button4.OnInteract += delegate () { PressButton4(); return false; };
        GetComponent<KMBombModule>().OnActivate += LightsOn;
	}

	void Start() 
	{
        var rnd = RuleSeedable.GetRNG();
        Debug.LogFormat("[Color Numbers #{0}] Using rule seed: {1}", moduleId, rnd.Seed);
        if (rnd.Seed != 1)
        {
            ledValues = rnd.ShuffleFisherYates(ledValues);
        }
        if (colorPicked == false)
		{
			PickLEDColor();
			colorPicked = true;
		}
	}

    // Runs when the lights turn on in game
    void LightsOn()
    {
        if (!moduleSolved)
            led.material = colorOptions[ledIndex];
    }
	
	void PickLEDColor()
	{
        string[] buttonNames = new string[] { "one", "two", "three", "four" };
		ledIndex = UnityEngine.Random.Range(0,4);
        if (colorblindActive)
            colorblindText.text = colorOptions[ledIndex].name;
        Debug.LogFormat("[Color Numbers #{0}] The color of the LED is {1}", moduleId, colorOptions[ledIndex].name);
        Debug.LogFormat("[Color Numbers #{0}] The correct button to press is button {1}", moduleId, buttonNames[ledValues[ledIndex]]);
    }
	
	void PressButton1 ()
	{
        if (!moduleSolved)
        {
            Debug.LogFormat("[Color Numbers #{0}] You pressed button one", moduleId);
            button1.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button1.transform);
            //Execute if the LED is red
            if (ledValues[ledIndex] == 0)
            {
                Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                led.material = off;
                if (colorblindActive)
                    colorblindText.text = "";
            }
            //If it's not red, execute this
            else
            {
                Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
	}
	
	void PressButton2 ()
	{
        if (!moduleSolved)
        {
            Debug.LogFormat("[Color Numbers #{0}] You pressed button two", moduleId);
            button2.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button2.transform);
            //Execute if the LED is blue
            if (ledValues[ledIndex] == 1)
            {
                Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                led.material = off;
                if (colorblindActive)
                    colorblindText.text = "";
            }
            //If it's not blue, execute this
            else
            {
                Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
	}
	
	void PressButton3 ()
	{
        if (!moduleSolved)
        {
            Debug.LogFormat("[Color Numbers #{0}] You pressed button three", moduleId);
            button3.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button3.transform);
            //Execute if the LED is green
            if (ledValues[ledIndex] == 2)
            {
                Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                led.material = off;
                if (colorblindActive)
                    colorblindText.text = "";
            }
            //If it's not green, execute this
            else
            {
                Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
	}
	
	void PressButton4 ()
	{
        if (!moduleSolved)
        {
            Debug.LogFormat("[Color Numbers #{0}] You pressed button four", moduleId);
            button4.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button4.transform);
            //Execute if the LED is yellow
            if (ledValues[ledIndex] == 3)
            {
                Debug.LogFormat("[Color Numbers #{0}] That is correct, module solved", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                led.material = off;
                if (colorblindActive)
                    colorblindText.text = "";
            }
            //If it's not yellow, execute this
            else
            {
                Debug.LogFormat("[Color Numbers #{0}] That is wrong, strike", moduleId);
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
	}

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press # [Presses the button labelled with the number '#'] | !{0} colorblind [Toggles colorblind mode]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*colorblind\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (colorblindActive)
            {
                colorblindActive = false;
                colorblindText.text = "";
            }
            else
            {
                colorblindActive = true;
                colorblindText.text = colorOptions[ledIndex].name;
            }
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int temp = 0;
                if (!int.TryParse(parameters[1], out temp))
                {
                    yield return "sendtochaterror The specified label " + parameters[1] + "' is not a number!";
                    yield break;
                }
                if (temp < 1 || temp > 4)
                {
                    yield return "sendtochaterror The specified label " + parameters[1] + "' is not in range 1-4!";
                    yield break;
                }
                KMSelectable[] pressables = new KMSelectable[] { button1, button2, button3, button4 };
                pressables[temp - 1].OnInteract();
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the label of the button to press!";
            }
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        KMSelectable[] pressables = new KMSelectable[] { button1, button2, button3, button4 };
        pressables[ledValues[ledIndex]].OnInteract();
        yield return new WaitForSeconds(0.1f);
    }
}
