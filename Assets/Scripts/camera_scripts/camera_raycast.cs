using UnityEngine;
using System.Collections;

public class camera_raycast : MonoBehaviour {

    private RaycastHit vHit;
    private RaycastHit[] vHits;
    private Ray vRay;

    private bool isClicked;
    private Vector3 ini_clicked_pos, current_clicked_pos;
    private float incremento_altura;

    private GameObject hitMeshGroundObject;

	// Use this for initialization
	void Start () {
        isClicked = false;
        incremento_altura = 0;
        vHit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {

            isClicked = true;
            current_clicked_pos = Input.mousePosition;

            if (Input.GetAxis("Mouse Y") > 0) {
                incremento_altura = Mathf.Clamp(Vector3.Magnitude(current_clicked_pos - ini_clicked_pos),0,0.25f);
            } else if (Input.GetAxis("Mouse Y") < 0) {
                incremento_altura = -1 * Mathf.Clamp(Vector3.Magnitude(current_clicked_pos - ini_clicked_pos), 0, 0.25f);
            }
        } 
        if(Input.GetMouseButtonUp(0)){
            isClicked = false;
            incremento_altura = 0;
            this.hitMeshGroundObject = null;
        }
        if (Input.GetMouseButtonDown(0)) {
            ini_clicked_pos = Input.mousePosition;
        }

	}

    void FixedUpdate() {
        if (isClicked) {

            vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(vRay.origin, vRay.direction * 160, Color.red);
            

            if (Physics.Raycast(vRay, out vHit, 160)) {
                //Debug.Log("(x,y,z): (" + vHit.transform.position.x + "," + vHit.transform.position.y + "," + vHit.transform.position.z + ")");
                this.hitMeshGroundObject = vHit.transform.gameObject;
                this.hitMeshGroundObject.transform.position = new Vector3(vHit.transform.position.x, vHit.transform.position.y + incremento_altura, vHit.transform.position.z);
            } else {

            }
            
        }
    }
}
