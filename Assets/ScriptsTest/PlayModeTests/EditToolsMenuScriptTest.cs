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
//using Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject;
//using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static UnityEngine.Assertions.Assert;
using TMPro;
using System;

public class EditToolsMenuScriptTest {

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

    [UnityTest]
    public IEnumerator InitializeCorrectlyTest() {

        // wait for scene to finish loading
        yield return new WaitWhile(() => sceneLoaded == false);

        // find menu object
        GameObject toolsMenu = GameObject.Find("ToolsMenu");
        EditToolsMenuScript script = toolsMenu.GetComponent<EditToolsMenuScript>();

        // it found initial resources
        IsNull( script.workingTile );

        IsNotNull( script.flagMenu );
        IsNotNull( script.unitTypeDropdown );
        IsNotNull( script.unitFaceDropdown );
        IsNotNull( script.unitGroupDropdown );
        IsNotNull( script.unitStateDropdown );

        AreEqual( 0, script.unitStateDropdown.value );

        IsNotNull( script.mapTitleLabel );
        IsNotNull( script.mapSizeLabel );
        IsNotNull( script.tileTypeLabel );
        IsNotNull( script.tileFlagLabel );

        AreEqual( "Peasant/Goblin/Skeleton/Ghost", 
            String.Join( "/", script.unitTypeOptions ));
        AreEqual( "North/NorthEast/East/SouthEast/South/SouthWest/West/NorthWest", 
            String.Join( "/", script.unitFaceOptions ));
        AreEqual( "White(hero)/Red/Green/Blue", 
            String.Join( "/", script.unitGroupOptions ));
        AreEqual( "Active/Alert/Stunned/Sleep", 
            String.Join( "/", script.unitStateOptions ));

        // end of test
        yield return null;
    }
}
