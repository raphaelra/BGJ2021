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
	[SerializeField] float move_rate = 1f;

	public GameObject playerModel;

	void Start(){
		controller = GetComponent<CharacterController>();
		//StartCoroutine("Move");
	}

	void Update(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");	
		
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
		//moveDirection = new Vector3(horizontal * moveSpeed, moveDirection.y, vertical * moveSpeed);

		if(controller.isGrounded){
			if (Input.GetKeyDown(KeyCode.Space)){
				anim.SetBool("land", false);
				if(this.gameObject.transform.position.y >= 2f)
				{
					anim.SetTrigger("jump2");
					moveDirection.y = jumpForce+5;
				}else {
					anim.SetTrigger("jump");
					moveDirection.y = jumpForce;
				}
			}else 
			{
				anim.SetBool("land", true);	
			}
		}

		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		controller.Move(moveDirection * Time.deltaTime);

		if(horizontal != 0 || vertical != 0){
			anim.SetBool("walk", true);
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		//anim.SetFloat("Speed", (Mathf.Abs(vertical)) + Mathf.Abs(horizontal));

	}

	void OnControllerColliderHit(ControllerColliderHit other){
   		if (other.transform.tag == "Wall"){
			moveSpeed = 0;
			moveDirection = new Vector3(0, 0, 0);
			anim.SetTrigger("death");
    		Debug.Log ("Player bateu na parede");
   		}
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
