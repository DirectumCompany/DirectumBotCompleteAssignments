using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace DirRX.ChatBotTask.Server
{
  public class ModuleFunctions
  {
    /// <summary>
    /// Отправить уведомления о заданиях.
    /// </summary>
    [Remote, Public]
    public virtual void SendNotification()
    {
      var previousRun = GetLastNotificationDate();
      var notificationDate = Calendar.Now;
      try
      {
        TrySendNewAssignmentsToChatBot(previousRun, notificationDate);
      }
      finally
      {
        UpdateLastNotificationDate(notificationDate);
      }
    }
    
    /// <summary>
    /// Получить дату последней рассылки уведомлений.
    /// </summary>
    /// <returns>Дата последней рассылки.</returns>
    public static DateTime GetLastNotificationDate()
    {
      // CORE: строковая константа.
      var key = "LastChatBotNotificationOfAssignment";
      var command = string.Format(Queries.Module.SelectDocflowParamsValue, key);
      try
      {
        var date = Sungero.Docflow.PublicFunctions.Module.ExecuteScalarSQLCommand(command);
        Logger.DebugFormat("Last notification date in DB is {0} (UTC)", date);
        // CORE: использование System.Globalization.DateTimeStyles.
        DateTime result = Calendar.FromUtcTime(DateTime.Parse(date.ToString(), null, System.Globalization.DateTimeStyles.AdjustToUniversal));

        if ((result - Calendar.Now).TotalDays > 1)
          return Calendar.Today;
        else
          return result;
      }
      catch (Exception ex)
      {
        Logger.Error("Error while getting last notification date", ex);
        return Calendar.Today;
      }
    }
    
    /// <summary>
    /// Обновить дату последней рассылки уведомлений.
    /// </summary>
    /// <param name="notificationDate">Дата рассылки уведомлений.</param>
    [Public]
    public static void UpdateLastNotificationDate(DateTime notificationDate)
    {
      // CORE: строковая константа.
      var key = "LastChatBotNotificationOfAssignment";
      
      // CORE: SQL-запрос: Обновление параметра даты последней рассылки уведомлений.
      var newDate = notificationDate.Add(-Calendar.UtcOffset).ToString("yyyy-MM-ddTHH:mm:ss.ffff+0");
      
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.Module.InsertOrUpdateDocflowParamsValue, new[] { key, newDate });
      
      Logger.DebugFormat("Last notification date is set to {0} (UTC)", newDate);
    }
    
    /// <summary>
    /// Проверить тип задания.
    /// </summary>
    /// <param name="typeName">Наименование типа задания.</param>
    /// <returns>Возвращает true, если задание: простое, на ознакомление.
    /// Иначе - false. </returns>
    public virtual bool CheckAssignmentType(string typeName)
    {
      return (typeName == Constants.Module.SimpleAssignmentName) ||
        (typeName == Constants.Module.AcquaintanceAssignmentName);
    }
    
    /// <summary>
    /// Запустить рассылку по новым заданиям.
    /// </summary>
    /// <param name="previousRun">Дата прошлого запуска рассылки.</param>
    /// <param name="notificationDate">Дата текущей рассылки.</param>
    /// <returns>True, если хотя бы одно сообщение было отправлено, иначе - false.</returns>
    [Public]
    public virtual void TrySendNewAssignmentsToChatBot(DateTime previousRun, DateTime notificationDate)
    {
      var newAssignments = Sungero.Workflow.Assignments
        .GetAll(a => previousRun <= a.Created && a.Created < notificationDate && a.IsRead == false && a.Status != Sungero.Workflow.AssignmentBase.Status.Aborted)
        .ToList();
      
      // Получаем задания, по которым отправка еще не завершена.
      foreach (var chatBotProcess in DirRX.ChatBotTask.ChatBotProcessAssignments.GetAll(x => x.Status != DirRX.ChatBotTask.ChatBotProcessAssignment.Status.Closed))
      {
        var assignmentsNeedRepeat = Sungero.Workflow.Assignments.GetAll(x => x.Id == chatBotProcess.AssignmentId && x.Status == Sungero.Workflow.Assignment.Status.InProcess && x.IsRead == false).FirstOrDefault();
        if (assignmentsNeedRepeat != null)
          newAssignments.Add(assignmentsNeedRepeat);
      }
      
      Logger.DebugFormat("Found {0} assignments", newAssignments.Count);
      foreach (var assignment in newAssignments)
      {
        var employee = DirRX.Solution.Employees.As(assignment.Performer);
        if (employee == null)
          continue;
        if (employee.IsAllowExecuteThroughChatBot != true)
          continue;
        TrySendAssignmentMessage(assignment);
      }
      if (!newAssignments.Any())
        Logger.Debug("No new assignments for sending");
    }
    
    #region Отправка сообщений пользователю.
    /// <summary>
    /// Отправка сообщения пользователю чат-бота о новом задании.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    [Public]
    public virtual void TrySendAssignmentMessage(Sungero.Workflow.IAssignment assignment)
    {
      var chatBotUser = DirRX.ChatBotInfrastructure.ChatBotUsers.Null;
      var employee = Sungero.Company.Employees.As(assignment.Performer);
      if (employee != null)
        chatBotUser = DirRX.ChatBotInfrastructure.ChatBotUsers.GetAll(u => u.Person != null && Equals(u.Person, employee.Person)).FirstOrDefault();
      
      if (this.CheckAssignmentType(assignment.Info.Name) && chatBotUser != null)
      {
        SendAssignmentMessage(assignment, chatBotUser);
        Logger.DebugFormat("Sending assignment type: {0}", assignment.Info.Name);
      }
    }
    
    /// <summary>
    /// Отправка сообщения пользователю чат-бота о задании.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    [Public]
    public virtual void SendAssignmentMessage(Sungero.Workflow.IAssignment assignment, DirRX.ChatBotInfrastructure.IChatBotUser chatBotUser)
    {
      var documents = GetAttachements(assignment);
      var processAssignmentType = GetChatBotProcessAssignmentType(assignment);
      var message = processAssignmentType.IncomingInstructionText;
      Logger.DebugFormat("message: {0}", message);
      var actions = GetResultVariants(assignment);
      Logger.DebugFormat("action: {0}", actions[0].ResultLocale);
      var chatBotProcessAssignment = DirRX.ChatBotTask.ChatBotProcessAssignments.Create();
      chatBotProcessAssignment.AssignmentId = assignment.Id;
      chatBotProcessAssignment.ChatBotProcess = DirRX.ChatBotInfrastructure.PublicFunctions.Module.StartProcess(processAssignmentType.AssignmentType, chatBotUser);
      chatBotProcessAssignment.Save();
      Logger.DebugFormat("Send assignment: {0}", assignment.Id);
      bool isError = false;
      try
      {
        DirRX.ChatBotInfrastructure.PublicFunctions.Module.SendAttachmentsMessage(
          message, new List<DirRX.ChatBotInfrastructure.IChatBotUser> {chatBotUser}, chatBotProcessAssignment.ChatBotProcess, processAssignmentType.AssignmentType,
          !string.IsNullOrEmpty(actions[0].ResultLocale) ? DirRX.ChatBotInfrastructure.PublicFunctions.Module.CreateSimpleAction(actions[0].ResultLocale) : string.Empty,
          !string.IsNullOrEmpty(actions[1].ResultLocale) ? DirRX.ChatBotInfrastructure.PublicFunctions.Module.CreateSimpleAction(actions[1].ResultLocale) : string.Empty,
          !string.IsNullOrEmpty(actions[2].ResultLocale) ? DirRX.ChatBotInfrastructure.PublicFunctions.Module.CreateSimpleAction(actions[2].ResultLocale) : string.Empty,
          !string.IsNullOrEmpty(actions[3].ResultLocale) ? DirRX.ChatBotInfrastructure.PublicFunctions.Module.CreateSimpleAction(actions[3].ResultLocale) : string.Empty,
          !string.IsNullOrEmpty(actions[4].ResultLocale) ? DirRX.ChatBotInfrastructure.PublicFunctions.Module.CreateSimpleAction(actions[4].ResultLocale) : string.Empty,
          documents[0], documents[1], documents[2], documents[3], documents[3]
         );
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("Message sending failed. Error: {0}", ex.Message);
        isError = true;
      }
      if (!isError)
      {
        chatBotProcessAssignment.Status = DirRX.ChatBotTask.ChatBotProcessAssignment.Status.Closed;
        chatBotProcessAssignment.Save();
      }
    }
    
    /// <summary>
    /// Получение всех вложений к заданию в виде массива.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    public virtual Sungero.Content.IElectronicDocument[] GetAttachements(Sungero.Workflow.IAssignmentBase assignment)
    {
      var documentsList = new List<Sungero.Content.IElectronicDocument> {};
      foreach (var doc in assignment.AllAttachments)
      {
        if (Sungero.Content.ElectronicDocuments.Is(doc))
        {
          documentsList.Add(Sungero.Content.ElectronicDocuments.As(doc));
          Logger.DebugFormat("Attachement: {0}", doc.Id);
        }
      }
      var remainedLength = 5 - documentsList.Count;
      while (remainedLength > 0)
      {
        remainedLength--;
        documentsList.Add(Sungero.Content.ElectronicDocuments.Null);
      }
      var list = documentsList.ToArray();
      return documentsList.ToArray();
    }
    
    /// <summary>
    /// Получить тип и текстовые данные для выполняемого задания.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    /// <returns>Правило выполнения задания.</returns>
    public virtual Structures.Module.ChatBotProcessAssignmentType GetChatBotProcessAssignmentType(Sungero.Workflow.IAssignmentBase assignment)
    {
      var typeName = assignment.Info.Name;
      var assignmentText = Resources.AssignmentTextFormat(assignment.Subject, assignment.Author.Name, assignment.Deadline.HasValue ? assignment.Deadline.Value.ToString() : " - ", assignment.Task.ActiveText);
      // Простое задание.
      if (typeName == Constants.Module.SimpleAssignmentName)
      {
        var resultItems = new List<Structures.Module.AllowResultData>() {
          Structures.Module.AllowResultData.Create("Complete", Resources.SimpleAssignmentCompleteText) };
        return Structures.Module.ChatBotProcessAssignmentType.Create(Constants.Module.SimpleAssignmentName, assignmentText, resultItems);
      }
      
      // Задание на ознакомление с документом.
      if (typeName == Constants.Module.AcquaintanceAssignmentName)
      {
        var resultItems = new List<Structures.Module.AllowResultData>() {
          Structures.Module.AllowResultData.Create(Sungero.RecordManagement.AcquaintanceAssignment.Result.Acquainted.Value, Resources.AcquaintanceAssignmentAcquaintanedText) };
        return Structures.Module.ChatBotProcessAssignmentType.Create(Constants.Module.AcquaintanceAssignmentName, assignmentText, resultItems);
      }
    }
    
    /// <summary>
    /// Получить список вариантов для выполнения задания.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    /// <returns>Список вариантов выполнения задания.</returns>
    private List<Structures.Module.AllowResultCodes> GetResultVariants(Sungero.Workflow.IAssignmentBase assignment)
    {
      // Ссылки на выполнения задания.
      var results = new List<Structures.Module.AllowResultCodes>();
      
      var processAssignmentType = GetChatBotProcessAssignmentType(assignment);
      if (processAssignmentType != null)
      {
        // Для простых заданий, ссылка генерируется по другому.
        if (Sungero.Workflow.SimpleAssignments.Is(assignment))
        {
          var simpleAssignment = Sungero.Workflow.SimpleAssignments.As(assignment);
          var code = new Structures.Module.AllowResultCodes();
          code.ResultCode = "Complete";
          code.ResultLocale = simpleAssignment.Info.Actions.Complete.LocalizedName;
          results.Add(code);
        }
        else
        {
          Sungero.Metadata.EntityMetadata meta = Sungero.Domain.Shared.TypeExtension.GetEntityMetadata(assignment);
          foreach (var res in processAssignmentType.AllowResults)
          {
            var code = new Structures.Module.AllowResultCodes();
            code.ResultCode = res.Result;
            code.ResultLocale = Sungero.Domain.Shared.ResourceExtensions.GetLocalizedName(meta.Actions.Single(a => a.Name == res.Result));
            results.Add(code);
          }
        }
      }
      var remainedLength = 5 - results.Count;
      while (remainedLength > 0)
      {
        remainedLength--;
        var code = new Structures.Module.AllowResultCodes();
        code.ResultCode = string.Empty;
        code.ResultLocale = string.Empty;
        results.Add(code);
      }
      return results;
    }
    
    #endregion
    
    #region Получение и обработка сообщений от пользователя.
    
    /// <summary>
    /// Обработка сообщения.
    /// </summary>
    /// <param name="chatJob">Джоб.</param>
    [Public]
    public virtual void ReceiveJob(DirRX.ChatBotInfrastructure.IChatJob chatJob)
    {
      Logger.DebugFormat("ProcessStage: {0}", chatJob.ProcessStage);
      if (CheckAssignmentType(chatJob.ProcessStage))
      {
        Logger.Debug("Start ProcessAssignment");
        ProcessAssignment(chatJob);
      }
    }
    
    #endregion
    
    #region Выполнение заданий из бота.
    
    /// <summary>
    /// Выполнение задания.
    /// </summary>
    /// <param name="chatJob">Джоб.</param>
    public virtual void ProcessAssignment(DirRX.ChatBotInfrastructure.IChatJob chatJob)
    {
      var chatBotProcessAssignment = DirRX.ChatBotTask.ChatBotProcessAssignments.GetAll().Where(x => Equals(x.ChatBotProcess, chatJob.Process)).FirstOrDefault();
      if (chatBotProcessAssignment == null)
        return;
      var assignment = Sungero.Workflow.Assignments.GetAll().Where(x => x.Id == chatBotProcessAssignment.AssignmentId && x.Status == Sungero.Workflow.Assignment.Status.InProcess).FirstOrDefault();
      if (assignment != null)
      {
        Logger.DebugFormat("Find assignment {0}", assignment.Id);
        var processAssignmentType = GetChatBotProcessAssignmentType(assignment);
        var resultVariant = GetResultVariants(assignment).Where(x => x.ResultLocale == chatJob.Action).FirstOrDefault();
        if (resultVariant != null)
        {
          var assignmentResult = resultVariant.ResultCode;
          var res = processAssignmentType.AllowResults.Where(x => x.Result == assignmentResult).FirstOrDefault();
          var activeText = res != null ? res.ResultAssignmentText : string.Empty;
          CompleteAssigment(assignment, assignmentResult, activeText);
        }
      }
      DirRX.ChatBotTask.ChatBotProcessAssignments.Delete(chatBotProcessAssignment);
    }
    
    public void CompleteAssigment(Sungero.Workflow.IAssignment assignment, string assignmentResult, string activeText)
    {
      // Выполнить задание.
      this.ExecuteAssigment(assignment, assignmentResult, activeText);
    }
    
    /// <summary>
    /// Выполнить задание.
    /// </summary>
    /// <param name="assignment">Задание.</param>
    /// <param name="resultComplete">Результат выполнения задания.</param>
    /// <param name="activeText">Текст задания.</param>
    private void ExecuteAssigment(Sungero.Workflow.IAssignment assignment, string resultComplete, string activeText)
    {
      assignment.ActiveText = activeText;
      // Выполнение простого задания. Т.к. он платформенный, то выполняется таким образом.
      if (Sungero.Workflow.SimpleAssignments.Is(assignment) && resultComplete == "Complete")
      {
        assignment.Complete(null);
      }
      else
      {
        if (this.CheckAssignmentType(assignment.Info.Name))
          assignment.Complete(new Enumeration(resultComplete));
      }
    }
    
    #endregion
    
    
  }

}