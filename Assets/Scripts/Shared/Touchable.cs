// file Touchable.cs
// Correctly backfills the missing Touchable concept in Unity.UI's OO chain.
//
//  This is usually used to allow invisible buttons to be 'clicked'.
//  Buttons are best made 'invisible' by removing the image, BUT
//  the imnage is the part that is 'clickable'.  Go figure.
//
//  SEE: https://stackoverflow.com/questions/36888780/how-to-make-an-invisible-transparent-button-work/36892803
//

using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Touchable))]
public class Touchable_Editor : Editor { 
    public override void OnInspectorGUI(){}
}
#endif

public class Touchable:Text { 
    protected override void Awake() { base.Awake();} 
}