
/// ==================================================================
/// Employee.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.Solution.Client
{
  public class Employee :
    global::Sungero.Company.Client.Employee, global::DirRX.Solution.IEmployee
  {
    #region Fields and properties

    public static new readonly global::System.Guid ClassTypeGuid = global::System.Guid.Parse("7dc35907-1ed5-4ae7-bb1e-1ff8315f1e69");

    public override global::System.Guid TypeGuid
    {
      get { return global::DirRX.Solution.Client.Employee.ClassTypeGuid; }
    }

    public override string TypeName
    {
      get { return "DirRX.Solution.IEmployee, Sungero.Domain.Interfaces"; }
    }

      public override string DisplayValue
      {
        get { return this.Name; }
        set { this.Name = value; }
      }

      public override string DisplayPropertyName
      {
        get { return "Name"; }
      }


      public override string AdditionalInfo
      {
        get { return this.JobTitle == null ? string.Empty : this.JobTitle.ToString(); }
      }

    public new global::DirRX.Solution.IEmployeeState State
    {
      get
      {
        return (global::DirRX.Solution.IEmployeeState)base.State;
      }
    }

    protected override global::Sungero.Domain.Shared.IEntityState CreateState()
    {
      return new global::DirRX.Solution.Shared.EmployeeState(this);
    }

    public new global::DirRX.Solution.IEmployeeInfo Info
    {
      get
      {
        return (global::DirRX.Solution.IEmployeeInfo)base.Info;
      }
    }

    public new global::DirRX.Solution.IEmployeeAccessRights AccessRights
    {
      get
      {
        return (global::DirRX.Solution.IEmployeeAccessRights)base.AccessRights;
      }
    }

    protected override global::Sungero.Domain.Shared.IEntityAccessRights CreateAccessRights()
    {
      return new global::DirRX.Solution.Client.EmployeeAccessRights(this);
    }

        protected global::Sungero.Domain.Client.SimpleProperty<global::System.Boolean?> _IsAllowExecuteThroughChatBot;

        public virtual global::System.Boolean? IsAllowExecuteThroughChatBot
        {
          get { return this._IsAllowExecuteThroughChatBot.Value; }
          set { this._IsAllowExecuteThroughChatBot.Value = value; }
        }










    #endregion

    #region Methods

    protected override object CreateActionsHandlers()
    {
      return new global::DirRX.Solution.Client.EmployeeActions(this);
    }

    protected override object CreateCollectionActionsHandlers()
    {
      return new global::DirRX.Solution.Client.EmployeeCollectionActions();
    }

    protected override object CreateAnyChildEntityActionsHandlers()
    {
      return new global::DirRX.Solution.Client.EmployeeAnyChildEntityActions();
    }

    protected override object CreateAnyChildEntityCollectionActionsHandlers()
    {
      return new global::DirRX.Solution.Client.EmployeeAnyChildEntityCollectionActions();
    }


    protected override global::Sungero.Domain.Client.EntityFunctions CreateClientFunctions()
    {
      return new global::DirRX.Solution.Client.EmployeeFunctions(this);
    }

    protected override global::Sungero.Domain.Shared.EntityFunctions CreateSharedFunctions()
    {
      return new global::DirRX.Solution.Shared.EmployeeFunctions(this);
    }
    protected override object CreateHandlers() {
      return new global::DirRX.Solution.EmployeeClientHandlers(this);
    }
    protected override object CreateSharedHandlers() {
      return new global::DirRX.Solution.EmployeeSharedHandlers(this);
    }

    #endregion

    #region Framework events

    protected void IsAllowExecuteThroughChatBotChangedHandler()
    {
      var args = new global::Sungero.Domain.Shared.BooleanPropertyChangedEventArgs(this.State.Properties.IsAllowExecuteThroughChatBot, this.IsAllowExecuteThroughChatBot, this);
     ((global::DirRX.Solution.EmployeeSharedHandlers)this.SharedHandlers).IsAllowExecuteThroughChatBotChanged(args);
    }



  protected global::System.Boolean? IsAllowExecuteThroughChatBotValueInputHandler(global::System.Boolean? value)
  {
    var args = new global::Sungero.Presentation.BooleanValueInputEventArgs(this.IsAllowExecuteThroughChatBot, value, this, this.Info.Properties.IsAllowExecuteThroughChatBot);
    ((global::DirRX.Solution.EmployeeClientHandlers)this.Handlers).IsAllowExecuteThroughChatBotValueInput(args);
    return args.NewValue;
  }



    #endregion

    #region Constructors







    public Employee()
    {
            this._IsAllowExecuteThroughChatBot = new global::Sungero.Domain.Client.SimpleProperty<global::System.Boolean?>("IsAllowExecuteThroughChatBot", this);
            this._IsAllowExecuteThroughChatBot.ValueChanged += (sender, e) => { this.IsAllowExecuteThroughChatBotChangedHandler(); };
            this.AddProperty(this._IsAllowExecuteThroughChatBot);








    }

    #endregion

  }
}

/// ==================================================================
/// EmployeePresenter.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.Solution.Client
{
  public class EmployeePresenter<T> :
    global::Sungero.Company.Client.EmployeePresenter<T>
    where T : class, global::DirRX.Solution.IEmployee
  {
    #region Fields and properties




    #endregion

    #region Methods

    #endregion

    #region Framework events

    protected override void EntityPropertyChangedEventHandler(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
    {
      base.EntityPropertyChangedEventHandler(sender, e);
    }

    #endregion



    #region Constructors

    private void Init()
    {
              this._LoginCollectionPresenter
              .Query = global::Sungero.Domain.Client.Session.GetValuesForNavigationProperty<global::Sungero.CoreEntities.ILogin>(() => this.Entity, typeof(T), "Login");

              this._PersonCollectionPresenter
              .Query = global::Sungero.Domain.Client.Session.GetValuesForNavigationPropertyWithoutSourceEntity<global::Sungero.Parties.IPerson>(() => this.Entity.Id, typeof(T), "Person");

              this._DepartmentCollectionPresenter
              .Query = global::Sungero.Domain.Client.Session.GetValuesForNavigationPropertyWithoutSourceEntity<global::Sungero.Company.IDepartment>(() => this.Entity.Id, typeof(T), "Department");

              this._JobTitleCollectionPresenter
              .Query = global::Sungero.Domain.Client.Session.GetValuesForNavigationPropertyWithoutSourceEntity<global::Sungero.Company.IJobTitle>(() => this.Entity.Id, typeof(T), "JobTitle");


    }

    public EmployeePresenter()
    {
      this.Init();
    }

    #endregion
  }
}

/// ==================================================================
/// EmployeeCollectionPresenter.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.Solution.Client
{
  public class EmployeeCollectionPresenter<T> : 
    global::Sungero.Company.Client.EmployeeCollectionPresenter<T>
    where T: class, global::DirRX.Solution.IEmployee
  {
    #region Actions



    #endregion

    #region Methods


    #endregion

    public EmployeeCollectionPresenter(global::System.Linq.IQueryable<T> query, OnLookup onLookup)
      : base(query, onLookup)
    {
    }

    public EmployeeCollectionPresenter(global::System.Linq.IQueryable<T> query)
      : this(query, null) { }

    public EmployeeCollectionPresenter(OnLookup onLookup)
      : this(null, onLookup) { }

    public EmployeeCollectionPresenter()
      : this(null, null) { }
  }
}

/// ==================================================================
/// EmployeeRepositoryImplementer.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.Solution.Client
{ 
  public class EmployeeRepositoryImplementer<T> : 
      global::Sungero.Company.Client.EmployeeRepositoryImplementer<T>,
      global::DirRX.Solution.IEmployeeRepositoryImplementer<T>
      where T : global::DirRX.Solution.IEmployee
    {
       public new global::DirRX.Solution.IEmployeeAccessRights AccessRights
       {
          get { return (global::DirRX.Solution.IEmployeeAccessRights)base.AccessRights; }
       }

       public new global::DirRX.Solution.IEmployeeInfo Info
       {
          get { return (global::DirRX.Solution.IEmployeeInfo)base.Info; }
       }

       protected override global::Sungero.Domain.Shared.IEntityAccessRights CreateAccessRights()
       {
         return new global::DirRX.Solution.Client.EmployeeTypeAccessRights(typeof(T));
       }
    }
}

/// ==================================================================
/// EmployeeAccessRights.g.cs
/// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirRX.Solution.Client
{
  public class EmployeeAccessRights : 
    Sungero.Company.Client.EmployeeAccessRights, DirRX.Solution.IEmployeeAccessRights
  {

    public EmployeeAccessRights(global::Sungero.Domain.Shared.IEntity entity) : base(entity)
    {
    }
  }

  public class EmployeeTypeAccessRights : 
    Sungero.Company.Client.EmployeeTypeAccessRights, DirRX.Solution.IEmployeeAccessRights
  {

    public EmployeeTypeAccessRights(global::System.Type entityType) : base(entityType)
    {
    }
  }
}
