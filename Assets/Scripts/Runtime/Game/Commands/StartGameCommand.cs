using Nara.Core.Architecture;
using Nara.Game;
using Nara.System;
using UnityEngine;

public class StartGameCommand : Command
{
    protected override void OnExecute()
    {
        GameApp.Inteface.GetSystem<EventBus>().Raise(new StartGameEvent());  
    }
}
public class StartGameEvent : IEvent
{

}
