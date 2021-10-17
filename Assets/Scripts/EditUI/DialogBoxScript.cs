//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Most fields are set by developer.
/// The actions are set by the dialog invocation.
/// </summary>
public class DialogBoxScript : MonoBehaviour {

    public GameObject modalPanel;   // dialog becomes modal when panel is provided

    [Header("Header")]
    [SerializeField]
    internal Transform headerArea;
    [SerializeField]
    internal TMP_Text titleTxt;

    [Header("Content")]
    [SerializeField]
    internal Transform contentArea;

    [Space()]
    [SerializeField]
    internal Transform horzLayout;
    [SerializeField]
    internal Image horzImage;
    [SerializeField]
    internal TMP_Text horzTxt;

    [Space()]
    [SerializeField]
    internal Transform vertLayout;
    [SerializeField]
    internal Image vertImage;
    [SerializeField]
    internal TMP_Text vertText;

    [Space()]
    [SerializeField]
    internal Transform fileLayout;
    [SerializeField]
    internal TMP_Dropdown filenameDD;
    [SerializeField]
    internal Transform fileSavePanel;
    [SerializeField]
    internal TMP_InputField filenameInp;
    [SerializeField]
    internal TMP_Text fileTxt;

    [Header("Footer")]
    [SerializeField]
    internal Transform footerArea;
    [SerializeField]
    internal Button confirmBtn;
    [SerializeField]
    internal Button declineBtn;
    [SerializeField]
    internal Button otherBtn;

    internal Action onConfirm,onDecline,onOther;

    internal bool isSaveDialog;
    internal int selectedFileIndex;
    internal List<string> filenamesForDialog;
 
//======================================================================================================================

    /// <summary>
    /// One time changes
    /// </summary>
	public void Awake() {
        this.filenameDD.onValueChanged.AddListener( OnFileSelect );
	}

//======================================================================================================================

	/// <summary>
	/// Uses Horizontal layout, image is optional.  Other button is unused.
	/// </summary>
	/// <param name="title"></param>
	/// <param name="image"></param>
	/// <param name="message"></param>
	/// <param name="confirm"></param>
	public void ShowAcknowledgeDialog( string title, Sprite image, string message ) {

        // hide non-default panels, move dialog, activate modal panel
        Prep();

        if (title!=null) { 
            this.titleTxt.gameObject.SetActive(true);
            this.titleTxt.text = title;
        }

        // use horz layout
        this.horzLayout.gameObject.SetActive(true);

        if (image!=null) { 
            this.horzImage.gameObject.SetActive( true );
            this.horzImage.sprite = image;
        }
        this.horzTxt.text = message;

        // setup buttons and functions
        this.confirmBtn.gameObject.SetActive(true);

        // all ready, show it!
        Show();
	}
    
	/// <summary>
	/// Uses Horizontal layout, image is optional.  Other button is unused.
	/// </summary>
	/// <param name="title"></param>
	/// <param name="image"></param>
	/// <param name="message"></param>
	/// <param name="confirm"></param>
	public void ShowConfirmDialog( string title, Sprite image, string message, Action confirm ) {

        // hide non-default panels, move dialog, activate modal panel
        Prep();

        if (title!=null) { 
            this.titleTxt.gameObject.SetActive(true);
            this.titleTxt.text = title;
        }

        // use horz layout
        this.horzLayout.gameObject.SetActive(true);

        if (image!=null) { 
            this.horzImage.gameObject.SetActive( true );
            this.horzImage.sprite = image;
        }
        this.horzTxt.text = message;

        // setup buttons and functions
        this.onConfirm = confirm;

        this.confirmBtn.gameObject.SetActive(true);
        this.declineBtn.gameObject.SetActive(true);

        // all ready, show it!
        Show();
	}
    
    /// <summary>
    /// Uses Horizontal layout, image is optional.  Other button is unused.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="image"></param>
    /// <param name="message"></param>
    /// <param name="confirm"></param>
    public void ShowFileLoadDialog( List<string> filenames, Action confirm ) {

        // hide non-default panels, move dialog, activate modal panel
        Prep();

        this.titleTxt.gameObject.SetActive(true);
        this.titleTxt.text = "Select File to Load";

        // use file layout
        this.fileLayout.gameObject.SetActive(true);
        this.filenameDD.gameObject.SetActive(true);
 
        this.filenameDD.ClearOptions();
        this.filenameDD.AddOptions( filenames );
        this.filenamesForDialog = filenames;
  
        // setup buttons and functions
        this.onConfirm = confirm;

        this.confirmBtn.gameObject.SetActive(true);
        this.declineBtn.gameObject.SetActive(true);

        // all ready, show it!
        Show();
	}
    
    /// <summary>
    /// Uses Horizontal layout, image is optional.  Other button is unused.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="image"></param>
    /// <param name="message"></param>
    /// <param name="confirm"></param>
    public void ShowFileSaveDialog( List<string> filenames, Action confirm ) {

        // hide non-default panels, move dialog, activate modal panel
        Prep();

        this.titleTxt.gameObject.SetActive(true);
        this.titleTxt.text = "Select Filename to Save";

        // use file layout
        this.fileLayout.gameObject.SetActive(true);
        this.filenameDD.gameObject.SetActive(true);
        this.fileSavePanel.gameObject.SetActive(true);
 
        this.filenameDD.ClearOptions();
        this.filenameDD.AddOptions( filenames );
        this.filenamesForDialog = filenames;

        this.isSaveDialog = true;
  
        // setup buttons and functions
        this.onConfirm = confirm;

        this.confirmBtn.gameObject.SetActive(true);
        this.declineBtn.gameObject.SetActive(true);

        // all ready, show it!
        Show();
	}

    public void OnFileSelect(int value) {
         selectedFileIndex = value; 
        if (isSaveDialog) this.filenameInp.text = filenamesForDialog[value];
	}

    public string GetSelectedFilename() {
        if (isSaveDialog) return this.filenameInp.text;
        return filenamesForDialog[selectedFileIndex];
	}

    
//======================================================================================================================

    /// <summary>
    /// Called at the start of a dialog, closes all defaults to minimize dialog logic.
    /// </summary>
    internal void Prep() {

        selectedFileIndex = 0;
        this.isSaveDialog = false;

        this.titleTxt.gameObject.SetActive( false );

        this.horzLayout.gameObject.SetActive( false );
        this.vertLayout.gameObject.SetActive( false );
        this.fileLayout.gameObject.SetActive( false );
        
        this.horzImage.gameObject.SetActive( false );
        this.vertImage.gameObject.SetActive( false );
        this.fileSavePanel.gameObject.SetActive( false );

        this.declineBtn.gameObject.SetActive( false );
        this.otherBtn.gameObject.SetActive( false );

        this.onConfirm = null;
        this.onDecline = null;
        this.onOther = null;

        if (modalPanel!=null) modalPanel.SetActive(true);
	}

    internal void Show() {
        //transform.localPosition = new Vector3( 200 -1100, 0);   // 0, -700 ?
        gameObject.SetActive(true);
	}

    internal void Hide() {

        gameObject.SetActive(false);
        if (modalPanel!=null) modalPanel.SetActive(false);

	}

//======================================================================================================================

    public void DoConfirm() {
        Hide();
print("DO CONFIGRM = "+onConfirm);
        if (onConfirm!=null) onConfirm.Invoke();
    }

    public void DoDecline() {
        Hide();
        if (onDecline!=null) onDecline.Invoke();
    }

    public void DoOther() {
        Hide();
        if (onOther!=null) onOther.Invoke();
	}

}
