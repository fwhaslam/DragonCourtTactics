//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace EditUI {

	using System;
	using Shared;
	using UnityEngine;
	using UnityEngine.EventSystems;

	using static Shared.UnityTools;


	/// <summary>
	/// Setting up your popup panel.
	/// Create the panel, leave it active somewhere offscreen.
	/// Add invisible buttons which are 'Touchable'.  Add symbols to the buttons.
	/// Add a trigger button, link it here as the 'pickerButton'.
	/// Add this script to the panel.
	/// </summary>
	public class PopupPanelScript : MonoBehaviour, IDeselectHandler {	//, IPointerClickHandler, ISelectHandler

		public GameObject pickerButton;
		public GameObject animatorOwner;

		internal Camera viewCam;
		internal RectTransform panelRect;
		internal GameObject addIcon, cutIcon;
		internal Animator showHideAnim;

		internal int mark = 0;
		internal readonly string ANIM_KEY = "OpenSizePanel";

		/// <summary>
		/// Called just before object is instantiated.
		/// Might be called DURING SizePickerButtonClicked
		/// </summary>
		public void Start() {

			viewCam = GameObject.Find("UI Camera").GetComponent<Camera>();
			panelRect = GetComponent<RectTransform>();
			showHideAnim = animatorOwner.GetComponent<Animator>();

			addIcon = GameObject.Find("AddRowIcon");
			cutIcon = GameObject.Find("CutRowIcon");

		}

		/// <summary>
		/// When the deselect occurs in picker or panel, ignore it and wait for button events.
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDeselect(BaseEventData eventData) {
//print(">>>> On DESelect eventData   mark=" + (++mark));

			// WORKAROUND: unity deselect events occure before button events, so can close panel before selection
			// take no action if deselect is in these tools, wait for pointer-click event
			if (eventData.GetType() == typeof(PointerEventData)) {
				Vector2 where = ((PointerEventData)eventData).position;
				if (IsScreenClickInObject(pickerButton, where, viewCam)) return;
				if (IsScreenClickInObject(gameObject, where, viewCam)) return;
			}
	
			// action
			Hide();
		}


		/// <summary>
		/// Called when Picker button is clicked.  Attach to SizePicker.
		/// </summary>
		public void PickerButtonClicked() {
//print(">>>> Size Picker Clicked showing?="+IsActive()+"   mark="+(++mark) );

			if (!IsShowing()) {
				Show();
			}
			else {
				Hide();
			}

		}

//======================================================================================================================

		internal bool IsShowing() {
			return showHideAnim.GetBool(ANIM_KEY);
		}

		public void Show() {

			if (IsShowing()) return;

			//transform.position = pickerButton.transform.position;
			//gameObject.SetActive(true);

			showHideAnim.SetBool( ANIM_KEY, true );
			UnityTools.SetSelected( gameObject );
		}

		public void Hide() {

			if (!IsShowing()) return;

			//gameObject.SetActive(false);

			showHideAnim.SetBool( ANIM_KEY, false );
			UnityTools.SetSelected( null );			// ??
		}
	}

}