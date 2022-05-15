using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.AI; //for using navmesh
using UnityEngine.EventSystems; //for checking with is touching UI

public class PlayerController : MonoBehaviour
{ 
    public bool canPickup;
    public bool canCook;
    public bool hasItem;
    
    GameObject plate;

    ///Movement
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        plate = GameObject.Find("Plate");
        agent = GetComponent<NavMeshAgent>();

        canPickup = false;
        hasItem = false;
        canCook = false;          
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsPointerOverUIObject()) 
        {                   
            
            if (Input.touchCount > 0)
            {                  
                ApplyMovementWithTouch();                                
            }
       

            if (Input.GetMouseButton(0))
            {
                ApplyMovementWithMouse();
            }


            if (Input.GetMouseButtonDown(0))
            {
                agent.speed = 20;
            }

            if(Input.GetMouseButtonUp(0))
            {
                agent.speed = 10;
            }                        
                       
        }
        
        if (Input.GetButtonDown("Enable Debug Button 1"))
        {
            Debug.Log("checking PlayeController");
            Debug.Log("can cook " + canCook);    
            Debug.Log("can PickUp " + canPickup);
            Debug.Log(DoesThePlateIsEmpty());
        }

        
    }

    private void ApplyMovementWithMouse()
    {
        RaycastHit hit;

        Ray rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayMouse, out hit, 100))
        {
            agent.isStopped = false;
            agent.updateRotation = true;
            agent.destination = hit.point;

            if ((transform.position - agent.destination).magnitude < .1f)
            {
                agent.isStopped = true;
                agent.updateRotation = false;
            }
        }    
        
    }

    private void ApplyMovementWithTouch()
    {
        RaycastHit hit;
        
        Ray rayTouch = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);


        if (Physics.Raycast(rayTouch, out hit, 100))
        {
            agent.isStopped = false;
            agent.updateRotation = true;
            agent.destination = hit.point;

            if ((transform.position - agent.destination).magnitude < 1f)
            {
                agent.isStopped = true;
                agent.updateRotation = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Supply"))
        {
            canPickup = true;            
            
            Debug.Log("colidiu ");
        }

      

        if (other.gameObject.name == "supply"){}

        if (other.gameObject.tag == "Supply") {}
    }

   


    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.name == "oven")
        {
            canCook = true;            
        }

    }

    
    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
        canCook = false;
    }

    public bool DoesThePlateIsEmpty()
    {
        if(plate.transform.childCount > 1)
        {
            //then the player have something, 1 because it already contains a cyllinder there
            return false;
        }else 
            return true;
        

    }

    private bool IsPointerOverUIObject()
    {
        //copied from the unity forum, unity does not have an easy way to check if your a clicking the ui or the world behind it
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }



}
