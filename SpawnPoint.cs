using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]Transform playerTransform;
    [SerializeField] GameObject _enemy; //�G�̃v���n�u
    [SerializeField] float _SpawnRate = 1f; //�X�|�[�����[�g
    [SerializeField] bool _isPlayerInRange = false; //�v���C���[���˒����ɓ��������ǂ���
    [SerializeField] AudioClip shotSE;//�e�o����

    private float _nextFireTime = 0f; //���̓G���o��܂ł̎���
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //�v���C���[�̃g�����X�t�H�[���擾
    }

    void Update()
    {
        if (_isPlayerInRange && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / _SpawnRate;
        }
    }

    void Shoot()
    {
        if (playerTransform != null)
        {
            audioSource.PlayOneShot(shotSE);
            //�e�𐶐�
            GameObject instance = (GameObject)Instantiate(_enemy, transform.position, Quaternion.identity);

            ////�v���C���[�̕������v�Z
            //Vector3 direction = (_player.position - transform.position).normalized;

            ////���ɗ͂�������
            //bullet.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;
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
