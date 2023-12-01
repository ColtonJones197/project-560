using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace RazorAppView
{
    public static class ChessPlayerDatabase
    {
        public static IReadOnlyList<Player> players = new SqlPlayerRepository(@"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False").RetrievePlayers();

    }
}
