using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class test1 {

	[Test]
	public void test1SimplePasses() {
		// Use the Assert class to test conditions.
	}

	[Test]
	public void GameObject_CreatedWithGiven_WillHaveTheName()
	{
		var go = new GameObject("MyGameObject");
		Assert.AreEqual("MyGameObject", go.name);
	}
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator test1WithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
	[Test]
	public void LogAssertExample()
	{
		//Expect a regular log message
		LogAssert.Expect(LogType.Log, "Log message");
		//A log message is expected so without the following line
		//the test would fail
		Debug.Log("Log message");
		//An error log is printed
		Debug.LogError("Error message");
		//Without expecting an error log, the test would fail
		LogAssert.Expect(LogType.Error, "Error message");
	}

}
