using Leap;
using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GestureListener : MonoBehaviour
{
    public TextMesh text;
    public RiggedHand hand;
    public string selectedGesture;
    // Next update in second
    private int nextUpdate = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the next update is reached
        //if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;

            if (hand.IsTracked)
            {
                var gestureDictionary = new Dictionary<string, float>();
                gestureDictionary.Add("rockStrength", GetRockStrength(hand.GetLeapHand()));
                gestureDictionary.Add("paperStrength", GetPaperStrength(hand.GetLeapHand()));
                gestureDictionary.Add("sissorsStrength", GetSissorsStrength(hand.GetLeapHand()));
                gestureDictionary.Add("fuStrength", GetFUStrength(hand.GetLeapHand()));

                var chosenGesture = gestureDictionary.FirstOrDefault(x => x.Value.Equals(gestureDictionary.Values.Max()));

                switch (chosenGesture.Key)
                {
                    case "rockStrength":
                        selectedGesture = "rock";
                        text.text = "ROCK!";
                        break;
                    case "paperStrength":
                        selectedGesture = "paper";
                        text.text = "PAPER!";
                        break;
                    case "sissorsStrength":
                        selectedGesture = "sissors";
                        text.text = "SISSORS!";
                        break;
                    case "fuStrength":
                        selectedGesture = "fu";
                        text.text = "Wow, that was rude!";
                        break;
                }
            }
            else
            {
                selectedGesture = "untracked";
                text.text = "Make your choice";
                Debug.Log(selectedGesture);
            }
        }
    }

    /// <summary>
    /// Returns a confidence value from 0 to 1 indicating how strongly the Hand is making a fist.
    /// </summary>
    public static float GetRockStrength(Hand hand)
    {
        return (Vector3.Dot(hand.Fingers[1].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[2].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[3].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[4].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[0].Direction.ToVector3(), -hand.RadialAxis())
              ).Map(-5, 5, 0, 1);
    }

    /// <summary>
    /// Returns a confidence value from 0 to 1 indicating how strongly the Hand is making a fist.
    /// </summary>
    public static float GetPaperStrength(Hand hand)
    {
        return (Vector3.Dot(hand.Fingers[1].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[2].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[3].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[4].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[0].Direction.ToVector3(), hand.RadialAxis())
              ).Map(-5, 5, 0, 1);
    }

    /// <summary>
    /// Returns a confidence value from 0 to 1 indicating how strongly the Hand is making a fist.
    /// </summary>
    public static float GetSissorsStrength(Hand hand)
    {
        return (Vector3.Dot(hand.Fingers[1].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[2].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[3].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[4].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[0].Direction.ToVector3(), -hand.RadialAxis())
              ).Map(-5, 5, 0, 1);
    }

    /// <summary>
    /// Returns a confidence value from 0 to 1 indicating how strongly the Hand is making a fist.
    /// </summary>
    public static float GetFUStrength(Hand hand)
    {
        return (Vector3.Dot(hand.Fingers[1].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[2].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[3].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[4].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[0].Direction.ToVector3(), -hand.RadialAxis())
              ).Map(-5, 5, 0, 1);
    }

    /// <summary>
    /// Returns a confidence value from 0 to 1 indicating how strongly the Hand is making a fist.
    /// </summary>
    public static float GetPointingStrength(Hand hand)
    {
        return (Vector3.Dot(hand.Fingers[1].Direction.ToVector3(), hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[2].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[3].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[4].Direction.ToVector3(), -hand.DistalAxis())
              + Vector3.Dot(hand.Fingers[0].Direction.ToVector3(), -hand.RadialAxis())
              ).Map(-5, 5, 0, 1);
    }
}
