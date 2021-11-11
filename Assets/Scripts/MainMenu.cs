using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button[] levelButtons = new Button[12];
    private int _openedLevels = 1;
    private const int LevelsCount = 12;
    void Start()
    {
        _openedLevels = PlayerPrefs.GetInt("openedLevels");

        for (int i = 1; i < levelButtons.Length; ++i)
            levelButtons[i].interactable = false;

        for (int i = 0; i < _openedLevels; ++i)
            levelButtons[i].interactable = true;
    }

    public void Reset()
    {
        for (int i = 1; i < levelButtons.Length; i++)
            levelButtons[i].interactable = false;
        PlayerPrefs.DeleteAll();
        _openedLevels = 1;
    }

    public void OpenAllLevels()
    {
        PlayerPrefs.SetInt("openedLevels", LevelsCount);
        for (int i = 1; i < levelButtons.Length; i++)
            levelButtons[i].interactable = true;
    }
}
