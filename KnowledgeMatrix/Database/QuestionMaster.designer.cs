﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KnowledgeMatrix.Database
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	public partial class QuestionMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertQuestionMaster(QuestionMaster instance);
    partial void UpdateQuestionMaster(QuestionMaster instance);
    partial void DeleteQuestionMaster(QuestionMaster instance);
    #endregion
		
		public QuestionMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public QuestionMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public QuestionMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public QuestionMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<QuestionMaster> QuestionMasters
		{
			get
			{
				return this.GetTable<QuestionMaster>();
			}
		}
		
		public System.Data.Linq.Table<QuestionAssociation> QuestionAssociations
		{
			get
			{
				return this.GetTable<QuestionAssociation>();
			}
		}
		
		public System.Data.Linq.Table<QuestionDetails> QuestionDetails
		{
			get
			{
				return this.GetTable<QuestionDetails>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class QuestionMaster : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _QuesNo;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnQuesNoChanging(int value);
    partial void OnQuesNoChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public QuestionMaster()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuesNo", IsPrimaryKey=true)]
		public int QuesNo
		{
			get
			{
				return this._QuesNo;
			}
			set
			{
				if ((this._QuesNo != value))
				{
					this.OnQuesNoChanging(value);
					this.SendPropertyChanging();
					this._QuesNo = value;
					this.SendPropertyChanged("QuesNo");
					this.OnQuesNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class QuestionAssociation
	{
		
		private string _SNo;
		
		private int _ParentQuesNo;
		
		private int _ChildQuesNo;
		
		private int _TotalQuestions;
		
		public QuestionAssociation()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SNo", CanBeNull=false, IsDbGenerated=true)]
		public string SNo
		{
			get
			{
				return this._SNo;
			}
			set
			{
				if ((this._SNo != value))
				{
					this._SNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentQuesNo")]
		public int ParentQuesNo
		{
			get
			{
				return this._ParentQuesNo;
			}
			set
			{
				if ((this._ParentQuesNo != value))
				{
					this._ParentQuesNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ChildQuesNo")]
		public int ChildQuesNo
		{
			get
			{
				return this._ChildQuesNo;
			}
			set
			{
				if ((this._ChildQuesNo != value))
				{
					this._ChildQuesNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalQuestions")]
		public int TotalQuestions
		{
			get
			{
				return this._TotalQuestions;
			}
			set
			{
				if ((this._TotalQuestions != value))
				{
					this._TotalQuestions = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class QuestionDetails
	{
		
		private int _QuesNo;
		
		private string _Question;
		
		private string _AnswerType;
		
		private string _Answer;
		
		private string _QuestionOptions;
		
		private string _Complexity;
		
		private string _Pattern;
		
		private string _CorrectAnswerDetails;
		
		private string _AnswerConcept;
		
		private string _QuesDetSNo;
		
		public QuestionDetails()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuesNo")]
		public int QuesNo
		{
			get
			{
				return this._QuesNo;
			}
			set
			{
				if ((this._QuesNo != value))
				{
					this._QuesNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Question", CanBeNull=false)]
		public string Question
		{
			get
			{
				return this._Question;
			}
			set
			{
				if ((this._Question != value))
				{
					this._Question = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnswerType", CanBeNull=false)]
		public string AnswerType
		{
			get
			{
				return this._AnswerType;
			}
			set
			{
				if ((this._AnswerType != value))
				{
					this._AnswerType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Answer", CanBeNull=false)]
		public string Answer
		{
			get
			{
				return this._Answer;
			}
			set
			{
				if ((this._Answer != value))
				{
					this._Answer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuestionOptions", CanBeNull=false)]
		public string QuestionOptions
		{
			get
			{
				return this._QuestionOptions;
			}
			set
			{
				if ((this._QuestionOptions != value))
				{
					this._QuestionOptions = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Complexity", CanBeNull=false)]
		public string Complexity
		{
			get
			{
				return this._Complexity;
			}
			set
			{
				if ((this._Complexity != value))
				{
					this._Complexity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pattern", CanBeNull=false)]
		public string Pattern
		{
			get
			{
				return this._Pattern;
			}
			set
			{
				if ((this._Pattern != value))
				{
					this._Pattern = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CorrectAnswerDetails", CanBeNull=false)]
		public string CorrectAnswerDetails
		{
			get
			{
				return this._CorrectAnswerDetails;
			}
			set
			{
				if ((this._CorrectAnswerDetails != value))
				{
					this._CorrectAnswerDetails = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnswerConcept", CanBeNull=false)]
		public string AnswerConcept
		{
			get
			{
				return this._AnswerConcept;
			}
			set
			{
				if ((this._AnswerConcept != value))
				{
					this._AnswerConcept = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuesDetSNo", CanBeNull=false)]
		public string QuesDetSNo
		{
			get
			{
				return this._QuesDetSNo;
			}
			set
			{
				if ((this._QuesDetSNo != value))
				{
					this._QuesDetSNo = value;
				}
			}
		}
	}
}
#pragma warning restore 1591