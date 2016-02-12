using UnityEngine;
using System.Collections;

public static class Events {

    public static System.Action OnSettings = delegate { };
    public static System.Action OnLoginAdvisor = delegate { };

    public static System.Action<int, float> OnNewHiscore = delegate { };
    public static System.Action<GameObject> OnUIClicked = delegate { };    

    public static System.Action<string> OnPreloadScene = delegate { };
    public static System.Action<string> OnSceneLoad = delegate { };
    public static System.Action OnLoadSceneReady = delegate { };
    public static System.Action OnSceneReset = delegate { };
    public static System.Action OnTransition = delegate { };
    public static System.Action OnTransitionReady = delegate { };
    public static System.Action OnPoolAllItemsInScene = delegate { };
    public static System.Action<PowerupManager.types> OnPowerUp = delegate { };
    public static System.Action<PowerupManager.types> OnPowerUpShoot = delegate { };
    public static System.Action OnHeroPowerUpOff = delegate { };
    

    //The game:
    public static System.Action StartGame = delegate { };
    public static System.Action OnGameOver = delegate { };
    public static System.Action OnLevelComplete = delegate { };
    public static System.Action<SwipeDetector.directions> OnSwipe = delegate { };

    //laneID, distance
    public static System.Action<int, float> OnAddCoins = delegate { };

    public static System.Action<float, float> OnSaveVolumes = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
    public static System.Action<float> OnSoundsVolumeChanged = delegate { };
    public static System.Action<bool> OnCapsChanged = delegate { };
    public static System.Action<string> OnVoice = delegate { };
    public static System.Action<string> OnSoundFX = delegate { };
    public static System.Action<string> OnSoundFXLoop = delegate { };
    
    public static System.Action<string> OnMusicChange = delegate { };

    public static System.Action<bool> OnGamePaused = delegate { };
    public static System.Action OnGameRestart = delegate { };
    public static System.Action<float, bool> OnChangeSpeed = delegate { };
    public static System.Action OnResetSpeed = delegate { };

    public static System.Action OnChangeLane = delegate { };
    public static System.Action OnChangeLaneComplete = delegate { };

    public static System.Action OnHeroDie = delegate { };
    public static System.Action OnHeroCrash = delegate { };
    public static System.Action OnHeroDash = delegate { };
    public static System.Action OnHeroCelebrate = delegate { };
    
    public static System.Action OnExplotion = delegate { };

    public static System.Action<int> OnScoreAdd = delegate { };
    public static System.Action OnStartCountDown = delegate { };
    public static System.Action OnBarInit = delegate { };
    public static System.Action OnBarReady = delegate { };
    
    
    

}
