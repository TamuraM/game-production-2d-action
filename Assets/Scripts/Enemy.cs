using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _bullet = default;
    [SerializeField] private Transform _player = default;
    [SerializeField] private float _x = 5;
    [SerializeField] private float _y = 5;
    private float _timer = 1;

    [SerializeField] private int _life = 3;

    private void Start()
    {
        
        if(_player == null)
        {
            _player = GameObject.FindObjectOfType<Player>().transform;
        }

    }

    void Update()
    {
        bool isNearyPlayer = Mathf.Abs(transform.position.x - _player.position.x) <= _x
            && Mathf.Abs(transform.position.y - _player.position.y) <= _y;

        if (isNearyPlayer)
        {
            _timer += Time.deltaTime;

            //‚±‚¤‚°‚«
            if(_timer >= 1)
            {
                Vector3 shotPos = new Vector3(transform.position.x - transform.localScale.x, transform.position.y, 0);
                Instantiate(_bullet, shotPos, this.transform.rotation);
                _timer = 0;
            }

        }

    }

    public void Damage(int damage)
    {
        _life -= damage;

        if( _life <= 0 )
        {
            Destroy(gameObject);
        }

    }

}
