using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelsManager : MonoBehaviour
{
    public Button level2;
    public Button level3;
    int levelNum;
    public GameObject panel;
    static string levelPrefString = "Level Num";
    public const string jumpPrefsString = "Jump Unlocked";
    public const string dashPrefsString = "Dash Unlocked";
    public const string gunPrefsString = "Gun Unlocked";
    void Update()
    {
        levelNum = PlayerPrefs.GetInt(levelPrefString);
        if (levelNum >= 1 && level2 != null && level3 != null)
        {
            level2.interactable = true;
            Player.jumpUnlocked = true;
            if (levelNum >= 2)
            {
                level3.interactable = true;
                Player.dashUnlocked = true;
                if (levelNum == 3)
                {
                    Player.gunUnlocked = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public static void Level1Done()
    {
        if (PlayerPrefs.GetInt(levelPrefString) < 1)
        {
            PlayerPrefs.SetInt(levelPrefString, 1);
        }
        PlayerPrefs.SetInt(jumpPrefsString, 1);
        SceneManager.LoadScene(2);
    }
    public static void Level2Done()
    {
        if (PlayerPrefs.GetInt(levelPrefString) < 2)
        {
            PlayerPrefs.SetInt(levelPrefString, 2);
        }
        PlayerPrefs.SetInt(dashPrefsString, 1);
        SceneManager.LoadScene(3);
    }
    public static void Level3Done()
    {
        if (PlayerPrefs.GetInt(levelPrefString) < 3)
        {
            PlayerPrefs.SetInt(levelPrefString, 3);
        }
        PlayerPrefs.SetInt(gunPrefsString, 1);
        SceneManager.LoadScene(4);
    }
    public void Level1Click()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2Click()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3Click()
    {
        SceneManager.LoadScene(3);
    }
    public void EndlessClick()
    {
        SceneManager.LoadScene(4);
    }
    public void ResetPrefs()
    {
        PlayerPrefs.SetInt(jumpPrefsString, 0);
        PlayerPrefs.SetInt(dashPrefsString, 0);
        PlayerPrefs.SetInt(gunPrefsString, 0);
        PlayerPrefs.SetInt(levelPrefString, 0);
        level2.interactable = false;
        level3.interactable = false;
    }
    public void OpenInfo()
    {
        panel.SetActive(true);
    }
    public void CloseInfo()
    {
        panel.SetActive(false);
    }
}
