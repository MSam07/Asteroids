using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//doing all the work with spawn asteroids
public class AsteroidSpawner : MonoBehaviour 
{

	// Use this for initialization

	[SerializeField]
	GameObject prefabAsteroid;

	//need for saving collider radius
	float asteroidCircleColliderRadius;
	//need for work with Asteroid script
	Asteroid asteroidScript;
	void Start () 
	{
	// spawn and destroy asteroid to cache collider radius
	GameObject asteroid = Instantiate(prefabAsteroid) as GameObject;
	CircleCollider2D collider = asteroid.GetComponent<CircleCollider2D>();
	asteroidCircleColliderRadius = collider.radius;
	Destroy(asteroid);

	//working with Asteroid1   (appears on the right edge, moves left)
	GameObject asteroid1 = Instantiate(prefabAsteroid) as GameObject;
	asteroidScript = asteroid1.GetComponent<Asteroid>();
	Vector3 asteroid1Location = new Vector3 (ScreenUtils.ScreenRight - asteroidCircleColliderRadius,
											 0,
											 -Camera.main.transform.position.z);
	asteroidScript.Initialize(Direction.Left, asteroid1Location);
	

	//working with Asteroid2   (appears on the left edge, moves right)
	GameObject Asteroid2 = Instantiate(prefabAsteroid) as GameObject;
	asteroidScript = Asteroid2.GetComponent<Asteroid>();
	Vector3 asteroid2Location = new Vector3 (ScreenUtils.ScreenLeft + asteroidCircleColliderRadius,
											 0,
											 -Camera.main.transform.position.z);
	asteroidScript.Initialize(Direction.Right, asteroid2Location);

	//working with Asteroid3   (appears on the top edge, moves down)
	GameObject Asteroid3 = Instantiate(prefabAsteroid) as GameObject;
	asteroidScript = Asteroid3.GetComponent<Asteroid>();
	Vector3 asteroid3Location = new Vector3 (0,
											 ScreenUtils.ScreenTop - asteroidCircleColliderRadius,
											 -Camera.main.transform.position.z);
	asteroidScript.Initialize(Direction.Down, asteroid3Location);

	//working with Asteroid4   (appears on the bottom edge, moves up)
	GameObject Asteroid4 = Instantiate(prefabAsteroid) as GameObject;
	asteroidScript = Asteroid4.GetComponent<Asteroid>();
	Vector3 asteroid4Location = new Vector3 (0,
											 ScreenUtils.ScreenBottom + asteroidCircleColliderRadius,
											 -Camera.main.transform.position.z);
	asteroidScript.Initialize(Direction.Up, asteroid4Location);
	}
}
