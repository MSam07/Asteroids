using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//doing all the work with the asteroid
public class Asteroid : MonoBehaviour {
	
	[SerializeField]
	GameObject prefabExplosion;
	[SerializeField]

	//// saved for efficiency
	Sprite asteroidSprite1;
	[SerializeField]
	Sprite asteroidSprite2;
	[SerializeField]
	Sprite asteroidSprite3;
	void Start()
	{
		//work with sprites
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber == 0)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else if (spriteNumber == 1)
        {
            spriteRenderer.sprite = asteroidSprite2;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite3;
        }
		
	}
	public void Initialize (Direction direction, Vector3 location)
	{
		

		//set asteroid position
		transform.position = location;
		
		//set random angle based on direction
		float randomangle = Random.Range(0, 30) * Mathf.Deg2Rad;
		float angle;

		//gets the base angle depending on direction
		if (direction == Direction.Up)
		{
			angle = 75 * Mathf.Deg2Rad + randomangle;
		}
		else if (direction == Direction.Left)
		{
			angle = 165 * Mathf.Deg2Rad + randomangle;
		}
		else if (direction == Direction.Down)
		{
			angle = 255 * Mathf.Deg2Rad + randomangle;
		}
		else
		{
			angle = -15 * Mathf.Deg2Rad + randomangle;
		}
		StartMoving(angle, gameObject);
		
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		
		GameObject collisionObject = coll.gameObject;
		if (collisionObject.CompareTag("Bullet"))
		{	
			AudioManager.Play(AudioClipName.AsteroidHit);
			BigBlowUp(collisionObject);

			//blowing up the big asteroid, saving circlecollider
			Vector3 newScale = transform.localScale;
				if (newScale.x >= 0.5)
				{
					Instantiate(prefabExplosion, transform.position, Quaternion.identity);
					float CircleColliderRadius = GetComponent<CircleCollider2D>().radius;
					newScale.x /= 2;
					newScale.y /= 2;
					transform.localScale = newScale;
					CircleColliderRadius /= 2;
					Destroy(gameObject);
					SplitAsteroids(gameObject);
				}
				else
				{
				BigBlowUp(gameObject);
				}
			
		
		}
		
	}

	public void BigBlowUp(GameObject ship)
    {
		Instantiate(prefabExplosion, ship.transform.position, Quaternion.identity);
		Destroy(ship);
	}

	public void SplitAsteroids(GameObject rock)
	{	
		

		//creating and doing all the work with 1st little asteroid
		GameObject asteroid1 = Instantiate(gameObject,rock.transform.position,Quaternion.identity);
		asteroid1.GetComponent<CircleCollider2D>().enabled = true;
		asteroid1.GetComponent<Asteroid>().enabled = true;
		asteroid1.GetComponent<ScreenWrapper>().enabled = true;

		float angle1 = Random.Range(0, 2) * Mathf.PI;
		StartMoving(angle1, asteroid1);

		//creating and doing all the work with 2nd little asteroid
		GameObject asteroid2 = Instantiate(gameObject,rock.transform.position,Quaternion.identity);
		asteroid2.GetComponent<CircleCollider2D>().enabled = true;
		asteroid2.GetComponent<Asteroid>().enabled = true;
		asteroid2.GetComponent<ScreenWrapper>().enabled = true;

		float angle2 = Random.Range(0.1f, 2) * Mathf.PI;
		StartMoving(angle1, asteroid1);
		StartMoving(angle2, asteroid2);
	}
	
	//moving asteroids
	public void StartMoving(float angle, GameObject anything)
	{
		const float MinImpulseForce = 1f;
		const float MaxImpulseForce = 2.5f;
		// apply impulse force to get asteroid moving
		Vector2 moveDirection = new Vector2(
    		Mathf.Cos(angle), Mathf.Sin(angle));
		float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
		anything.GetComponent<Rigidbody2D>().AddForce(
    		moveDirection * magnitude,
    		ForceMode2D.Impulse);


	}
	
}
