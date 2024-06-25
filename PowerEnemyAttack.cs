using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parentObject;//親オブジェクト指定
    private PowerEnemy parentEnemy;

    private void Start()
    {
        parentEnemy = parentObject.GetComponent<PowerEnemy>();
        if(parentEnemy == null)
        {
            Debug.LogError("Parent object null");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)//プレイヤータグ取得
        {
            //親オブジェクトに攻撃指示
            parentObject.GetComponent<PowerEnemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //親オブジェクトに攻撃指示
            parentObject.GetComponent<PowerEnemy>().Attack(other.gameObject);
        }
    }
}
