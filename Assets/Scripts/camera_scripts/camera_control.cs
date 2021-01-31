using UnityEngine;
using System.Collections;

public class camera_control : MonoBehaviour {

    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;		// Speed of the camera when being panned

    private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
    private bool isPanning;		// Is the camera being panned?
    private bool isRotating;	// Is the camera being rotated?
    private bool rotated;

    private int map_size_camera_control;
    private Vector3 pos, move;

    public AudioSource ambience_field;
    public AudioSource ambience_atmosphere;

    void Start() {
        //map_size_camera_control = generate_suelo.getMapSize();
    }



    void Update() {
        // Get the mid mouse button
        // Rotate camera along X and Y axis
        if (Input.GetMouseButton(2)) {
            if (Input.GetMouseButtonDown(2)) {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                //dibujar puntero origen
            }
            pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            //dibujar linea desde mouseOrigin hacia pos

            this.transform.Rotate(0, pos.x * turnSpeed * Time.deltaTime, 0, Space.Self);

        }



        // Move the camera on it's XY plane
        if (Input.GetMouseButton(1)) {
            if (Input.GetMouseButtonDown(1)) {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
            }

            pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            move = new Vector3(pos.x * panSpeed * Time.deltaTime, 0, pos.y * panSpeed * Time.deltaTime);


            /*
            if (this.transform.position.x < 0 && pos.x < 0) {
                move.x = 0;
            }
            if (this.transform.position.x > map_size_camera_control && pos.x > 0) {
                move.x = map_size_camera_control;
            }
            if (this.transform.position.z < 0 && pos.y < 0) {
                move.z = 0;
            }
            if (this.transform.position.z > map_size_camera_control && pos.y > 0) {
                move.z = map_size_camera_control;
            }
            //LOS LIMITES NO FUNCIONAN, LA CAMARA SE VA A TOMAR POR CULO
            */

            this.transform.Translate(move, Space.Self);

        }


        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, 47);
            ambience_field.volume = 2f / Camera.main.orthographicSize;
            ambience_atmosphere.volume = Camera.main.orthographicSize / 26f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, 2);
            ambience_field.volume = 2f / Camera.main.orthographicSize;
            ambience_atmosphere.volume = Camera.main.orthographicSize / 26f;
        }


        // Get the mid mouse button && right mouse button
        //centra la imagen con rotacion y posicion inicial
        if (Input.GetMouseButton(1) && Input.GetMouseButton(2)) {
            this.transform.localEulerAngles = new Vector3(0, 315, 0);
            //this.transform.position = new Vector3(0, 0, 0);
        }

    }
}