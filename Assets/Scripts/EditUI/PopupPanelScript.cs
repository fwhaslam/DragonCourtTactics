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
	/// Standard Popup Panel.
	/// An onscreen button opens and closes the panel, which is an animation.
	/// Deselect event hides panel, buttons in panel apply choice.
	/// </summary>
	public class PopupPanelScript : MonoBehaviour, IDeselectHandler {

		public GameObject pickerButton;
		public GameObject animatorOwner;
		public string animBoolKey;

		internal Camera viewCam;
		internal RectTransform panelRect;
		internal GameObject addIcon, cutIcon;
		internal Animator showHideAnim;

		internal int mark = 0;


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
print(">>>> Popup Picker Clicked name=" + gameObject.name + "   mark=" + (++mark));

			if (!IsShowing()) {
				Show();
			}
			else {
				Hide();
			}

		}

//======================================================================================================================

		internal bool IsShowing() {
			return showHideAnim.GetBool(animBoolKey);
		}

		public void Show() {

			if (IsShowing()) return;

			//transform.position = pickerButton.transform.position;
			//gameObject.SetActive(true);

			showHideAnim.SetBool( animBoolKey, true );
			UnityTools.SetSelected( gameObject );
		}

		public void Hide() {

			if (!IsShowing()) return;

			//gameObject.SetActive(false);

			showHideAnim.SetBool( animBoolKey, false );
			//UnityTools.SetSelected( null );			// not right, I don't know how to 'unselect', but apparently the OnDeselect event occurs after
		}
	}

}