using System;

namespace MTGLeagueManager.Model
{
    public class Match
    {
        public Match(Player p1, Player p2, DateTime date)
        {
            this.Player1 = p1;
            this.Player2 = p2;
            this.Date = date;
        }
        public DateTime Date { get; set; }

        public Player Player1 { get; set; }
        
        public Player Player2 { get; set; }

        public void UpdateScore(int scoreP1, int scoreP2)
        {
            if (ResultApplied)
                throw new Exception("Result already applied");

            Score1 = scoreP1;
            Score2 = scoreP2;

            var winP1 = GetWinPercentFor(Player1, Player2);
            var winP2 = GetWinPercentFor(Player2, Player1);

            var isP1Win = Score1 > Score2;
            double coeff1 = isP1Win ? 1 : 0;
            double coeff2 = isP1Win ? 0 : 1;

            Points1 = (int)Math.Round(League.KVALUE * (coeff1 - winP1));
            Points2 = (int)Math.Round(League.KVALUE * (coeff2 - winP2));

            Player1.Points += Points1;
            Player2.Points += Points2;

            ResultApplied = true;
        }

        public double GetWinPercentFor(Player player, Player opponent)
        {
            return 1 / (Math.Pow(10, (opponent.Points - player.Points) / 400) + 1);
        }

        public int Score1 { get; set; }
        
        public int Score2 { get; set; }

        public int Points1 { get; set; }

        public int Points2 { get; set; }

        public bool ResultApplied { get; private set; }
    }
}