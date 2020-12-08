using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{
    HiveTextures hive;
    [SerializeField] GameObject beePrefab;
    GameObject[] bees;


    [SerializeField]
    AudioClip working;
    [SerializeField]
    AudioClip pop;
    [SerializeField]
    AudioClip push;

    AudioSource audio;

    int inHive;
    float counter;

    void Start()
    {
        bees = new GameObject[3];
        inHive = bees.Length;
        counter = 10;
        for (int i = 0; i < bees.Length; i++) 
        {
            bees[i] = GameObject.Instantiate(beePrefab);
            bees[i].SetActive(false);
            bees[i].GetComponent<BeeController>().Hive = this;
        }
        hive = GetComponent<HiveTextures>();
        audio = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);
        }
    }

    void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
        if (counter > 20 && inHive > 0 && Random.Range(0f, 100f) < 5f) 
        {
            PopBee();
            counter = 0;
        }
        if (inHive > 0 && Random.Range(0f, 100f) < 0.4f) 
        {
            audio.PlayOneShot(working, 1);
        }
    }

    void PopBee() 
    {
        inHive--;
        for (int i = 0; i < bees.Length; i++)
        {
            if (!bees[i].activeInHierarchy)
            {
                bees[i].transform.position = transform.position;
                bees[i].SetActive(true);
                audio.PlayOneShot(pop, 1f);
                break;
            }
        }
    }

    public void PushBee() 
    {
        inHive++;
        hive.FillHive();
        audio.PlayOneShot(push, 1f);
        counter = 0;
    }
}
