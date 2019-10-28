using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int score;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddToScore(int scoreToAdd)
    {
        // adds to current score
        score += scoreToAdd;
    }
}//
