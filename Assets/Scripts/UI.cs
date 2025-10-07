using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("Healthbar")]
    [SerializeField] private Image _health;
    public Player_Movement Player_Movement; /// to find Player////

    [Header ("Crystals&Score")]
    public Text _crystals;
    private int _score;

    [Header("Restart")]
    public GameObject _panel;
    public Button _restart;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Player_Movement = FindObjectOfType<Player_Movement>();
        _crystals.text = _score.ToString();
       
    }
 
    void Update()
    {
        _health.fillAmount = Player_Movement.Health.currenthealth / 100f;
    }

    public void OnCrystals()
    {
        _score +=1;
        _crystals.text = _score.ToString();
    }

    public void Restart()
    {
        _panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

}
