using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_logic : MonoBehaviour
{
    playerid m_playerid;
    float timer;
    float Max_time = 2.0f;
    Rigidbody m_rigidbody;
    private float speed = 15;
    [SerializeField] ParticleSystem exposion;
    [SerializeField] ParticleSystem exposion1;
    [SerializeField] ParticleSystem exposion2;
    [SerializeField] ParticleSystem exposion3;
    [SerializeField] GameObject ball;


    // Start is called before the first frame update
    void Start()
    {
        timer = Max_time;
        m_rigidbody = GetComponent<Rigidbody>();
        if(m_rigidbody)
        {
            m_rigidbody.velocity = transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            player_logic m_playerlogic = other.GetComponent<player_logic>();
            if(m_playerlogic)
            {
                m_playerlogic.die();
            }
            exposion.Play(true);
            ball.SetActive(false);
            m_rigidbody.velocity = Vector3.zero;
        }
        if (other.gameObject.tag == "Fireball")
        {
            exposion1.Play(true);
            exposion2.Play(true);
            exposion3.Play(true);
            ball.SetActive(false);
            m_rigidbody.velocity = Vector3.zero;
        }
    }
}
