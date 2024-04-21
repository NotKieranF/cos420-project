using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class IntegrationTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void IntegrationTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator IntegrationTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(1);
        
        // Get references to player objects
        GameObject player1 = GameObject.Find("Player 1");
        GameObject player2 = GameObject.Find("Player 2");
        
        // Check if references to player objects were able to be found
        Assert.IsNotNull(player1, "Player 1 not loaded");
        Assert.IsNotNull(player2, "Player 2 not loaded");
        
        yield return null;
    }
    
    // Run once before any tests
    [OneTimeSetUp]
    public void SetUp()
    {
        // Load scene to test
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
