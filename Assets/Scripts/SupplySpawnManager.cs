using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SupplySpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject supply;
    [SerializeField]
    int NumberOfSupplies;
       

    private List<GameObject> supplies = new List<GameObject>();


    void Start()
    {             
        for (int i = 0; i < NumberOfSupplies; i++)      
        {            
                DoSpawnSupply();
        }
    }
  
    void Update()
    {
       

        if (Input.GetButtonDown("Jump"))
        {
            foreach (GameObject supply in supplies)
            {
            Destroy(supply);
            }          
        }

        if (Input.GetButtonDown("Enable Debug Button 1"))
        {          
            foreach (GameObject supply in supplies)
            {
                Debug.Log(supply.ToString());
            }
        }
        DestroyObject();
    }

   

    void DoSpawnSupply()
    {
        //what is happening here is that im instantiating the supply where there is a Navmesh, to make sure that the supply always appear where the player can go.

        NavMeshHit hit;
        Vector3 hitnav = new Vector3(0, 1, 0);
        Vector3 randomPosition = Random.insideUnitSphere * 20f;
        float maximumDistance = 100f;
              

        NavMesh.SamplePosition(randomPosition, out hit, maximumDistance, 1);
        supply.transform.position = hit.position;


        GameObject currentSupply = Instantiate(supply, hit.position + hitnav, transform.rotation);
       

        //var supplySpawned = Instantiate(supply, hit.position + hitnav, transform.rotation);
                        //have to create a variable to store the clone
        supplies.Add(currentSupply);
                        //just hit.position is not enough, need the rotation as well, since is a transform operation not a Vector3

    }

    void DestroyObject()
    {
       /* 
        * Cant use a foreach loop to remove an item from the List
        * 
        * foreach(GameObject supply in supplies)
        {
            if (supply.GetComponent<SupplyController>().cooked && supply.GetComponent<SupplyController>().goalCollision)
            {
                // supply.SetActive(false);
                supplies.Remove(supply);
                Destroy(supply);                
            }
        }*/

        for(int i=0; i < supplies.Count; i++)
        {
            if ((supplies[i].GetComponent<SupplyController>().cooked) && 
                (supplies[i].GetComponent<SupplyController>().goalCollision)) 
            {
                             
                Destroy(supplies[i]);
                supplies.RemoveAt(i);
            }

        }
       
    }



   
}
