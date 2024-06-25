using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField]Transform playerTransform;// プレイヤーのtransform
    [SerializeField] GameObject _bullet; //弾のプレハブ 
    [SerializeField] float _bulletSpeed = 10f; //弾の速度
    [SerializeField] float _fireRate = 1f; //発射レート
    [SerializeField] bool _isPlayerInRange = false; //プレイヤーが射程内に入ったかどうか
    [SerializeField] AudioClip shotSE;//弾出す音

    private float _nextFireTime = 0f; //次の弾が出るまでの時間
    private AudioSource audioSource;


    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //プレイヤーのトランスフォーム取得
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
            //弾を生成
            GameObject bullet = (GameObject)Instantiate(_bullet, transform.position, Quaternion.identity);

            //プレイヤーの方向を計算
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            //球に力を加える
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * _bulletSpeed;
            }
            else
            {
                Debug.Log("bulletのRigidbodyがありません");
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
