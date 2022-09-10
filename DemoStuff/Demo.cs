using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    HealthBar healthBar;
    float maxhealth = 100;
    float health;
    float timer;
    bool full = true;
    float dir = 1;
    float sphere = 0;
    float cube = 0;
    string s = "";
    GameObject sphereGO, cubeGO;
    bool sphereB, cubeB;
    public CollisionObserver collionObserver;

    public Collider placementCollider => collionObserver.Collider;
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxhealth * 0.8f;
        healthBar.SetHealthImageColour(Color.green);
        //AudioManager.instance.Play("Music");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && full)
        {
            health = maxhealth * 0.4f;
            healthBar.SetHealthbar(health / maxhealth);
            timer += 8;
            full = !full;
            dir *= -1;
            AudioManager.instance.Play("Hit");
        }
        else if (timer <= 0 && !full)
        {
            health = maxhealth * 0.8f;
            healthBar.SetHealthbar(health / maxhealth);
            timer += 8;
            full = !full;
            dir *= -1;
            AudioManager.instance.Play("Life");
        }
        Vector3 move = new Vector3(dir, 0, 0);
        transform.position += move * Time.deltaTime;

        foreach (Collider collider in collionObserver.Enter)
        {
            if (collider.gameObject.name.Contains("Plane"))
            {
                continue;
            }
            if (collider.gameObject.name.Contains("Sphere"))
            {
                sphereGO = collider.gameObject;
                sphere++;
                if (sphere == 1)
                {
                    AudioManager.instance.Play("Enable");
                }
            }
            if (collider.gameObject.name.Contains("Cube"))
            {
                cubeGO = collider.gameObject;
                cube++;
                if (cube == 1)
                {
                    AudioManager.instance.Play("Enable");
                }
            }
        }

        foreach (Collider collider in collionObserver.Exit)
        {
            if (collider.gameObject.name.Contains("Plane"))
            {
                continue;
            }
            if (collider.gameObject.name.Contains("Sphere"))
            {
                sphere--;
                if (sphere == 0)
                {
                    AudioManager.instance.Play("Disable");
                }
            }
            if (collider.gameObject.name.Contains("Cube"))
            {
                cube--;
                if (cube == 0)
                {
                    AudioManager.instance.Play("Disable");
                }
            }
        }

        UpdateStuff();
        healthBar.SetHealthBarName("Collision: " + s);
    }

    void UpdateStuff()
    {
        s = "";
        if (sphere > 0)
        {
            s += "Sphere ";
            Renderer m = sphereGO.GetComponent<Renderer>();
            if (m != null)
            {
                m.material.color = Color.green;
            }
        }
        else
        {
            if (cubeGO != null)
            {
                Renderer m = sphereGO.GetComponent<Renderer>();
                if (m != null)
                {
                    m.material.color = Color.red;
                }
            }
        }

        if (cube > 0)
        {
            s += "Cube ";
            Renderer m = cubeGO.GetComponent<Renderer>();
            if (m != null)
            {
                m.material.color = Color.green;
            }
        }
        else
        {
            if (cubeGO != null)
            {
                Renderer m = cubeGO.GetComponent<Renderer>();
                if (m != null)
                {
                    m.material.color = Color.red;
                }
            }
        }
    }
}
