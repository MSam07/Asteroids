using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//makes the ship flying and doing some basic cool things
    public class Ship : MonoBehaviour {
    Rigidbody2D rb = new Rigidbody2D();
    Vector2 thrustDirection = new Vector2(1,0);
    Vector2 thrustDirection1 = new Vector2(0,0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond=100;

    //working with explosion
    [SerializeField]
	GameObject prefabExplosion;

    //working with bullet
    [SerializeField]
    GameObject prefabBullet;
    
    HUD hud;
   
    // Use this for initialization

    void Start () 
    {
    rb = GetComponent<Rigidbody2D>();
    hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

    }
    void FixedUpdate()
    {
        //detect input on the Thrust axis and apply the thrust
        float thrustimput = Input.GetAxis("Thrust");
        if (thrustimput > 0)
        {
            rb.AddForce((ThrustForce*thrustDirection),ForceMode2D.Force);
        }

        //Calculate Rotation amount and apply
        float rotationInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        if (rotationInput < 0)
        {
        rotationAmount *= -1;
        transform.Rotate(Vector3.forward, rotationAmount);

        //implementation of the movement in the course of the turn;
        thrustDirection1.x = Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z);
        thrustDirection1.y = Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z);
        thrustDirection = thrustDirection1;
        }
        if (rotationInput > 0)
        {
            transform.Rotate(Vector3.forward, rotationAmount);
            thrustDirection1.x = Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z);
            thrustDirection1.y = Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z);
            thrustDirection = thrustDirection1;
        }

    }
    

    //Destroy ship when it collides with asteroid
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Asteroid")
        {
            BlowUp(gameObject);
            AudioManager.Play(AudioClipName.PlayerDeath);
            hud.StopGameTimer();
        }
    }

    public void BlowUp(GameObject ship)
    {
		Instantiate(prefabExplosion, ship.transform.position, Quaternion.identity);
		Destroy(ship);
	}


    //implementation of moving bullets in the direction the ship is pointing
	public void ApplyForce(Vector2 direction, Rigidbody2D rb)
	{
		const int Magnitude = 10;
		rb.AddForce(direction * Magnitude, ForceMode2D.Impulse);

	} 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet = Instantiate(prefabBullet) as GameObject;
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            ApplyForce(thrustDirection, rbBullet);
            rbBullet.transform.position = gameObject.transform.position;
            AudioManager.Play(AudioClipName.PlayerShot);

        }
         //stop timer then all asteroids are gone
        if (GameObject.FindWithTag("Asteroid") == null)
        {
            hud.StopGameTimer();
        }
    }
}
