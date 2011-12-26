using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using Common;

namespace CharacterServer.Console
{
    [ConsoleHandler("daccount", 1, "<Username>")]
    public class DelteAccount : IConsoleHandler
    {
        public bool HandleCommand(string command, List<string> args)
        {
            Account Acct = Program.AcctMgr.GetAccountByUsername(args[0]);
            if (Acct == null)
            {
                Log.Error("DeleteAccount", "Deleting '" + args[0] + "' failed");
                return false;
            }

            AccountMgr.AccountDB.DeleteObject(Acct);
            Log.Info("DeleteAccount", "Account " + args[0] + " successfully deleted");

            return true;
        }
    }
}
