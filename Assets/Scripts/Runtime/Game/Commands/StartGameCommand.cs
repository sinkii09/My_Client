using Nara.Core.Architecture;
using Nara.Game;
using Nara.System;
using Nara.System.UI;
using UnityEngine;

public class StartGameCommand : Command
{
    protected override void OnExecute()
    {

    }
}

public class ClosePopupEvent : IEvent
{
    PopupUI popupUI;
    public ClosePopupEvent(PopupUI popupUI)
    {
        this.popupUI = popupUI;
    }
}
