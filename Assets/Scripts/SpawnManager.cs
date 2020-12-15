using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab; // Clones da naveinimiga
    [SerializeField]
    private GameObject[] _powerupsPrefab; //powerups

    //UIManager
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    void Update()
    {
        if (_uiManager._menuOn == true)
        {
            DestroyImmediate(this.gameObject); //Destroi o gerenciador de spawn no game over para que não fique gerando mais elementos desnecessarios após a morte do player
        }
    }

    public IEnumerator SpawnEnemyRoutine() //Método para spawn de inimigos
    {
        while (true)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.75f, 7.75f), 6.5f, 0), Quaternion.identity); //Spawn dos inimigos
            yield return new WaitForSeconds(5.0f); //tempo entra os spawns de inimigos
        }
    }

    public IEnumerator SpawnPowerupsRoutine()
    {
        int typePowerup; 
        while (true)
        {
            typePowerup = Random.Range(0, 3);
            yield return new WaitForSeconds(15.0f); //Tempo de spawn entre os power ups 
            Instantiate(_powerupsPrefab[typePowerup], new Vector3(Random.Range(-7.75f, 7.75f), 6.5f, 0), Quaternion.identity); //a cada 15segundos spawna um powerup aleatório
        }
    }
}
