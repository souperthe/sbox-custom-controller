using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Sandbox.MovieMaker.Properties;
using Sandbox.Services;

namespace SuperGame;
public class StateManager : Component
{

    private Dictionary<String, PlayerState> allStates = [];
    
    public PlayerState currentState;
    
    [Property]
    public CharacterManager character;
    [Property]
    public GameObject stateHolder;
    
    [Property]
    public string startingState = "Idle";
    [Property]
    public bool runStates = true;


    public void prettyPrint(params object[] desiredItems)
    {

        string finalizedString = "";

        foreach (object item in desiredItems)
        {

            string stringedItem = item.ToString();

            finalizedString += stringedItem;
            
            continue;
        }

        Log.Info("StateManager -> " + finalizedString);

        return;
    }

    public void switchState(string desiredState, string enterMessage = "")
    {

        PlayerState lastState = currentState;
        string lowerState = desiredState.ToLower();

        if ( !allStates.ContainsKey(lowerState) )
        {
            prettyPrint(desiredState, " does not exist!");
            return;
        }

        PlayerState newState = allStates[lowerState];

        newState.enterState(enterMessage);

        if (lastState != null)
        {
            lastState.exitState();
        }

        prettyPrint("swapped to state: ", lowerState);

        currentState = newState;
        //stateValid = true;
        return;
    }

    private void setupStates()
    {
        prettyPrint("Now setting up states");

        List<GameObject> allChildren = stateHolder.Children;

        foreach (GameObject child in allChildren)
        {

            PlayerState possibleState = child.Components.Get<PlayerState>();
            string lowerName = child.Name.ToLower();

            if (possibleState == null)
            {
                continue;
            }

            possibleState.core = this;
            possibleState.coreCharacter = character;

            allStates[lowerName] = possibleState;

            prettyPrint(child.Name, " has been setup!");
            
            continue;
        }

        return;
    }

    protected override void OnAwake()
    {
        setupStates();

        switchState(startingState);

        return;
    }

	protected override void OnUpdate()
	{

        if ( currentState != null )
        {
            currentState.updateState(Time.Delta);
            return;
        }


        return;
	}

}