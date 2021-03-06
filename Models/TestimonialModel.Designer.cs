﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace TEAM3OIE2S.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TestimonialEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TestimonialEntities object using the connection string found in the 'TestimonialEntities' section of the application configuration file.
        /// </summary>
        public TestimonialEntities() : base("name=TestimonialEntities", "TestimonialEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TestimonialEntities object.
        /// </summary>
        public TestimonialEntities(string connectionString) : base(connectionString, "TestimonialEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TestimonialEntities object.
        /// </summary>
        public TestimonialEntities(EntityConnection connection) : base(connection, "TestimonialEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TestimonialView> TestimonialViews
        {
            get
            {
                if ((_TestimonialViews == null))
                {
                    _TestimonialViews = base.CreateObjectSet<TestimonialView>("TestimonialViews");
                }
                return _TestimonialViews;
            }
        }
        private ObjectSet<TestimonialView> _TestimonialViews;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TestimonialViews EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTestimonialViews(TestimonialView testimonialView)
        {
            base.AddObject("TestimonialViews", testimonialView);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TestimonialDBModels", Name="TestimonialView")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TestimonialView : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TestimonialView object.
        /// </summary>
        /// <param name="testimonialID">Initial value of the testimonialID property.</param>
        public static TestimonialView CreateTestimonialView(global::System.Int32 testimonialID)
        {
            TestimonialView testimonialView = new TestimonialView();
            testimonialView.testimonialID = testimonialID;
            return testimonialView;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                OnfirstnameChanging(value);
                ReportPropertyChanging("firstname");
                _firstname = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("firstname");
                OnfirstnameChanged();
            }
        }
        private global::System.String _firstname;
        partial void OnfirstnameChanging(global::System.String value);
        partial void OnfirstnameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String content
        {
            get
            {
                return _content;
            }
            set
            {
                OncontentChanging(value);
                ReportPropertyChanging("content");
                _content = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("content");
                OncontentChanged();
            }
        }
        private global::System.String _content;
        partial void OncontentChanging(global::System.String value);
        partial void OncontentChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> date
        {
            get
            {
                return _date;
            }
            set
            {
                OndateChanging(value);
                ReportPropertyChanging("date");
                _date = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("date");
                OndateChanged();
            }
        }
        private Nullable<global::System.DateTime> _date;
        partial void OndateChanging(Nullable<global::System.DateTime> value);
        partial void OndateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 testimonialID
        {
            get
            {
                return _testimonialID;
            }
            set
            {
                if (_testimonialID != value)
                {
                    OntestimonialIDChanging(value);
                    ReportPropertyChanging("testimonialID");
                    _testimonialID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("testimonialID");
                    OntestimonialIDChanged();
                }
            }
        }
        private global::System.Int32 _testimonialID;
        partial void OntestimonialIDChanging(global::System.Int32 value);
        partial void OntestimonialIDChanged();

        #endregion

    
    }

    #endregion

    
}
