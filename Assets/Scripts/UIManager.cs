using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // BIblioteca para usar os elementos e métodos de uma UI

public class UIManager : MonoBehaviour
{
    //lives
    [SerializeField]
    private Sprite[] livesPlayer; // array com todas as imagens de vidas possiveis do player (0-3)
    [SerializeField]
    private Image livesImageDisplay; //imagem que será mostrada no display e atualizará sempre que o player tomar dano

    //score
    private int score = 0;
    [SerializeField]
    private Text textScore;

    //MainMenu
    [SerializeField]
    private GameObject _mainMenu;
    public bool _menuOn;

    void Start()
    {
        _menuOn = true;
    }

    public void UpdateLives(int currentLives) //Método para atualizar a vida do player na tela
    {
        livesImageDisplay.sprite = livesPlayer[currentLives]; //mostra na tela a imagem selecionada de acordo com o current live (proveniente do player)
    }

    public void UpdateScore() //Método para aumenta ros pontos
    {
        score += 10; // aumenta os pontos em 10
        textScore.text = "Score: " + score; // exibe os pontos na tela
    }

    public void ResetScore()
    {
        score = 0; //zera o score
        textScore.text = "Score: " + score; //printa ele na tela
    }
    
    public void EnableMenu() //ativa o menu e muda a variavel menuOn
    {
        _mainMenu.SetActive(true);
        _menuOn = true;
    }

    public void DisableMenu() // desativa o menu e muda a variavel menuOn
    {
        _mainMenu.SetActive(false);
        _menuOn = false;
    }

}
