using System;

namespace Model.Commands
{
    [Serializable]
    public enum CommandName
    {
        MoveForward = 1,
        TurnLeft = 2,
        TurnRight = 3,
        Jump = 4,
        ChangeCubeLightStatus = 5
    }
}