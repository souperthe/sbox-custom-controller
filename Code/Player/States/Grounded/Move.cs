using System;
using Sandbox.Services;

namespace SuperGame.States;

public class MoveState : PlayerState
{

    private float desiredSpeed = 16.0f;
	public override void enterState( string enterMessage = "" )
	{
		//prettyPrint("entered idle");
		return;
	}

	public override void exitState()
	{
		return;
	}

	public override void updateState( float delta )
    {

        core.character.velocity.x = core.character.cameraDirection.x*desiredSpeed;
        core.character.velocity.y = core.character.cameraDirection.y*desiredSpeed;

		if ( !core.character.mainController.IsOnGround )
		{
			core.switchState("Jump");

			return;
		}

		if ( Input.Pressed("Jump") )
		{
			core.switchState("Jump", "enterjump");

			return;
		} 

		
        if ( Input.AnalogMove.Length == 0.0f )
		{
			core.switchState("Idle");
			
			return;
		}

		coreCharacter.lookToward(
			coreCharacter.cameraDirection, 16.0f
		);
        
        return;
    }

    
}