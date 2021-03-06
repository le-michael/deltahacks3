﻿using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
    public float speed;
    private Transform camPos;
    public float lifeAfterHit;

    private PlayerHealth playerhealth;
    private bool hitCam;
    private Vector3 direction;
    private Vector3 rotateDir;
    private GameManager gm;

	// Use this for initialization
	void Start () {
        camPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerhealth = GameObject.Find("PlayerManager").GetComponent<PlayerHealth>();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rotateDir = new Vector3(15, 35, 30);
        direction = (camPos.position - transform.position).normalized;
        //lifeAfterHit = 2f;
        speed = 10f;
        hitCam = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 targetDir = camPos.position - transform.position;
        transform.Rotate(rotateDir * speed* Time.deltaTime);
        if (hitCam == true)
        {
            return;
        }

        if ((camPos.position - transform.position).normalized != direction)
        {
            hitCam = true;
            HitPlayer();
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            hitCam = true;
            playerhealth.GotHit();
            HitPlayer();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Hand")
        {
            gm.AddScore(1);
        }
    }

   

    void HitPlayer()
    {
        //Debug.Log("Hit camera!");
        Invoke("DestroyAsteroid", lifeAfterHit);
    }

    void DestroyAsteroid()
    {
        Destroy(gameObject);
    }
}
