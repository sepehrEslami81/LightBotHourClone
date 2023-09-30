using System;
using Model.Commands;
using UnityEngine;

namespace Model.Ui
{
    /// <summary>
    /// A model to save the command sprite to change the command button sprite
    /// </summary>
    [Serializable]
    public class CommandSpriteModel
    {
        [SerializeField] private CommandName commandName;
        [SerializeField] private Sprite iconSprite;

        public Sprite IconSprite => iconSprite;
        public CommandName CommandName => commandName;
    }
}