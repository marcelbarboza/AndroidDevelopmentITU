using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SupplyController : MonoBehaviour
{
    GameObject player;
    GameObject plate;    
    GameObject ui;

    public Button button;

    //for cooking timer, 
    private float t = 0;
    private float duration = 1.5f;
    //for button delay.
    float timer = 0f;
    float durationButton = 0.25f;
    //Unity always consider variables private unless said otherwise.



    Renderer materialController;
    
    Material mat;
    

    
    Transform plateTrans;
   

    UICode uicode;

    bool isControllable;
    bool cookingSupply;

    //if you want your variable to be public, but not be displayed on the inpspector.
    [HideInInspector]
    public bool goalCollision, cooked;
    //public bool cooked;


   

    // Start is called before the first frame update


    void Start()
    {
        player = GameObject.Find("Player");
                           

       //for changing the colour when is cooking
        materialController = GetComponent<Renderer>();
        mat = GetComponent<MeshRenderer>().material;

        mat.color = new Color(0, 1, 0, 1);

        //finding an object on the scene, then referencing a component from that object
        plate = GameObject.Find("Plate");
        plateTrans = plate.GetComponent<Transform>();

        ui = GameObject.Find("UI");
        uicode = ui.GetComponent<UICode>();


        cookingSupply = false;
        isControllable = false;
        cooked = false;
        goalCollision = false;

        




        // to make the button on the interface work
        button = FindObjectOfType<Button>();
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);

        

    }

    // Update is called once per frame
    void Update()
    {
        if (cookingSupply == true)
        {
            Cooking();
        }

        if (transform.parent != null)
        {
            timer += Time.deltaTime;
        }
    }

    //fixedupdate for RigibBody movements
    private void FixedUpdate()
    {

        
        //The Input.GetButton(string) can be changed/add on Project Setting, on the Input Manager panel 
        if (Input.GetButton("Action"))
        {
            if (isControllable == true && player.GetComponent<PlayerController>().DoesThePlateIsEmpty())
            {
                AttachToPlate();
            }            
        }
                     

        if (!player.GetComponent<PlayerController>().DoesThePlateIsEmpty() &&
             transform.parent != null)
        {
            if (Input.GetButton("Drop")) {
            DettachFromPlate();
            }
        }

        
    }

    private void AttachToPlate()
    {
        transform.position = plateTrans.transform.position + new Vector3(0, 1.5f, 0);
        transform.parent = plate.transform;

        
    }
    
    public void ButtonClicked() 
    {
        if (isControllable == true && player.GetComponent<PlayerController>().DoesThePlateIsEmpty()) 
        { 
            Debug.Log("button clicked");
            AttachToPlate();
        }

        
        
        if (player.GetComponent<PlayerController>().DoesThePlateIsEmpty() == false && timer>=durationButton)
        {
            timer = 0; 
            Debug.Log("button clicked");
            DettachFromPlate();
        }

    }

    private void DettachFromPlate()
    {
        Debug.Log("Drop");
        transform.parent = null;             
    }

    private void Cooking()
    {

        materialController.material.color = Color.Lerp(Color.green, Color.red, t);
        Debug.Log("cooking");
        if (t < 1)
        {
            t += Time.deltaTime / duration; }

        if (materialController.material.color == Color.red) {
            cooked = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        

        if (playerController == true)
        {
            Debug.Log("collided with  " + gameObject);
        }

        if(playerController == true && playerController.canPickup == true) 
        {
            isControllable = true; 
        }

        if (other.gameObject.name == "oven" && (transform.parent == plate.transform))
        {
            cookingSupply = true;
            Debug.Log("collided with Oven " );
        }


        if(other.gameObject.name == "Goal")
        {
            if(cooked == true)
            {
                ui.GetComponent<UICode>().numCarrotsCooked++;
                goalCollision = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        isControllable = false;
        cookingSupply = false;
        goalCollision = false;

    }

    


}
