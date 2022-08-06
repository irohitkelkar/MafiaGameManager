using System;
using System.Collections.Generic;
using System.Text;

namespace Mafia_GameManager.Core
{
    public class Character
    {
        public string Name { get; set; }

        public string Description
        {
            get
            {
                string desc;
                switch (Name)
                {
                    case "Mafia":
                        desc = "Burn them All !!";
                        break;

                    case "Doctor":
                        desc = "Save the city";
                        break;

                    case "Police":
                        desc = "Catch the Mafia";
                        break;

                    default:
                        desc = "Spot the Mafia";
                        break;

                }

                return desc;
            }
        }
        public Character(string name)
        {
            Name = name;
        }

    }
}
