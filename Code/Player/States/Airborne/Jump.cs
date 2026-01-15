using System;
using Sandbox.Services;

namespace SuperGame.States;

public class JumpState : PlayerState
{

    private float desiredSpeed = 16.0f;
	private float jumpPower = 32.0f;
	public override void enterState( string enterMessage = "" )
	{

		if (enterMessage == "enterjump")
		{
			prettyPrint("jumping");
			coreCharacter.WorldPosition += Vector3.Up*10;
			core.character.velocity.z = jumpPower;

		}
		return;
	}

	public override void exitState()
	{
		return;
	}

	public override void updateState( float delta )
    {

		Vector3 velocityGoto = coreCharacter.cameraDirection*desiredSpeed;
		float lerpAmount = 8 * delta;

        coreCharacter.velocity.x = coreCharacter.velocity.x.LerpTo(velocityGoto.x, lerpAmount);
        coreCharacter.velocity.y = coreCharacter.velocity.y.LerpTo(velocityGoto.y, lerpAmount);

		//core.character.velocity.z = jumpPower;

		coreCharacter.velocity.z -= coreCharacter.gravityScale * delta;

		if ( coreCharacter.isGrounded() )
		{

			core.switchState("idle");
			coreCharacter.velocity.z = 0.0f;
			
			return;
		}

		coreCharacter.lookToward(
			coreCharacter.cameraDirection, 4.0f
		);
        
        return;
    }

    
}