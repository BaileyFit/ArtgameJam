using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {

    public List<Transform> Bodyparts = new List<Transform>();
    public GameObject bodyprefab;

    public int startSize;

    public float speed = 1;
    public float minDis = 0.25f;
    public float rotationspeed = 50;

    private float dis;
    private Transform curBodypart;
    private Transform prevBodypart;
   

	// Use this for initialization
	void Start () {
		for(int i = 0; i < startSize - 1; i++)
        {
            AddBodyPart();
        }
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        /*if (Input.GetKeyDown("Q"))
        {
            AddBodyPart();
        }*/
	}

    public void Move()
    {
        float curSpeed = speed;
        if(Input.GetAxis("Vertical") !=0)
        {
            Bodyparts[0].Rotate(Vector3.right * rotationspeed * Time.deltaTime * Input.GetAxis("Vertical"));
        }

        Bodyparts[0].Translate(Bodyparts[0].forward * curSpeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
        {
            Bodyparts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        for(int i = 1; i < Bodyparts.Count; i++)
        {
            curBodypart = Bodyparts[i];
            prevBodypart = Bodyparts[i - 1];
            dis = Vector3.Distance(prevBodypart.position, curBodypart.position);

            Vector3 newpos = prevBodypart.position;
            //newpos.y = Bodyparts[0].position.y;

            float T = Time.deltaTime * dis / minDis * speed;
            if(T > 0.7f)
            {
                T = 0.7f;
            }
            curBodypart.position = Vector3.Slerp(curBodypart.position, newpos, T);
            curBodypart.rotation = Quaternion.Slerp(curBodypart.rotation, prevBodypart.rotation, T);
        }

    }


    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(bodyprefab, Bodyparts[Bodyparts.Count - 1].position, Bodyparts[Bodyparts.Count - 1].rotation) as GameObject).transform;

        newpart.SetParent(transform);

        Bodyparts.Add(newpart);
    }
}
