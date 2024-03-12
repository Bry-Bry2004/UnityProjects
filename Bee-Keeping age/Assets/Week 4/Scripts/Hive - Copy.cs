using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public float honeyProductionRate = 5f;
    public int startingNumberOfBees = 5;
    public Bee beePrefab;
    public int nectarCount = 0;
    public int honeyCount = 0;

    private float timer = 0f;
    private bool isCountingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < startingNumberOfBees; i++)
        {
            Bee newBee = Instantiate(beePrefab, transform.position, Quaternion.identity);
            newBee.Init(this); //uhhh
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(nectarCount > 0 && !isCountingDown)
        {
            isCountingDown = true;
            timer = honeyProductionRate;
        }
        if(isCountingDown)
        {
            timer -= Time.deltaTime;
            
            if(timer <= 0)
            {
                ProduceHoney();
            }
        }
    }

    void ProduceHoney()
    {
        if(nectarCount > 0)
        {
            nectarCount--;
            honeyCount++;

            if(nectarCount >  0)
            {
                timer = honeyProductionRate;
            }
            else
            {
                isCountingDown = false;
            }
        }
    }

    public void GiveNectar()
    {
        nectarCount++;
        if(!isCountingDown)
        {
            isCountingDown = true;
            timer = honeyProductionRate;
        }
    }
}
