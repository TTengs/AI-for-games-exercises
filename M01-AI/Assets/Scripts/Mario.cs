using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : Agent {

    public int score = 0;
    public int states = 0;

    protected override void FiniteStateMachine()
    {
        switch (states)
        {
            case 0:
                MoveTo(GetClosestCoin());
                break;
            case 1:
                Debug.Log("Mario wins, score = " + score);
                break;
        }
    }

    Transform GetClosestCoin()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        float minDistance = float.PositiveInfinity;
        GameObject closestCoin = null;
        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(coin.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = coin;
            }
        }
        return closestCoin.transform;
    }

    private void killBoos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Boo");
        foreach (GameObject boo in enemies)
        {
            Destroy(boo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            score++;
            Debug.Log("Got a coin!");
            Destroy(other.gameObject);
            if (score == 5)
            {
                killBoos();
                states = 1;
            }
        }
        if (other.gameObject.tag == "Boo")
        {
            Debug.Log("Mamma mia! [score:" + score + "]");
            Destroy(gameObject);
        }
    }
}
