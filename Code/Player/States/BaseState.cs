
namespace SuperGame;
public abstract class PlayerState : Component
{

    public StateManager core;
    public CharacterManager coreCharacter;

    protected void prettyPrint(params object[] desiredItems)
    {

        string finalizedString = "";

        foreach (object item in desiredItems)
        {

            string stringedItem = item.ToString();

            finalizedString += stringedItem;
            
            continue;
        }

        Log.Info(GameObject.Name + " -> " + finalizedString);

        return;
    }

    public abstract void enterState(string enterMessage = "");
    public abstract void exitState();
    public abstract void updateState(float delta);

}