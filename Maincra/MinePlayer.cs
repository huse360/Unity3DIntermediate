
// Visita mi canal: https://youtu.be/weUTRB9ClIk


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidFuncVoid();
public class MinePlayer : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    Dictionary<Vector3, Quaternion> rotations; 

    public GameObject selection;
    public GameObject prefab;

    Vector3 diff;

    Vector3 mouseDelta = Vector3.zero;
    Vector3 amount = new Vector3(-137, -38, 50);


    Vector3 cameraDir = Vector3.zero;
    Quaternion cameraRot = Quaternion.identity;
    Vector3 cameraPos = Vector3.zero;

    Vector3 focusPos = Vector3.zero;

    VoidFuncVoid cameraAction;

    public float sensitivity = 5.0f;

    public float zoomNear = 20;
    public float zoomFar = 300;

    Vector3 repeatLastPos = Vector3.zero;
    Vector3 lastDiff = Vector3.zero;

    
    enum UserEvents {
        NONE = 0
                                //Raycast HIT
        ,HIT =   0b_0000_0001
                                //Mouse Buttons
        ,LMB =   0b_0000_0010
        ,RMB =   0b_0000_0100
        ,MMB =   0b_0000_1000
        ,LMB_D = 0b_0001_0000
        ,RMB_D = 0b_0010_0000
                                //Keys
        ,ALT =   0b_0100_0000
        ,F   =   0b_1000_0000
        ,SP    = 0b_1_0000_0000
    };

    Dictionary<UserEvents, VoidFuncVoid> userActions; 
    UserEvents userEvents;

    void NOP() {}  //No OPeration

   

    // Start is called before the first frame update
    void Start()
    {
          rotations = new Dictionary<Vector3, Quaternion>() {
             {Vector3.back, Quaternion.identity}
            ,{Vector3.up, Quaternion.Euler(90,0,0)}
            ,{Vector3.left, Quaternion.Euler(0,90,0)}
            ,{Vector3.forward, Quaternion.Euler(0,180,0)}
            ,{Vector3.down, Quaternion.Euler(270,0,0)}
            ,{Vector3.right, Quaternion.Euler(0,270,0)}
        };


        cameraAction = NOP;

        CameraRotateView();
       
        userActions = new Dictionary<UserEvents, VoidFuncVoid>() {
            {UserEvents.NONE, ()=>{}}

            //Cuando el raton esta sobre un objeto
            ,{UserEvents.HIT, ()=>{}}

            //Clic
            ,{UserEvents.HIT | UserEvents.LMB | UserEvents.LMB_D, ()=>{}}

            //Clic derecho
            ,{UserEvents.HIT | UserEvents.RMB | UserEvents.RMB_D, ()=>{}}

            //Repeat last...
            ,{ UserEvents.SP, ()=>{}}
            ,{ UserEvents.HIT | UserEvents.SP, ()=>{}}


            //Rotate View
            ,{UserEvents.ALT | UserEvents.LMB, ()=>{}}

             //Rotate View
            ,{ UserEvents.HIT | UserEvents.ALT | UserEvents.LMB, ()=>{}}

             //Focus View
            ,{ UserEvents.HIT | UserEvents.F , ()=>{}}

            ,{ UserEvents.F , ()=>{}}

             //Pan View
            ,{ UserEvents.MMB , ()=>{}}
            ,{ UserEvents.HIT | UserEvents.MMB , ()=>{}}

            ,{ UserEvents.HIT | UserEvents.ALT | UserEvents.MMB , ()=>{}}

            ,{  UserEvents.ALT | UserEvents.MMB , ()=>{}}


             //Right clic Zoom
            ,{ UserEvents.HIT | UserEvents.ALT | UserEvents.RMB, ()=>{}}
            ,{ UserEvents.ALT | UserEvents.RMB, ()=>{}}

        };

    }


    void CameraPanView(){}
    void CameraRBMZoom(){}
    void CameraJustWheelZoom(){}
    void CameraRotateView(){}
    void CameraReposition(){}

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        userEvents = UserEvents.NONE;
        cameraAction = CameraJustWheelZoom;

        //Le pegamos a algo?
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f)) { userEvents |= UserEvents.HIT;}
        
        //Tecla alt
        if (Input.GetKey(KeyCode.LeftAlt)) { userEvents |= UserEvents.ALT;}
        if (Input.GetKey(KeyCode.RightAlt)) { userEvents |= UserEvents.ALT;}

        //Tecla F
        if (Input.GetKeyDown(KeyCode.F)) { userEvents |= UserEvents.F;}

        //Espacio
        if (Input.GetKeyDown(KeyCode.Space)) { userEvents |= UserEvents.SP;}

        
        //Botones del raton
        if (Input.GetMouseButton(0)) { userEvents |= UserEvents.LMB;}
        if (Input.GetMouseButton(1)) { userEvents |= UserEvents.RMB;}
        if (Input.GetMouseButton(2)) { userEvents |= UserEvents.MMB;}

        if (Input.GetMouseButtonDown(0)) { userEvents |= UserEvents.LMB_D;}
        if (Input.GetMouseButtonDown(1)) { userEvents |= UserEvents.RMB_D;}
       

        selection.SetActive(false);

        if (userActions.ContainsKey(userEvents)) { userActions[userEvents]();}

    }


    void LateUpdate()
    {
        cameraAction();

    }

}
