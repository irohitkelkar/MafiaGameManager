using System;
using System.Collections.Generic;
using System.Text;

namespace Mafia_GameManager.Core
{
    public class GameManager
    {
        //Variables
        private List<Player> _players;
        private List<Character> _characters;
        private int _maxPlayers;


        //Properties
        public int NoOfMafia{ get; set; }
        public int NoOfDoctor { get; set; }
        public int NoOfPolice { get; set; }
        public int NoOfCitizens{ get; set; }

        public int NoOfPlayers { get => _players.Count; }

        public GameData CurrentGameData { get; private set; }

        public GameManager()
        {
            _players = new List<Player>();
            _characters = new List<Character>();
        }

        public void InitGame()
        {
            _characters.Clear();
            _characters.AddRange(GenerateCharacterArray());
            _maxPlayers = _characters.Count;
        }
        public void AddPlayer(Player pl)
        {
            _players.Add(pl);
        }

        public void AddPlayers(List<Player> pl)
        {
            _players.AddRange(pl);
        }

        public void ClearPlayerList()
        {
            _players.Clear();
        }

        public void RemovePlayer(Player pl)
        {
            _players.Remove(pl);
        }

        private IEnumerable<Character> GenerateCharacterArray()
        {
           List<Character> list = new List<Character>();

            for (int i = 1; i <= NoOfMafia; i++)
            {
                list.Add(new Character("Mafia"));
            }

            for (int i = 1; i <= NoOfDoctor; i++)
            {
                list.Add(new Character("Doctor"));
            }

            for (int i = 1; i <= NoOfPolice; i++)
            {
                list.Add(new Character("Police"));
            }

            for (int i = 1; i <= NoOfCitizens; i++)
            {
                list.Add(new Character("Citizen"));
            }

            return list;
        }

        public GameData GenerateGame()
        {
            GameData gameData = new GameData();

            Random random = new Random();
            var tempPlayers = new List<Player>(_players);
            var tempCharacters = new List<Character>(_characters);

            if(tempPlayers.Count != tempCharacters.Count)
            {
                gameData.Error = "No of Player should be equal to No Of Characters";
            }
            else
            {
                gameData.Players = new List<Player>();

                while (tempPlayers.Count > 0)
                {
                    int x = random.Next(tempPlayers.Count);
                    int y = random.Next(tempCharacters.Count);

                    tempPlayers[x].GameCharacter = tempCharacters[y];
                    gameData.Players.Add(tempPlayers[x]);

                    tempPlayers.RemoveAt(x);
                    tempCharacters.RemoveAt(y);
                    _maxPlayers--;
                }

            }

            CurrentGameData = gameData;

            return gameData;
        }
    }
}
