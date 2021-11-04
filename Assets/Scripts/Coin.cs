using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    public int CoinValue => coinValue;
    private CoinCounter _counter;

    void Start()
    {
        _counter = FindObjectOfType<CoinCounter>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")){
            _counter.AddValue(coinValue);
            Destroy(gameObject);
            _counter.CheckIfVictory();
        }
    }
}
