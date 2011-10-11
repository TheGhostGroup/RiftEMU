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

namespace Common
{
    public class RiftServer  :TCPManager
    {
        public RiftServer()
            : base()
        {
            PacketOut.OpcodeInLen = true;
            PacketOut.OpcodeReverse = true;
            PacketOut.SizeInLen = false;
            PacketOut.Struct = PackStruct.SizeAndOpcode;
            PacketOut.SizeLen = 0;

            PacketProcessor.RegisterDefinitions();
        }

        protected override BaseClient GetNewClient()
        {
            return new RiftClient(this);
        }
    }
}
