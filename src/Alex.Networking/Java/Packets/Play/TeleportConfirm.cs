﻿using Alex.Networking.Java.Util;

namespace Alex.Networking.Java.Packets.Play
{
    public class TeleportConfirm : Packet<TeleportConfirm>
    {
	    public TeleportConfirm()
	    {
		    PacketId = 0x00;
	    }

	    public int TeleportId;

	    public override void Decode(MinecraftStream stream)
	    {
		    TeleportId = stream.ReadVarInt();
	    }

	    public override void Encode(MinecraftStream stream)
	    {
		    stream.WriteVarInt(TeleportId);
	    }
    }
}
