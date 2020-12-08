using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public NestController Hive;
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    float range = 3;
    bool pollinated;
    [SerializeField]
    Material Normal;
    [SerializeField]
    Material Nectar;
    [SerializeField]
    GameObject[] BeeParts;
    bool wandering;
    Vector3 wanderDir;


    float counter;
    float wait;

    void Start()
    {
        wandering = true;
        pollinated = false;
        counter = Random.Range(0f, 20f);
        wait = Random.Range(3, 6f); ;
        newWander();
        retexture();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void newWander()
    {
        wanderDir = new Vector3(Random.Range(-range + Hive.transform.position.x, range + Hive.transform.position.x),
            Random.Range(0 + Hive.transform.position.y, (range*2) + Hive.transform.position.y),
            Random.Range(-range + Hive.transform.position.z, range + Hive.transform.position.z));
    }

    void move()
    {
        Vector3 other = transform.position;
        float speedModifier = 1;
        if (pollinated) 
        {
            other = Hive.transform.position;
            if (Vector3.Distance(transform.position, other) < 0.3f)
            {
                pollinated = false;
                retexture();
                Hive.PushBee();
                gameObject.SetActive(false);
            }
        } 
        else 
        {
            if (wandering)
            {
                counter -= Time.fixedDeltaTime;
                other = wanderDir;
                if (Vector3.Distance(transform.position, other) < 0.3f)
                {
                    wait -= Time.fixedDeltaTime;

                    if (wait <= 0)
                    {
                        newWander();
                        wait = Random.Range(3, 6f);
                    }
                }
                if (counter <= 0) 
                {
                    wandering = false;
                    newWander();
                    counter = 0;
                }
            }
            else 
            {
                GameObject poppy = GameObject.FindGameObjectWithTag("Poppy");
                if (poppy == null) 
                {
                    wandering = true;
                    return;
                }
                other = poppy.transform.position;

                if (Vector3.Distance(transform.position, other) < 0.8f)
                    speedModifier = 0;
                if (Vector3.Distance(transform.position, other) < 1f) 
                {
                    counter += Time.fixedDeltaTime;
                    if (counter > 10) 
                    {
                        counter = Random.Range(0f,20f);
                        pollinated = true;
                        wandering = true;
                        retexture();
                    }
                }
            }
        }
        Vector3 direction = other - transform.position;
        if(Vector3.Distance(transform.position, other) > 0.001f)
            transform.position += direction.normalized * speed * Time.fixedDeltaTime * speedModifier;
        if (Vector3.Distance(transform.position, other) > 1f)
        {
            transform.LookAt(other);
            gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    void retexture() 
    {
        Material material = Normal;
        if (pollinated)
            material = Nectar;
        foreach (GameObject part in BeeParts)
        {
            part.GetComponent<SkinnedMeshRenderer>().material = material;
        }
    }
}
