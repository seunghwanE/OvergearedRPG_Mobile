
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager inst;


    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.LogWarning("Login");
                if (LoadingController.inst != null)
                { LoadingController.inst.StartCor(); }

                if (DataController.inst.uid.Equals(string.Empty))
                { StartCoroutine(GetIdToken()); }
            }
            else
            {
                Debug.LogWarning("Login fail");
                if(LoadingController.inst != null)
                { LoadingController.inst.loginSign.SetActive(true); }
            }
        });
    }

    IEnumerator GetIdToken()
    {
        while (string.IsNullOrEmpty(Social.localUser.id))
            yield return null;
        
        DataController.inst.uid = Social.localUser.id;
        DataController.inst.es3.Sync();
    }

    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        Debug.LogWarning("Login out");
    }

    #region 클라우드 저장

    ISavedGameClient SavedGame()
    { return PlayGamesPlatform.Instance.SavedGame; }

    public void LoadCloud()
    {
#if UNITY_EDITOR
        ErrorSign.inst.SimpleSignSet(Language.inst.strArray[182], 0);
#elif UNITY_ANDROID
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
              SavedGame().OpenWithAutomaticConflictResolution("mysave",
               DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);
            }
        });
#endif
    }

    void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadData);
    }

    void LoadData(SavedGameRequestStatus status, byte[] LoadedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            DataController.inst.es3.Clear();
            DataController.inst.es3.SaveRaw(LoadedData);
            DataController.inst.es3.Sync();
            ErrorSign.inst.SimpleSignSet(Language.inst.strArray[182], 0);
            Invoke("Quit", 2f);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[183], 20); }
        MainSceneController.inst.loadPopImage.SetActive(false);
        MainSceneController.inst.loadPopText.text = Language.inst.strArray[184];
        Invoke("SetOffLoadPop", 2f);
    }
    public void Quit()
    { InputController.inst.Quit(); }
    public void SetOffLoadPop()
    { MainSceneController.inst.loadPopObj.SetActive(false); }



    public void SaveCloud()
    {
#if UNITY_EDITOR
        ErrorSign.inst.SimpleSignSet(Language.inst.strArray[180], 0);
#elif UNITY_ANDROID
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                SavedGame().OpenWithAutomaticConflictResolution("mysave",
                    DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGame);
            }
        });
#endif
    }

    public void SaveGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            byte[] bytes = DataController.inst.es3.LoadRawBytes();
            SavedGame().CommitUpdate(game, update, bytes, SaveData);
        }
    }

    void SaveData(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[180], 0); }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[181], 50); }
    }
    #endregion


    public void ReportLevel(long level)
    {
        PlayGamesPlatform.Instance.ReportScore(level, GPGSIds.leaderboard_level, (bool success) =>
        {
        //if (success);
        //else;
    });
    }
    public void ReportKill(long kill)
    {
        PlayGamesPlatform.Instance.ReportScore(kill, GPGSIds.leaderboard_kill_count, (bool success) =>
        {
        //if (success);
        //else;
    });
    }
    public void ReportPower(long power)
    {
        PlayGamesPlatform.Instance.ReportScore(power, GPGSIds.leaderboard_power, (bool success) =>
        {
        //if (success);
        //else;
    });
    }
    public void ShowLeaderboardUI()
    {
        // Sign In 이 되어있지 않은 상태라면
        // Sign In 후 리더보드 UI 표시 요청할 것
        if (Social.localUser.authenticated == false)
        { LogIn(); }

        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
}