using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text _price;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _shop;
    [SerializeField] private Button[] _levels;
    [SerializeField] private Button[] _buyButtons;
    [SerializeField] private Sprite[] _skins;

    private List<bool> _buttonSetted = new List<bool>(4) { false, false, false, false };
    private int _selectedSprite = -1;
    private Button _selected;

    public List<bool> levelsUnlocked = new List<bool>(4) {true, false, false, false };
    
    private void Awake()
    {
        _shop.SetActive(false);
        LevelsUpdate();
        _gameManager.HideGame();

        for (int i = 0; i < _buyButtons.Length; i++)
        {
            Button localButton = _buyButtons[i];
            _buyButtons[i].onClick.AddListener(() => SelectSpriteButton(localButton));
        }

    }
    private void SelectSpriteButton(Button button)
    {
        if(_selected != null)
            _selected.image.color = Color.white;
        _selectedSprite = int.Parse(button.transform.GetChild(0).name);
        button.image.color = Color.gray;
        _selected = button;
        _price.text = "500";
    }
    
    private void SetPlayerSprite(int id)
    {
        _gameManager.playerSprite = _skins[id];
        
    }
    public void BuySprite()
    {
        if (_gameManager.money >= 500 && _selectedSprite != -1)
        {
            SetPlayerSprite(_selectedSprite);
            _gameManager.money -= 500;
            _gameManager.TextsUpdate();
        }
    }
    public void ShowMain()
    {
        _shop.SetActive(false);
        _mainMenu.SetActive(true);
        _selectedSprite = -1;
        _price.text = "0";
    }
    public void ShowShop()
    {
        _shop.SetActive(true);
        _mainMenu.SetActive(false);
    }
    public void UnlockLevel(int id)
    {
        levelsUnlocked[id] = true;
        LevelsUpdate();
    }
    public void LevelsUpdate()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            Button localbutton = _levels[i];
            if (_buttonSetted[i] == false)
            {
                if (levelsUnlocked[i] == true)
                {
                    localbutton.onClick.AddListener(() => NewGameClick(localbutton));
                    _levels[i].transform.GetComponent<Image>().color = Color.gray;
                    _buttonSetted[i] = true;
                }
                else
                    _levels[i].transform.GetComponent<Image>().color = Color.black;
            }
        }
    }
    public void NewGameClick(Button button)
    {
        int levelId = int.Parse(button.transform.GetChild(1).name);
        _gameManager.ShowGame(levelId);
    }
}
