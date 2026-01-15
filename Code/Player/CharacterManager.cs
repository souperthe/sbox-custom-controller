using System;
using System.Runtime.InteropServices;
using Sandbox;


namespace SuperGame;
public class CharacterManager : Component
{

    [Property] public StateManager stateManager;
    [Property] public CharacterController mainController;
    [Property] public GameObject modelRoot;
    public CameraComponent mainCamera;

    public Vector3 velocity = new Vector3();
    public Vector3 cameraDirection = new Vector3();
    public float gravityScale = 122.0f;




    public bool canMove = true;

    private void prettyPrint(params object[] desiredItems)
    {

        string finalizedString = "";

        foreach (object item in desiredItems)
        {

            string stringedItem = item.ToString();

            finalizedString += stringedItem;
            
            continue;
        }

        Log.Info("CharacterManager -> " + finalizedString);

        return;
    }

    public void lookToward(Vector3 direction, float lerpAmount = 8.0f)
    {

        if ( direction == Vector3.Zero )
        {
            return;
        }

        Rotation newRotation = Rotation.LookAt(direction);
        Rotation lerpRotation = modelRoot.LocalRotation.LerpTo(newRotation, lerpAmount * Time.Delta);

        modelRoot.LocalRotation = lerpRotation;
        return;
    }

    public bool isGrounded()
    {
        return mainController.IsOnGround;
    }

    private Vector3 getCameraDirection()
    {

        CameraComponent currentCamera = Scene.Camera;

        Vector3 moveInput = Input.AnalogMove;
        Vector3 cameraForward = currentCamera.WorldRotation.Forward.Normal.WithZ(0);
        Vector3 cameraRight = currentCamera.WorldRotation.Right.Normal.WithZ(0);

        Vector3 finalDirection = (cameraRight * -moveInput.y) + (cameraForward * moveInput.x);
        
        return finalDirection.Normal;
    }


	protected override void OnAwake()
	{
        prettyPrint("starting up");
        mainCamera = Scene.Camera;

        //prettyPrint(mainCamera);

		return;
	}


	protected override void OnUpdate()
	{

        cameraDirection = getCameraDirection();

        if ( canMove )
        {
            mainController.Velocity = velocity*25;
            mainController.Move();
            mainController.ApplyFriction(0);
            mainController.Accelerate(mainController.Velocity);
        }
		
        return;
	}
    
}