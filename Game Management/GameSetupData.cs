using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupData : MonoBehaviour
{



    private static bool skipTutorial;
    private static gameMode gmode;
    private static character characterSelected;

    public void setGameMode(gameMode set)
    {
        gmode = set;
    }

    public void setCharacter(character set)
    {
        characterSelected = set;
    }

    public character getCharacter()
    {
        return characterSelected;
    }
    public gameMode getMultiplayer()
    {
        return gmode;
    }

    public void setName(string tname)
    {
        name = tname;
    }

    public void setTutorial(bool tutorialSkiped)
    {
        skipTutorial = tutorialSkiped;
    }

    public bool getTutorial()
    {
        return skipTutorial;
    }

    public string getName()
    {
        return name;
    }

}

public enum gameMode { Singleplayer, Multiplayer };
public enum character { Knight, Ninja};