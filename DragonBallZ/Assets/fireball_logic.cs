using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_logic : MonoBehaviour
{
    Rigidbody m_rigidbody;
    private float speed = 25;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if(m_rigidbody)
        {
            m_rigidbody.velocity = transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
