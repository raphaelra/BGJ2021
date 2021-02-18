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
		StartCoroutine("Move");
	}

	/*void Update(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		
		moveDirection = new Vector3(horizontal * moveSpeed, moveDirection.y, vertical * moveSpeed);

		if(controller.isGrounded){
			if (Input.GetKeyDown(KeyCode.Space)){
				moveDirection.y = jumpForce;
			}
		}

		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		controller.Move(moveDirection * Time.deltaTime);

		if(horizontal != 0 || vertical != 0){
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}


		//anim.SetFloat("Speed", (Mathf.Abs(vertical)) + Mathf.Abs(horizontal));

	}*/

	IEnumerator Move()
	{
		yield return new WaitForSeconds(move_rate);

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		moveDirection = new Vector3(horizontal * moveSpeed, moveDirection.y, vertical * moveSpeed);
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		controller.Move(moveDirection * Time.deltaTime);

		StartCoroutine("Move");
	}
}
