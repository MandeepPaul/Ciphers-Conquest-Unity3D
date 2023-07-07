using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _numberOfEnemies = 12;
    [SerializeField] private float x;

    private GameObject _enemy;


    void Update() 
    {
        if (_enemy == null) 
        {
            StartCoroutine(spawnEnemy());
        }
    }

    IEnumerator spawnEnemy()
    {
        float angle;
        
        for(int i = 0; i < _numberOfEnemies; i++)
        {
            _enemy = Instantiate(_enemyPrefab);
            _enemy.transform.position = new Vector3(x, 0, 0);
            _enemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);

            yield return new WaitForSeconds(1f);
        }     
    }

}
