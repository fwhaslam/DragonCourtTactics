//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace EditUI {

	using System;
	using Shared;
	using UnityEngine;
	using UnityEngine.EventSystems;


	public class ChangeSizePanelScript : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler {

		public GameObject sizePicker;
		public int rows,columns;

		internal Camera viewCam;
		internal RectTransform panelRect;
		internal GameObject addIcon, cutIcon;

		internal int mark = 0;

		/// <summary>
		/// Called just before object is instantiated.
		/// Might be called DURING SizePickerButtonClicked
		/// </summary>
		public void Start() {

			viewCam = GameObject.Find("UI Camera").GetComponent<Camera>();
			panelRect = GetComponent<RectTransform>();

			addIcon = GameObject.Find("AddRowIcon");
			cutIcon = GameObject.Find("CutRowIcon");

			Hide();
		}

		internal bool IsActive() { return gameObject.activeSelf; }

		/// <summary>
		/// When the 
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDeselect(BaseEventData eventData) {
print(">>>> On DESelect eventData   mark=" + (++mark));     // ??? space
			if (eventData.GetType() == typeof(PointerEventData)) {
				ClickForPanel(((PointerEventData)eventData).position);
			}
			else {
				Hide();
			}
		}

		/// <summary>
		/// When the 
		/// </summary>
		/// <param name="eventData"></param>
		public void OnSelect(BaseEventData eventData) {
print(">>>> On Select eventData   mark=" + (++mark));     // ??? space
print("EVENT TYPE = "+eventData.GetType() );
		}


		/// <summary>
		/// Click inside panel selects one of the options.
		/// </summary>
		/// <param name="eventData"></param>
		public void OnPointerClick(PointerEventData eventData) {
print(">>>> OnPointerClick mark="+(++mark));	// screen space
			ClickForPanel( eventData.position );
		}
		

		/// <summary>
		/// Called when SizePicker button is clicked.  Attach to SizePicker.
		/// </summary>
		public void SizePickerButtonClicked() {
print(">>>> Size Picker Clicked showing?="+IsActive()+"   mark="+(++mark) );

			if (!IsActive()) {
				Show();
			}
			else {
				Hide();
			}

		}

//======================================================================================================================

		/// <summary>
		/// Click events which trigger a response from the CHangeSizePanel.
		/// Deselect events can occur WITHIN the panel, which SHOULD alter the map.
		/// </summary>
		/// <param name="click"></param>
		internal void ClickForPanel( Vector2 click ) {

		// ASSUME CLICK IS SCREEN = 140 / 1200
print("CLICK="+click);

			bool isClickInPanel = UnityTools.IsScreenClickInObject(gameObject, click, viewCam);
			bool isClickInPicker = UnityTools.IsScreenClickInObject(sizePicker, click, viewCam);
print("InPanel="+isClickInPanel+"   InPicker="+isClickInPicker);
			// when the click is in the picker, the event is handled elsewhere
			if (!isClickInPanel && isClickInPicker) return;

			Hide();

			if (!isClickInPanel) {
				return;
			}

			Vector2 localPoint = UnityTools.ScreenPointToLocal( gameObject, click, viewCam );
			//Vector2 localPoint;
			//RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, click, viewCam, out localPoint);
print("LOCAL PT = " + localPoint);
			int columnPick = Mathf.Clamp( (int)( columns * localPoint.x / panelRect.rect.width ), 0, columns-1 );
			int rowPick = Mathf.Clamp( (int)( rows * localPoint.y / panelRect.rect.height ), 0, rows-1 );
			int pick = rowPick * columns + columnPick;
print("PICK="+pick);

			switch ( pick ) {
				case 0: MapHandlerScript.instance.AddRowToMap(); break;
				case 1: MapHandlerScript.instance.CutRowFromMap(); break;
			}

		}

		public void Show() {
print("SHOW showing="+IsActive());

			if (IsActive()) return;

			// rotate icons to match camera orientation
			//float turn = Camera.main.transform.localEulerAngles.z;
			//float rotateZ = MathTools.Modulus( turn, 90f );

			//addIcon.transform.rotation = Quaternion.Euler( 45f, 0, -rotateZ );
			//cutIcon.transform.rotation = Quaternion.Euler( 45f, 0, -rotateZ );

print(">>>> SETTING ISSHOWING TO TRUE");
			transform.position = sizePicker.transform.position;
			gameObject.SetActive(true);
			UnityTools.SetSelected( gameObject );
			
		}

		public void Hide() {
print("HIDE showing="+IsActive());
			if (!IsActive()) return;

print(">>>> SETTING ISSHOWING TO FALSE");
			gameObject.SetActive(false);
		}
	}

}