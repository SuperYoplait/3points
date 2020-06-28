﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidPott : MonoBehaviour
{
    public Transform _playerTransform;
    public Image HPBar;
    public Canvas HPCanvas;
    
    private Animator _animator;
    private float moveSpeed = 1f;
    public int statement = 0;  // 0: idle, 1: walk, 2: attack, 3: hit, 4: die
    private int walkState = 0;  // 0: left walk, 1: right walk
    public bool isCollide = false; // t: collide, f: not collide
    private bool hitState = false;  // t: 맞고있을때, 무적, f: 평상시, 맞을수 있음

    public static float HP = 100.0f;
    private float currentHP;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        _animator = GetComponent<Animator>();
        _animator.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {
        // 맞을 때
        if(currentHP != HP) {
            currentHP = HP;
            HPBar.fillAmount = HP / 100f;
            statement = 3;
        }
        // 죽을 때
        if(HP <= 0) {
            statement = 4;
        }

        if(!hitState && Input.GetKeyDown(KeyCode.X)) {
            hitState = true;
            Hit();
        }

    }

    void FixedUpdate() 
    {    
        if(statement == 0 && !isCollide && !hitState) {
            Idle();
        }
        else if(statement == 1 && !isCollide && !hitState) {
            Walk();
        }
        else if(statement == 2 && !hitState) {
            Attack();
        }
        else if(statement == 3) {
            _animator.Play("hit");
        }
        else if(statement == 4) {
            _animator.Play("die");
        }

    }

    void Idle()
    {
        Vector3 currentVector = transform.localScale;
        Vector3 currentCanvasVector = HPCanvas.transform.localScale;
        transform.localScale = currentVector;
        HPCanvas.transform.localScale = currentCanvasVector;
        _animator.Play("idle");
    }
    void Walk()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(walkState == 0) {    // 왼쪽으로 걸어갈 때
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(0.2f, 0.2f);
            HPCanvas.transform.localScale = new Vector3(0.0463f, 0.0463f);
        }
        else if(walkState == 1) {   // 오른쪽으로 걸어갈 때
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-0.2f, 0.2f);
            HPCanvas.transform.localScale = new Vector3(-0.0463f, 0.0463f);
        }
        _animator.Play("walk");
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }
    void Attack()
    {
       float dirX = _playerTransform.position.x - transform.position.x;

        if(dirX > 0) {  // 플레이어가 왼쪽이면
            transform.localScale = new Vector3(-0.2f, 0.2f);    // 왼쪽보고
            HPCanvas.transform.localScale = new Vector3(-0.0463f, 0.0463f);
        }
        else if(dirX < 0) { // 플레이어가 오른쪽이면
            transform.localScale = new Vector3(0.2f, 0.2f);   // 오른쪽보고
            HPCanvas.transform.localScale = new Vector3(0.0463f, 0.0463f);
        }
        _animator.Play("attack");
    }
    void Hit()
    {
        hitState = true;
        HP -= 10f;
    }
    void Die()
    {
        Destroy(this.gameObject);
    }

    void IdleWalkStateChange()  // idle, walk상태 결정하기
    {
        statement = Random.Range(0, 2); // 0~1사이 랜덤값 statement로 지정
    }
    void WalkStateChange()  // left, rightwalk 결정하기
    {
        walkState = Random.Range(0, 2);
    }

    void StatementChange(int index)
    {
        statement = index;
        hitState = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Bullet") {
            Hit();
        }
    }
}
