using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UIManager
    private UIManager _uiManager;

    // player
    [SerializeField]
    private GameObject _playerPrefab; //player que será instanciando toda vez que o jogo começar

    //Spawn Manager
    [SerializeField]
    private GameObject _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_uiManager._menuOn == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) //Caso o menu esteja ligado e o usuario aperte o botão esquerdo do mouse ou space
            {
                _uiManager.DisableMenu();  //disativa o menu
                _uiManager.ResetScore(); // reseta o scote
                Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity); //intancia o player
                Instantiate(_spawnManager, Vector3.zero, Quaternion.identity); //instancia um gerenciador de spawn
            }
        }
    }

}
