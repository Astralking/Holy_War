using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War
{
    public class TurnTracker
    {
        public int CurrentTurn { get; private set; }
        public int CurrentTeam { get; private set; }

        private readonly int _totalTeams;

        public TurnTracker(int totalTeams)
        {
            CurrentTurn = 1;
            CurrentTeam = 1;

            _totalTeams = totalTeams;
        }

        public bool NextTurn()
        {
            if (CurrentTeam != _totalTeams)
                CurrentTeam++;
            else
            {
                CurrentTurn++;
                CurrentTeam = 1;
            }

            return true;
        }
    }
}
