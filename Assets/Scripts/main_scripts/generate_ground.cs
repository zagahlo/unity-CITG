using UnityEngine;

public class generate_ground : MonoBehaviour {

    public GameObject ground_mesh_point;

    

    public int map_size, mountains_height;

    [Range (1,10)]
    public int roughness;

    private int i, j;

    private float meshGround_offset = 0.5f;

    private float[,] mesh_heightArray;

    // Use this for initialization
    void Start() {

        mesh_heightArray = new float[map_size, map_size];

        GameObject parent_ground = new GameObject("parent_ground");
        GameObject parent_meshGround = new GameObject("parent_meshGround");


        //CREAR MALLA TERRENO
        createGroundMesh(i, j, map_size, mountains_height, meshGround_offset, parent_meshGround);

        //COLOCAR TERRENO
        for (i = 0; i < map_size; i++) {
            for (j = 0; j < map_size; j++) {

                if (i < map_size - 1 && j < map_size - 1) {
                    prefabGround pGround = new prefabGround(parent_ground, i, j, (int)(getHeightMeshGroundArray(i, j) / 0.25f), (int)(getHeightMeshGroundArray(i + 1, j) / 0.25f), (int)(getHeightMeshGroundArray(i, j + 1) / 0.25f), (int)(getHeightMeshGroundArray(i + 1, j + 1) / 0.25f));
                }

            }
        }
        
    }


    // Update is called once per frame
    void Update() {
        // AÑADIR EN EL FUTURO NUEVAS CREACIONES DE TERRENOS 128x128 BASANDOSE EN onTriggerEnter onTriggerExit de Terreno_padre


    }


    void createGroundMesh(int i, int j, int map_size, int max_height, float meshGround_offset, GameObject parent_meshGround) {

        float height = 0;
        float prev_height_a = -1, prev_height_b = -1, prev_height_c = -1;

        for (i = 0; i < map_size; i++) {
            height = 0;
            for (j = 0; j < map_size; j++) {

                if (Random.Range(0,11) < roughness) {


                    if (Random.Range(0, 11) > (10 - max_height)) {
                        
                        height = height + 0.25f;

                    } else if (Random.Range(0, 11) < (10 - max_height)) {

                        height = height - 0.25f;
                    
                    }

                } else {
                    
                }


                //Comprobar concordancia bordes anteriores
                prev_height_a = -1;
                prev_height_b = -1;
                prev_height_c = -1;
                                
                if (i > 0) {
                    prev_height_b = (getHeightMeshGroundArray(i - 1, j));
                    if (prev_height_b > height) {
                        while(prev_height_b - height > 0.25f) {
                            height += 0.25f;
                        }
                    } else if(prev_height_b < height) {
                        while (height - prev_height_b > 0.25f) {
                            height -= 0.25f;
                        }
                    }

                    if (j > 0) {
                        prev_height_a = (getHeightMeshGroundArray(i - 1, j - 1));
                        if (prev_height_a > height) {
                            while (prev_height_a - height > 0.25f) {
                                height += 0.25f;
                            }
                        } else if (prev_height_a < height) {
                            while (height - prev_height_a > 0.25f) {
                                height -= 0.25f;
                            }
                        }
                    }
                    if (j < (map_size - 1)) {
                        prev_height_c = (getHeightMeshGroundArray(i - 1, j + 1));
                        if (prev_height_c > height) {
                            while (prev_height_c - height > 0.25f) {
                                height += 0.25f;
                            }
                        } else if (prev_height_c < height) {
                            while (height - prev_height_c > 0.25f) {
                                height -= 0.25f;
                            }
                        }
                    }
                }

               
                //create a single_mesh
                GameObject mesh_gr = Instantiate(ground_mesh_point, new Vector3(i - meshGround_offset, height, j - meshGround_offset), Quaternion.identity) as GameObject;
                mesh_gr.transform.parent = parent_meshGround.transform;
                setHeightMeshGroundArray(i, j, height);
                
            }
        }
    }

    void setHeightMeshGroundArray(int i, int j, float height) {
        this.mesh_heightArray[i, j] = height;
    }

    public float getHeightMeshGroundArray(int i, int j) {
        return this.mesh_heightArray[i, j];
    }
}
