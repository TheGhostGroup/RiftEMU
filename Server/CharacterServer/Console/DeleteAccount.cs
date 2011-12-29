/*
 * Copyright (C) 2011 Strawberry-Pr0jcts <http://strawberry-pr0jcts.com>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using Common;

namespace CharacterServer.Console
{
    [ConsoleHandler("daccount", 1, "<Username>")]
    public class DeleteAccount : IConsoleHandler
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
