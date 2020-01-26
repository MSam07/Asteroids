using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////moving from one part of the screen to another
public class ScreenWrapper : MonoBehaviour {
	float CircleColliderRadius;

	// Use this for initialization
	public void Start () 
	{
		CircleColliderRadius = GetComponent<CircleCollider2D>().radius;
	}
	public void OnBecameInvisible()
{
	//moving from one part of the screen to another
	Vector2 position = transform.position;
		if (position.x - CircleColliderRadius < ScreenUtils.ScreenLeft 
			|| 
			position.x + CircleColliderRadius > ScreenUtils.ScreenRight)
		{
			position.x *= -1;
		}
		
		if (position.y + CircleColliderRadius > ScreenUtils.ScreenTop
			||
			position.y - CircleColliderRadius < ScreenUtils.ScreenBottom
			)
		{
			position.y *= -1;
		}
		transform.position = position;
		}
		}
