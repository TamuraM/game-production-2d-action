using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    private InputAction _bangAction, _moveAction, _jumpAction, _shotAction;
    [SerializeField] private GameObject _bullet = default;
    [SerializeField] private GameObject _laser = default;
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private bool _isShotingLaser = false;

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _life = 5;

    [SerializeField] private byte _isJumping = 0b0000_0000;
    private float _minY = default;
    [SerializeField] private float _maxJumpPower = 2;
    [SerializeField] private float _nowJumpPower = 2;
    [SerializeField] private float _antiJumpPower = 2;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        _bangAction = actionMap["Bang"];
        _moveAction = actionMap["Move"];
        _jumpAction = actionMap["Jump"];
        _shotAction = actionMap["Shot"];

        _bangAction.performed += Bang;
        _jumpAction.performed += Jump;
        _shotAction.performed += Shot;

        _nowJumpPower = _maxJumpPower;
        _minY = transform.position.y;
    }

    private void Update()
    {
        float moveX = transform.position.x + _moveAction.ReadValue<float>() * _speed * Time.deltaTime;
        float moveY = transform.position.y;

        if (_isJumping > 0) //ジャンプしている間
        {
            //参考：https://www.youtube.com/watch?v=zxOBCjqP8-Y&list=PLi8SA3sbzYVSQmkQUa-zz7BcKL8YWAt8a&index=3
            moveY += _nowJumpPower * Time.deltaTime;
            _nowJumpPower -= _antiJumpPower * Time.deltaTime;

            if (transform.position.y < _minY)
            {
                moveY = _minY;
                _nowJumpPower = _maxJumpPower;
                _isJumping = 0;
            }

        }

        transform.position = new Vector3(moveX, moveY, 0);
    }

    public void Damage(int damage)
    {
        _life -= damage;
    }

    //通常弾を撃つ
    private void Bang(InputAction.CallbackContext context)
    {
        if (_isShotingLaser) return;

        Vector3 bulletShotPos = transform.position + transform.right * transform.localScale.x;
        var bullet = Instantiate(_bullet, bulletShotPos, this.transform.rotation);
        _bulletManager.AddPlayerBullet(bullet);
    }

    //ジャンプする
    private void Jump(InputAction.CallbackContext context)
    {

        if((_isJumping & 1) == 0) //まだジャンプしてなかったら
        {
            _isJumping = 1; //一度ジャンプしたことにする
            return;
        }

        if((_isJumping & 3) == 1) //すでに一度ジャンプしていたら
        {
            _isJumping = 3; //二段ジャンプしてることにする
            _nowJumpPower = _maxJumpPower;
            return;
        }

    }

    //ビームを撃つ
    private void Shot(InputAction.CallbackContext context)
    {
        if (_isShotingLaser) return;
        StartCoroutine(LaserShot(12));
        Debug.Log("ビーム");
    }

    private IEnumerator LaserShot(int waitFrame)
    {
        //参考：https://qiita.com/toRisouP/items/e6d4f114d434ee588044
        var shotLaser = Instantiate(_laser, transform.position, transform.rotation);
        _bulletManager.SetLaser(shotLaser.transform);
        _isShotingLaser = true;

        for(int i = 0; i < waitFrame; i++)
        {
            yield return null;
        }

        Destroy(shotLaser);
        _isShotingLaser = false;
    }

}
