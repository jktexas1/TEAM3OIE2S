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
    public partial class ImageDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ImageDBEntities object using the connection string found in the 'ImageDBEntities' section of the application configuration file.
        /// </summary>
        public ImageDBEntities() : base("name=ImageDBEntities", "ImageDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ImageDBEntities object.
        /// </summary>
        public ImageDBEntities(string connectionString) : base(connectionString, "ImageDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ImageDBEntities object.
        /// </summary>
        public ImageDBEntities(EntityConnection connection) : base(connection, "ImageDBEntities")
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
        public ObjectSet<Image> Images
        {
            get
            {
                if ((_Images == null))
                {
                    _Images = base.CreateObjectSet<Image>("Images");
                }
                return _Images;
            }
        }
        private ObjectSet<Image> _Images;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Images EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToImages(Image image)
        {
            base.AddObject("Images", image);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TEAM3OIE2SDBModels", Name="Image")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Image : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Image object.
        /// </summary>
        /// <param name="imageID">Initial value of the imageID property.</param>
        public static Image CreateImage(global::System.Int32 imageID)
        {
            Image image = new Image();
            image.imageID = imageID;
            return image;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 imageID
        {
            get
            {
                return _imageID;
            }
            set
            {
                if (_imageID != value)
                {
                    OnimageIDChanging(value);
                    ReportPropertyChanging("imageID");
                    _imageID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("imageID");
                    OnimageIDChanged();
                }
            }
        }
        private global::System.Int32 _imageID;
        partial void OnimageIDChanging(global::System.Int32 value);
        partial void OnimageIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> imageOrder
        {
            get
            {
                return _imageOrder;
            }
            set
            {
                OnimageOrderChanging(value);
                ReportPropertyChanging("imageOrder");
                _imageOrder = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("imageOrder");
                OnimageOrderChanged();
            }
        }
        private Nullable<global::System.Int32> _imageOrder;
        partial void OnimageOrderChanging(Nullable<global::System.Int32> value);
        partial void OnimageOrderChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String imageFilename
        {
            get
            {
                return _imageFilename;
            }
            set
            {
                OnimageFilenameChanging(value);
                ReportPropertyChanging("imageFilename");
                _imageFilename = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("imageFilename");
                OnimageFilenameChanged();
            }
        }
        private global::System.String _imageFilename;
        partial void OnimageFilenameChanging(global::System.String value);
        partial void OnimageFilenameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String imageTitle
        {
            get
            {
                return _imageTitle;
            }
            set
            {
                OnimageTitleChanging(value);
                ReportPropertyChanging("imageTitle");
                _imageTitle = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("imageTitle");
                OnimageTitleChanged();
            }
        }
        private global::System.String _imageTitle;
        partial void OnimageTitleChanging(global::System.String value);
        partial void OnimageTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> sliceThickness
        {
            get
            {
                return _sliceThickness;
            }
            set
            {
                OnsliceThicknessChanging(value);
                ReportPropertyChanging("sliceThickness");
                _sliceThickness = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("sliceThickness");
                OnsliceThicknessChanged();
            }
        }
        private Nullable<global::System.Int32> _sliceThickness;
        partial void OnsliceThicknessChanging(Nullable<global::System.Int32> value);
        partial void OnsliceThicknessChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> pixelSize
        {
            get
            {
                return _pixelSize;
            }
            set
            {
                OnpixelSizeChanging(value);
                ReportPropertyChanging("pixelSize");
                _pixelSize = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("pixelSize");
                OnpixelSizeChanged();
            }
        }
        private Nullable<global::System.Int32> _pixelSize;
        partial void OnpixelSizeChanging(Nullable<global::System.Int32> value);
        partial void OnpixelSizeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> seriesID
        {
            get
            {
                return _seriesID;
            }
            set
            {
                OnseriesIDChanging(value);
                ReportPropertyChanging("seriesID");
                _seriesID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("seriesID");
                OnseriesIDChanged();
            }
        }
        private Nullable<global::System.Int32> _seriesID;
        partial void OnseriesIDChanging(Nullable<global::System.Int32> value);
        partial void OnseriesIDChanged();

        #endregion

    
    }

    #endregion

    
}