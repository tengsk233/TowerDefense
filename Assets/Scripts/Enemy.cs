using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = 10;
    public float hp = 150;
    private float totalHp;
    public GameObject explosionEffect;
    private Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    public double attackRateTime = 1;//多少秒攻击一次
    public int damage = 20;
    public List<GameObject> Turrets = new List<GameObject>();
    private double timer = 0;
    // Use this for initialization
    void Start()
    {
        timer = attackRateTime;
        positions = Waypoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Turrets.Count > 0 && timer >= attackRateTime)
        {
            timer -= attackRateTime;
            Attack();
        }
        Move();
    }
    void Attack()
    {
        if (Turrets.Count > 0)
        {
            for (int i = 0; i < Turrets.Count; i++)
            {
                if (Turrets[i] != null)
                    Turrets[i].GetComponent<Turret>().TakeDamage(damage);
            }
            UpdateTurrets();
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateTurrets()
    {
        //Turrets.RemoveAll(null);
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < Turrets.Count; index++)
        {
            if (Turrets[index] = null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            Turrets.RemoveAt(emptyIndex[i] - i);
        }
    }
    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    //达到终点
    void ReachDestination()
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }


    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Turret")
        {
            Turrets.Add(col.gameObject);
        }
    }
    void OntriggerExit(Collider col)
    {
        if (col.tag == "Turret")
        {
            Turrets.Remove(col.gameObject);
        }
    }


}
