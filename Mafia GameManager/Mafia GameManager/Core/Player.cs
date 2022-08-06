using System;
using System.Collections.Generic;
using System.Text;

namespace Mafia_GameManager.Core
{
    public class Player
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Character GameCharacter { get; set; }
    }
}
