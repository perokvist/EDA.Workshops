using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS.Tests
{
    public class HighScoreView
    {
        public HighScoreView()
        {
            _gameState = new GameState();
            Rows = new List<ScoreRow>();
        }
        private GameState _gameState;
        public HighScoreView When(IEvent @event)
        {
            _gameState.When((dynamic)@event);
            return this;
        }

        public HighScoreView When(RoundEnded @event)
        {
            _gameState.When(@event);
            if (Rows.Any(r => r.PlayerId == @event.Winner))
            {
                var row = Rows.Single(r => r.PlayerId == @event.Winner);
                row.RoundsWon++;
                row.RoundsPlayed++;
                row.Rank--;
            }
            else
            {
                Rows.Add(new ScoreRow { PlayerId = @event.Winner, Rank = 0, RoundsPlayed = 1, RoundsWon = 1 });
            }

            if (Rows.Any(r => r.PlayerId == @event.Looser))
            {
                var row = Rows.Single(r => r.PlayerId == @event.Looser);
                row.RoundsPlayed++;
                row.Rank++;
            }
            else
            {
                Rows.Add(new ScoreRow { PlayerId = @event.Looser, Rank = 0, RoundsPlayed = 1, RoundsWon = 1 });
            }
            return this;
        }

        public HighScoreView When(GameEnded @event)
        {

            var winCountPlayer1 = _gameState.Rounds.Count(rw => rw.Winner == _gameState.Player1);
            var winCountPlayer2 = _gameState.Rounds.Count(rw => rw.Winner == _gameState.Player2);

            var scorePlayer1 = Rows.Single(r => r.PlayerId == _gameState.Player1);
            var scorePlayer2 = Rows.Single(r => r.PlayerId == _gameState.Player2);

            scorePlayer1.GamesPlayed++;
            scorePlayer2.GamesPlayed++;

            if (winCountPlayer1 > winCountPlayer2)
            {
                scorePlayer1.GamesWon++;
            }

            if (winCountPlayer2 > winCountPlayer1)
            {
                scorePlayer2.GamesWon++;
            }

            _gameState.When(@event);

            return this;
        }

        public HighScoreView When(GamePlayed @event)
        {
            if (Rows.Any(r => r.PlayerId == @event.Winner))
            {
                var row = Rows.Single(r => r.PlayerId == @event.Winner);
                row.GamesWon++;
                row.GamesPlayed++;
                row.Rank--;
            }
            else
            {
                Rows.Add(new ScoreRow { PlayerId = @event.Winner, Rank = 0, GamesPlayed = 1, GamesWon = 1 });
            }

            if (Rows.Any(r => r.PlayerId == @event.Looser))
            {
                var row = Rows.Single(r => r.PlayerId == @event.Looser);
                row.GamesPlayed++;
                row.Rank++;
            }
            else
            {
                Rows.Add(new ScoreRow { PlayerId = @event.Looser, Rank = 0, GamesPlayed = 1, GamesWon = 0 });
            }
            return this;
        }

        public List<ScoreRow> Rows { get; set; }
        public class ScoreRow
        {
            public int Rank { get; set; }
            public string PlayerId { get; set; }
            public int GamesWon { get; set; }
            public int RoundsWon { get; set; }
            public int GamesPlayed { get; set; }
            public int RoundsPlayed { get; set; }
        }
    }
}
