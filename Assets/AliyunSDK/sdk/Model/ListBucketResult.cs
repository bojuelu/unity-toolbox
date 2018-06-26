﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 

namespace Aliyun.OSS.Model
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot(Namespace="", IsNullable=false)]
    public partial class ListBucketResult
    {
        
        private string nameField;
        
        private string prefixField;

        private string encodingType;
        
        private string markerField;
        
        private int maxKeysField;
        
        private string delimiterField;
        
        private bool isTruncatedField;
        
        private string nextMarkerField;
        
        private ListBucketResultContents[] contentsField;
        
        private ListBucketResultCommonPrefixes[] commonPrefixesField;
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Prefix {
            get {
                return this.prefixField;
            }
            set {
                this.prefixField = value;
            }
        }

        /// <remarks/>
        public string EncodingType {
            get {
                return this.encodingType;
            }
            set {
                this.encodingType = value;
            }
        }
        
        /// <remarks/>
        public string Marker {
            get {
                return this.markerField;
            }
            set {
                this.markerField = value;
            }
        }
        
        /// <remarks/>
        public int MaxKeys {
            get {
                return this.maxKeysField;
            }
            set {
                this.maxKeysField = value;
            }
        }
        
        /// <remarks/>
        public string Delimiter {
            get {
                return this.delimiterField;
            }
            set {
                this.delimiterField = value;
            }
        }
        
        /// <remarks/>
        public bool IsTruncated {
            get {
                return this.isTruncatedField;
            }
            set {
                this.isTruncatedField = value;
            }
        }
        
        /// <remarks/>
        public string NextMarker {
            get {
                return this.nextMarkerField;
            }
            set {
                this.nextMarkerField = value;
            }
        }
        
        /// <remarks/>
        [XmlElement("Contents")]
        public ListBucketResultContents[] Contents {
            get {
                return this.contentsField;
            }
            set {
                this.contentsField = value;
            }
        }
        
        /// <remarks/>
        //        [XmlArray()]
        //        [System.Xml.Serialization.XmlArrayItemAttribute("CommonPrefixes")]
        [XmlElement("CommonPrefixes")]
        public ListBucketResultCommonPrefixes[] CommonPrefixes {
            get {
                return this.commonPrefixesField;
            }
            set {
                this.commonPrefixesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    public partial class ListBucketResultContents {
        
        private string keyField;
        
        private System.DateTime lastModifiedField;
        
        private string eTagField;
        
        private string typeField;
        
        private long sizeField;
        
        private string storageClassField;
        
        private Owner ownerField;
        
        /// <remarks/>
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime LastModified {
            get {
                return this.lastModifiedField;
            }
            set {
                this.lastModifiedField = value;
            }
        }
        
        /// <remarks/>
        public string ETag {
            get {
                return this.eTagField;
            }
            set {
                this.eTagField = value;
            }
        }
        
        /// <remarks/>
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        public long Size {
            get {
                return this.sizeField;
            }
            set {
                this.sizeField = value;
            }
        }
        
        /// <remarks/>
        public string StorageClass {
            get {
                return this.storageClassField;
            }
            set {
                this.storageClassField = value;
            }
        }
        
        /// <remarks/>
        public Owner Owner {
            get {
                return this.ownerField;
            }
            set {
                this.ownerField = value;
            }
        }
    }

    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot("CommonPrefixes")]
    public partial class ListBucketResultCommonPrefixes {
        
        private string[] prefixField;
        
        /// <remarks/>
        //    [XmlArrayItem("Prefix")]
        [XmlElement("Prefix")]
        public string[] Prefix {
            get {
                return this.prefixField;
            }
            set {
                this.prefixField = value;
            }
        }
    }
}
