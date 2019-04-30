using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace DirRX.Solution.Module.ChatBotInfrastructurePI.Server
{
  partial class ModuleFunctions
  {
    /// <summary>
    /// Обработка джоба.
    /// </summary>
    /// <param name="chatJob">Джоб.</param>
    [Public]
    public override void ProcessJob(DirRX.ChatBotInfrastructure.IChatJob chatJob)
    {
      base.ProcessJob(chatJob);
      DirRX.ChatBotTask.PublicFunctions.Module.ReceiveJob(chatJob);
    }
  }
}