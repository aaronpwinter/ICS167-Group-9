using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_MaxHealth;

    //Should be private in future
    public float m_CurrentHealth;

    private UIUnitCentralPublisher UIPublisher;


    void Start()
    {
        m_MaxHealth = gameObject.GetComponent<UnitScript>().unitData.maxHealth;
        m_CurrentHealth = m_MaxHealth;
        UIPublisher = GetComponent<UIUnitCentralPublisher>();
        UIPublisher.setMaxHealth(m_MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_CurrentHealth == 0)
        {
            //Death part
            GetComponent<UnitScript>().destroy();
        }
    }

    public void Damage(float d)
    {
        if (gameObject.GetComponent<UnitScript>().unitData.maxHealth != m_MaxHealth)
        {//Check if the Scriptable Object has been changed for whatever reason
            m_MaxHealth = gameObject.GetComponent<UnitScript>().unitData.maxHealth;
            m_CurrentHealth = m_MaxHealth;
        }
        m_CurrentHealth = Math.Max(m_CurrentHealth - d, 0);

        // Notify the UIUnitCentralPublisher's subscribers
        UIPublisher.substractHealth(d);
    }
}