using Leap;
using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GestureListener gestureListener;
    public TextMesh gameText;
    public RiggedHand hand;

    private string selectedGesture;
    private bool roundFinished = true;

    // the start time of the round in seconds
    private float roundStartTime = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selectedGesture = gestureListener.selectedGesture;
        Debug.Log("current Gesture: " + selectedGesture);
        var roundTime = Mathf.FloorToInt(Time.time) - roundStartTime;

        if (roundFinished && roundStartTime <= -1)
        {
            if (selectedGesture == "thumbsUp")
            {
                gameText.text = "Round Start, make your choice.";
                roundStartTime = Mathf.FloorToInt(Time.time);
                roundFinished = false;
            }
        }
        else if (!roundFinished && roundStartTime >= -1)
        {
            Debug.Log("roundTime: " + roundTime);
            if (roundTime <= 1)
            {
                gameText.text = "3...";
            }
            else if (roundTime <= 2)
            {
                gameText.text = "2...";
            }
            else if (roundTime <= 3)
            {
                gameText.text = "1...";
            }
            else if (roundTime <= 4)
            {
                gameText.text = "Go!";
            }
            else if (roundTime <= 5)
            {
                //Make the Randomly pick a gesture for the AI
                string[] gestures = { "rock", "paper", "sissors" };
                int randomIndex = UnityEngine.Random.Range(0, 3);
                var gestureAI = gestures[randomIndex];

                var gesturePlayer = selectedGesture;

                if (gesturePlayer == "rock" || gesturePlayer == "thumbsUp")
                {
                    gameText.text = "You picked: Rock\n";
                    if (gestureAI == "rock")
                    {
                        gameText.text += "The AI picked: Rock\nDRAW!";
                    }
                    else if (gestureAI == "paper")
                    {
                        gameText.text += "The AI picked: Paper\nYOU Lose!";
                    }
                    else if (gestureAI == "sissors")
                    {
                        gameText.text += "The AI picked: Sissors\nYOU WIN!";
                    }
                }
                else if (gesturePlayer == "paper")
                {
                    gameText.text = "You picked: Paper\n";
                    if (gestureAI == "rock")
                    {
                        gameText.text += "The AI picked: Rock\nYOU WIN!";
                    }
                    else if (gestureAI == "paper")
                    {
                        gameText.text += "The AI picked: Paper\nDRAW!";
                    }
                    else if (gestureAI == "sissors")
                    {
                        gameText.text += "The AI picked: Sissors\nYOU LOSE!";
                    }
                }
                else if (gesturePlayer == "sissors")
                {
                    gameText.text = "You picked: Sissors\n";
                    if (gestureAI == "rock")
                    {
                        gameText.text += "The AI picked: Rock\nLoose!";
                    }
                    else if (gestureAI == "paper")
                    {
                        gameText.text += "The AI picked: Paper\nYOU Win!";
                    }
                    else if (gestureAI == "sissors")
                    {
                        gameText.text += "The AI picked: Sissors\nYOU Draw!";
                    }
                }

                roundFinished = true;
            }
        }
        else if (roundFinished)
        {
            if (roundTime >= 9)
            {
                roundStartTime = -1;
            }
            else if (roundTime >= 8)
            {
                gameText.text = "\nThumbs up to play again :)";
            }
        }
    }
}
