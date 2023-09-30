using System;

namespace Model.Commands
{
    [Serializable]
    public enum CommandName
    {
        MoveForward,
        TurnLeft,
        TurnRight,
        Jump,
        ChangeCubeLightStatus,
        Procedure1
    }
}