/* InstrutionController.cs
 * Created by: Eric Wang
 * Date Created: November 27th, 2015
 * Date Modified: December 15th, 2015
 * Description: This script is used to manage button functions for the instructions screen.
 * Note: The script is no longer used because the instruction scene is merged with start scene in the current version.
 */

using UnityEngine;
using System.Collections;

public class InstructionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//PUBLIC METHODS ++++++++++++++++++++++++++++

	// Start Button Event Handler
	public void OnStartButtonClick() {
		Application.LoadLevel ("Start");
	}
}
