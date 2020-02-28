using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ProceduralChessBoardGenerator{
	 
	public class FlyCameraAdvanced : MonoBehaviour{

		public Slider camSpeedSlider;
		public static GameObject flyCamera;

		public float cameraSensitivity = 5.0f;
		public float climbSpeed = 10.0f;
		public float normalMoveSpeed = 10.0f;
		public float slowMoveFactor = 0.25f;
		public float fastMoveFactor = 5.0f;
		public float moveSpeed = 1000.0f;

		private float lerpSpeed = 100.0f;
		private float yaw = 0.0f;
		private float pitch = 0.0f;
		private float rotationX = 0.0f;
		private float rotationY = 0.0f;
		private bool inPosition = false;
	 
		void Awake (){
			flyCamera = this.gameObject;
		}
		
		bool isInputDisabled;
		
		void OnEnable() {
			isInputDisabled = false;
        }
	 
		void Update (){
			if(!isInputDisabled){
				if(Input.GetMouseButton(1)){
					yaw += cameraSensitivity * Input.GetAxis("Mouse X");
					pitch -= cameraSensitivity * Input.GetAxis("Mouse Y");
					pitch = Mathf.Clamp (pitch, -90, 90);
					transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
				}
		 
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
			
			}
		
		}
		
		public void OnValueChanged(float factor){
			
			climbSpeed = factor *5;
			normalMoveSpeed = factor*2;
		}
		
		void DisableInput(bool isInput){
			isInputDisabled = isInput;
		}
		
	}
}