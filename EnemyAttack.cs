using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parentObject;//�e�I�u�W�F�N�g�w��
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
        if (other.CompareTag("Player") && parentEnemy != null)//�v���C���[�^�O�擾
        {
            //�e�I�u�W�F�N�g�ɍU���w��
            parentObject.GetComponent<Enemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //�e�I�u�W�F�N�g�ɍU���w��
            parentObject.GetComponent<Enemy>().Attack(other.gameObject);
        }
    }
}
