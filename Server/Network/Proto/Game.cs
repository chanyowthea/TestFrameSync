// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Game.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Msg {

  /// <summary>Holder for reflection information generated from Game.proto</summary>
  public static partial class GameReflection {

    #region Descriptor
    /// <summary>File descriptor for Game.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GameReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpHYW1lLnByb3RvEgNtc2ciDgoMVURQTW92ZVN0YXJ0IgwKClVEUE1vdmVF",
            "bmQiHQoMVURQQ2hhbmdlRGlyEg0KBWFuZ2xlGAEgASgFIiIKD1VEUFJlbGVh",
            "c2VTa2lsbBIPCgdza2lsbElkGAEgASgFIjEKDFVEUEZyYW1lRGF0YRITCgtm",
            "cmFtZU51bWJlchgBIAEoBRIMCgRtc2dzGAIgAygMYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Msg.UDPMoveStart), global::Msg.UDPMoveStart.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Msg.UDPMoveEnd), global::Msg.UDPMoveEnd.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Msg.UDPChangeDir), global::Msg.UDPChangeDir.Parser, new[]{ "Angle" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Msg.UDPReleaseSkill), global::Msg.UDPReleaseSkill.Parser, new[]{ "SkillId" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Msg.UDPFrameData), global::Msg.UDPFrameData.Parser, new[]{ "FrameNumber", "Msgs" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UDPMoveStart : pb::IMessage<UDPMoveStart> {
    private static readonly pb::MessageParser<UDPMoveStart> _parser = new pb::MessageParser<UDPMoveStart>(() => new UDPMoveStart());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UDPMoveStart> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Msg.GameReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveStart() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveStart(UDPMoveStart other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveStart Clone() {
      return new UDPMoveStart(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UDPMoveStart);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UDPMoveStart other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UDPMoveStart other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class UDPMoveEnd : pb::IMessage<UDPMoveEnd> {
    private static readonly pb::MessageParser<UDPMoveEnd> _parser = new pb::MessageParser<UDPMoveEnd>(() => new UDPMoveEnd());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UDPMoveEnd> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Msg.GameReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveEnd() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveEnd(UDPMoveEnd other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPMoveEnd Clone() {
      return new UDPMoveEnd(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UDPMoveEnd);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UDPMoveEnd other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UDPMoveEnd other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class UDPChangeDir : pb::IMessage<UDPChangeDir> {
    private static readonly pb::MessageParser<UDPChangeDir> _parser = new pb::MessageParser<UDPChangeDir>(() => new UDPChangeDir());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UDPChangeDir> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Msg.GameReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPChangeDir() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPChangeDir(UDPChangeDir other) : this() {
      angle_ = other.angle_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPChangeDir Clone() {
      return new UDPChangeDir(this);
    }

    /// <summary>Field number for the "angle" field.</summary>
    public const int AngleFieldNumber = 1;
    private int angle_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Angle {
      get { return angle_; }
      set {
        angle_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UDPChangeDir);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UDPChangeDir other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Angle != other.Angle) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Angle != 0) hash ^= Angle.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Angle != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Angle);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Angle != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Angle);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UDPChangeDir other) {
      if (other == null) {
        return;
      }
      if (other.Angle != 0) {
        Angle = other.Angle;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Angle = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class UDPReleaseSkill : pb::IMessage<UDPReleaseSkill> {
    private static readonly pb::MessageParser<UDPReleaseSkill> _parser = new pb::MessageParser<UDPReleaseSkill>(() => new UDPReleaseSkill());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UDPReleaseSkill> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Msg.GameReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPReleaseSkill() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPReleaseSkill(UDPReleaseSkill other) : this() {
      skillId_ = other.skillId_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPReleaseSkill Clone() {
      return new UDPReleaseSkill(this);
    }

    /// <summary>Field number for the "skillId" field.</summary>
    public const int SkillIdFieldNumber = 1;
    private int skillId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SkillId {
      get { return skillId_; }
      set {
        skillId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UDPReleaseSkill);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UDPReleaseSkill other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SkillId != other.SkillId) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SkillId != 0) hash ^= SkillId.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (SkillId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(SkillId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SkillId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SkillId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UDPReleaseSkill other) {
      if (other == null) {
        return;
      }
      if (other.SkillId != 0) {
        SkillId = other.SkillId;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            SkillId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class UDPFrameData : pb::IMessage<UDPFrameData> {
    private static readonly pb::MessageParser<UDPFrameData> _parser = new pb::MessageParser<UDPFrameData>(() => new UDPFrameData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UDPFrameData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Msg.GameReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPFrameData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPFrameData(UDPFrameData other) : this() {
      frameNumber_ = other.frameNumber_;
      msgs_ = other.msgs_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UDPFrameData Clone() {
      return new UDPFrameData(this);
    }

    /// <summary>Field number for the "frameNumber" field.</summary>
    public const int FrameNumberFieldNumber = 1;
    private int frameNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FrameNumber {
      get { return frameNumber_; }
      set {
        frameNumber_ = value;
      }
    }

    /// <summary>Field number for the "msgs" field.</summary>
    public const int MsgsFieldNumber = 2;
    private static readonly pb::FieldCodec<pb::ByteString> _repeated_msgs_codec
        = pb::FieldCodec.ForBytes(18);
    private readonly pbc::RepeatedField<pb::ByteString> msgs_ = new pbc::RepeatedField<pb::ByteString>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<pb::ByteString> Msgs {
      get { return msgs_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UDPFrameData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UDPFrameData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (FrameNumber != other.FrameNumber) return false;
      if(!msgs_.Equals(other.msgs_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (FrameNumber != 0) hash ^= FrameNumber.GetHashCode();
      hash ^= msgs_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (FrameNumber != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(FrameNumber);
      }
      msgs_.WriteTo(output, _repeated_msgs_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (FrameNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FrameNumber);
      }
      size += msgs_.CalculateSize(_repeated_msgs_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UDPFrameData other) {
      if (other == null) {
        return;
      }
      if (other.FrameNumber != 0) {
        FrameNumber = other.FrameNumber;
      }
      msgs_.Add(other.msgs_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            FrameNumber = input.ReadInt32();
            break;
          }
          case 18: {
            msgs_.AddEntriesFrom(input, _repeated_msgs_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
