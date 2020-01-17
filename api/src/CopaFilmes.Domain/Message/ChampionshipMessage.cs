using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Domain.Message
{
    public static partial class Message
    {

        public static class ChampionshipMessage
        {
            public static string NumberParticipantsShouldBeEqual (int numberOfParticipants) => $"O número de participantes no campeonato deve ser igual a {numberOfParticipants}";
        }
    }
}
