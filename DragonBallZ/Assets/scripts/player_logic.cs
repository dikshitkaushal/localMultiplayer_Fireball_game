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
    Animator m_animator;
    CharacterController m_charactercontroller;
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
    [SerializeField] playerid m_playerid;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalaxis = Input.GetAxis("Horizontal"+m_playerid);
        m_verticalaxis = Input.GetAxis("Vertical"+m_playerid);
        if(m_animator)
        {
            m_animator.SetFloat("movement", Mathf.Max(Mathf.Abs(m_horizontalaxis), Mathf.Abs(m_verticalaxis)));
        }
        if(Input.GetButtonDown("Jump"+m_playerid) && m_charactercontroller.isGrounded)
        {
            isjumping = true;
        }
    }
    private void FixedUpdate()
    {
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

        m_horizontalmove =  m_horizontalaxis * speed * Time.deltaTime;
        m_verticalmove = m_verticalaxis * speed * Time.deltaTime;
        m_totalmove = new Vector3(m_horizontalmove, 0, m_verticalmove);
        if (m_totalmove!=Vector3.zero)
        {
            transform.forward = m_totalmove.normalized;
            
        }
        if (m_charactercontroller)
        {
            m_charactercontroller.Move(m_totalmove + m_jump);
        }


        if (m_charactercontroller.isGrounded)
        {
            m_jump.y = 0;
        }

    }
}
