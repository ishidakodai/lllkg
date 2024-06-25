using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField]Transform playerTransform;// �v���C���[��transform
    [SerializeField] GameObject _bullet; //�e�̃v���n�u 
    [SerializeField] float _bulletSpeed = 10f; //�e�̑��x
    [SerializeField] float _fireRate = 1f; //���˃��[�g
    [SerializeField] bool _isPlayerInRange = false; //�v���C���[���˒����ɓ��������ǂ���
    [SerializeField] AudioClip shotSE;//�e�o����

    private float _nextFireTime = 0f; //���̒e���o��܂ł̎���
    private AudioSource audioSource;


    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //�v���C���[�̃g�����X�t�H�[���擾
        if (playerTransform == null)
        {
            Debug.LogError("Player not found");
        }
    }

    void Update()
    {
        if (_isPlayerInRange && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    void Shoot()
    {
        if (playerTransform != null)
        {
            audioSource.PlayOneShot(shotSE);
            //�e�𐶐�
            GameObject bullet = (GameObject)Instantiate(_bullet, transform.position, Quaternion.identity);

            //�v���C���[�̕������v�Z
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            //���ɗ͂�������
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * _bulletSpeed;
            }
            else
            {
                Debug.Log("bullet��Rigidbody������܂���");
            }


        }
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
}
