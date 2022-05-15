using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICode : MonoBehaviour
{
    //Player need to import TextMeshPro essentials;

    TextMeshProUGUI uiGoalstxt;
    TextMeshProUGUI uiPointstxt;
    GameObject UIGoals;
    GameObject UIPoints;
    GameObject supplyCarrot;
    GameObject supplyKale;

    int numCarrots;
    int numKales;


    public int numCarrotsCooked;

    bool newGoal;
    // Start is called before the first frame update
    void Start()
    {
        UIGoals = GameObject.Find("UIGoals");
        UIPoints = GameObject.Find("UIPoints");
        uiGoalstxt = UIGoals.GetComponent<TextMeshProUGUI>();
        uiPointstxt = UIPoints.GetComponent<TextMeshProUGUI>();

        numCarrotsCooked = 0;

        newGoal = false;

        uiGoalstxt.text = "Cook " + numCarrots + " carrots and " + numKales + " kales";
        uiPointstxt.text = "You have cooked " + numCarrots;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enable Debug Button 1"))
        {
            newGoal = true;
            GenerateGoals();
            uiGoalstxt.text = "Cook " + numCarrots + " carrots and " + numKales + " kales";
        }

        uiPointstxt.text = "You have cooked " + numCarrotsCooked;
    } 
        
    void AddPoints()
        {
           // if()

        }
        
     void OnTriggerEnter(Collider other)
    { 
            Collider othercollider = other.GetComponent<Collider>();

    }

    int GenerateGoals() 
    {
        if(newGoal == true)
        {
            numCarrots = Random.Range(0, 5);
            numKales = Random.Range(0, 5);
            
            newGoal = false;

            return numCarrots + numKales;
        }
        else{
            return numCarrots + numKales;
        }                         
    }
}
