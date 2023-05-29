using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _bullet = default;
    [SerializeField] private BulletManager _bulletManager = null;
    [SerializeField] private Transform _player = default;
    [SerializeField] private float _nearyDistance = 5;
    private float _timer = 1;

    [SerializeField] private int _life = 3;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().transform;
        _bulletManager = GameObject.FindObjectOfType<BulletManager>();
        _bulletManager.AddEnemy(transform);
    }

    void Update()
    {
        //bool isNearyPlayer = Mathf.Abs(transform.position.x - _player.position.x) <= _x
        //    && Mathf.Abs(transform.position.y - _player.position.y) <= _y; //“ñ“_ŠÔ‚Ì‹——£‚©‚ç”»’è‚µ‚½‚Ù‚¤‚ª‚æ‚³‚»‚¤

        bool isNearyPlayer = Vector3.Distance(transform.position, _player.transform.position) < _nearyDistance;

        if (isNearyPlayer)
        {
            _timer += Time.deltaTime;

            //‚±‚¤‚°‚«
            if (_timer >= 1)
            {
                Vector3 shotPos = new Vector3(transform.position.x - transform.localScale.x, transform.position.y, 0);
                var bullet = Instantiate(_bullet, shotPos, this.transform.rotation);
                _bulletManager.AddEnemyBullet(bullet);
                _timer = 0;
            }

        }

    }

    public void Damage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
        {
            Destroy(gameObject);
        }

    }

}
