using System;

namespace MTGLeagueManager
{
    #region Player
    public class PlayerAlreadyCreated : Exception { }

    public class PlayerNotExist : Exception { }

    #endregion

    #region Match
    public class MatchNotCreated : Exception { }

    public class MatchAlreadyRemoved : Exception { }

    public class MatchAlreadyPlayed : Exception { }
    #endregion
}
