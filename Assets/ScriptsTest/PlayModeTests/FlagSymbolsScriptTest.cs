//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

using Shared;

using TMPro;
using System;
using Realm.Enums;

//using Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject;
//using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
//using static UnityEngine.Assertions.Assert;
using static UnityEngine.MonoBehaviour;
using static Tools.ReadFieldsTool;
using static NUnit.Framework.Assert;

/// <summary>
/// Ensure that the FlagSymbols element has one value for each FlagEnum.
/// </summary>
public class FlagSymbolsScriptTest {

    bool sceneLoaded;
       
    [OneTimeSetUp]
    public void OneTimeSetup() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene( GlobalValues.EditSceneName, LoadSceneMode.Single);
    }
       
    [OneTimeTearDown]
    public void OneTimeTearDown() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) => sceneLoaded = true;

//======================================================================================================================

    /// <summary>
    /// Values in the FlagSymbols table match the FlagEnum table.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator ConfiguredCorrectlyTest() {

        // wait for scene to finish loading
        yield return new WaitWhile(() => sceneLoaded == false);

        // find menu object
        GameObject toolsMenu = GameObject.Find("FlagSymbols");
        FlagSymbolsScript script = toolsMenu.GetComponent<FlagSymbolsScript>();

        // it found initial resources
        List<string> fieldNames = new List<string>();
        foreach( var name in Enum.GetNames( typeof(FlagEnum)) ) fieldNames.Add( name );

        // check 'none'
        IsFalse(  HasField(script,"None"), "Field [None] must not be defined.");
        fieldNames.Remove( "None");
        IsNull( FlagSymbolsScript.GetFlagSprite( FlagEnum.None ) );

        // check all other flags, should have a sprite and retrieve same from script
        foreach (var name in fieldNames) {

            IsTrue( HasField(script,name), "Field ["+name+"] must exist." );
            Sprite value = ReadField<FlagSymbolsScript,Sprite>(script,name);
            IsNotNull( value, "Field ["+name+"] must have value." );
            print("FIELD["+name+"] = "+value);

            FlagEnum flagId = (FlagEnum)Enum.Parse( typeof(FlagEnum), name );
            Sprite symbol = FlagSymbolsScript.GetFlagSprite( flagId );
            AreSame( value, symbol, "Field and Getter must return same Sprite for ["+name+"]" );
		}

        // end of test
        yield return null;
    }
}
