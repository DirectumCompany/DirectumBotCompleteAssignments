
/// ==================================================================
/// ModuleFunctions.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.ChatBotTask.Functions
{
  internal static partial class Module
  {
    internal static class Remote
    {
      /// <redirect project="DirRX.ChatBotTask.Server" type="DirRX.ChatBotTask.Server.ModuleFunctions" />
      internal static void SendNotification()
      {
      global::Sungero.Domain.Shared.RemoteFunctionExecutor.Execute(
          global::System.Guid.Parse("b48729d6-1056-4320-b51a-ffc0e085c717"),
          "SendNotification()");
      }

    }
    private static object GetFunctionsContainer()
    {
      return new global::DirRX.ChatBotTask.Client.ModuleFunctions();
    }

    private static object GetFinalFunctionsContainer(global::Sungero.Metadata.ModuleProjectType projectType)
    {
      var moduleId = new global::System.Guid("b48729d6-1056-4320-b51a-ffc0e085c717");
      var finalModuleMetadatda = global::Sungero.Metadata.MetadataExtension.GetFinal(global::Sungero.Metadata.Services.MetadataSearcher.FindModuleMetadata(moduleId) ?? global::Sungero.Metadata.Services.MetadataSearcher.FindLayerModuleMetadata(moduleId));
      var assemblyName = finalModuleMetadatda.GetAssemblyName(projectType);
      var moduleFunctionsType = global::System.Type.GetType(global::System.String.Format("{0}.{1}, {2}", finalModuleMetadatda.FunctionNamespace, "Module", assemblyName));
      return moduleFunctionsType.GetMethod("GetFunctionsContainer", global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Static).Invoke(null, null);
    }
  }
}

/// ==================================================================
/// ModuleClientPublicFunctions.g.cs
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
  public partial class ModuleClientPublicFunctions : global::DirRX.ChatBotTask.Client.IModuleClientPublicFunctions
  {
  }
}

/// ==================================================================
/// ModuleWidgetHandlers.g.cs
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
}
