using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _levelText;
    [SerializeField] private GameObject[] _gamePrototype;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _guide;
    [SerializeField] private MenuManager _menuManager;

    private GameObject game;
    private int _currentLevel;

    public Sprite playerSprite;
    public int money;

    private void Update()
    {
        if(game != null)
        {
            float direction = Input.GetAxis("Horizontal");
            game.transform.Rotate(new Vector3(0, 0, 60 * direction * -1 * Time.deltaTime));
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }
    public void Restart()
    {
        Play();
        _pausePanel.SetActive(false);
        Destroy(game);
        ShowGame(_currentLevel-1);
    }    
    public void Play()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }
    
    public void ToShop()
    {
        Play();
        _menuManager.ShowShop();
        GameOver(false);
    }
    public void ShowGame(int level)
    {
        
        _guide.SetActive(false);
        _gameScreen.SetActive(true);
        _menuScreen.SetActive(false);
        _pausePanel.SetActive(false);
        _currentLevel = level+1;
        if (_currentLevel == 1)
            _guide.SetActive(true);
        TextsUpdate();
        game = Instantiate(_gamePrototype[level], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        if(playerSprite != null)
        {
            Sphere sphere = FindObjectOfType<Sphere>();
            sphere.spriteRenderer.sprite = playerSprite;
        }
        
        
    }

    public void TextsUpdate()
    {
        _moneyText.text = money.ToString();
        _levelText.text = _currentLevel.ToString();
    }
    public void HideGame()
    {
        _gameScreen.SetActive(false);
        _menuScreen.SetActive(true);
        _pausePanel.SetActive(false);

    }
    
    public void GameOver(bool isCompleted)
    {
        Destroy(game);
        if(isCompleted == true && _currentLevel < _menuManager.levelsUnlocked.Count)
        {
            _menuManager.UnlockLevel(_currentLevel);
            money += 1000;
            TextsUpdate();
        }
        Play();
        HideGame();
    }
    
}
