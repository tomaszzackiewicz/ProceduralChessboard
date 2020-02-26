using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ProceduralChessBoardGenerator{
	 
	public class FlyCameraAdvanced : MonoBehaviour{
	 
		public float cameraSensitivity = 5.0f; //90;
		public float climbSpeed = 10.0f;
		public float normalMoveSpeed = 10.0f;
		public float slowMoveFactor = 0.25f;
		public float fastMoveFactor = 5.0f;
	 
		private float rotationX = 0.0f;
		private float rotationY = 0.0f;
		
		private bool inPosition = false;
		public float moveSpeed = 1000.0f;
		private float lerpSpeed = 100.0f;
		
		private float yaw = 0.0f;
		private float pitch = 0.0f;
		
		public Slider camSpeedSlider;
		
		//public GameObject prevCamera;
		public static GameObject flyCamera;
	 
		void Awake (){
			//Screen.lockCursor = true;
			//Cursor.lockState = CursorLockMode.Locked;
			flyCamera = this.gameObject;
		}
		
		bool isInputDisabled;
		
		void OnEnable() {
			isInputDisabled = false;
			//StaticEventsManager.disableInput += DisableInput;
        }
	 
		void Update (){
			if(!isInputDisabled){
				if(Input.GetMouseButton(1)){
					//rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
					//rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
					//rotationY = Mathf.Clamp (rotationY, -90, 90);
					yaw += cameraSensitivity * Input.GetAxis("Mouse X");
					pitch -= cameraSensitivity * Input.GetAxis("Mouse Y");
					pitch = Mathf.Clamp (pitch, -90, 90);
					transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
				}
		 
				//transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
				//transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		 
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)){
					transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
				}else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)){
					transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
				}else{
					transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
				}
		 
		 
				if (Input.GetKey (KeyCode.PageUp)) {
					transform.position += transform.up * climbSpeed * Time.deltaTime;
				}
				if (Input.GetKey (KeyCode.PageDown)) {
					transform.position -= transform.up * climbSpeed * Time.deltaTime;
				}
				
				/* if(camSpeedSlider){
					int factor = System.Convert.ToInt32(camSpeedSlider.value);
					
					
				} */
			}
			
			
			
	 
		}
		
		public void OnValueChanged(float factor){
			
			climbSpeed = factor *5;
			normalMoveSpeed = factor*2;
			//slowMoveFactor += factor * 0.1f;
			//fastMoveFactor += factor * 1;
		}
		
		
		void DisableInput(bool isInput){
			isInputDisabled = isInput;
		}
		
		void OnDisable() {
			//StaticEventsManager.disableInput -= DisableInput;
        }
		
		/* public void MoveTowardsTarget(GameObject target){
			if(targetObject.GetComponent<Renderer>() != null){
				offset = target.GetComponent<Renderer>().bounds.size/1.5f;
			}else if(targetObject.GetComponent<Collider>() != null){
				offset = target.GetComponent<Collider>().bounds.size/1.5f;
			}else{
				Debug.Log("No bounds!");
			}
			GameObject cam = CameraManagerDeskworld.activeCamera;
			cam.transform.position = target.transform.position + offset;
			cam.transform.LookAt(target.transform);
			
		} */
		
		
		
	}
}