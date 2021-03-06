using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
	float horizontal;
	float vertical;
	public float moveSpeed;
	public float jumpForce;
	public CharacterController controller;

	private Vector3 moveDirection;
	public float gravityScale;

	public Animator anim;
	public Transform pivot;
	public float rotateSpeed;

	public GameObject playerModel;

	private bool isDead = false;
	private Vector3 firstPosition;

	public ParticleSystem system1, system2;

	public bool terminouFase = false;
	public bool parado = true;

	void Start(){
		controller = GetComponent<CharacterController>();
		firstPosition = transform.position;
		anim.SetTrigger("dance");
		terminouFase = false;
		//StartCoroutine("Move");
	}

	void Update(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		
		if (!isDead)
		{
			if (Input.GetKeyDown(KeyCode.W)){
				moveDirection = new Vector3(0f * moveSpeed, moveDirection.y, 1f * moveSpeed);
			}else if (Input.GetKeyDown(KeyCode.S)){
				moveDirection = new Vector3(0f * moveSpeed, moveDirection.y, -1f * moveSpeed);
			}else if (Input.GetKeyDown(KeyCode.D)){
				moveDirection = new Vector3(1f * moveSpeed, moveDirection.y, 0f * moveSpeed);
			}else if (Input.GetKeyDown(KeyCode.A)){
				moveDirection = new Vector3(-1f * moveSpeed, moveDirection.y, 0f * moveSpeed);
			}else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){
				moveDirection = new Vector3(1f * moveSpeed, moveDirection.y, 1f * moveSpeed);
			}else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){
				moveDirection = new Vector3(-1f * moveSpeed, moveDirection.y, 1f * moveSpeed);
			}else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
				moveDirection = new Vector3(1f * moveSpeed, moveDirection.y, -1f * moveSpeed);
			}else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){
				moveDirection = new Vector3(-1f * moveSpeed, moveDirection.y, -1f * moveSpeed);
			}
			controller.Move(moveDirection * Time.deltaTime);
			//moveDirection = new Vector3(horizontal * moveSpeed, moveDirection.y, vertical * moveSpeed);
		}else
		{
			StartCoroutine("Revive");
		}

		if(controller.isGrounded){
			anim.SetBool("fall", false);
			if (Input.GetKeyDown(KeyCode.Space)){
				anim.SetBool("land", false);	
				if(this.gameObject.transform.position.y >= 2f)
				{
					anim.SetTrigger("jump2");
					moveDirection.y = jumpForce+8;
				}else {
					anim.SetTrigger("jump");
					moveDirection.y = jumpForce+3;
				}
			}else 
			{
				anim.SetBool("land", true);	
			}
		}else
		{	
			if(this.gameObject.transform.position.y >= 7f)
			{
				anim.SetBool("fall", true);	
			}
			moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		}

		if(((horizontal != 0 || vertical != 0)) && !isDead){
			anim.SetBool("walk", true);
			parado = false;
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		//anim.SetFloat("Speed", (Mathf.Abs(vertical)) + Mathf.Abs(horizontal));

		if(controller.isGrounded && !isDead && terminouFase == false && parado == false)
		{
			system1.Play();
			system2.Play();
		}else {
			system1.Stop();
			system2.Stop();
		}

	}

	void OnControllerColliderHit(ControllerColliderHit other){
   		if (other.transform.tag == "Wall"){
			anim.SetBool("death", true);
			moveDirection = new Vector3 (0,0,0);
			isDead = true;
			moveSpeed = 0;
    		Debug.Log ("Player bateu na parede");
   		}
 	}

	public IEnumerator Revive()
	{		
			
		yield return new WaitForSeconds(1f);

		isDead = false;	
		parado = true;
		firstPosition.y = transform.position.y;
		transform.position = new Vector3(firstPosition.x, 20f, firstPosition.z);
		playerModel.transform.rotation = Quaternion.Euler(0f, -135f, 0f);
		anim.SetBool("death", false);
		anim.SetBool("walk", false);
		anim.SetBool("land", false);
		moveDirection = new Vector3 (0,0,0);		
		moveSpeed = 20;
	}

	/*IEnumerator Move()
	{
		yield return new WaitForSeconds(move_rate);

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		moveDirection = new Vector3(horizontal * moveSpeed, moveDirection.y, vertical * moveSpeed);
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		controller.Move(moveDirection * Time.deltaTime);

		//rotate
		if(horizontal != 0 || vertical != 0){
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		StartCoroutine("Move");
	}*/
}
