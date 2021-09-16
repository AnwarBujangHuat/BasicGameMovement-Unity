using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rigi;
	public Animator anime;
	public enum dragonstate { idle, walk, jump, fall, crouch,death,attack }
	public dragonstate currentstate = dragonstate.idle;
	public Collider2D collide;
	public LayerMask ground;
	public LayerMask Enemy;
	public Transform fire;
	// [SerializeField]
   // private Animator hasHealthPotio;

	private void Start()
	{
		print("hello and welcome to my game");
		Time.timeScale = 0;
	}

	private void Update()
	{

		float Horizontaldirection = Input.GetAxis("Horizontal");
		float Verticaldirection = Input.GetAxis("Vertical");
		float Attack = Input.GetAxis("Fire1");

		idl(Horizontaldirection,Verticaldirection, Attack);
		horizoltal(Horizontaldirection);
		verticle(Verticaldirection);


		if (Attack > 0 && collide.IsTouchingLayers(ground))
		{
			if (transform.localScale.x == 1) { rigi.velocity = new Vector2(5, rigi.velocity.y); 
				currentstate = dragonstate.attack; }
				else
				{
					rigi.velocity = new Vector2(-5, rigi.velocity.y);
					transform.localScale = new Vector2(transform.localScale.x, 1);
					currentstate = dragonstate.attack;
				}

				if (collide.IsTouchingLayers(ground)) { }else { currentstate = dragonstate.fall; }

		}
		else if (collide.IsTouchingLayers(Enemy))
		{
			print("Auch");
			currentstate = dragonstate.death;
		}

		anime.SetInteger("currentstate", (int)currentstate);

	}

		void horizoltal(float hd)
		{
			if (hd < 0)
			{
				rigi.velocity = new Vector2(-3, rigi.velocity.y);
				transform.localScale = new Vector2(-1, 1);
				fire.localRotation = Quaternion.Euler(0, 180, 0);
				currentstate = dragonstate.walk;
			}

			else if (hd > 0)
			{
				rigi.velocity = new Vector2(3, rigi.velocity.y);
				transform.localScale = new Vector2(1, 1);
				fire.localRotation = Quaternion.Euler(0, 0, 0);
				currentstate = dragonstate.walk;
			}
		}

		void verticle(float vd)
		{
			if (vd > 0 && collide.IsTouchingLayers(ground))
			{
				rigi.velocity = new Vector2(rigi.velocity.x, 5);
				currentstate = dragonstate.jump;
			}
			else if (vd < 0 && collide.IsTouchingLayers(ground))
			{
				currentstate = dragonstate.crouch;
			}
			else if (rigi.velocity.y < 0)
			{
				currentstate = dragonstate.fall;
			}
		}


		void idl(float hd, float vd, float attack)
		{
			if(vd==0 && hd==0 && attack==0 && rigi.velocity.y==0 && collide.IsTouchingLayers(ground))
			{
				currentstate = dragonstate.idle;
			}
		}
	}



