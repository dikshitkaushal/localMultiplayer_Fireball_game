using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerid
{
    _P1,
    _P2,
}

public class player_logic : MonoBehaviour
{
    public ParticleSystem continuouspower1;
    public ParticleSystem boostuppower1;

    public ParticleSystem continuouspower2;
    public ParticleSystem boostuppower2;
    Animator m_animator;
    public GameObject target1_p1;
    public GameObject target2_p2;
    public GameObject fireball;
    public CharacterController m_charactercontroller;
    float m_horizontalmove;
    float m_verticalmove;
    Vector3 m_jump;
    Vector3 m_totalmove;
    float m_jumpheight = 0.25f;
    float m_gravity = 0.97f;
    float m_horizontalaxis;
    float m_verticalaxis;
    bool isjumping = false;
    private float speed = 5f;
    bool issliding = false;
    bool isdead = false;
    [SerializeField] playerid m_playerid = playerid._P1;
    bool iscasting = false;
    public GameObject slidingpos;
    bool isboostup = false;
    public GameObject slidingpos2;
    float respawntime = 3;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        /*
        boostuppower1.enableEmission = false;
        boostuppower2.enableEmission = false;*/
        timer = respawntime;
        m_animator = GetComponent<Animator>();
        m_charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isdead)
        {
            if (m_playerid == playerid._P1)
            {
                continuouspower1.Stop();
            }
            if (m_playerid == playerid._P2)
            {
                continuouspower2.Stop();
            }
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                respawn();
            }
            return;
        }
        if (!issliding)
        {
            m_horizontalaxis = Input.GetAxis("Horizontal" + m_playerid);
            m_verticalaxis = Input.GetAxis("Vertical" + m_playerid);
            if (m_animator)
            {
                m_animator.SetFloat("movement", Mathf.Max(Mathf.Abs(m_horizontalaxis), Mathf.Abs(m_verticalaxis)));
            }
            if (Input.GetButtonDown("Jump" + m_playerid) && m_charactercontroller.isGrounded)
            {
                isjumping = true;
            }
            if (Input.GetButtonDown("Fire1" + m_playerid))
            {
                iscasting = true;
                m_animator.SetTrigger("fireball");
            }
            if (Input.GetButtonDown("Fire3" + m_playerid))
            {
                isboostup = true;
                if (m_playerid == playerid._P1)
                {
                    boostuppower1.Play();
                }
                if (m_playerid == playerid._P2)
                {
                    boostuppower2.Play();
                }
                m_animator.SetTrigger("boostup");
            }
        }

        if (Input.GetButtonDown("Fire2" + m_playerid))
        {
            m_charactercontroller.enabled = false;
            issliding = true;
            m_animator.SetTrigger("slide");
        }
        if (issliding)
        {
            if (m_playerid == playerid._P1)
            {
                continuouspower1.Stop();
            }
            if (m_playerid == playerid._P2)
            {
                continuouspower2.Stop();
            }
        }
        /*        if (!issliding)
                {
                    continuouspower1.enableEmission = true;
                    continuouspower2.enableEmission = true;
                }*/
        /*if (issliding)
        {
            transform.position += transform.forward * Time.deltaTime * 5;
        }*/

    }

    private void respawn()
    {
        if (m_playerid == playerid._P1)
        {
            continuouspower1.Play();
        }
        if (m_playerid == playerid._P2)
        {
            continuouspower2.Play();
        }
        timer = respawntime;
        isdead = false;
        m_charactercontroller.enabled = true;
        if (m_animator)
        {
            m_animator.SetTrigger("respawn");
        }
    }

    private void FixedUpdate()
    {
        /*if(m_playerid==playerid._P1)
        {
            Vector3 direction = target2_p2.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.124f);
        }
        if (m_playerid == playerid._P2)
        {
            Vector3 direction = target1_p1.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.124f);
        }*/
        //jumping logic
        if (isjumping)
        {
            isjumping = false;
            m_jump.y = m_jumpheight;
        }
        m_jump.y -= m_gravity * Time.deltaTime;



        /* if (!m_charactercontroller.isGrounded)
         {
             if (m_jump.y > 0)
             {
                 m_jump.y -= m_gravity * Time.deltaTime;
             }
             else
             {
                 m_jump.y -= m_gravity * Time.deltaTime * 2;
             }

         }

         if (isjumping)
         {
             m_jump.y = m_jumpheight;
             isjumping = false; ;
         }
         Debug.Log(m_jump.y);
 */
        //moving logic

        m_horizontalmove = m_horizontalaxis * speed * Time.deltaTime;
        m_verticalmove = m_verticalaxis * speed * Time.deltaTime;
        m_totalmove = new Vector3(m_horizontalmove, 0, m_verticalmove);
        /*if (issliding)
        {
            
        }*/
        if (m_totalmove != Vector3.zero)
        {

            transform.forward = m_totalmove.normalized;

        }


        if (iscasting || isboostup)
        {
            m_totalmove = Vector3.zero;
        }

        if (m_charactercontroller && !issliding)
        {
            m_charactercontroller.Move(m_totalmove + m_jump);
        }


        if (m_charactercontroller.isGrounded)
        {
            m_jump.y = 0;
        }

    }
    public void iscastingfirvall(bool casting)
    {
        iscasting = casting;
    }
    public void ischaractercontroller(bool iscontrolled)
    {
        if (m_playerid == playerid._P1)
        {
            transform.position = slidingpos.transform.position;
        }
        else if (m_playerid == playerid._P2)
        {
            transform.position = slidingpos2.transform.position;
        }
        issliding = iscontrolled;
        if (m_playerid == playerid._P1)
        {
            continuouspower1.Play();
        }
        if (m_playerid == playerid._P2)
        {
            continuouspower2.Play();
        }
        if (!issliding)
        {
            m_charactercontroller.enabled = true;
        }


    }
    public void fireballthrow()
    {
        if (m_playerid == playerid._P1)
        {
            Instantiate(fireball, target1_p1.transform.position, transform.rotation);
        }
        else if (m_playerid == playerid._P2)
        {
            Instantiate(fireball, target2_p2.transform.position, transform.rotation);
        }
    }
    public void die()
    {
        isdead = true;

        if (m_animator)
        {
            m_animator.SetTrigger("die");
        }
    }
    public void ispowerup(bool powerup)
    {
        isboostup = powerup;
        if (!isboostup)
        {
            if (m_playerid == playerid._P1)
            {
                boostuppower1.Stop();
                
            }
            if (m_playerid == playerid._P2)
            {
                boostuppower2.Stop();
            }
        }
    }
}
