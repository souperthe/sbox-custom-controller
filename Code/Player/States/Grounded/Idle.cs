using Sandbox.Services;

namespace SuperGame.States;

public class IdleState : PlayerState
{
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

		core.character.velocity = Vector3.Zero;

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


		if ( Input.AnalogMove.Length != 0.0f )
		{
			core.switchState("Move");

			return;
		}
        
        return;
    }

    
}