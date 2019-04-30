using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using DirRX.Solution.Employee;

namespace DirRX.Solution
{
  partial class EmployeeServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.IsAllowExecuteThroughChatBot = false;
    }
  }

}