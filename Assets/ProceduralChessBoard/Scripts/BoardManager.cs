using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralChessBoardGenerator{

	public class BoardManager : MonoBehaviour {
		
		public GameObject tilePrefab;
		public Material black;
		public Material white;
		public int size;
		public GameObject popUpWindow;
		public Text widthText;
		public Text lengthText;
		public Text popupText;

		private List<GameObject> _tiles;
		private int _cubeCount;
		private float _tileSize;
		private bool _isReversed;
		private bool _isRegular = true;
		private int _factor = 1;
		
		void Awake(){
			var renderer = tilePrefab.GetComponent<MeshRenderer>();
			_tileSize = renderer.bounds.size.x;
			_cubeCount = (int)(size * size * _factor);
		}

		void Start () {
			popUpWindow.SetActive(false);
			if(EvenChecking(size) && MinimumChecking(size)){
				widthText.text = size.ToString();
				lengthText.text = (size * _factor).ToString();
				_tiles = CreateTiles(_cubeCount);	
			}else{
				StartCoroutine(PopUpWindowCor());
			}
		}
		
		bool EvenChecking(int boardSize){
			return (boardSize % 2 == 0)?true:false;
		}
		
		bool MinimumChecking(int minSize){
			return (minSize >= 8)?true:false;
		}
		
		void Update () {
			if(Input.GetKeyDown(KeyCode.Space)){
				OnRecreateBoardButton();
			}
		}
		
		IEnumerator RecreateTilesCor(){
			yield return new WaitForSeconds(1.0f);
			_cubeCount = (int)(size * size * _factor);
			_tiles = CreateTiles(_cubeCount);
		}
		
		private List<GameObject> CreateTiles(int count){
			int x = 0;
			int z = 0;
			_tiles = new List<GameObject>();
			int n = 0;
			for(int i = 0; i < count; i++){
				GameObject tile = Instantiate(tilePrefab) as GameObject;
				tile.name = tilePrefab.name + n;
				tile.transform.SetParent(transform,false);
				
				x = i /(size);
				z = (i - x * size);	
				
				if(i % 2 == 0){
					
					ReversedColors(tile, i);
				}else{
					NormalColors(tile, i);
				}
					
				tile.transform.localPosition = new Vector3(x,0,z);
				n++;
				_tiles.Add(tile);
			}
			
			return _tiles;
		}
		
		void NormalColors(GameObject tile, int i){
			Renderer renderer = tile.GetComponent<Renderer>();
			if (i % size == 0){
				_isReversed = !_isReversed;
			}
			if(_isReversed){
				renderer.material.color = Color.white;
			}else{
				renderer.material.color = Color.black;	
			}
		}
		
		void ReversedColors(GameObject tile, int i){
			Renderer renderer = tile.GetComponent<Renderer>();
			if (i % size == 0){
				_isReversed = !_isReversed;
			}
			if(_isReversed){
				renderer.material.color = Color.black;
			}else{
				renderer.material.color = Color.white;	
			}
		}
		
		IEnumerator PopUpWindowCor(){
			popUpWindow.SetActive(true);
			yield return new WaitForSeconds(5.0f);
			popUpWindow.SetActive(false);
		}
		
		public void OnInsertTilesCountIF(string param){
			size = int.Parse(param);
		}
		
		public void ToggleSquareToggle(bool isSquare){
			_isRegular = isSquare;
			if(_isRegular){
				_factor = 1;
			}else{
				_factor = 2;
			}
		}
		
		public void OnRecreateBoardButton(){
			if(MinimumChecking(size)){
				if(EvenChecking(size)){
					foreach(Transform child in transform){
						GameObject.Destroy(child.gameObject);
					}
					_tiles.Clear();
					StartCoroutine(RecreateTilesCor());
					widthText.text = size.ToString();
					lengthText.text = (size * _factor).ToString();
				}else{
					popupText.text = "The Tiles Count needs to be even. Please try again.";
					StartCoroutine(PopUpWindowCor());
				}
			}else{
				popupText.text = "The Tiles Count needs to be greater than 8. Please try again.";
				StartCoroutine(PopUpWindowCor());
			}
		}
	}
}
