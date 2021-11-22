using Leap;
using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureListener : MonoBehaviour
{
    public TextMesh Text;
    public RiggedHand hand;
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

            if(hand.IsTracked)
            {
                var rockStrength = GetRockStrength(hand.GetLeapHand());
                var paperStrength = GetPaperStrength(hand.GetLeapHand());
                var sissorsStrength = GetSissorsStrength(hand.GetLeapHand());
                var fuStrength = GetFUStrength(hand.GetLeapHand());

                var estimatedGesture = Math.Max(rockStrength, Math.Max(paperStrength, sissorsStrength));
                
                if(rockStrength > paperStrength && rockStrength > sissorsStrength && rockStrength > fuStrength)
                {
                    Text.text = "ROCK!";
                    Debug.Log("ROCK!");
                }
                else if (paperStrength > rockStrength && paperStrength > sissorsStrength && paperStrength > fuStrength)
                {
                    Text.text = "PAPER!";
                    Debug.Log("PAPER!");
                }
                else if (sissorsStrength > rockStrength && sissorsStrength > paperStrength && sissorsStrength > fuStrength)
                {
                    Text.text = "SISSORS!";
                    Debug.Log("SISSORS!");
                }

                else if (fuStrength > rockStrength && fuStrength > paperStrength && fuStrength > sissorsStrength)
                {
                    Text.text = "Wow, that was rude!";
                    Debug.Log("Rude!");
                }
            }
            else
            {
                Text.text = "Make your choice";
                //Debug.Log("Untracked!");
            }
            /*
            switch(estimatedGesture)
            {
                case rockStrength > Math.Max(paperStrength, sissorsStrength):
                    Debug.Log("Fist Strength: " + );
                    break;
            }
            */

            //Debug.Log("Fist Strength: " + );
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
