using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//make bullets move
public class Bullet : MonoBehaviour {

	[SerializeField]
	GameObject prefabExplosion;

	//need for work with timer
	const float Bulletdestroydelay = 1.5f;
	Timer deathTimer;


	// Use this for initialization
	void Start () {

		//add a Timer component, set the timer duration, and run the timer
		deathTimer = gameObject.AddComponent<Timer>();
		deathTimer.Duration = Bulletdestroydelay;
		deathTimer.Run();
		
	}
	
	// Update is called once per frame
	void Update () {

	//check for time to destroy bullet
	if (deathTimer.Finished)
	{
		BigBang(gameObject);
	}
	
		
	}

	public void BigBang(GameObject ship)
    {	
		Instantiate(prefabExplosion, ship.transform.position, Quaternion.identity);
		Destroy(ship);
	}
	


}
