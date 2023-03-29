using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("UI Manager is NULL.");
            }

            return _instance;
        }
    }

    [SerializeField]
    protected TMP_Text _playerDiamondCountText;
    [SerializeField]
    protected Image _selectionImage;
    [SerializeField]
    protected Image[] _lifeUnits;
    
    [SerializeField]
    private TMP_Text _diamondCount;
    public TMP_Text DiamondCount 
    {
        get { return _diamondCount; }
        set { _diamondCount = value; } 
    }

    public int PlayerDiamondCount
    {
        get
        {
            if (_playerDiamondCountText != null) 
            {
                return int.Parse(_playerDiamondCountText.text);
            } else { return 0; }
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int diamondCount)
    {
        _playerDiamondCountText.text = $"{diamondCount}G";
        _selectionImage.gameObject.SetActive(false);
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.gameObject.SetActive(true);
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateDiamondCount(int count)
    {
        DiamondCount.text = $"{count}";
    }

    public void UpdateLives(int livesRemaining)
    {
        switch(livesRemaining)
        {
            case 3:
                _lifeUnits[3].gameObject.SetActive(false); 
                break;
            case 2:
                _lifeUnits[2].gameObject.SetActive(false);
                break;
            case 1:
                _lifeUnits[1].gameObject.SetActive(false);
                break;
            case 0:
                _lifeUnits[0].gameObject.SetActive(false);
                break;
            default: break;
        }
    }
}
