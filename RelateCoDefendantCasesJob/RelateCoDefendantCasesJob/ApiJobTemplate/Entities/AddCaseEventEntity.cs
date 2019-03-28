﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.81.0.
// 
namespace Tyler.Odyssey.API.JobTemplate
{
  using System.Xml.Serialization;
  using Tyler.Odyssey.API.Shared;


  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "Message", IsNullable = false)]
  public partial class AddCaseEventEntity : BaseGeneratedAPIEntity
  {

    private string caseIDField;

    private string caseEventTypeField;

    private string dateField;

    private string timeField;

    private string date2Field;

    private string commentField;

    private string docketableFlagField;

    private string includeOnAppealField;

    private JusticeEventLOGBOOK logBookField;

    private string instrumentNumberField;

    private JusticeEventPartyType[] party1Field;

    private JusticeEventPartyType[] party2Field;

    private string judgeField;

    private string dueDateField;

    private string completedDateField;

    private string hearingDateField;

    private string hearingTimeField;

    private string hearingNodeIDField;

    private string[] chargeIDField;

    private string[] causeOfActionIDField;

    private decimal amountField;

    private bool amountFieldSpecified;

    private string pleaField;

    private JusticeEventAgingClock agingClockField;

    private bool agingClockFieldSpecified;

    private string assessFeesField;

    private string recordingNeededField;

    private JusticeEventStatus caseEventStatusField;

    private JusticeLinkDocument[] linkDocumentsField;

    private string numberField;

    private LOCALADDCASEEVENT messageTypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string CaseID
    {
      get
      {
        return this.caseIDField;
      }
      set
      {
        this.caseIDField = value;
      }
    }

    /// <remarks/>
    public string CaseEventType
    {
      get
      {
        return this.caseEventTypeField;
      }
      set
      {
        this.caseEventTypeField = value;
      }
    }

    /// <remarks/>
    public string Date
    {
      get
      {
        return this.dateField;
      }
      set
      {
        this.dateField = value;
      }
    }

    /// <remarks/>
    public string Time
    {
      get
      {
        return this.timeField;
      }
      set
      {
        this.timeField = value;
      }
    }

    /// <remarks/>
    public string Date2
    {
      get
      {
        return this.date2Field;
      }
      set
      {
        this.date2Field = value;
      }
    }

    /// <remarks/>
    public string Comment
    {
      get
      {
        return this.commentField;
      }
      set
      {
        this.commentField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
    public string DocketableFlag
    {
      get
      {
        return this.docketableFlagField;
      }
      set
      {
        this.docketableFlagField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
    public string IncludeOnAppeal
    {
      get
      {
        return this.includeOnAppealField;
      }
      set
      {
        this.includeOnAppealField = value;
      }
    }

    /// <remarks/>
    public JusticeEventLOGBOOK LogBook
    {
      get
      {
        return this.logBookField;
      }
      set
      {
        this.logBookField = value;
      }
    }

    /// <remarks/>
    public string InstrumentNumber
    {
      get
      {
        return this.instrumentNumberField;
      }
      set
      {
        this.instrumentNumberField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Party1")]
    public JusticeEventPartyType[] Party1
    {
      get
      {
        return this.party1Field;
      }
      set
      {
        this.party1Field = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Party2")]
    public JusticeEventPartyType[] Party2
    {
      get
      {
        return this.party2Field;
      }
      set
      {
        this.party2Field = value;
      }
    }

    /// <remarks/>
    public string Judge
    {
      get
      {
        return this.judgeField;
      }
      set
      {
        this.judgeField = value;
      }
    }

    /// <remarks/>
    public string DueDate
    {
      get
      {
        return this.dueDateField;
      }
      set
      {
        this.dueDateField = value;
      }
    }

    /// <remarks/>
    public string CompletedDate
    {
      get
      {
        return this.completedDateField;
      }
      set
      {
        this.completedDateField = value;
      }
    }

    /// <remarks/>
    public string HearingDate
    {
      get
      {
        return this.hearingDateField;
      }
      set
      {
        this.hearingDateField = value;
      }
    }

    /// <remarks/>
    public string HearingTime
    {
      get
      {
        return this.hearingTimeField;
      }
      set
      {
        this.hearingTimeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string HearingNodeID
    {
      get
      {
        return this.hearingNodeIDField;
      }
      set
      {
        this.hearingNodeIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ChargeID", DataType = "integer")]
    public string[] ChargeID
    {
      get
      {
        return this.chargeIDField;
      }
      set
      {
        this.chargeIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CauseOfActionID", DataType = "integer")]
    public string[] CauseOfActionID
    {
      get
      {
        return this.causeOfActionIDField;
      }
      set
      {
        this.causeOfActionIDField = value;
      }
    }

    /// <remarks/>
    public decimal Amount
    {
      get
      {
        return this.amountField;
      }
      set
      {
        this.amountField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool AmountSpecified
    {
      get
      {
        return this.amountFieldSpecified;
      }
      set
      {
        this.amountFieldSpecified = value;
      }
    }

    /// <remarks/>
    public string Plea
    {
      get
      {
        return this.pleaField;
      }
      set
      {
        this.pleaField = value;
      }
    }

    /// <remarks/>
    public JusticeEventAgingClock AgingClock
    {
      get
      {
        return this.agingClockField;
      }
      set
      {
        this.agingClockField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool AgingClockSpecified
    {
      get
      {
        return this.agingClockFieldSpecified;
      }
      set
      {
        this.agingClockFieldSpecified = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
    public string AssessFees
    {
      get
      {
        return this.assessFeesField;
      }
      set
      {
        this.assessFeesField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
    public string RecordingNeeded
    {
      get
      {
        return this.recordingNeededField;
      }
      set
      {
        this.recordingNeededField = value;
      }
    }

    /// <remarks/>
    public JusticeEventStatus CaseEventStatus
    {
      get
      {
        return this.caseEventStatusField;
      }
      set
      {
        this.caseEventStatusField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("LinkDocument", IsNullable = false)]
    public JusticeLinkDocument[] LinkDocuments
    {
      get
      {
        return this.linkDocumentsField;
      }
      set
      {
        this.linkDocumentsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string Number
    {
      get
      {
        return this.numberField;
      }
      set
      {
        this.numberField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public LOCALADDCASEEVENT MessageType
    {
      get
      {
        return this.messageTypeField;
      }
      set
      {
        this.messageTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
    public string NodeID
    {
      get
      {
        return this.nodeIDField;
      }
      set
      {
        this.nodeIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ReferenceNumber
    {
      get
      {
        return this.referenceNumberField;
      }
      set
      {
        this.referenceNumberField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
    public string UserID
    {
      get
      {
        return this.userIDField;
      }
      set
      {
        this.userIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Source
    {
      get
      {
        return this.sourceField;
      }
      set
      {
        this.sourceField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Justice.Event.LOGBOOK")]
  public partial class JusticeEventLOGBOOK
  {

    private string volumeField;

    private string pageField;

    private string numberOfPagesField;

    /// <remarks/>
    public string Volume
    {
      get
      {
        return this.volumeField;
      }
      set
      {
        this.volumeField = value;
      }
    }

    /// <remarks/>
    public string Page
    {
      get
      {
        return this.pageField;
      }
      set
      {
        this.pageField = value;
      }
    }

    /// <remarks/>
    public string NumberOfPages
    {
      get
      {
        return this.numberOfPagesField;
      }
      set
      {
        this.numberOfPagesField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Justice.LinkDocument")]
  public partial class JusticeLinkDocument
  {

    private string linkDocumentIDField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string LinkDocumentID
    {
      get
      {
        return this.linkDocumentIDField;
      }
      set
      {
        this.linkDocumentIDField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Justice.Event.Status")]
  public partial class JusticeEventStatus
  {

    private string dateField;

    private string statusField;

    private string commentField;

    /// <remarks/>
    public string Date
    {
      get
      {
        return this.dateField;
      }
      set
      {
        this.dateField = value;
      }
    }

    /// <remarks/>
    public string Status
    {
      get
      {
        return this.statusField;
      }
      set
      {
        this.statusField = value;
      }
    }

    /// <remarks/>
    public string Comment
    {
      get
      {
        return this.commentField;
      }
      set
      {
        this.commentField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Justice.Event.PartyType")]
  public partial class JusticeEventPartyType
  {

    private string partyIDField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string PartyID
    {
      get
      {
        return this.partyIDField;
      }
      set
      {
        this.partyIDField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Justice.Event.AgingClock")]
  public enum JusticeEventAgingClock
  {

    /// <remarks/>
    N,

    /// <remarks/>
    SR,

    /// <remarks/>
    SP,

    /// <remarks/>
    R,
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LOCAL.ADDCASEEVENT")]
  public enum LOCALADDCASEEVENT
  {

    /// <remarks/>
    AddCaseEvent,
  }
}
