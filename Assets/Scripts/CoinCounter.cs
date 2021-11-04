using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private GameObject winnerUIParent;
    [SerializeField] private GameObject inGameParent;
    [SerializeField] private LevelManager levelManager;
    private int _currentValue = 0;
    public int CurrentValue{
        get => _currentValue;
        set => _currentValue = value;    
    }
    private int _maxValue = 1;
    public int MaxValue{
        get => _maxValue;
        set => _maxValue = value;
    }

    // //Sauvegarde de currentValue
    // public int CurrentValue{
    //     get => PlayerPrefs.GetInt("currentValue",0);
    //     set => PlayerPrefs.SetInt("currentValue",value);
    // }

    public void SetCounter(Level level){
        _currentValue = 0;
        _maxValue = 0;
        foreach(Transform coin in level.LevelPrefab.transform){
            Debug.Log(coin.tag+ " "+coin.name);
            if (coin.tag == "Coin"){
                _maxValue+= coin.GetComponent<Coin>().CoinValue;
            }
        }
        SetCounterText();
     }

    public void SetCounterText(){
        counterText.text = _currentValue.ToString()+"/"+_maxValue.ToString();
    }

    public void AddValue(int valueToAdd){
        _currentValue += valueToAdd;
        SetCounterText();
    }

    public void SetMaxValue(int maxValue){
        _maxValue = maxValue;
    }

    public void CheckIfVictory(){
        if (_maxValue == _currentValue){
            inGameParent.SetActive(false);
            levelManager.DestroyLevels();
            winnerUIParent.SetActive(true);
        }
    }
}
