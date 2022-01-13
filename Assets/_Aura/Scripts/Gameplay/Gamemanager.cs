using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    PREP,
    SWIPING,
    ACTION,
    PAUSE,
    WIN,
    LOOSE
}

/// <summary>
/// Controls gameplay rules
/// No. Of Rounds
/// Win Condidtion
/// Loose Condition
/// No of balls to spawn pa round
/// Pause condition
/// keeps track of Game Phases.
/// Triggers UI Updates based on score
/// Triggers score serialization
/// Triggers Ads via AdManager
/// </summary>
public class Gamemanager : MonoBehaviour
{
    [SerializeField] int ballCountPaRound;
    public int GetBallCountPaRound()
    {
        return ballCountPaRound;
    }
}
