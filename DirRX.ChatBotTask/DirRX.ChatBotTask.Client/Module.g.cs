
/// ==================================================================
/// Module.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.ChatBotTask.Client
{
  public partial class ChatBotTaskModule : global::Sungero.Domain.Shared.ModuleBase
  {
    public override global::System.Guid Id
    {
      get { return global::System.Guid.Parse("b48729d6-1056-4320-b51a-ffc0e085c717"); }
    }

    public override string Name
    {
      get { return "DirRX.ChatBotTask"; }
    }

    public override void RegisterTypes()
    {
      global::Sungero.Domain.Shared.EntityInfoRegister.RegisterEntityInfo(new global::System.Guid("eef6834c-36a0-490f-8d7b-4962162f9890"), new DirRX.ChatBotTask.Shared.ChatBotProcessAssignmentInfo(typeof(global::DirRX.ChatBotTask.IChatBotProcessAssignment)));
      global::Sungero.Domain.Shared.ModuleManager.Instance.Container.RegisterType<global::DirRX.ChatBotTask.Client.IChatBotProcessAssignmentClientPublicFunctions, global::DirRX.ChatBotTask.Client.ChatBotProcessAssignmentClientPublicFunctions>();
      global::Sungero.Domain.Shared.ModuleManager.Instance.Container.RegisterType<global::DirRX.ChatBotTask.Shared.IChatBotProcessAssignmentSharedPublicFunctions, global::DirRX.ChatBotTask.Shared.ChatBotProcessAssignmentSharedPublicFunctions>();


      global::Sungero.Domain.Shared.ModuleManager.Instance.Container.RegisterType<global::DirRX.ChatBotTask.IChatBotProcessAssignmentFilterState, global::DirRX.ChatBotTask.Shared.ChatBotProcessAssignment.ChatBotProcessAssignmentFilterState>();



      global::Sungero.Domain.Shared.ModuleManager.Instance.Container.RegisterType<global::DirRX.ChatBotTask.Client.IModuleClientPublicFunctions, global::DirRX.ChatBotTask.Client.ModuleClientPublicFunctions>();
      global::Sungero.Domain.Shared.ModuleManager.Instance.Container.RegisterType<global::DirRX.ChatBotTask.Shared.IModuleSharedPublicFunctions, global::DirRX.ChatBotTask.Shared.ModuleSharedPublicFunctions>();
    }
  }
}
