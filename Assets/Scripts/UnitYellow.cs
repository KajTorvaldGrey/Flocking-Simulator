using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitYellow : MonoBehaviour
{
	public GameObject manager;
	public int RandomLimiter = 10;
    
    // Initialise location
    public Vector2 location = Vector2.zero;     
    // Initialise velocity
    public Vector2 velocity = Vector2.zero;
    // Initialise unit goal
    Vector2 Unitgoal = Vector2.zero;
    // Initialise force
    Vector2 Force = Vector2.zero;
    
    // Generated Methods

    // Use this for initialization
    void Start()
    {
		startSetting();
    }

    // Update is called once per frame
    void Update()
    {
		flock();
    }

    // Custom Methods
	void startSetting()
    {
        Push(new Vector2(Random.Range(-RandomLimiter, -RandomLimiter), (Random.Range(-RandomLimiter, RandomLimiter))));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        CreategoalLocation();
    }

	void CreategoalLocation()
    {
        Unitgoal = PlayerController.location;
    }

	void flock()
    {
        location = transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;
        if (Random.Range(0, 1) < 1) {
            Vector2 orientation = alignment();
            Vector2 proximity = cohesion();
            Unitgoal = Seek(Unitgoal);
            Force = orientation + proximity; //+ 0.5f*Unitgoal;
            Force = Force.normalized;
            WrapUnit();
        }      
        Push(Force);      
    }

	Vector2 alignment()
    {
        float neighbourLimit = manager.GetComponent<SimManager>().neighbourLimit;
        Vector2 sum = new Vector2(0, 0);
        Vector2 steer = new Vector2(0, 0);
        float count = 0;
        foreach (GameObject other in manager.GetComponent<SimManager>().UnitsRed) {
            if (other == this.gameObject) continue; {
                float distance = Vector2.Distance(location, other.GetComponent<UnitRed>().location);
                if (distance < neighbourLimit) {
                    sum = sum + other.GetComponent<UnitRed>().velocity;
                    count++;
                }
            }
            if (count > 0) {
                sum = sum / count;
                steer = sum - velocity;
                return steer;
            }
        }
        return Vector2.zero;
    }

    Vector2 cohesion()
    {
        float neighbourDistance = manager.GetComponent<SimManager>().neighbourLimit;
        Vector2 sum = new Vector2(0, 0);
        float count = 0;

        foreach (GameObject other in manager.GetComponent<SimManager>().UnitsRed) {
            if (other == this.gameObject) continue; {
                float distance = Vector2.Distance(location, other.GetComponent<UnitRed>().location);
                if (distance < neighbourDistance) {
                    sum = sum + other.GetComponent<UnitRed>().location;
                    count++;
                }
            }
            if (count > 0) {
                sum = sum / count;
                return Seek(sum);
            }
        }
        return Vector2.zero;      
    }

	Vector2 Seek(Vector2 Target)
    {
        return (Target - location);
    }

	void Push(Vector2 speed)
    {
        Vector3 force = new Vector3(speed.x, speed.y, 0);
        this.GetComponent<Rigidbody2D>().AddForce(force);
    }

	void WrapUnit() //x = 43 y = 20
    {
        if (transform.position.x > 45 || transform.position.x < -45) {
            transform.position = new Vector2(-(location.x), location.y);
        }
        if (transform.position.y > 22 || transform.position.y < -22) {
            transform.position = new Vector2(location.x, -(location.y));
        }
    }
}
