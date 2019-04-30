using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace DirRX.ChatBotTask.Structures.Module
{
  /// <summary>
  /// Структура с результатом выполнения задания и сопроводительным текстом.
  /// </summary>
  partial class AllowResultData
  {
    /// <summary>
    /// Результат выполнения задания.
    /// </summary>
    public string Result { get; set; }
    
    /// <summary>
    /// Результат выполнения задания.
    /// </summary>
    public string ResultAssignmentText { get; set; }
  }
  
  /// <summary>
  /// Структура для хранения типа задания, выполнимого через чат-бота.
  /// </summary>
  partial class ChatBotProcessAssignmentType
  {
    /// <summary>
    /// Тип задания.
    /// </summary>
    public string AssignmentType { get; set; }
    
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string IncomingInstructionText { get; set; }
    
    /// <summary>
    /// Результаты выполнения задания с учетом порядка раположения кнопок выполнения данного задания.
    /// </summary>
    public List<DirRX.ChatBotTask.Structures.Module.AllowResultData> AllowResults { get; set; }
  }
  
  /// <summary>
  /// Структура с результатами выполнения задания (код и локализация).
  /// </summary>
  partial class AllowResultCodes
  {
    /// <summary>
    /// Результат выполнения задания.
    /// </summary>
    public string ResultCode { get; set; }
    
    /// <summary>
    /// Результат выполнения задания.
    /// </summary>
    public string ResultLocale { get; set; }
  }
}