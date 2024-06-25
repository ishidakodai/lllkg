using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parentObject;//�e�I�u�W�F�N�g�w��
    private NomalEnemy parentEnemy;

    private void Start()
    {
        parentEnemy = parentObject.GetComponent<NomalEnemy>();
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
            parentObject.GetComponent<NomalEnemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //�e�I�u�W�F�N�g�ɍU���w��
            parentObject.GetComponent<NomalEnemy>().Attack(other.gameObject);
        }
    }
}
