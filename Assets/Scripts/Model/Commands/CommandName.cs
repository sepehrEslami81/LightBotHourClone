using System;

namespace Model.Commands
{
    /// <summary>
    /// Specifies the name of the command using enum so that we can better access the commands
    /// </summary>
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