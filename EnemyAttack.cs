using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parentObject;//親オブジェクト指定
    private Enemy parentEnemy;

    private void Start()
    {
        parentEnemy = parentObject.GetComponent<Enemy>();
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
            parentObject.GetComponent<Enemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //親オブジェクトに攻撃指示
            parentObject.GetComponent<Enemy>().Attack(other.gameObject);
        }
    }
}
