﻿/*
 * Copyright (C) 2011 Strawberry-Pr0jcts <http://strawberry-pr0jcts.com>
 * Copyright (C) 2011 APS http://AllPrivateServer.com
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

namespace CharacterServer
{
    class CharacterServer
    {
        static public CharacterConfig Config;
        static public RpcServer Server;

        static public AccountMgr AcctMgr;

        [STAThread()]
        static void Main(string[] args)
        {
            Log.Texte("", "---------------------------------------------------------------", ConsoleColor.DarkBlue);
            Log.Texte("", " _______ _________ _______ _________ _______  _______          ", ConsoleColor.Cyan);
            Log.Texte("", "(  ____ )\\__   __/(  ____ \\__   __/(  ____ \\(       )|\\     /|", ConsoleColor.Cyan);
            Log.Texte("", "| (    )|   ) (   | (    \\/   ) (   | (    \\/| () () || )   ( |", ConsoleColor.Cyan);
            Log.Texte("", "| (____)|   | |   | (__       | |   | (__    | || || || |   | |", ConsoleColor.Cyan);
            Log.Texte("", "|     __)   | |   |  __)      | |   |  __)   | |(_)| || |   | |", ConsoleColor.Cyan);
            Log.Texte("", "| (\\ (      | |   | (         | |   | (      | |   | || |   | |", ConsoleColor.Cyan);
            Log.Texte("", "| ) \\ \\_____) (___| )         | |   | (____/\\| )   ( || (___) |", ConsoleColor.Cyan);
            Log.Texte("", "|/   \\__/\\_______/|/          )_(   (_______/|/     \\|(_______)", ConsoleColor.Cyan);
            Log.Texte("", "www.Strawberry-Pr0jcts.com", ConsoleColor.DarkCyan);
            Log.Texte("", "---------------------------------------------------------------", ConsoleColor.DarkBlue);

            Config = new CharacterConfig();

            // Loading log level from file
            if (!Log.InitLog(Config.LogLevel,"Character"))
                ConsoleMgr.WaitAndExit(2000);

            AccountMgr.AccountDB = DBManager.Start(Config.AccountDB.Total(), ConnectionType.DATABASE_MYSQL, "Accounts");
            if (AccountMgr.AccountDB == null)
                ConsoleMgr.WaitAndExit(2000);

            // Starting Remote Server
            Server = new RpcServer(Config.RpcClientStartingPort, 1);
            if (!Server.Start(Config.RpcIP, Config.RpcPort))
                ConsoleMgr.WaitAndExit(2000);

            // Starting Accounts Manager
            AcctMgr = Server.GetLocalObject<AccountMgr>();
            if(AcctMgr == null)
                ConsoleMgr.WaitAndExit(2000);

            AcctMgr.LoadRealms();

            // Listening Client
            if (!TCPManager.Listen<RiftServer>(Config.CharacterServerPort, "CharacterServer"))
                ConsoleMgr.WaitAndExit(2000);

            ConsoleMgr.Start();
        }
    }
}
