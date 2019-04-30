namespace DirRX.ChatBotTask.Structures.Module
{
  [global::System.Serializable]
  public partial class AllowResultData : global::Sungero.Domain.Shared.ISimpleAppliedStructure
  {

    public static AllowResultData Create()
    {
      return new AllowResultData();
    }

    public static AllowResultData Create(global::System.String result, global::System.String resultAssignmentText)
    {
      return new AllowResultData
      {
        Result = result,
        ResultAssignmentText = resultAssignmentText
      };
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((this.Result != null ? this.Result.GetHashCode() : 0) * 199) ^ ((this.ResultAssignmentText != null ? this.ResultAssignmentText.GetHashCode() : 0) * 199);
      }
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AllowResultData)obj);
    }

    public static bool operator ==(AllowResultData left, AllowResultData right)
    {
      if (ReferenceEquals(left, right))
        return true;

      if (((object)left) == null || ((object)right) == null)
        return false;

      return left.Equals(right);
    }

    public static bool operator !=(AllowResultData left, AllowResultData right)
    {
      return !(left == right);
    }

    protected bool Equals(AllowResultData other)
    {
      return object.Equals(this.Result, other.Result)
             && object.Equals(this.ResultAssignmentText, other.ResultAssignmentText);
    }
  }

  [global::System.Serializable]
  public partial class ChatBotProcessAssignmentType : global::Sungero.Domain.Shared.ISimpleAppliedStructure
  {

    public static ChatBotProcessAssignmentType Create()
    {
      return new ChatBotProcessAssignmentType();
    }

    public static ChatBotProcessAssignmentType Create(global::System.String assignmentType, global::System.String incomingInstructionText, global::System.Collections.Generic.List<global::DirRX.ChatBotTask.Structures.Module.AllowResultData> allowResults)
    {
      return new ChatBotProcessAssignmentType
      {
        AssignmentType = assignmentType,
        IncomingInstructionText = incomingInstructionText,
        AllowResults = allowResults
      };
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((this.AssignmentType != null ? this.AssignmentType.GetHashCode() : 0) * 199) ^ ((this.IncomingInstructionText != null ? this.IncomingInstructionText.GetHashCode() : 0) * 199) ^ ((this.AllowResults != null ? this.AllowResults.GetHashCode() : 0) * 199);
      }
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ChatBotProcessAssignmentType)obj);
    }

    public static bool operator ==(ChatBotProcessAssignmentType left, ChatBotProcessAssignmentType right)
    {
      if (ReferenceEquals(left, right))
        return true;

      if (((object)left) == null || ((object)right) == null)
        return false;

      return left.Equals(right);
    }

    public static bool operator !=(ChatBotProcessAssignmentType left, ChatBotProcessAssignmentType right)
    {
      return !(left == right);
    }

    protected bool Equals(ChatBotProcessAssignmentType other)
    {
      return object.Equals(this.AssignmentType, other.AssignmentType)
             && object.Equals(this.IncomingInstructionText, other.IncomingInstructionText)
             && global::System.Linq.Enumerable.SequenceEqual(this.AllowResults, other.AllowResults);
    }
  }

  [global::System.Serializable]
  public partial class AllowResultCodes : global::Sungero.Domain.Shared.ISimpleAppliedStructure
  {

    public static AllowResultCodes Create()
    {
      return new AllowResultCodes();
    }

    public static AllowResultCodes Create(global::System.String resultCode, global::System.String resultLocale)
    {
      return new AllowResultCodes
      {
        ResultCode = resultCode,
        ResultLocale = resultLocale
      };
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((this.ResultCode != null ? this.ResultCode.GetHashCode() : 0) * 199) ^ ((this.ResultLocale != null ? this.ResultLocale.GetHashCode() : 0) * 199);
      }
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AllowResultCodes)obj);
    }

    public static bool operator ==(AllowResultCodes left, AllowResultCodes right)
    {
      if (ReferenceEquals(left, right))
        return true;

      if (((object)left) == null || ((object)right) == null)
        return false;

      return left.Equals(right);
    }

    public static bool operator !=(AllowResultCodes left, AllowResultCodes right)
    {
      return !(left == right);
    }

    protected bool Equals(AllowResultCodes other)
    {
      return object.Equals(this.ResultCode, other.ResultCode)
             && object.Equals(this.ResultLocale, other.ResultLocale);
    }
  }
}