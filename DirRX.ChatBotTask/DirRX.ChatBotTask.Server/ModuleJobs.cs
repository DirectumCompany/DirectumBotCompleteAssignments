using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace DirRX.ChatBotTask.Server
{
  public class ModuleJobs
  {

	  /// <summary>
    /// Отправка уведомления о заданиях.
    /// </summary>
    public virtual void SendChatBotNotification()
    {
      Functions.Module.SendNotification();
    }

  }
}