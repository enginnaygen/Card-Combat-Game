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
        
    }

   public void StartGame()
    {
        selectPanel.SetActive(true);
    }

    public void DefensiveGame()
    {
        SceneManager.LoadScene(defensiveBattleSceneName);
    }

    public void OffensiveGame()
    {
        SceneManager.LoadScene(offensiveBattleSceneName);

    }

    public void CrazyGame()
    {
        SceneManager.LoadScene(crazyBattleSceneName);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
