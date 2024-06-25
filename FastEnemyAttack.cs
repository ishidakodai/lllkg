using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parent;//親オブジェクト指定
    private FastEnemy parentEnemy;

    private void Start()
    {
        if (parent != null)
        {
            parentEnemy = parent.GetComponent<FastEnemy>();
            if (parentEnemy == null)
            {
                Debug.LogError("親オブジェクトにFastEnemyコンポーネントが見つかりません");
            }

        }
        else
        {
            Debug.LogError("親オブジェクトが指定されていません");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null) // プレイヤータグ取得と親オブジェクトの存在チェック
        {
            //親オブジェクトに攻撃指示
            parent.GetComponent<FastEnemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //親オブジェクトに攻撃指示
            parent.GetComponent<FastEnemy>().Attack(other.gameObject);
        }
    }
}
