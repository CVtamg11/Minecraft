using System;
using Alex.Common.Utils.Vectors;
using MiNET.Net;
using NLog;

namespace Alex.Net.Bedrock.Packets
{
  public class EntityDelta : McpeMoveEntityDelta
  {
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    private PlayerLocation Current { get; set; } = new PlayerLocation();
    
    private int _dX;
    private int _dY;
    private int _dZ;
    private int _dPitch;
    private int _dYaw;
    private int _dHeadYaw;

    public bool HasX = false;
    public bool HasY = false;
    public bool HasZ = false;

    public bool HasPitch = false;
    public bool HasYaw = false;
    public bool HasHeadYaw = false;
    
    public EntityDelta()
    {
      this.Id = (byte) 111;
      this.IsMcpe = true;
    }

    protected override void DecodePacket()
    {
      this.Id = this.IsMcpe ? (byte) this.ReadVarInt() : this.ReadByte();
      this.runtimeEntityId = this.ReadUnsignedVarLong();
      this.flags = this.ReadUshort(false);
      
      Current = new PlayerLocation();
      if (((int) this.flags & 1) != 0)
      {
        //this._dX = this.ReadSignedVarInt();
        Current.X = this.ReadFloat();
        HasX = true;
      }

      if (((int) this.flags & 2) != 0)
      {
        // this._dY = this.ReadSignedVarInt();
        Current.Y = this.ReadFloat();
        HasY = true;
      }

      if (((int) this.flags & 4) != 0)
      {
        //this._dZ = this.ReadSignedVarInt();
        Current.Z = this.ReadFloat();
        HasZ = true;
      }

      float num = 45f / 32f;
      if (((int) this.flags & 8) != 0)
      {
        Current.Pitch = (float)this.ReadByte() * num;
        HasPitch = true;
      }

      if (((int) this.flags & 16) != 0)
      {
        Current.Yaw = (float) this.ReadByte() * num;
        HasYaw = true;
      }

      if (((int) this.flags & 32) != 0)
      {
        Current.HeadYaw = (float) this.ReadByte() * num;
        HasHeadYaw = true;
      }

      if (((int) this.flags & 64) == 0)
        return;
      
      this.isOnGround = true;
    }

    protected override void ResetPacket()
    {
      base.ResetPacket();
      this.runtimeEntityId = 0L;
      this.flags = (ushort) 0;

      _dX = 0;
      _dY = 0;
      _dZ = 0;
      _dPitch = 0;
      _dYaw = 0;
      _dHeadYaw = 0;

      HasX = false;
      HasY = false;
      HasZ = false;

      HasPitch = false;
      HasYaw = false;
      HasHeadYaw = false;
      this.isOnGround = false;
    }

    public static int ToIntDelta(float current, float prev)
    {
      return BitConverter.SingleToInt32Bits(current) - BitConverter.SingleToInt32Bits(prev);
    }

    public static float FromIntDelta(float prev, int delta)
    {
      return BitConverter.Int32BitsToSingle(BitConverter.SingleToInt32Bits(prev) + delta);
    }

    /// <inheritdoc />
    public override void Reset()
    {
      base.Reset();
      
      HasZ = false;
      HasX = false;
      HasY = false;
      HasYaw = false;
      HasHeadYaw = false;
      HasPitch = false;
    }

    public PlayerLocation GetCurrentPosition(PlayerLocation previousPosition)
    {
      var pos = previousPosition;
      pos.X = HasX ? Current.X : previousPosition.X;
      pos.Y = HasY ? Current.Y : previousPosition.Y;
      pos.Z = HasZ ? Current.Z : previousPosition.Z;
      
      pos.HeadYaw = HasHeadYaw ? -Current.HeadYaw : previousPosition.HeadYaw;
      pos.Yaw = HasYaw ? -Current.Yaw : previousPosition.Yaw;
      pos.Pitch = HasPitch ? -Current.Pitch : previousPosition.Pitch;

      pos.OnGround = this.isOnGround;

    /*  if (((int) this.flags & 1) != 0)
        Current.X = FromIntDelta(previousPosition.X, this._dX);
      
      if (((int) this.flags & 2) != 0)
        Current.Y = FromIntDelta(previousPosition.Y, this._dY);
      
      if (((int) this.flags & 4) != 0)
        Current.Z = FromIntDelta(previousPosition.Z, this._dZ);*/
      
      return pos;
    }
  }
}