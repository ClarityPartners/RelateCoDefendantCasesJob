﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tyler.Odyssey.API.JobTemplate
{
  using System.Xml.Serialization;
  using Tyler.Odyssey.API.Shared;

  // 
  // This source code was auto-generated by xsd, Version=4.6.1055.0.
  // 


  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "Result", IsNullable = false)]
  public partial class AddRelatedCaseResultEntity : BaseGeneratedAPIEntity
  {

    private BASEREQUIREDTRUEBOOL successField;

    /// <remarks/>
    public BASEREQUIREDTRUEBOOL Success
    {
      get
      {
        return this.successField;
      }
      set
      {
        this.successField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "BASE.REQUIREDTRUEBOOL")]
  public enum BASEREQUIREDTRUEBOOL
  {

    /// <remarks/>
    @true,
  }

}