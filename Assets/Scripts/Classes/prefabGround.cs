using UnityEngine;

public class prefabGround : MonoBehaviour {

    int[] coords = new int[4];
    int min_height, max_height;
    int i;
    float height_float;
    bool offset_height = true;

    public prefabGround(GameObject parent_ground, int x, int z, int oo, int ol, int lo, int ll) {
        this.coords[0] = oo;
        this.coords[1] = ol;
        this.coords[2] = lo;
        this.coords[3] = ll;

        min_height = 0;
        max_height = 1;

        for (i = 0; i < coords.Length; i++) {
            if (coords[i] < min_height) {
                min_height = coords[i];
                height_float = min_height * 0.25f;
                offset_height = false;
            } else if (coords[i] > max_height) {
                max_height = coords[i];
                height_float = (max_height * 0.25f) - 0.25f;
                offset_height = false;
            }
        }
        for (i = 0; i < coords.Length; i++) {
            if (min_height < 0) {
                coords[i] += Mathf.Abs(min_height);
            } else if (max_height > 1) {
                coords[i] -= Mathf.Abs(max_height - 1);
            }
        }

       
        if ( offset_height && (coords[0] == 1 && coords[1] == 1 && coords[2] == 1 && coords[3] == 1) ) {
            height_float = 0;
        } else if (coords[0] == 0 && coords[1] == 0 && coords[2] == 0 && coords[3] == 0) {
            height_float -= 0.25f;
        }

        //Debug.Log(coords[0] + ", " + coords[1] + ", " + coords[2] + ", " + coords[3]);

        GameObject newPGround = Resources.Load("Prefabs/ground_prefab/" + coords[0] + coords[1] + coords[2] + coords[3], typeof(GameObject)) as GameObject;

        var newPG = Instantiate(newPGround, new Vector3(x, height_float, z), newPGround.transform.rotation) as GameObject;
        newPG.transform.parent = parent_ground.transform;

        
    }

    public int getPrefabGround(int cord) {
        return this.coords[cord];
    }

    public void setPrefabGround() {

    }
}