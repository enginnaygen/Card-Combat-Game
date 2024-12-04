using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const string defensiveBattleSceneName = "Battle Defensive";
    const string offensiveBattleSceneName = "Battle Offensive";
    const string crazyBattleSceneName = "Battle Crazy";
    
    [SerializeField] GameObject selectPanel;
    void Start()
    {
        AudioManager.Instance.PlayMenuMusic();

    }

   public void StartGame()
    {
        selectPanel.SetActive(true);
    }

    public void DefensiveGame()
    {
        AudioManager.Instance.PlayeSFX(0);

        SceneManager.LoadScene(defensiveBattleSceneName);
    }

    public void OffensiveGame()
    {
        AudioManager.Instance.PlayeSFX(0);

        SceneManager.LoadScene(offensiveBattleSceneName);

    }

    public void CrazyGame()
    {
        AudioManager.Instance.PlayeSFX(0);

        SceneManager.LoadScene(crazyBattleSceneName);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayBattleSelectSong()
    {
        AudioManager.Instance.PlaySelectPanelMusic();

        AudioManager.Instance.PlayeSFX(0);

    }
}
