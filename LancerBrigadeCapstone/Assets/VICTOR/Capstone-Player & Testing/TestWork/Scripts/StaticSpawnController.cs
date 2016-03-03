using UnityEngine;
using System.Collections;
using XInputDotNetPure;

/**
 * @author Victor Haskins
 * class StaticSpawnController used between scenes to understand how many
 * players are active in the game
 */
public static class StaticSpawnController {
    //bools to find if players are active or not.
    static bool player1Active;
    static bool player2Active;

    /// <summary>
    /// parameter determined player sets if they are active or not.
    /// </summary>
    /// <param name="index">to find Player's controller Index</param>
    /// <param name="setting">true or false for if activated</param>
    public static void ActivatePlayer(PlayerIndex index, bool setting)
    {
        if (index == PlayerIndex.One)
        {
            player1Active = setting;
        }
        if (index == PlayerIndex.Two)
        {
            player2Active = setting;
        }
    }

    /// <summary>
    /// returns an integer that will be read by other classes to see if the players are 
    /// </summary>
    /// <returns></returns>
    public static int GetSetPlayers()
    {
        int toReturn = 0;

        if (player1Active && player2Active)
            toReturn = 3;//both players are activated
        else if (player2Active && !player1Active)
            toReturn = 2;//only player 2 is activated
        else if (player1Active && !player2Active)
            toReturn = 1;//only player 1 is activated
        else
            toReturn = 0;//neither player's activated

        return toReturn;
    }
}
